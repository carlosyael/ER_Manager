using System;
using System.IO;
using System.Linq;

namespace EldenRingSaveManager
{
    public enum EstadoPartida
    {
        Vanilla,
        Seamless,
        Mixto,
        Desconocido
    }

    public static class SaveFileManager
    {
        public static string ObtenerRutaPorDefecto()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string eldenRingFolder = Path.Combine(appData, "EldenRing");
            
            if (Directory.Exists(eldenRingFolder))
            {
                var directories = Directory.GetDirectories(eldenRingFolder);
                var steamIdFolder = directories.FirstOrDefault(d => 
                {
                    string folderName = Path.GetFileName(d);
                    return folderName.All(char.IsDigit) && folderName.Length > 0;
                });

                if (steamIdFolder != null)
                {
                    return steamIdFolder;
                }
                
                return eldenRingFolder; 
            }
            
            return string.Empty;
        }

        public static EstadoPartida DetectarEstadoActual(string rutaCarpeta)
        {
            if (string.IsNullOrEmpty(rutaCarpeta) || !Directory.Exists(rutaCarpeta))
                return EstadoPartida.Desconocido;

            bool tieneVanilla = Directory.GetFiles(rutaCarpeta, "*.sl2").Length > 0;
            bool tieneSeamless = Directory.GetFiles(rutaCarpeta, "*.co2").Length > 0;

            if (tieneVanilla && tieneSeamless) return EstadoPartida.Mixto;
            if (tieneVanilla) return EstadoPartida.Vanilla;
            if (tieneSeamless) return EstadoPartida.Seamless;

            return EstadoPartida.Desconocido;
        }

        /// <summary>
        /// Crea una copia de seguridad de la carpeta completa antes de transformarla.
        /// </summary>
        public static void GenerarRespaldo(string rutaCarpeta)
        {
            if (string.IsNullOrEmpty(rutaCarpeta) || !Directory.Exists(rutaCarpeta)) return;

            DirectoryInfo dirEldenRing = Directory.GetParent(rutaCarpeta);
            if (dirEldenRing == null) return;

            string carpetaBackups = Path.Combine(dirEldenRing.Parent?.FullName ?? dirEldenRing.FullName, "EldenRing_Backups");
            if (!Directory.Exists(carpetaBackups))
            {
                Directory.CreateDirectory(carpetaBackups);
            }

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string backupDestino = Path.Combine(carpetaBackups, $"Backup_{timestamp}");
            Directory.CreateDirectory(backupDestino);

            // Copiamos solo los archivos sueltos (.sl2, .co2, .bak) para ser rápidos y precisos
            string[] archivos = Directory.GetFiles(rutaCarpeta);
            foreach (var archivo in archivos)
            {
                string extension = Path.GetExtension(archivo).ToLowerInvariant();
                if (extension == ".sl2" || extension == ".co2" || extension == ".bak")
                {
                    string destFile = Path.Combine(backupDestino, Path.GetFileName(archivo));
                    File.Copy(archivo, destFile, true);
                }
            }
            Logger.Write($"Backup completado exitosamente en: {backupDestino}");
        }

        public static void TransformarArchivos(string rutaCarpeta, bool haciaSeamless)
        {
            if (string.IsNullOrEmpty(rutaCarpeta) || !Directory.Exists(rutaCarpeta))
                throw new DirectoryNotFoundException("La ruta de partidas especificada no existe.");

            // Feature 1: Backup antes de transformar
            GenerarRespaldo(rutaCarpeta);

            string extOrigen = haciaSeamless ? ".sl2" : ".co2";
            string extDestino = haciaSeamless ? ".co2" : ".sl2";

            var archivos = Directory.GetFiles(rutaCarpeta, $"*{extOrigen}");
            var archivosBak = Directory.GetFiles(rutaCarpeta, $"*{extOrigen}.bak");

            var todosLosArchivos = archivos.Concat(archivosBak).ToList();

            if (todosLosArchivos.Count == 0) return;

            foreach (var archivoOrigen in todosLosArchivos)
            {
                string nombreArchivo = Path.GetFileName(archivoOrigen);
                string nuevoNombre = nombreArchivo.Replace(extOrigen, extDestino);
                string archivoDestino = Path.Combine(rutaCarpeta, nuevoNombre);

                if (File.Exists(archivoDestino))
                {
                    throw new IOException($"El archivo de destino ya existe ({nuevoNombre}). Operación cancelada para evitar sobrescribir progreso.");
                }

                File.Move(archivoOrigen, archivoDestino);
            }
            Logger.Write($"Archivos transformados exitosamente de {extOrigen} a {extDestino}.");
        }

        /// <summary>
        /// Cambia los archivos co2 actuales entre perfiles.
        /// </summary>
        public static void AplicarPerfil(string rutaCarpeta, string perfilAnterior, string perfilNuevo)
        {
            if (string.IsNullOrEmpty(rutaCarpeta) || !Directory.Exists(rutaCarpeta) || perfilAnterior == perfilNuevo) return;
            if (string.IsNullOrEmpty(perfilNuevo)) return;

            string carpetaPerfiles = Path.Combine(rutaCarpeta, "Perfiles");
            if (!Directory.Exists(carpetaPerfiles)) Directory.CreateDirectory(carpetaPerfiles);

            // Guardar progreso actual del perfil anterior
            if (!string.IsNullOrEmpty(perfilAnterior) && perfilAnterior != "Predeterminado")
            {
                string carpetaAnterior = Path.Combine(carpetaPerfiles, perfilAnterior);
                if (!Directory.Exists(carpetaAnterior)) Directory.CreateDirectory(carpetaAnterior);

                var archivosSeamlessActuales = Directory.GetFiles(rutaCarpeta, "*.co2*");
                foreach (var f in archivosSeamlessActuales)
                {
                    string destino = Path.Combine(carpetaAnterior, Path.GetFileName(f));
                    File.Move(f, destino, true);
                }
                Logger.Write($"Perfil guardado: {perfilAnterior}");
            }

            // Cargar progreso del perfil nuevo
            if (perfilNuevo != "Predeterminado")
            {
                string carpetaNueva = Path.Combine(carpetaPerfiles, perfilNuevo);
                if (Directory.Exists(carpetaNueva))
                {
                    var archivosSeamlessNuevos = Directory.GetFiles(carpetaNueva, "*.co2*");
                    foreach (var f in archivosSeamlessNuevos)
                    {
                        string destino = Path.Combine(rutaCarpeta, Path.GetFileName(f));
                        File.Copy(f, destino, true);
                    }
                }
                Logger.Write($"Perfil cargado: {perfilNuevo}");
            }
        }

        /// <summary>
        /// Elimina archivos basura que puedan ocasionar crasheos en el juego.
        /// </summary>
        public static void LimpiarTemporales(string rutaJuego)
        {
            if (string.IsNullOrEmpty(rutaJuego)) return;
            string dir = Path.GetDirectoryName(rutaJuego);
            if (!Directory.Exists(dir)) return;

            int borrados = 0;
            // Seamless Co-op deja minidumps cuando falla
            var mdmpFiles = Directory.GetFiles(dir, "*.mdmp");
            foreach (var file in mdmpFiles)
            {
                try { File.Delete(file); borrados++; } catch { }
            }

            // Archivos temporales directos
            var tmpFiles = Directory.GetFiles(dir, "*.tmp");
            foreach (var file in tmpFiles)
            {
                try { File.Delete(file); borrados++; } catch { }
            }

            Logger.Write($"Limpieza completada: Se eliminaron {borrados} archivos temporales/crash dumps.");
        }
    }
}
