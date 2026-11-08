# 💍 Elden Ring: Seamless Co-op Save Manager & Launcher v5.2

> 🌍 También disponible en: [English](README.md)

Una herramienta "Todo en Uno" desarrollada en C# (.NET 8 / Windows Forms) diseñada para gestionar automáticamente los archivos de guardado de *Elden Ring*. Permite alternar de forma rápida y segura entre el juego base (Vanilla) y el mod *Seamless Co-op*, con instalación automática del mod, editor de configuración integrado, backups inteligentes y mucho más.

## 📖 Sobre el Proyecto

El mod *Seamless Co-op* requiere que los archivos de guardado tengan la extensión `.co2` en lugar del `.sl2` oficial para evitar que el sistema antitrampas (Easy Anti-Cheat) detecte el progreso modificado en los servidores oficiales.

Normalmente, los jugadores deben renombrar estos archivos manualmente cada vez que quieren cambiar de modo de juego. Este proyecto automatiza completamente ese proceso mediante una interfaz gráfica sencilla y la creación de accesos directos inteligentes.

## ✨ Funcionalidades

### Core
* **⚙️ Conversión Automática de Partidas:** Transforma tus archivos `.sl2` (y `.bak`) a `.co2` y viceversa de forma transparente, reduciendo al mínimo el riesgo de baneos.
* **🔍 Auto-Detección de Rutas:** Encuentra automáticamente la carpeta de guardado de Elden Ring (`AppData\Roaming\EldenRing\<SteamID>`).
* **🚀 Modo Híbrido (GUI & CLI):**
  * **Modo Interfaz Gráfica:** Abre la aplicación normalmente para configurar rutas, perfiles y el mod.
  * **Modo Silencioso:** Usa los accesos directos del escritorio para ejecutar la transformación en segundo plano, lanzar el juego y cerrarse automáticamente.
* **🖥️ Generador de Accesos Directos:** Crea iconos dedicados en tu escritorio para "Lanzar Vanilla" o "Lanzar Seamless Co-op", con soporte para iconos personalizados (`.ico`).
* **🔒 Detección de Steam:** Verifica que Steam esté en ejecución antes de lanzar el juego. Si no lo está, lo inicia automáticamente desde el registro de Windows.
* **🛡️ Protección Anti-Conflicto:** Impide lanzar el juego si ya hay una instancia de `eldenring.exe` en ejecución.

### Backups & Perfiles
* **💾 Backups Automáticos:** Antes de cada transformación de archivos, se genera un respaldo con fecha y hora en `EldenRing_Backups/`, garantizando que tu progreso nunca se pierda.
* **👥 Sistema Multi-Perfil Cooperativo:** Crea perfiles aislados para distintas campañas cooperativas (ej: "Coop con María", "Coop Raid"). Cada perfil mantiene sus propios archivos `.co2` separados.

### Instalador & Configuración del Mod
* **📥 Instalador Automático desde GitHub:** Descarga e instala/actualiza *Seamless Co-op* directamente desde las [releases de LukeYui](https://github.com/LukeYui/EldenRingSeamlessCoopRelease/releases) con un solo clic. Migra de forma inteligente los valores de tu archivo `ersc_settings.ini` existente hacia la nueva versión, preservando cualquier ajuste nuevo añadido por el autor del mod.
* **📝 Editor de Configuración INI Dinámico:** Pestaña integrada que lee dinámicamente el archivo `ersc_settings.ini` y lo presenta en una cuadrícula editable (Ajuste / Valor). Los cambios se guardan de forma quirúrgica, respetando los comentarios y la estructura original del archivo.
* **🔧 Modo Experto:** Botón toggle para alternar entre la vista de formulario visual y el modo de texto plano para usuarios avanzados.

### Ajustes & Localización
* **🌍 Soporte Multi-Idioma:** Alterna entre Inglés y Español desde la pestaña de Ajustes. La interfaz se actualiza al instante sin necesidad de reiniciar.
* **🎨 Selección de Tema:** Alterna en tiempo real entre el Tema Claro y el Tema Oscuro (con paleta azul profundo y dorado inspirada en Elden Ring).
* **🎮 Selector de Versión de Juego:** Preparado para soporte futuro de Dark Souls Remastered y Dark Souls III.
* **🔄 Auto-Actualizador:** Comprueba automáticamente si hay nuevas versiones al iniciar y permite actualizar y reiniciar con un solo clic desde GitHub Releases.

### Mantenimiento
* **🧹 Limpiador de Crash Dumps:** Elimina archivos temporales de crasheo (`.mdmp`, `.tmp`) que se acumulan en la carpeta del juego.
* **📋 Registro de Actividad (Logs):** Toda la actividad queda registrada visualmente en la interfaz y persistida en `app.log`.

## 🚀 Cómo Usar

1. **Descarga** la última versión del `.exe` desde la pestaña de [Releases](https://github.com/carlosyael/ER_Manager/releases).
   > ⚠️ El archivo pesa ~154 MB porque incluye el runtime de .NET embebido. **No necesitas instalar nada adicional.**
2. **Ejecuta** `EldenRingSaveManager.exe`.
3. Haz clic en **"Autodetectar"** para encontrar tu carpeta de guardados o selecciónala manualmente.
4. Selecciona la ruta de tu `eldenring.exe` (Vanilla).
5. *(Opcional)* Haz clic en **"Descargar/Act. Seamless Co-op"** para instalar el mod automáticamente.
6. Haz clic en **"Crear Accesos Directos"** para generar los lanzadores en tu escritorio.
7. ¡Listo! Usa los accesos directos de tu escritorio para jugar al modo que prefieras.

### Pestaña de Configuración
1. Navega a la pestaña **"Configuración Seamless Co-op"**.
2. Edita cualquier parámetro directamente en la cuadrícula (contraseña, invasiones, escalado, etc.).
3. Pulsa **"Guardar Cambios"** — el archivo original se modifica sin perder comentarios ni estructura.

### Pestaña de Ajustes
1. Navega a la pestaña **"Ajustes"**.
2. Cambia el **Idioma** entre English y Español.
3. Alterna el **Tema** entre Claro y Oscuro.
4. Consulta la versión actual y pulsa **"Buscar Actualizaciones"** desde GitHub.

## 🛠️ Tecnologías

* **Lenguaje:** C# (.NET 8)
* **UI:** Windows Forms (WinForms)
* **Persistencia:** JSON (`config.json`) — compatible con apps Single-File
* **Red:** `HttpClient` + GitHub REST API v3
* **Compresión:** `System.IO.Compression` para extracción inteligente de ZIPs
* **Shortcuts:** `WScript.Shell` COM dinámico para creación nativa de `.lnk`
* **Distribución:** Ejecutable único portable (`PublishSingleFile`, self-contained, win-x64)

## 📁 Estructura del Proyecto

```
EldenRingSaveManager/
├── Program.cs                  # Punto de entrada (CLI/GUI routing + crash handlers)
├── FormPrincipal.cs            # Lógica de la interfaz (perfiles, instalador, editor INI, ajustes)
├── FormPrincipal.Designer.cs   # Layout de la UI (TabControl, DataGridView, controles)
├── SaveFileManager.cs          # Backups, transformación .sl2↔.co2, perfiles, limpieza
├── ModInstaller.cs             # Descarga de GitHub API + extracción inteligente y migración de INI
├── AppUpdater.cs               # Comprobador e instalador de actualizaciones vía GitHub Releases
├── LocalizationManager.cs      # Sistema multi-idioma i18n (en/es)
├── ThemeManager.cs             # Motor de temas Claro y Oscuro
├── ShortcutCreator.cs          # Creador de accesos directos .lnk
├── ConfigHelper.cs             # Persistencia de configuración en config.json
├── Logger.cs                   # Registro de actividad en app.log
└── EldenRingSaveManager.csproj # Proyecto .NET 8 WinForms
```

## 👨‍💻 Autor

Desarrollado por **Carlos Yael De Los Santos Zorrilla**.

## ⚠️ Descargo de Responsabilidad

Este proyecto es una herramienta de código abierto creada por fans. No está afiliada, asociada, autorizada, respaldada ni conectada oficialmente de ninguna manera con FromSoftware, Bandai Namco, ni con los creadores del mod *Seamless Co-op*. **Usa esta herramienta bajo tu propio riesgo.** Se recomienda hacer copias de seguridad manuales de tus archivos de guardado antes de usar gestores de terceros.
