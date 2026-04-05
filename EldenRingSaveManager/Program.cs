using System;
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
            try
            {
                string dt = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                Application.ThreadException += (s, e) => {
                    File.WriteAllText(Path.Combine(dt, "ERManager_Crash_Thread.txt"), e.Exception.ToString());
                };
                AppDomain.CurrentDomain.UnhandledException += (s, e) => {
                    File.WriteAllText(Path.Combine(dt, "ERManager_Crash_Domain.txt"), e.ExceptionObject?.ToString());
                };

                ApplicationConfiguration.Initialize();

                // Initialize localization early for CLI messages
                LocalizationManager.Initialize();

                // Silent / CLI Mode (When invoked from desktop shortcuts)
                if (args.Length > 0)
                {
                    string argumento = args[0].ToLowerInvariant();
                    
                    string rutaPartidas = ConfigHelper.GetSetting("RutaPartidas");
                    string rutaVanilla = ConfigHelper.GetSetting("RutaVanilla");
                    string rutaSeamless = ConfigHelper.GetSetting("RutaSeamless");

                    Logger.Write(LocalizationManager.Get("CliStarting", argumento));

                    if (string.IsNullOrEmpty(rutaPartidas))
                    {
                        Logger.Write("[CLI] Error: Save path not configured.");
                        MessageBox.Show(
                            LocalizationManager.Get("CliSavePathNotConfigured"), 
                            LocalizationManager.Get("CliConfigError"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        Logger.Write(LocalizationManager.Get("CliUnknownArg", argumento));
                        Environment.Exit(1);
                    }

                    try
                    {
                        // Steam detection
                        Process[] steamProcs = Process.GetProcessesByName("steam");
                        if (steamProcs.Length == 0)
                        {
                            Logger.Write(LocalizationManager.Get("CliSteamNotActive"));
                            string steamPath = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam", "SteamExe", null);
                            if (!string.IsNullOrEmpty(steamPath) && File.Exists(steamPath))
                            {
                                Process.Start(steamPath);
                                Logger.Write(LocalizationManager.Get("CliSteamLaunched"));
                                System.Threading.Thread.Sleep(3000);
                            }
                            else
                            {
                                Logger.Write(LocalizationManager.Get("CliSteamNotFound"));
                                MessageBox.Show(
                                    LocalizationManager.Get("CliSteamRequired"), 
                                    LocalizationManager.Get("CliWarning"),
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                Environment.Exit(1);
                            }
                        }

                        // Validate game is not already running
                        Process[] procesos = Process.GetProcessesByName("eldenring");
                        if (procesos.Length > 0)
                        {
                            Logger.Write(LocalizationManager.Get("CliGameRunning"));
                            MessageBox.Show(
                                LocalizationManager.Get("CliGameRunningMsg"), 
                                LocalizationManager.Get("CliGameInExecution"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Environment.Exit(1);
                        }

                        // Transformation logic
                        EstadoPartida estadoActual = SaveFileManager.DetectarEstadoActual(rutaPartidas);
                        
                        bool necesitaTransformar = (haciaSeamless && estadoActual == EstadoPartida.Vanilla) || 
                                                   (!haciaSeamless && estadoActual == EstadoPartida.Seamless);
                                                   
                        if (necesitaTransformar)
                        {
                            Logger.Write(LocalizationManager.Get("CliTransforming"));
                            SaveFileManager.TransformarArchivos(rutaPartidas, haciaSeamless);
                        }
                        else if (estadoActual == EstadoPartida.Mixto)
                        {
                            Logger.Write(LocalizationManager.Get("CliMixedState"));
                            MessageBox.Show(
                                LocalizationManager.Get("CliMixedStateMsg"), 
                                LocalizationManager.Get("CliCriticalWarning"),
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Environment.Exit(1);
                        }

                        // Launch the game
                        if (string.IsNullOrEmpty(ejecutableALanzar))
                        {
                            Logger.Write(LocalizationManager.Get("CliExePathEmpty"));
                            MessageBox.Show(
                                LocalizationManager.Get("CliExePathNotConfigured", argumento), 
                                LocalizationManager.Get("CliError"),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(1);
                        }

                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = ejecutableALanzar,
                            UseShellExecute = true, 
                            WorkingDirectory = System.IO.Path.GetDirectoryName(ejecutableALanzar)
                        };
                        Process.Start(startInfo);
                        Logger.Write(LocalizationManager.Get("CliGameLaunched", ejecutableALanzar));
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(LocalizationManager.Get("CliCriticalError", ex.Message));
                        MessageBox.Show(
                            LocalizationManager.Get("CliBackgroundError", ex.Message),
                            LocalizationManager.Get("CliExecutionError"),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(1);
                    }

                    Environment.Exit(0);
                }
                else
                {
                    // GUI Mode
                    Application.Run(new FormPrincipal());
                }
            }
            catch (Exception ex)
            {
                string dt = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                File.WriteAllText(Path.Combine(dt, "ERManager_Crash_Main.txt"), ex.ToString());
                MessageBox.Show(LocalizationManager.Get("CliCriticalStartupFailure", ex.Message));
            }
        }
    }
}
