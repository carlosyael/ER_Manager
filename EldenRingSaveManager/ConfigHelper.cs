using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EldenRingSaveManager
{
    public static class ConfigHelper
    {
        private static string ConfigPath => Path.Combine(System.AppContext.BaseDirectory, "config.json");
        private static Dictionary<string, string> Cache;

        static ConfigHelper()
        {
            try
            {
                if (File.Exists(ConfigPath))
                {
                    string json = File.ReadAllText(ConfigPath);
                    Cache = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                }
            }
            catch { }

            if (Cache == null)
            {
                Cache = new Dictionary<string, string>();
            }
        }

        public static string GetSetting(string key)
        {
            return Cache.ContainsKey(key) ? Cache[key] : null;
        }

        public static void SaveSetting(string key, string value)
        {
            Cache[key] = value ?? "";
            try
            {
                string json = JsonSerializer.Serialize(Cache, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ConfigPath, json);
            }
            catch (Exception ex)
            {
                Logger.Write($"[ConfigHelper] Error guardando config.json: {ex.Message}");
            }
        }
    }
}
