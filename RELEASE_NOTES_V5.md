# Elden Ring: Seamless Co-op Save Manager v5.0

The ultimate all-in-one organizer and launcher for Elden Ring! This tool automatically manages save file rotation, protects your progress, and lets you configure the Seamless Co-op mod without ever touching hidden internal files.

---

## 🌟 Core Features

- **⚙️ Automatic Save Conversion:** Seamlessly switch between your Vanilla characters (`.sl2`) and your modded profiles (`.co2`), reducing the risk of bans to 0% by ensuring the correct launcher always matches the correct save format.

- **👥 Multi-Profile Co-op System:** Create and rotate isolated save "profiles" for different co-op campaigns. Play one campaign with "Group A" and a completely separate one with "Group B" without saves overwriting each other.

- **🚀 Dual-Mode Launcher (GUI & CLI):**
  - **GUI Mode:** Open the app to configure paths, profiles, and mod settings visually.
  - **Silent Mode (CLI):** Use one-click desktop shortcuts that swap files in the background, launch the game, and close instantly — no loading windows.

- **🔒 Steam Auto-Detection:** The app checks the Windows registry to ensure Steam is running before injecting the game, preventing ghost crashes from the mod.

- **🛡️ Anti-Conflict Protection:** Blocks launching if `eldenring.exe` is already running, preventing save corruption.

- **💾 Automatic Backups:** Before every file transformation, a timestamped backup (`YYYYMMDD_HHMMSS`) is created in `/EldenRing_Backups/`. Your progress is literally impossible to corrupt.

- **📥 One-Click Mod Installer:** Downloads and installs/updates Seamless Co-op directly from [LukeYui's GitHub releases](https://github.com/LukeYui/EldenRingSeamlessCoopRelease/releases) with a single button press.

- **📝 Dynamic INI Config Editor:** A dedicated tab reads your `ersc_settings.ini` line-by-line and displays it in an interactive grid. Edit passwords, invasions, scaling, and other settings visually, then save — preserving all original comments and structure.

- **🔧 Expert Mode:** Toggle between the visual form editor and raw text mode for advanced users.

- **🖥️ Custom Shortcut Creator:** Generate desktop shortcuts for "Launch Vanilla" and "Launch Seamless Co-op" with support for custom `.ico` icons.

- **🧹 Crash Dump Cleaner:** Remove accumulated `.mdmp` and `.tmp` crash files from the game folder.

- **📋 Activity Log:** All actions, backups, and events are logged in the UI and persisted to `app.log`.

---

## ✨ What's New in v5.0

### 🌍 Multi-Language Interface (i18n)
The entire UI now defaults to **English** and can be instantly switched to **Spanish** from the Settings tab. All buttons, labels, dialogs, messages, and log entries update in real-time — no restart required.

### ⚙️ Settings Tab
A brand-new **Settings** tab with:
- **Language selector** — English / Español
- **Theme selector** — Light / Dark
- **Game version selector** — Elden Ring (active), Dark Souls Remastered & Dark Souls III (coming soon)
- **About & Updates** section with version info and update status

### 🌙 Dark Theme
A fully implemented **dark theme** with an Elden Ring-inspired palette:
- Deep navy backgrounds with golden accent highlights
- Styled buttons with hover/press effects
- Dark DataGridView with alternating rows and gold headers
- Applies to every control in the application instantly

### 🔄 Auto-Updater
- **Automatic check on startup:** The app silently checks GitHub for newer releases every time it launches.
- **Manual check:** A "Check for Updates" button in Settings for on-demand checking.
- **One-click update:** If a new version is found, download and install it with a single click — the app restarts automatically with the new version.
- **Smart version parsing:** Handles any GitHub tag format (e.g. `ER_ManagerV5`, `v5.0.0`, `release-5.0`).

### 🔀 Smart INI Migration on Mod Update
Previously, updating the Seamless Co-op mod would either overwrite your `ersc_settings.ini` (losing your settings) or skip it entirely (missing new config options). Now the installer uses a **3-pass migration strategy:**
1. **Read** — Captures all your current settings (password, scaling, invasions, etc.)
2. **Extract** — Installs the new INI with any new options the mod author added
3. **Migrate** — Carries over your old values into matching keys in the new file

This ensures you always have the latest config structure while keeping all your customizations.

### 📄 English README
The project README has been fully translated to English, with the original Spanish version preserved as `README.es.md`.

---

## 📦 Installation
**Zero setup required.** This is a single-file portable executable (~154 MB) with the .NET 8 runtime embedded. Just download, place it anywhere, and run.

1. Download `EldenRingSaveManager.exe` from this release
2. Run it — no installation needed
3. Click **Autodetect** to find your saves, configure your paths, and you're ready to go
