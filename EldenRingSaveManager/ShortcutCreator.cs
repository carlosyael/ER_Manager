using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

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

                // Use proper COM interop with IShellLink (type-safe, AV-friendly)
                IShellLink link = (IShellLink)new ShellLink();

                string appExe = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

                link.SetPath(appExe);
                link.SetArguments(argumentoCommandLine);
                link.SetWorkingDirectory(Path.GetDirectoryName(appExe));

                if (!string.IsNullOrEmpty(rutaIcono) && File.Exists(rutaIcono))
                {
                    link.SetIconLocation(rutaIcono, 0);
                }
                else if (!string.IsNullOrEmpty(rutaEjecutableEldenRing) && File.Exists(rutaEjecutableEldenRing))
                {
                    link.SetIconLocation(rutaEjecutableEldenRing, 0);
                }

                // Save the shortcut via IPersistFile
                IPersistFile file = (IPersistFile)link;
                file.Save(rutaLink, false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el acceso directo '{nombreLink}': {ex.Message}", ex);
            }
        }

        // --- COM Interop definitions (type-safe, no dynamic/WScript.Shell) ---

        [ComImport]
        [Guid("00021401-0000-0000-C000-000000000046")]
        private class ShellLink { }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("000214F9-0000-0000-C000-000000000046")]
        private interface IShellLink
        {
            void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, IntPtr pfd, int fFlags);
            void GetIDList(out IntPtr ppidl);
            void SetIDList(IntPtr pidl);
            void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
            void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
            void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
            void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
            void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
            void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
            void GetHotkey(out short pwHotkey);
            void SetHotkey(short wHotkey);
            void GetShowCmd(out int piShowCmd);
            void SetShowCmd(int iShowCmd);
            void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
            void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
            void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
            void Resolve(IntPtr hwnd, int fFlags);
            void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }
    }
}
