using System.Collections.Generic;

namespace EldenRingSaveManager
{
    public static class LocalizationManager
    {
        private static string _currentLanguage = "en";

        private static readonly Dictionary<string, Dictionary<string, string>> Strings = new()
        {
            ["en"] = new Dictionary<string, string>
            {
                // --- Window Title ---
                ["WindowTitle"] = "Elden Ring: Seamless Co-op Save Manager v{0}",

                // --- Tab Names ---
                ["TabGeneral"] = "Launcher && Profiles",
                ["TabSeamlessConfig"] = "Seamless Co-op Config",
                ["TabSettings"] = "Settings",

                // --- GroupBox Titles ---
                ["GroupSavePaths"] = "Save Files Path && Profiles",
                ["GroupExecutables"] = "Executable Paths",
                ["GroupMaintenance"] = "Extra Maintenance",
                ["GroupLogs"] = "Activity Logs",
                ["GroupLanguage"] = "Language",
                ["GroupTheme"] = "Theme",
                ["GroupGameVersion"] = "Game Version",
                ["GroupAbout"] = "About && Updates",

                // --- Labels ---
                ["LabelActiveProfile"] = "Active Profile:",
                ["LabelVanilla"] = "Vanilla:",
                ["LabelSeamlessCoop"] = "Seamless Co-op:",
                ["LabelLanguage"] = "Interface Language:",
                ["LabelTheme"] = "Application Theme:",
                ["LabelGameVersion"] = "Target Game:",
                ["LabelVersion"] = "Current Version: v{0}",
                ["LabelUpdateAvailable"] = "🆕 New version v{0} available!",
                ["LabelUpToDate"] = "✅ You are using the latest version.",
                ["LabelCheckingUpdate"] = "⏳ Checking for updates...",

                // --- Buttons ---
                ["BtnSelect"] = "Select...",
                ["BtnAutodetect"] = "Autodetect",
                ["BtnNewProfile"] = "+ New Co-op Profile",
                ["BtnDownloadMod"] = "Download/Update Seamless Co-op",
                ["BtnCleanCrashDumps"] = "Clean Crash Dumps",
                ["BtnCreateShortcuts"] = "Create Desktop Shortcuts",
                ["BtnReloadFile"] = "Reload File",
                ["BtnSaveChanges"] = "Save Changes",
                ["BtnToggleExpert"] = "Toggle Expert Mode (Text)",
                ["BtnToggleVisual"] = "Toggle Visual Mode (Form)",
                ["BtnCheckUpdates"] = "Check for Updates",
                ["BtnUpdateNow"] = "Update Now",
                ["BtnOk"] = "OK",

                // --- Status ---
                ["StatusVanilla"] = "Current status: Vanilla (.sl2)",
                ["StatusSeamless"] = "Current status: Seamless (.co2)",
                ["StatusMixed"] = "Status: Mixed (Risk of loss)",
                ["StatusUnknown"] = "Current status: Unknown",

                // --- Messages ---
                ["MsgAppStarted"] = "Application started. Welcome to Elden Ring Save Manager v{0}.",
                ["MsgPathUpdated"] = "Save path updated to: {0}",
                ["MsgPathAutodetected"] = "Path auto-detected and saved.",
                ["MsgMissingPaths"] = "Please configure all paths before creating shortcuts.",
                ["MsgAttention"] = "Attention",
                ["MsgCustomIcon"] = "Would you like to choose a custom icon for Seamless Co-op? (If No, the game icon will be used)",
                ["MsgCustomIconTitle"] = "Custom Icon",
                ["MsgShortcutsCreated"] = "Desktop shortcuts created.",
                ["MsgShortcutsCreatedSuccess"] = "Desktop shortcuts created successfully.",
                ["MsgSuccess"] = "Success",
                ["MsgErrorCreatingShortcuts"] = "Error creating shortcuts: {0}",
                ["MsgProfileChanged"] = "Profile changed from {0} to {1}. Files rotated.",
                ["MsgErrorRotatingProfiles"] = "Error rotating profiles: {0}",
                ["MsgNewProfilePrompt"] = "Name for the new profile (e.g. Coop With Maria):",
                ["MsgNewProfileTitle"] = "New Profile",
                ["MsgNewProfileCreated"] = "New profile '{0}' created and selected.",
                ["MsgSelectVanillaFirst"] = "Please select your eldenring.exe (Vanilla) first to know where to install the mod.",
                ["MsgPathRequired"] = "Path Required",
                ["MsgInstallModConfirm"] = "Do you want to download from GitHub and install/update Seamless Co-op automatically in {0}?",
                ["MsgInstallMod"] = "Install Mod",
                ["MsgInstallingMod"] = "Starting automatic Seamless Co-op installation...",
                ["MsgCreatingShortcutsAuto"] = "Creating updated shortcuts automatically...",
                ["MsgInstallComplete"] = "Installation completed successfully! Files extracted and shortcut updated.",
                ["MsgCompleted"] = "Completed",
                ["MsgInstallError"] = "Error during installation: {0}",
                ["MsgInstallErrorGithub"] = "Failed connecting to GitHub or extracting: {0}",
                ["MsgInstallErrorTitle"] = "Installation Error",
                ["MsgCleanCrashConfirm"] = "Clean temporary crash files (.mdmp)?",
                ["MsgCleanTitle"] = "Cleanup",
                ["MsgCleanDone"] = "Crash dump cleanup executed.",
                ["MsgIniReloaded"] = "INI mapping reloaded.",
                ["MsgVanillaPathNotSet"] = "Vanilla base path not set.",
                ["MsgIniNotFound"] = "The ersc_settings.ini file was not found.",
                ["MsgIniSaved"] = "Seamless variables modified successfully.",
                ["MsgSaved"] = "Saved",
                ["MsgIniSavedLog"] = "Structured INI file was saved preserving its original comments.",
                ["MsgIniSaveError"] = "Error saving the INI: {0}",
                ["MsgIniSaveErrorTitle"] = "I/O Failure",
                ["MsgUiStarted"] = "[GUI] UI started successfully. v{0}",

                // --- DataGridView Columns ---
                ["ColSetting"] = "Setting",
                ["ColValue"] = "Value",

                // --- CLI Messages ---
                ["CliStarting"] = "[CLI] Starting with argument {0}",
                ["CliSavePathNotConfigured"] = "The save path is not configured. Please open the application normally and configure it.",
                ["CliConfigError"] = "Configuration Error",
                ["CliUnknownArg"] = "[CLI] Unknown argument: {0}",
                ["CliSteamNotActive"] = "[CLI] Steam is not active. Attempting to launch...",
                ["CliSteamLaunched"] = "[CLI] Steam launched via registry.",
                ["CliSteamNotFound"] = "[CLI] Steam not found. Aborting launch.",
                ["CliSteamRequired"] = "Elden Ring requires Steam to be active and it could not be auto-started. Please open Steam and try again.",
                ["CliWarning"] = "Warning",
                ["CliGameRunning"] = "[CLI] Block: Elden Ring is already running.",
                ["CliGameRunningMsg"] = "Elden Ring is currently running. Close the game completely before switching between Vanilla and Seamless Co-op.",
                ["CliGameInExecution"] = "Game Running",
                ["CliTransforming"] = "[CLI] Starting transformation...",
                ["CliMixedState"] = "[CLI] MIXED state in save files. Risk of loss.",
                ["CliMixedStateMsg"] = "Cannot continue: MIXED state detected in save files (.sl2 and .co2 present at the same time). This could cause progress loss. Check the folder manually.",
                ["CliCriticalWarning"] = "Critical Warning",
                ["CliExePathEmpty"] = "[CLI] Executable path is empty.",
                ["CliExePathNotConfigured"] = "The executable path for the command '{0}' is not configured.",
                ["CliError"] = "Error",
                ["CliGameLaunched"] = "[CLI] Game launched via: {0}",
                ["CliCriticalError"] = "[CLI] Critical error: {0}",
                ["CliBackgroundError"] = "A background error occurred:\n{0}",
                ["CliExecutionError"] = "Execution Error",
                ["CliCriticalStartupFailure"] = "Critical startup failure: {0}",

                // --- ModInstaller ---
                ["ModExeNotConfigured"] = "Please configure the Vanilla executable (eldenring.exe) path first.",
                ["ModDirNotFound"] = "Could not determine the game source directory.",
                ["ModNoAssets"] = "No downloads (assets) found in the latest GitHub version.",
                ["ModNoZip"] = "No .zip file found in the release.",

                // --- Update ---
                ["UpdateAvailable"] = "Update Available",
                ["UpdateAvailableMsg"] = "A new version (v{0}) is available.\n\nRelease Notes:\n{1}\n\nWould you like to update now?",
                ["UpdateDownloading"] = "Downloading update...",
                ["UpdateInstalling"] = "Installing update, the application will restart...",
                ["UpdateError"] = "Error checking for updates: {0}",
                ["UpdateErrorTitle"] = "Update Error",

                // --- Settings ---
                ["ThemeLight"] = "Light",
                ["ThemeDark"] = "Dark",
                ["GameEldenRing"] = "Elden Ring",
                ["GameDS1"] = "Dark Souls Remastered (Coming Soon)",
                ["GameDS3"] = "Dark Souls III (Coming Soon)",
            },

            ["es"] = new Dictionary<string, string>
            {
                // --- Window Title ---
                ["WindowTitle"] = "Elden Ring: Seamless Co-op Save Manager v{0}",

                // --- Tab Names ---
                ["TabGeneral"] = "Lanzador && Perfiles",
                ["TabSeamlessConfig"] = "Configuración Seamless Co-op",
                ["TabSettings"] = "Ajustes",

                // --- GroupBox Titles ---
                ["GroupSavePaths"] = "Ruta de Partidas Guardadas y Perfiles",
                ["GroupExecutables"] = "Rutas de Ejecutables",
                ["GroupMaintenance"] = "Mantenimiento Extra",
                ["GroupLogs"] = "Logs de Actividad",
                ["GroupLanguage"] = "Idioma",
                ["GroupTheme"] = "Tema",
                ["GroupGameVersion"] = "Versión del Juego",
                ["GroupAbout"] = "Acerca de y Actualizaciones",

                // --- Labels ---
                ["LabelActiveProfile"] = "Perfil Activo:",
                ["LabelVanilla"] = "Vanilla:",
                ["LabelSeamlessCoop"] = "Seamless Co-op:",
                ["LabelLanguage"] = "Idioma de la Interfaz:",
                ["LabelTheme"] = "Tema de la Aplicación:",
                ["LabelGameVersion"] = "Juego Objetivo:",
                ["LabelVersion"] = "Versión Actual: v{0}",
                ["LabelUpdateAvailable"] = "🆕 ¡Nueva versión v{0} disponible!",
                ["LabelUpToDate"] = "✅ Estás usando la última versión.",
                ["LabelCheckingUpdate"] = "⏳ Buscando actualizaciones...",

                // --- Buttons ---
                ["BtnSelect"] = "Seleccionar...",
                ["BtnAutodetect"] = "Autodetectar",
                ["BtnNewProfile"] = "+ Nuevo Perfil Co-op",
                ["BtnDownloadMod"] = "Descargar/Act. Seamless Co-op",
                ["BtnCleanCrashDumps"] = "Limpiar Crash Dumps",
                ["BtnCreateShortcuts"] = "Crear Accesos Directos",
                ["BtnReloadFile"] = "Recargar Archivo",
                ["BtnSaveChanges"] = "Guardar Cambios",
                ["BtnToggleExpert"] = "Toggle Modo Experto (Texto)",
                ["BtnToggleVisual"] = "Toggle Formulario (Visual)",
                ["BtnCheckUpdates"] = "Buscar Actualizaciones",
                ["BtnUpdateNow"] = "Actualizar Ahora",
                ["BtnOk"] = "Aceptar",

                // --- Status ---
                ["StatusVanilla"] = "Estado actual: Vanilla (.sl2)",
                ["StatusSeamless"] = "Estado actual: Seamless (.co2)",
                ["StatusMixed"] = "Estado: Mixto (Riesgo pérdida)",
                ["StatusUnknown"] = "Estado actual: Desconocido",

                // --- Messages ---
                ["MsgAppStarted"] = "Aplicación iniciada. Bienvenido a Elden Ring Save Manager v{0}.",
                ["MsgPathUpdated"] = "Ruta de partidas actualizada a: {0}",
                ["MsgPathAutodetected"] = "Ruta autodetectada y guardada.",
                ["MsgMissingPaths"] = "Faltan rutas por configurar antes de crear los accesos.",
                ["MsgAttention"] = "Atención",
                ["MsgCustomIcon"] = "¿Deseas elegir un icono personalizado para Seamless Co-op? (Si marcas No, usará el del juego)",
                ["MsgCustomIconTitle"] = "Icono Personalizado",
                ["MsgShortcutsCreated"] = "Accesos directos creados con éxito.",
                ["MsgShortcutsCreatedSuccess"] = "Accesos directos creados.",
                ["MsgSuccess"] = "Éxito",
                ["MsgErrorCreatingShortcuts"] = "Error al crear accesos: {0}",
                ["MsgProfileChanged"] = "Perfil cambiado de {0} a {1}. Archivos rotados.",
                ["MsgErrorRotatingProfiles"] = "Error al rotar perfiles: {0}",
                ["MsgNewProfilePrompt"] = "Nombre del nuevo perfil (ej: Coop Con Maria):",
                ["MsgNewProfileTitle"] = "Nuevo Perfil",
                ["MsgNewProfileCreated"] = "Nuevo perfil '{0}' creado y seleccionado.",
                ["MsgSelectVanillaFirst"] = "Por favor, selecciona primero tu eldenring.exe (Vanilla) para saber dónde instalar el mod.",
                ["MsgPathRequired"] = "Ruta Necesaria",
                ["MsgInstallModConfirm"] = "¿Deseas descargar de GitHub e instalar/actualizar Seamless Co-op automáticamente en {0}?",
                ["MsgInstallMod"] = "Instalar Mod",
                ["MsgInstallingMod"] = "Iniciando instalación automática de Seamless Co-op...",
                ["MsgCreatingShortcutsAuto"] = "Creando accesos directos actualizados automáticamente...",
                ["MsgInstallComplete"] = "¡Instalación completada con éxito! Archivos descomprimidos y acceso directo actualizado.",
                ["MsgCompleted"] = "Completado",
                ["MsgInstallError"] = "Error al instalar: {0}",
                ["MsgInstallErrorGithub"] = "Fallo conectando a GitHub o extrayendo: {0}",
                ["MsgInstallErrorTitle"] = "Error de Instalación",
                ["MsgCleanCrashConfirm"] = "¿Limpiar archivos temporales de crash (.mdmp)?",
                ["MsgCleanTitle"] = "Limpieza",
                ["MsgCleanDone"] = "Limpieza de crash dumps ejecutada.",
                ["MsgIniReloaded"] = "Mapeo de INI recargado.",
                ["MsgVanillaPathNotSet"] = "Ruta Base Vanilla no fijada.",
                ["MsgIniNotFound"] = "No se encontró el archivo ersc_settings.ini.",
                ["MsgIniSaved"] = "Variables de Seamless modificadas con éxito.",
                ["MsgSaved"] = "Guardado",
                ["MsgIniSavedLog"] = "Archivo INI estructurado fue guardado cuidando sus comentarios originales.",
                ["MsgIniSaveError"] = "Error al guardar el INI: {0}",
                ["MsgIniSaveErrorTitle"] = "Fallo de I/O",
                ["MsgUiStarted"] = "[GUI] UI Iniciada correctamente. v{0}",

                // --- DataGridView Columns ---
                ["ColSetting"] = "Ajuste",
                ["ColValue"] = "Valor",

                // --- CLI Messages ---
                ["CliStarting"] = "[CLI] Iniciando con argumento {0}",
                ["CliSavePathNotConfigured"] = "La ruta de partidas guardadas no está configurada. Por favor abre la aplicación normalmente y configúrala.",
                ["CliConfigError"] = "Error de Configuración",
                ["CliUnknownArg"] = "[CLI] Argumento desconocido: {0}",
                ["CliSteamNotActive"] = "[CLI] Steam no está activo. Intentando lanzarlo...",
                ["CliSteamLaunched"] = "[CLI] Steam iniciado mediante registro.",
                ["CliSteamNotFound"] = "[CLI] No se encontró Steam. Abortando lanzamiento.",
                ["CliSteamRequired"] = "Elden Ring requiere Steam activo para funcionar y no se pudo autoiniciar. Por favor abre Steam e inténtalo de nuevo.",
                ["CliWarning"] = "Aviso",
                ["CliGameRunning"] = "[CLI] Bloqueo: Elden Ring ya está en ejecución.",
                ["CliGameRunningMsg"] = "Elden Ring está actualmente en ejecución. Cierra el juego completamente antes de cambiar entre Vanilla y Seamless Co-op.",
                ["CliGameInExecution"] = "Juego en Ejecución",
                ["CliTransforming"] = "[CLI] Iniciando transformación...",
                ["CliMixedState"] = "[CLI] Estado MIXTO en archivos de guardado. Riesgo de pérdida.",
                ["CliMixedStateMsg"] = "No se puede continuar: Estado MIXTO detectado en los archivos de guardado (.sl2 y .co2 presentes a la vez). Esto podría causar pérdida de progreso. Revisa manualmente la carpeta.",
                ["CliCriticalWarning"] = "Advertencia Crítica",
                ["CliExePathEmpty"] = "[CLI] Ruta del ejecutable vacía.",
                ["CliExePathNotConfigured"] = "La ruta del ejecutable para el comando '{0}' no está configurada.",
                ["CliError"] = "Error",
                ["CliGameLaunched"] = "[CLI] Juego lanzado vía: {0}",
                ["CliCriticalError"] = "[CLI] Error crítico: {0}",
                ["CliBackgroundError"] = "Ocurrió un error en segundo plano:\n{0}",
                ["CliExecutionError"] = "Error de Ejecución",
                ["CliCriticalStartupFailure"] = "Falla crítica en inicio: {0}",

                // --- ModInstaller ---
                ["ModExeNotConfigured"] = "Por favor configura la ruta del ejecutable Vanilla (eldenring.exe) primero.",
                ["ModDirNotFound"] = "No se pudo determinar el directorio de origen del juego.",
                ["ModNoAssets"] = "No se encontraron descargas (assets) en la última versión de GitHub.",
                ["ModNoZip"] = "No se encontró un archivo .zip en la release.",

                // --- Update ---
                ["UpdateAvailable"] = "Actualización Disponible",
                ["UpdateAvailableMsg"] = "Una nueva versión (v{0}) está disponible.\n\nNotas de la versión:\n{1}\n\n¿Deseas actualizar ahora?",
                ["UpdateDownloading"] = "Descargando actualización...",
                ["UpdateInstalling"] = "Instalando actualización, la aplicación se reiniciará...",
                ["UpdateError"] = "Error buscando actualizaciones: {0}",
                ["UpdateErrorTitle"] = "Error de Actualización",

                // --- Settings ---
                ["ThemeLight"] = "Claro",
                ["ThemeDark"] = "Oscuro",
                ["GameEldenRing"] = "Elden Ring",
                ["GameDS1"] = "Dark Souls Remastered (Próximamente)",
                ["GameDS3"] = "Dark Souls III (Próximamente)",
            }
        };

        public static string CurrentLanguage => _currentLanguage;

        public static void Initialize()
        {
            string savedLang = ConfigHelper.GetSetting("Language");
            if (!string.IsNullOrEmpty(savedLang) && Strings.ContainsKey(savedLang))
                _currentLanguage = savedLang;
            else
                _currentLanguage = "en";
        }

        public static void SetLanguage(string langCode)
        {
            if (Strings.ContainsKey(langCode))
            {
                _currentLanguage = langCode;
                ConfigHelper.SaveSetting("Language", langCode);
            }
        }

        public static string Get(string key)
        {
            if (Strings.TryGetValue(_currentLanguage, out var langDict) && langDict.TryGetValue(key, out var value))
                return value;

            // Fallback to English
            if (Strings.TryGetValue("en", out var enDict) && enDict.TryGetValue(key, out var enValue))
                return enValue;

            return $"[{key}]"; // Debug: missing key indicator
        }

        public static string Get(string key, params object[] args)
        {
            string template = Get(key);
            try { return string.Format(template, args); }
            catch { return template; }
        }

        public static string[] GetAvailableLanguages() => new[] { "en", "es" };

        public static string GetLanguageDisplayName(string langCode) => langCode switch
        {
            "en" => "English",
            "es" => "Español",
            _ => langCode
        };
    }
}
