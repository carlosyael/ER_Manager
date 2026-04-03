using System;
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

        public static async Task<string> InstalarActualizacionAsync(string rutaVanillaExe)
        {
            if (string.IsNullOrEmpty(rutaVanillaExe) || !File.Exists(rutaVanillaExe))
                throw new FileNotFoundException("Por favor configura la ruta del ejecutable Vanilla (eldenring.exe) primero.");

            string directorioJuego = Path.GetDirectoryName(rutaVanillaExe);
            if (string.IsNullOrEmpty(directorioJuego))
                throw new DirectoryNotFoundException("No se pudo determinar el directorio de origen del juego.");

            Logger.Write("[ModInstaller] Solicitando última versión a GitHub API...");

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("EldenRingSaveManager", "2.0"));
                
                var response = await client.GetAsync(GithubApiUrl);
                response.EnsureSuccessStatusCode();

                string jsonContent = await response.Content.ReadAsStringAsync();
                
                using (JsonDocument doc = JsonDocument.Parse(jsonContent))
                {
                    JsonElement root = doc.RootElement;
                    if (!root.TryGetProperty("assets", out JsonElement assets) || assets.GetArrayLength() == 0)
                        throw new Exception("No se encontraron descargas (assets) en la última versión de GitHub.");

                    // Buscar el primer archivo .zip
                    string downloadUrl = string.Empty;
                    foreach (var asset in assets.EnumerateArray())
                    {
                        string nombre = asset.GetProperty("name").GetString();
                        if (nombre != null && nombre.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                        {
                            downloadUrl = asset.GetProperty("browser_download_url").GetString();
                            break;
                        }
                    }

                    if (string.IsNullOrEmpty(downloadUrl))
                        throw new Exception("No se encontró un archivo .zip en la release.");

                    Logger.Write($"[ModInstaller] Iniciando descarga desde: {downloadUrl}");
                    
                    string tempZipPath = Path.Combine(Path.GetTempPath(), "SeamlessCoopUpdate.zip");
                    
                    var downloadResponse = await client.GetAsync(downloadUrl);
                    downloadResponse.EnsureSuccessStatusCode();

                    using (var fs = new FileStream(tempZipPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await downloadResponse.Content.CopyToAsync(fs);
                    }

                    Logger.Write("[ModInstaller] Descarga completada. Extrayendo archivos...");
                    
                    // Extraer preservando ersc_settings.ini si ya existe
                    ExtraerZIPInteligentemente(tempZipPath, directorioJuego);

                    // Borrar zip temporal
                    File.Delete(tempZipPath);

                    Logger.Write("[ModInstaller] Instalación completada.");
                    
                    // Intentar devolver la posible ubicación del instalador
                    string posibleLauncher = Path.Combine(directorioJuego, "launch_elden_ring_seamlesscoop.exe");
                    if (!File.Exists(posibleLauncher))
                    {
                        posibleLauncher = Path.Combine(directorioJuego, "ersc_launcher.exe"); // Nombres alternativos
                    }
                    
                    return posibleLauncher;
                }
            }
        }

        private static void ExtraerZIPInteligentemente(string zipPath, string targetDir)
        {
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (string.IsNullOrEmpty(entry.Name)) // Es un directorio
                    {
                        Directory.CreateDirectory(Path.Combine(targetDir, entry.FullName));
                        continue;
                    }

                    string destinoFinal = Path.Combine(targetDir, entry.FullName);
                    Directory.CreateDirectory(Path.GetDirectoryName(destinoFinal)); // Asegurar carpeta padre
                    
                    // Lógica para preservar la configuración Seamless (.ini/.config)
                    if (destinoFinal.EndsWith("ersc_settings.ini", StringComparison.OrdinalIgnoreCase))
                    {
                        if (File.Exists(destinoFinal))
                        {
                            Logger.Write("[ModInstaller] Se encontró ersc_settings.ini existente. Se preservará tu configuración original.");
                            continue; // Ignoramos la extracción de este archivo
                        }
                    }

                    entry.ExtractToFile(destinoFinal, overwrite: true);
                }
            }
        }
    }
}
