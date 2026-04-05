# 💍 Elden Ring: Seamless Co-op Save Manager & Launcher v5.0

> 🌍 Also available in: [Español](README.es.md)

An all-in-one tool built in C# (.NET 8 / Windows Forms) designed to automatically manage *Elden Ring* save files. It allows you to quickly and safely switch between the base game (Vanilla) and the *Seamless Co-op* mod, with automatic mod installation, built-in configuration editor, smart backups, and much more.

## 📖 About the Project

The *Seamless Co-op* mod requires save files to use the `.co2` extension instead of the official `.sl2` to prevent the anti-cheat system (Easy Anti-Cheat) from detecting modified progress on official servers.

Normally, players must rename these files manually every time they want to switch game modes. This project fully automates that process through a simple graphical interface and the creation of smart desktop shortcuts.

## ✨ Features

### Core
* **⚙️ Automatic Save Conversion:** Transforms your `.sl2` (and `.bak`) files to `.co2` and vice versa transparently, minimizing the risk of bans.
* **🔍 Auto-Detection of Paths:** Automatically finds the Elden Ring save folder (`AppData\Roaming\EldenRing\<SteamID>`).
* **🚀 Hybrid Mode (GUI & CLI):**
  * **GUI Mode:** Open the application normally to configure paths, profiles, and the mod.
  * **Silent Mode:** Use desktop shortcuts to perform the transformation in the background, launch the game, and close automatically.
* **🖥️ Shortcut Generator:** Creates dedicated icons on your desktop to "Launch Vanilla" or "Launch Seamless Co-op", with support for custom icons (`.ico`).
* **🔒 Steam Detection:** Checks whether Steam is running before launching the game. If it isn't, it starts it automatically from the Windows registry.
* **🛡️ Anti-Conflict Protection:** Prevents launching the game if an instance of `eldenring.exe` is already running.

### Backups & Profiles
* **💾 Automatic Backups:** Before each file transformation, a timestamped backup is created in `EldenRing_Backups/`, ensuring your progress is never lost.
* **👥 Multi-Profile Co-op System:** Create isolated profiles for different co-op campaigns (e.g., "Coop with Maria", "Coop Raid"). Each profile maintains its own separate `.co2` files.

### Mod Installer & Configuration
* **📥 Automatic Installer from GitHub:** Downloads and installs/updates *Seamless Co-op* directly from [LukeYui's releases](https://github.com/LukeYui/EldenRingSeamlessCoopRelease/releases) with a single click. Preserves your existing `ersc_settings.ini` configuration file during updates.
* **📝 Dynamic INI Configuration Editor:** Built-in tab that dynamically reads the `ersc_settings.ini` file and presents it in an editable grid (Setting / Value). Changes are saved surgically, respecting the original comments and file structure.
* **🔧 Expert Mode:** Toggle button to switch between the visual form view and plain text mode for advanced users.

### Settings & Localization
* **🌍 Multi-Language Support:** Switch between English and Spanish from the Settings tab. The interface updates instantly.
* **🎨 Theme Selection:** Light and Dark theme options (Dark theme coming soon).
* **🎮 Game Version Selector:** Prepared for future Dark Souls Remastered and Dark Souls III support.
* **🔄 Auto-Updater:** Automatically checks for new versions on startup and allows one-click updates from GitHub Releases.

### Maintenance
* **🧹 Crash Dump Cleaner:** Removes temporary crash files (`.mdmp`, `.tmp`) that accumulate in the game folder.
* **📋 Activity Log:** All activity is visually logged in the interface and persisted in `app.log`.

## 🚀 How to Use

1. **Download** the latest `.exe` from the [Releases](https://github.com/carlosyael/ER_Manager/releases) tab.
   > ⚠️ The file is ~154 MB because it includes the embedded .NET runtime. **You don't need to install anything additional.**
2. **Run** `EldenRingSaveManager.exe`.
3. Click **"Autodetect"** to find your save folder or select it manually.
4. Select the path to your `eldenring.exe` (Vanilla).
5. *(Optional)* Click **"Download/Update Seamless Co-op"** to install the mod automatically.
6. Click **"Create Desktop Shortcuts"** to generate the launchers on your desktop.
7. You're all set! Use the desktop shortcuts to play whichever mode you prefer.

### Configuration Tab
1. Navigate to the **"Seamless Co-op Config"** tab.
2. Edit any parameter directly in the grid (password, invasions, scaling, etc.).
3. Click **"Save Changes"** — the original file is modified without losing comments or structure.

### Settings Tab
1. Navigate to the **"Settings"** tab.
2. Change the **Language** between English and Español.
3. View the current version and **Check for Updates** from GitHub.

## 🛠️ Technologies

* **Language:** C# (.NET 8)
* **UI:** Windows Forms (WinForms)
* **Persistence:** JSON (`config.json`) — compatible with Single-File apps
* **Networking:** `HttpClient` + GitHub REST API v3
* **Compression:** `System.IO.Compression` for smart ZIP extraction
* **Shortcuts:** `WScript.Shell` dynamic COM for native `.lnk` creation
* **Distribution:** Portable single-file executable (`PublishSingleFile`, self-contained, win-x64)

## 📁 Project Structure

```
EldenRingSaveManager/
├── Program.cs                  # Entry point (CLI/GUI routing + crash handlers)
├── FormPrincipal.cs            # UI logic (profiles, installer, INI editor, settings)
├── FormPrincipal.Designer.cs   # UI layout (TabControl, DataGridView, controls)
├── SaveFileManager.cs          # Backups, .sl2↔.co2 transformation, profiles, cleanup
├── ModInstaller.cs             # GitHub API download + smart ZIP extraction
├── AppUpdater.cs               # Self-update checker via GitHub Releases
├── LocalizationManager.cs      # Multi-language i18n system (en/es)
├── ShortcutCreator.cs          # Desktop shortcut (.lnk) creator
├── ConfigHelper.cs             # Configuration persistence in config.json
├── Logger.cs                   # Activity logging to app.log
└── EldenRingSaveManager.csproj  # .NET 8 WinForms project
```

## 👨‍💻 Author

Developed by **Carlos Yael De Los Santos Zorrilla**.

## ⚠️ Disclaimer

This project is an open-source tool created by fans. It is not affiliated, associated, authorized, endorsed, or officially connected in any way with FromSoftware, Bandai Namco, or the creators of the *Seamless Co-op* mod. **Use this tool at your own risk.** It is recommended to manually back up your save files before using third-party managers.
