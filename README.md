# 💍 Elden Ring: Seamless Co-op Save Manager & Launcher

Una herramienta híbrida desarrollada en C# (Windows Forms) diseñada para gestionar automáticamente los archivos de guardado de *Elden Ring*. Permite alternar de forma rápida y segura entre el juego base (Vanilla) y el mod *Seamless Co-op*, automatizando la conversión de extensiones de guardado (`.sl2` ↔ `.co2`) para evitar baneos y gestionar tu progreso sin complicaciones.

## 📖 Sobre el Proyecto

El mod *Seamless Co-op* requiere que los archivos de guardado tengan la extensión `.co2` en lugar del `.sl2` oficial para evitar que el sistema antitrampas (Easy Anti-Cheat) detecte el progreso modificado en los servidores oficiales. 

Normalmente, los jugadores deben renombrar estos archivos manualmente cada vez que quieren cambiar de modo de juego. Este proyecto automatiza completamente ese proceso mediante una interfaz gráfica sencilla y la creación de accesos directos inteligentes.

## ✨ Funcionalidades Principales

* **⚙️ Conversión Automática:** Transforma tus archivos `.sl2` (y `.bak`) a `.co2` y viceversa con un solo clic o de forma invisible.
* **🔍 Auto-Detección de Rutas:** Encuentra automáticamente la carpeta de guardado de Elden Ring (`AppData\Roaming\EldenRing\<SteamID>`).
* **🚀 Modo Híbrido (GUI & CLI):** * **Modo Interfaz Gráfica:** Abre la aplicación normalmente para configurar tus rutas y crear los accesos directos.
  * **Modo Silencioso:** Usa los accesos directos del escritorio para ejecutar la transformación en segundo plano, lanzar el juego correspondiente y cerrar la app automáticamente.
* **🖥️ Generador de Accesos Directos:** Crea iconos dedicados en tu escritorio para "Lanzar Vanilla" o "Lanzar Seamless Co-op".

## 🚀 Cómo usar

1. **Descarga** la última versión desde la pestaña de [Releases](#).
2. **Ejecuta** el programa `EldenRingSaveManager.exe`.
3. Haz clic en **"Autodetectar"** para encontrar tu carpeta de guardados, o selecciónala manualmente.
4. Selecciona las rutas de tus ejecutables (`eldenring.exe` y el lanzador del mod).
5. Haz clic en **"Crear Accesos Directos en el Escritorio"**.
6. ¡Listo! Ahora solo tienes que usar los nuevos accesos directos de tu escritorio para jugar al modo que prefieras. El programa hará el cambio de archivos de forma invisible antes de abrir el juego.

## 🛠️ Tecnologías Utilizadas

* **Lenguaje:** C#
* **Framework:** .NET Core / .NET Framework (Windows Forms)
* **Librerías:** `Windows Script Host Object Model` (para la creación nativa de accesos directos `.lnk`).

## 🗺️ Hoja de Ruta (Próximas Funcionalidades)

- [ ] **Backups Automáticos:** Creación de una copia de seguridad con fecha y hora antes de cada transformación para evitar cualquier riesgo de corrupción.
- [ ] **Gestión de Perfiles Multijugador:** Capacidad de guardar múltiples archivos `.co2` separados por perfiles (ej. "Campaña con Amigo A", "Campaña con Amigo B").
- [ ] **Detector de Steam:** Verificación automática de que Steam está en ejecución antes de lanzar el juego.

## 👨‍💻 Autor

Desarrollado por **Carlos Yael De Los Santos Zorrilla**.

## ⚠️ Descargo de Responsabilidad

Este proyecto es una herramienta de código abierto creada por fans. No está afiliada, asociada, autorizada, respaldada ni conectada oficialmente de ninguna manera con FromSoftware, Bandai Namco, ni con los creadores del mod *Seamless Co-op*. **Usa esta herramienta bajo tu propio riesgo.** Se recomienda encarecidamente hacer copias de seguridad manuales de tus archivos de guardado antes de usar gestores de terceros.
