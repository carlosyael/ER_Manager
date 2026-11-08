# Elden Ring: Seamless Co-op Save Manager v5.2

The ultimate all-in-one organizer and launcher for Elden Ring! This release restores the complete automated experience: one-click Seamless Co-op downloads directly from GitHub, automatic application self-updates, smart INI migration, and Steam auto-detection.

---

## 🔄 What's Changed in v5.2

- **Restored One-Click Seamless Co-op Installer:** Automatically downloads and extracts the latest Seamless Co-op release directly from [LukeYui's GitHub releases](https://github.com/LukeYui/EldenRingSeamlessCoopRelease/releases) without requiring manual browser downloads or local `.zip` selection.
- **Restored Full Auto-Updater:** Silent startup update check, on-demand "Check for Updates" button in Settings, and one-click automatic download, replacement, and restart (`PerformUpdateAsync`).
- **Restored Automatic Steam Launch:** Automatically detects and launches Steam via the Windows Registry if it is not running when launching the game from desktop shortcuts.
- **Updated Documentation (EN & ES):** Synchronized both `README.md` and `README-es.md` with all v5.2 features and project structure.

---

## 🌟 Core Features

- **⚙️ Automatic Save Conversion:** Seamlessly switch between your Vanilla characters (`.sl2`) and your modded profiles (`.co2`), reducing the risk of bans to 0% by ensuring the correct launcher always matches the correct save format.

- **👥 Multi-Profile Co-op System:** Create and rotate isolated save "profiles" for different co-op campaigns. Play one campaign with "Group A" and a completely separate one with "Group B" without saves overwriting each other.

- **🚀 Dual-Mode Launcher (GUI & CLI):**
  - **GUI Mode:** Open the app to configure paths, profiles, and mod settings visually.
  - **Silent Mode (CLI):** Use one-click desktop shortcuts that swap files in the background, launch the game, and close instantly — no loading windows.

- **🔒 Steam Auto-Detection:** Checks the Windows registry to ensure Steam is running before injecting the game, preventing ghost crashes from the mod.

- **🛡️ Anti-Conflict Protection:** Blocks launching if `eldenring.exe` is already running, preventing save corruption.

- **💾 Automatic Backups:** Before every file transformation, a timestamped backup (`YYYYMMDD_HHMMSS`) is created in `/EldenRing_Backups/`. Your progress is literally impossible to corrupt.

- **📥 One-Click Mod Installer & Smart INI Migration:** Downloads and installs/updates Seamless Co-op directly from GitHub with a single button press, using a **3-pass migration strategy** to preserve all your custom `ersc_settings.ini` values while keeping new options added in mod updates.

- **📝 Dynamic INI Config Editor & Expert Mode:** Edit passwords, invasions, scaling, and other settings visually in an interactive grid or raw text mode — preserving all original comments and structure.

- **🌍 Multi-Language (EN/ES) & 🌙 Dark Theme:** Instant language switching between English and Español, plus an Elden Ring-inspired dark navy and gold theme.

- **🖥️ Custom Shortcut Creator & 🧹 Crash Dump Cleaner:** Generate desktop shortcuts with custom `.ico` support and clean up `.mdmp`/`.tmp` crash files.

---

## 📦 Installation
**Zero setup required.** This is a single-file portable executable (~154 MB) with the .NET 8 runtime embedded. Just download, place it anywhere, and run.

1. Download `EldenRingSaveManager.exe` from this release
2. Run it — no installation needed
3. Click **Autodetect** to find your saves, configure your paths, and you're ready to go
