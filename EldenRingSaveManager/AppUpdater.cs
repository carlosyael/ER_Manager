using System;
using System.Diagnostics;
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
        public const string NexusModsUrl = "https://www.nexusmods.com/eldenring/mods/9574?tab=files";

        public class UpdateInfo
        {
            public bool UpdateAvailable { get; set; }
            public string LatestVersion { get; set; } = "";
            public string ReleaseNotes { get; set; } = "";
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
        /// Opens the Nexus Mods page in the user's default browser for manual download.
        /// </summary>
        public static void OpenNexusModsPage()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = NexusModsUrl,
                    UseShellExecute = true
                });
                Logger.Write($"[AppUpdater] Opened Nexus Mods page: {NexusModsUrl}");
            }
            catch (Exception ex)
            {
                Logger.Write($"[AppUpdater] Error opening Nexus Mods page: {ex.Message}");
                throw;
            }
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
