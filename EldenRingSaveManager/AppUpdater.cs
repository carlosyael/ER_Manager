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
        /// Returns true if latestVersion is newer than currentVersion.
        /// </summary>
        private static bool IsNewerVersion(string latestVersion, string currentVersion)
        {
            try
            {
                // Clean up version strings
                latestVersion = latestVersion.Trim().TrimStart('v', 'V');
                currentVersion = currentVersion.Trim().TrimStart('v', 'V');

                var latest = ParseVersion(latestVersion);
                var current = ParseVersion(currentVersion);

                return latest > current;
            }
            catch
            {
                // If parsing fails, do a simple string comparison
                return string.Compare(latestVersion, currentVersion, StringComparison.OrdinalIgnoreCase) > 0;
            }
        }

        private static Version ParseVersion(string versionString)
        {
            // Handle versions like "5.0", "5.0.0", "5.0.0.0"
            string[] parts = versionString.Split('.');
            int major = parts.Length > 0 ? int.Parse(parts[0]) : 0;
            int minor = parts.Length > 1 ? int.Parse(parts[1]) : 0;
            int build = parts.Length > 2 ? int.Parse(parts[2]) : 0;
            int revision = parts.Length > 3 ? int.Parse(parts[3]) : 0;
            return new Version(major, minor, build, revision);
        }
    }
}
