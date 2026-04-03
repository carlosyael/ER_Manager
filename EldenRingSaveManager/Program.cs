using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace EldenRingSaveManager
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();

            // Modo Silencioso / CLI (Cuando se invoca desde el acceso directo)
            if (args.Length > 0)
            {
                string argumento = args[0].ToLowerInvariant();
                
                string rutaPartidas = ConfigHelper.GetSetting("RutaPartidas");
                string rutaVanilla = ConfigHelper.GetSetting("RutaVanilla");
                string rutaSeamless = ConfigHelper.GetSetting("RutaSeamless");

                Logger.Write($"[CLI] Iniciando con argumento {argumento}");

                if (string.IsNullOrEmpty(rutaPartidas))
                {
                    Logger.Write("[CLI] Error: Ruta de partidas no configurada.");
                    MessageBox.Show("La ruta de partidas guardadas no está configurada. Por favor abre la aplicación normalmente y configúrala.", 
                                    "Error de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }

                bool haciaSeamless = false;
                string ejecutableALanzar = null;

                if (argumento == "-launch_vanilla")
                {
                    haciaSeamless = false;
                    ejecutableALanzar = rutaVanilla;
                }
                else if (argumento == "-launch_seamless")
                {
                    haciaSeamless = true;
                    ejecutableALanzar = rutaSeamless;
                }
                else
                {
                    Logger.Write($"[CLI] Argumento desconocido: {argumento}");
                    Environment.Exit(1);
                }

                try
                {
                    // Feature 3: Detector de Steam Abierto
                    Process[] steamProcs = Process.GetProcessesByName("steam");
                    if (steamProcs.Length == 0)
                    {
                        Logger.Write("[CLI] Steam no está activo. Intentando lanzarlo...");
                        string steamPath = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamExe", null);
                        if (!string.IsNullOrEmpty(steamPath) && File.Exists(steamPath))
                        {
                            Process.Start(steamPath);
                            Logger.Write("[CLI] Steam iniciado mediante registro.");
                            System.Threading.Thread.Sleep(3000); // Darle tiempo a Steam para reaccionar
                        }
                        else
                        {
                            Logger.Write("[CLI] No se encontró Steam. Abortando lanzamiento.");
                            MessageBox.Show("Elden Ring requiere Steam activo para funcionar y no se pudo autoiniciar. Por favor abre Steam e inténtalo de nuevo.", 
                                            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Environment.Exit(1);
                        }
                    }

                    // Validar que el juego no esté ejecutándose
                    Process[] procesos = Process.GetProcessesByName("eldenring");
                    if (procesos.Length > 0)
                    {
                        Logger.Write("[CLI] Bloqueo: Elden Ring ya está en ejecución.");
                        MessageBox.Show("Elden Ring está actualmente en ejecución. Cierra el juego completamente antes de cambiar entre Vanilla y Seamless Co-op.", 
                                        "Juego en Ejecución", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Environment.Exit(1);
                    }

                    // Lógica de transformación
                    EstadoPartida estadoActual = SaveFileManager.DetectarEstadoActual(rutaPartidas);
                    
                    bool necesitaTransformar = (haciaSeamless && estadoActual == EstadoPartida.Vanilla) || 
                                               (!haciaSeamless && estadoActual == EstadoPartida.Seamless);
                                               
                    if (necesitaTransformar)
                    {
                        Logger.Write("[CLI] Inciando transformación...");
                        SaveFileManager.TransformarArchivos(rutaPartidas, haciaSeamless);
                    }
                    else if (estadoActual == EstadoPartida.Mixto)
                    {
                        Logger.Write("[CLI] Estado MIXTO en archivos de guardado. Riesgo de pérdida.");
                        MessageBox.Show("No se puede continuar: Estado MIXTO detectado en los archivos de guardado (.sl2 y .co2 presentes a la vez). " +
                                        "Esto podría causar pérdida de progreso. Revisa manualmente la carpeta.", 
                                        "Advertencia Crítica", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Environment.Exit(1);
                    }

                    // Lanzar el juego
                    if (string.IsNullOrEmpty(ejecutableALanzar))
                    {
                        Logger.Write("[CLI] Ruta del ejecutable vacía.");
                        MessageBox.Show($"La ruta del ejecutable para el comando '{argumento}' no está configurada.", 
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(1);
                    }

                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = ejecutableALanzar,
                        UseShellExecute = true, 
                        WorkingDirectory = System.IO.Path.GetDirectoryName(ejecutableALanzar)
                    };
                    Process.Start(startInfo);
                    Logger.Write($"[CLI] Juego lanzado vía: {ejecutableALanzar}");
                }
                catch (Exception ex)
                {
                    Logger.Write($"[CLI] Error crítico: {ex.Message}");
                    MessageBox.Show($"Ocurrió un error en segundo plano:\n{ex.Message}", "Error de Ejecución", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }

                Environment.Exit(0);
            }
            else
            {
                // Modo GUI
                Application.Run(new FormPrincipal());
            }
        }
    }
}
