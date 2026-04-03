using System;
using System.IO;

namespace EldenRingSaveManager
{
    public static class Logger
    {
        private static readonly string logFilePath;

        static Logger()
        {
            // Guarda app.log en la misma carpeta donde reside este ejecutable
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            logFilePath = Path.Combine(appDir, "app.log");
        }

        public static void Write(string message)
        {
            try
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string logEntry = $"[{timestamp}] {message}{Environment.NewLine}";
                
                // Mantiene el archivo vivo y añade líneas al final
                File.AppendAllText(logFilePath, logEntry);
            }
            catch
            {
                // Si falla el logger por permisos, lo ignoramos de forma silenciosa para no crashear la app.
            }
        }
    }
}
