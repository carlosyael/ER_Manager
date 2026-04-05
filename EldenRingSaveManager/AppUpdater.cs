using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace EldenRingSaveManager
{
    public static class AppUpdater
    {
        public const string AppVersion = "5.0.0";
        private const string GithubApiUrl = "https://api.github.com/repos/carlosyael/ER_Manager/releases/latest";

        public class UpdateInfo
        {
            public bool UpdateAvailable { get; set; }
            public string LatestVersion { get; set; } = "";
            public string DownloadUrl { get; set; } = "";
            public string ReleaseNotes { get; set; } = "";
            public string AssetName { get; set; } = "";
        }

        /// <summary>
        /// Checks GitHub for a newer release of this application.
        /// </summary>
        public static async Task<UpdateInfo> CheckForUpdateAsync()
        {
            var result = new UpdateInfo();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.Add(
                        new ProductInfoHeaderValue("EldenRingSaveManager", AppVersion));
                    client.Timeout = TimeSpan.FromSeconds(10);

                    var response = await client.GetAsync(GithubApiUrl);
                    response.EnsureSuccessStatusCode();

                    string jsonContent = await response.Content.ReadAsStringAsync();

                    using (JsonDocument doc = JsonDocument.Parse(jsonContent))
                    {
                        JsonElement root = doc.RootElement;

                        // Get tag name (version)
                        string tagName = root.GetProperty("tag_name").GetString() ?? "";
                        string latestVersion = tagName.TrimStart('v', 'V');

                        result.LatestVersion = latestVersion;

                        // Get release notes
                        if (root.TryGetProperty("body", out JsonElement bodyElement))
                            result.ReleaseNotes = bodyElement.GetString() ?? "No release notes.";

                        // Compare versions
                        if (IsNewerVersion(latestVersion, AppVersion))
                        {
                            result.UpdateAvailable = true;

                            // Find the .exe asset to download
                            if (root.TryGetProperty("assets", out JsonElement assets))
                            {
                                foreach (var asset in assets.EnumerateArray())
                                {
                                    string name = asset.GetProperty("name").GetString() ?? "";
                                    if (name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                                    {
                                        result.DownloadUrl = asset.GetProperty("browser_download_url").GetString() ?? "";
                                        result.AssetName = name;
                                        break;
                                    }
                                }

                                // If no .exe found, try .zip
                                if (string.IsNullOrEmpty(result.DownloadUrl))
                                {
                                    foreach (var asset in assets.EnumerateArray())
                                    {
                                        string name = asset.GetProperty("name").GetString() ?? "";
                                        if (name.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                                        {
                                            result.DownloadUrl = asset.GetProperty("browser_download_url").GetString() ?? "";
                                            result.AssetName = name;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write($"[AppUpdater] Error checking for updates: {ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Downloads the update and creates a batch script to replace the current exe and restart.
        /// </summary>
        public static async Task PerformUpdateAsync(string downloadUrl, string assetName)
        {
            string currentExe = Process.GetCurrentProcess().MainModule?.FileName
                ?? throw new InvalidOperationException("Cannot determine current executable path.");
            string currentDir = Path.GetDirectoryName(currentExe)!;
            string tempDir = Path.Combine(currentDir, "_update_temp");

            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
            Directory.CreateDirectory(tempDir);

            string downloadPath = Path.Combine(tempDir, assetName);

            Logger.Write($"[AppUpdater] Downloading update from: {downloadUrl}");

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.Add(
                    new ProductInfoHeaderValue("EldenRingSaveManager", AppVersion));
                client.Timeout = TimeSpan.FromMinutes(5);

                var response = await client.GetAsync(downloadUrl);
                response.EnsureSuccessStatusCode();

                using (var fs = new FileStream(downloadPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await response.Content.CopyToAsync(fs);
                }
            }

            Logger.Write("[AppUpdater] Download completed. Preparing update script...");

            // Determine the new exe path
            string newExePath;
            if (assetName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
            {
                // Extract zip and find the exe inside
                string extractDir = Path.Combine(tempDir, "extracted");
                System.IO.Compression.ZipFile.ExtractToDirectory(downloadPath, extractDir);
                
                // Find the exe in extracted files
                string[] exeFiles = Directory.GetFiles(extractDir, "*.exe", SearchOption.AllDirectories);
                if (exeFiles.Length == 0)
                    throw new FileNotFoundException("No executable found in the update package.");
                
                newExePath = exeFiles[0];
            }
            else
            {
                newExePath = downloadPath;
            }

            // Create a batch script that waits for this process to exit, then replaces the exe
            string batchPath = Path.Combine(tempDir, "update.bat");
            string currentExeName = Path.GetFileName(currentExe);

            string batchContent = $@"@echo off
:: Wait for the application to close
timeout /t 3 /nobreak > nul

:: Replace the executable  
copy /y ""{newExePath}"" ""{currentExe}""

:: Restart the application
start """" ""{currentExe}""

:: Clean up
timeout /t 2 /nobreak > nul
rmdir /s /q ""{tempDir}""
";

            File.WriteAllText(batchPath, batchContent);

            // Launch the batch script and exit the application
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c \"{batchPath}\"",
                UseShellExecute = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(psi);

            Logger.Write("[AppUpdater] Update script launched. Application will restart.");

            // Exit the current application
            Environment.Exit(0);
        }

        /// <summary>
        /// Compares two version strings (e.g. "5.1.0" vs "5.0.0").
        /// Handles non-standard tags like "ER_ManagerV4" by extracting numeric parts.
        /// Returns true if latestVersion is newer than currentVersion.
        /// </summary>
        private static bool IsNewerVersion(string latestVersion, string currentVersion)
        {
            try
            {
                var latest = ExtractVersion(latestVersion);
                var current = ExtractVersion(currentVersion);

                if (latest == null || current == null)
                    return false;

                return latest > current;
            }
            catch
            {
                return false; // If we can't parse, assume no update available
            }
        }

        /// <summary>
        /// Extracts a Version from a string that may contain non-numeric characters.
        /// Handles: "5.0.0", "v5.0.0", "ER_ManagerV4", "V4.1", "release-2.3.1", etc.
        /// </summary>
        private static Version? ExtractVersion(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            // Extract all digit groups separated by dots or other chars
            var digits = new System.Collections.Generic.List<int>();
            string currentNum = "";

            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    currentNum += c;
                }
                else if (currentNum.Length > 0)
                {
                    digits.Add(int.Parse(currentNum));
                    currentNum = "";
                    // If we hit a dot after digits, continue collecting
                    // If we hit a non-dot, we might have found the full version already
                    if (c != '.' && digits.Count > 0)
                    {
                        // Only stop if we already have at least one digit group
                        // and the separator is not a dot (e.g. "V4" → stop, "4.1" → continue)
                    }
                }
            }
            if (currentNum.Length > 0)
                digits.Add(int.Parse(currentNum));

            if (digits.Count == 0)
                return null;

            int major = digits.Count > 0 ? digits[0] : 0;
            int minor = digits.Count > 1 ? digits[1] : 0;
            int build = digits.Count > 2 ? digits[2] : 0;
            int revision = digits.Count > 3 ? digits[3] : 0;

            return new Version(major, minor, build, revision);
        }
    }
}
