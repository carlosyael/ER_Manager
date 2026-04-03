using System;
using System.IO;

namespace EldenRingSaveManager
{
    public static class ShortcutCreator
    {
        public static void CrearAccesoDirecto(string nombreLink, string argumentoCommandLine, string rutaEjecutableEldenRing, string rutaIcono = null)
        {
            try
            {
                string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string rutaLink = Path.Combine(escritorio, $"{nombreLink}.lnk");

                // Utilizando WScript.Shell de forma dinámica para evitar referencias COM directas y problemas de compatibilidad
                Type t = Type.GetTypeFromProgID("WScript.Shell");
                dynamic shell = Activator.CreateInstance(t);
                dynamic shortcut = shell.CreateShortcut(rutaLink);

                // El acceso directo debe apuntar al .exe de esta aplicación C#
                string appExe = System.Reflection.Assembly.GetExecutingAssembly().Location;
                
                // En .NET Core/.NET 8, GetExecutingAssembly().Location puede devolver el archivo .dll
                // Por lo tanto, lo cambiamos a .exe para asegurar que inicie correctamente
                if (appExe.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                {
                    appExe = appExe.Substring(0, appExe.Length - 4) + ".exe";
                }

                shortcut.TargetPath = appExe;
                shortcut.Arguments = argumentoCommandLine;
                shortcut.WorkingDirectory = Path.GetDirectoryName(appExe);

                if (!string.IsNullOrEmpty(rutaIcono) && File.Exists(rutaIcono))
                {
                    shortcut.IconLocation = rutaIcono;
                }
                else if (!string.IsNullOrEmpty(rutaEjecutableEldenRing) && File.Exists(rutaEjecutableEldenRing))
                {
                    shortcut.IconLocation = $"{rutaEjecutableEldenRing},0";
                }

                shortcut.Save();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el acceso directo '{nombreLink}': {ex.Message}", ex);
            }
        }
    }
}
