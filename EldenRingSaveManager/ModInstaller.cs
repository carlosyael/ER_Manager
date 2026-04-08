using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace EldenRingSaveManager
{
    public static class ModInstaller
    {
        private const string GithubApiUrl = "https://api.github.com/repos/LukeYui/EldenRingSeamlessCoopRelease/releases/latest";
        public const string SeamlessNexusUrl = "https://www.nexusmods.com/eldenring/mods/510?tab=files";

        public class SeamlessVersionInfo
        {
            public string LatestVersion { get; set; } = "";
            public string ReleaseNotes { get; set; } = "";
        }

        /// <summary>
        /// Checks GitHub for the latest Seamless Co-op version (read-only, no download).
        /// </summary>
        public static async Task<SeamlessVersionInfo> CheckSeamlessVersionAsync()
        {
            var result = new SeamlessVersionInfo();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("EldenRingSaveManager", "2.0"));
                client.Timeout = TimeSpan.FromSeconds(10);

                var response = await client.GetAsync(GithubApiUrl);
                response.EnsureSuccessStatusCode();

                string jsonContent = await response.Content.ReadAsStringAsync();

                using (JsonDocument doc = JsonDocument.Parse(jsonContent))
                {
                    JsonElement root = doc.RootElement;

                    string tagName = root.GetProperty("tag_name").GetString() ?? "";
                    result.LatestVersion = tagName.TrimStart('v', 'V');

                    if (root.TryGetProperty("body", out JsonElement bodyElement))
                        result.ReleaseNotes = bodyElement.GetString() ?? "";
                }
            }

            return result;
        }

        /// <summary>
        /// Opens the Seamless Co-op Nexus Mods page in the user's default browser.
        /// </summary>
        public static void OpenSeamlessNexusPage()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = SeamlessNexusUrl,
                    UseShellExecute = true
                });
                Logger.Write($"[ModInstaller] Opened Seamless Co-op Nexus page: {SeamlessNexusUrl}");
            }
            catch (Exception ex)
            {
                Logger.Write($"[ModInstaller] Error opening Nexus page: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Installs Seamless Co-op from a locally downloaded .zip file.
        /// Preserves existing ersc_settings.ini values during upgrade.
        /// </summary>
        public static string InstalarDesdeZip(string zipPath, string rutaVanillaExe)
        {
            if (string.IsNullOrEmpty(rutaVanillaExe) || !File.Exists(rutaVanillaExe))
                throw new FileNotFoundException(LocalizationManager.Get("ModExeNotConfigured"));

            string directorioJuego = Path.GetDirectoryName(rutaVanillaExe);
            if (string.IsNullOrEmpty(directorioJuego))
                throw new DirectoryNotFoundException(LocalizationManager.Get("ModDirNotFound"));

            if (!File.Exists(zipPath))
                throw new FileNotFoundException(LocalizationManager.Get("ModZipNotFound"));

            Logger.Write($"[ModInstaller] Installing from local zip: {zipPath}");

            // Extract preserving ersc_settings.ini if it already exists
            ExtraerZIPInteligentemente(zipPath, directorioJuego);

            Logger.Write("[ModInstaller] Installation from local zip completed.");

            // Try to return the possible launcher path
            string posibleLauncher = Path.Combine(directorioJuego, "launch_elden_ring_seamlesscoop.exe");
            if (!File.Exists(posibleLauncher))
            {
                posibleLauncher = Path.Combine(directorioJuego, "ersc_launcher.exe");
            }

            return posibleLauncher;
        }

        private static void ExtraerZIPInteligentemente(string zipPath, string targetDir)
        {
            // Before extracting, read old INI values if the file exists
            Dictionary<string, string> oldIniValues = null;
            string iniRelativePath = null;

            // First pass: identify if there's an ersc_settings.ini in the zip and read old values
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (!string.IsNullOrEmpty(entry.Name) && 
                        entry.FullName.EndsWith("ersc_settings.ini", StringComparison.OrdinalIgnoreCase))
                    {
                        iniRelativePath = entry.FullName;
                        string existingIniPath = Path.Combine(targetDir, entry.FullName);
                        
                        if (File.Exists(existingIniPath))
                        {
                            oldIniValues = ParseIniValues(File.ReadAllLines(existingIniPath));
                            Logger.Write($"[ModInstaller] Old INI read: {oldIniValues.Count} settings captured for migration.");
                        }
                        break;
                    }
                }
            }

            // Second pass: extract everything (including the new INI)
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (string.IsNullOrEmpty(entry.Name)) // Directory
                    {
                        Directory.CreateDirectory(Path.Combine(targetDir, entry.FullName));
                        continue;
                    }

                    string destinoFinal = Path.Combine(targetDir, entry.FullName);
                    Directory.CreateDirectory(Path.GetDirectoryName(destinoFinal));
                    entry.ExtractToFile(destinoFinal, overwrite: true);
                }
            }

            // Third pass: migrate old INI values into the newly extracted INI
            if (oldIniValues != null && oldIniValues.Count > 0 && !string.IsNullOrEmpty(iniRelativePath))
            {
                string newIniPath = Path.Combine(targetDir, iniRelativePath);
                if (File.Exists(newIniPath))
                {
                    MigrateIniValues(newIniPath, oldIniValues);
                }
            }
        }

        /// <summary>
        /// Parses an INI file's lines into a dictionary of key=value pairs, ignoring comments and sections.
        /// </summary>
        private static Dictionary<string, string> ParseIniValues(string[] lines)
        {
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var line in lines)
            {
                string trimmed = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmed) || trimmed.StartsWith(";") || 
                    trimmed.StartsWith("#") || trimmed.StartsWith("["))
                    continue;

                int idx = trimmed.IndexOf('=');
                if (idx > 0)
                {
                    string key = trimmed.Substring(0, idx).Trim();
                    string value = trimmed.Substring(idx + 1).Trim();
                    result[key] = value;
                }
            }
            return result;
        }

        /// <summary>
        /// Takes the new INI file and replaces values of matching keys with the old values.
        /// Preserves new keys, comments, structure, and formatting of the new version.
        /// </summary>
        private static void MigrateIniValues(string newIniPath, Dictionary<string, string> oldValues)
        {
            string[] newLines = File.ReadAllLines(newIniPath);
            int migratedCount = 0;

            for (int i = 0; i < newLines.Length; i++)
            {
                string trimmed = newLines[i].Trim();
                if (string.IsNullOrWhiteSpace(trimmed) || trimmed.StartsWith(";") || 
                    trimmed.StartsWith("#") || trimmed.StartsWith("["))
                    continue;

                int idx = trimmed.IndexOf('=');
                if (idx > 0)
                {
                    string key = trimmed.Substring(0, idx).Trim();
                    
                    // If the old config had this same key, migrate its value
                    if (oldValues.TryGetValue(key, out string oldValue))
                    {
                        newLines[i] = $"{key} = {oldValue}";
                        migratedCount++;
                    }
                }
            }

            File.WriteAllLines(newIniPath, newLines);
            Logger.Write($"[ModInstaller] INI migration complete: {migratedCount} settings carried over, new settings preserved with defaults.");
        }
    }
}
