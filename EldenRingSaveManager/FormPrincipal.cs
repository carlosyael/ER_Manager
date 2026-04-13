using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace EldenRingSaveManager
{
    public partial class FormPrincipal : Form
    {
        private bool isUpdatingProfiles = false;
        private bool isUpdatingLanguage = false;
        private bool isUpdatingTheme = false;

        private string CurrentIniPath => !string.IsNullOrEmpty(txtRutaVanilla.Text) 
            ? Path.Combine(Path.GetDirectoryName(txtRutaVanilla.Text), "SeamlessCoop", "ersc_settings.ini")
            : string.Empty;

        public FormPrincipal()
        {
            LocalizationManager.Initialize();
            InitializeComponent();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            txtRutaPartidas.Text = ConfigHelper.GetSetting("RutaPartidas") ?? string.Empty;
            txtRutaVanilla.Text = ConfigHelper.GetSetting("RutaVanilla") ?? string.Empty;
            txtRutaSeamless.Text = ConfigHelper.GetSetting("RutaSeamless") ?? string.Empty;

            CargarPerfiles();
            ActualizarEstado();
            ThemeManager.Initialize();
            InitializeSettingsTab();
            ApplyLocalization();
            ThemeManager.ApplyTheme(this);
            
            Logger.Write(LocalizationManager.Get("MsgUiStarted", AppUpdater.AppVersion));
            Log(LocalizationManager.Get("MsgAppStarted", AppUpdater.AppVersion));

            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
        }

        // --- Localization ---
        private void ApplyLocalization()
        {
            // Window title
            this.Text = LocalizationManager.Get("WindowTitle", AppUpdater.AppVersion);

            // Tab names
            tabGeneral.Text = LocalizationManager.Get("TabGeneral");
            tabSeamlessConfig.Text = LocalizationManager.Get("TabSeamlessConfig");
            tabSettings.Text = LocalizationManager.Get("TabSettings");

            // GroupBox titles
            groupBox1.Text = LocalizationManager.Get("GroupSavePaths");
            groupBox2.Text = LocalizationManager.Get("GroupExecutables");
            groupBox4.Text = LocalizationManager.Get("GroupMaintenance");
            groupBox3.Text = LocalizationManager.Get("GroupLogs");
            grpLanguage.Text = LocalizationManager.Get("GroupLanguage");
            grpTheme.Text = LocalizationManager.Get("GroupTheme");
            grpGameVersion.Text = LocalizationManager.Get("GroupGameVersion");
            grpAbout.Text = LocalizationManager.Get("GroupAbout");

            // Labels
            lblPerfil.Text = LocalizationManager.Get("LabelActiveProfile");
            label1.Text = LocalizationManager.Get("LabelVanilla");
            label2.Text = LocalizationManager.Get("LabelSeamlessCoop");
            lblLanguage.Text = LocalizationManager.Get("LabelLanguage");
            lblTheme.Text = LocalizationManager.Get("LabelTheme");
            lblGameVersion.Text = LocalizationManager.Get("LabelGameVersion");
            lblVersionInfo.Text = LocalizationManager.Get("LabelVersion", AppUpdater.AppVersion);
            lblNexusUrl.Text = $"Nexus Mods: {AppUpdater.NexusModsUrl}";

            // Buttons
            btnSeleccionarPartidas.Text = LocalizationManager.Get("BtnSelect");
            btnAutodetectar.Text = LocalizationManager.Get("BtnAutodetect");
            btnNuevoPerfil.Text = LocalizationManager.Get("BtnNewProfile");
            btnSeleccionarVanilla.Text = LocalizationManager.Get("BtnSelect");
            btnSeleccionarSeamless.Text = LocalizationManager.Get("BtnSelect");
            btnActualizador.Text = LocalizationManager.Get("BtnDownloadMod");
            btnLimpiarCache.Text = LocalizationManager.Get("BtnCleanCrashDumps");
            btnCrearAccesos.Text = LocalizationManager.Get("BtnCreateShortcuts");
            btnRecargarIni.Text = LocalizationManager.Get("BtnReloadFile");
            btnGuardarIni.Text = LocalizationManager.Get("BtnSaveChanges");
            btnModoExperto.Text = txtSeamlessIni.Visible 
                ? LocalizationManager.Get("BtnToggleVisual") 
                : LocalizationManager.Get("BtnToggleExpert");

            // DataGridView columns
            colProperty.HeaderText = LocalizationManager.Get("ColSetting");
            colValue.HeaderText = LocalizationManager.Get("ColValue");

            // Update status label based on current estado
            ActualizarEstado();

            // Update theme/game dropdowns text (they're localized)
            RefreshThemeDropdown();
            RefreshGameVersionDropdown();
        }

        private void InitializeSettingsTab()
        {
            // Language dropdown
            isUpdatingLanguage = true;
            cmbLanguage.Items.Clear();
            foreach (var lang in LocalizationManager.GetAvailableLanguages())
                cmbLanguage.Items.Add(LocalizationManager.GetLanguageDisplayName(lang));
            
            string currentLang = LocalizationManager.CurrentLanguage;
            string displayName = LocalizationManager.GetLanguageDisplayName(currentLang);
            if (cmbLanguage.Items.Contains(displayName))
                cmbLanguage.SelectedItem = displayName;
            else
                cmbLanguage.SelectedIndex = 0;
            isUpdatingLanguage = false;

            // Theme dropdown
            RefreshThemeDropdown();

            // Game version dropdown
            RefreshGameVersionDropdown();

            // Version info
            lblVersionInfo.Text = LocalizationManager.Get("LabelVersion", AppUpdater.AppVersion);
        }

        private void RefreshThemeDropdown()
        {
            isUpdatingTheme = true;
            cmbTheme.Items.Clear();
            cmbTheme.Items.Add(LocalizationManager.Get("ThemeLight"));
            cmbTheme.Items.Add(LocalizationManager.Get("ThemeDark"));
            cmbTheme.SelectedIndex = ThemeManager.IsDarkMode ? 1 : 0;
            isUpdatingTheme = false;
        }

        private void cmbTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdatingTheme) return;

            string theme = cmbTheme.SelectedIndex == 1 ? "Dark" : "Light";
            ThemeManager.SetTheme(theme);
            ThemeManager.ApplyTheme(this);
            Log(theme == "Dark" ? "🌙 Dark theme applied." : "☀️ Light theme applied.");
        }

        private void RefreshGameVersionDropdown()
        {
            cmbGameVersion.Items.Clear();
            cmbGameVersion.Items.Add(LocalizationManager.Get("GameEldenRing"));
            cmbGameVersion.Items.Add(LocalizationManager.Get("GameDS1"));
            cmbGameVersion.Items.Add(LocalizationManager.Get("GameDS3"));
            cmbGameVersion.SelectedIndex = 0;
        }

        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdatingLanguage) return;

            string[] langs = LocalizationManager.GetAvailableLanguages();
            if (cmbLanguage.SelectedIndex >= 0 && cmbLanguage.SelectedIndex < langs.Length)
            {
                string selectedLang = langs[cmbLanguage.SelectedIndex];
                if (selectedLang != LocalizationManager.CurrentLanguage)
                {
                    LocalizationManager.SetLanguage(selectedLang);
                    ApplyLocalization();
                    Log(LocalizationManager.Get("MsgAppStarted", AppUpdater.AppVersion));
                }
            }
        }



        // --- Gestión de Configuración Base ---
        private void btnSeleccionarPartidas_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (!string.IsNullOrEmpty(txtRutaPartidas.Text) && Directory.Exists(txtRutaPartidas.Text))
                    fbd.SelectedPath = txtRutaPartidas.Text;

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtRutaPartidas.Text = fbd.SelectedPath;
                    GuardarConfiguracion("RutaPartidas", txtRutaPartidas.Text);
                    ActualizarEstado();
                    Log(LocalizationManager.Get("MsgPathUpdated", txtRutaPartidas.Text));
                }
            }
        }

        private void btnAutodetectar_Click(object sender, EventArgs e)
        {
            string rutaDeteccion = SaveFileManager.ObtenerRutaPorDefecto();
            if (!string.IsNullOrEmpty(rutaDeteccion))
            {
                txtRutaPartidas.Text = rutaDeteccion;
                GuardarConfiguracion("RutaPartidas", txtRutaPartidas.Text);
                ActualizarEstado();
                Log(LocalizationManager.Get("MsgPathAutodetected"));
            }
        }

        private void btnSeleccionarVanilla_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Executable|*.exe" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtRutaVanilla.Text = ofd.FileName;
                    GuardarConfiguracion("RutaVanilla", txtRutaVanilla.Text);
                    CargarConfiguracionIni();
                }
            }
        }

        private void btnSeleccionarSeamless_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Launcher|*.exe;*.lnk" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtRutaSeamless.Text = ofd.FileName;
                    GuardarConfiguracion("RutaSeamless", txtRutaSeamless.Text);
                }
            }
        }

        // --- Accesos Directos ---
        private void btnCrearAccesos_Click(object sender, EventArgs e)
        {
            CrearAccesosMetodo(interactivo: true);
        }

        private void CrearAccesosMetodo(bool interactivo)
        {
            if (string.IsNullOrEmpty(txtRutaPartidas.Text) || string.IsNullOrEmpty(txtRutaVanilla.Text) || string.IsNullOrEmpty(txtRutaSeamless.Text))
            {
                if (interactivo) MessageBox.Show(
                    LocalizationManager.Get("MsgMissingPaths"),
                    LocalizationManager.Get("MsgAttention"),
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string iconCoop = txtRutaSeamless.Text.EndsWith(".lnk", StringComparison.OrdinalIgnoreCase) ? txtRutaVanilla.Text : txtRutaSeamless.Text;
                
                if (interactivo)
                {
                    DialogResult customIcon = MessageBox.Show(
                        LocalizationManager.Get("MsgCustomIcon"), 
                        LocalizationManager.Get("MsgCustomIconTitle"),
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (customIcon == DialogResult.Cancel) return;
                    if (customIcon == DialogResult.Yes)
                    {
                        using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Icon (*.ico)|*.ico" })
                        {
                            if (ofd.ShowDialog() == DialogResult.OK) iconCoop = ofd.FileName;
                        }
                    }
                }

                ShortcutCreator.CrearAccesoDirecto("ER - Vanilla", "-launch_vanilla", txtRutaVanilla.Text, txtRutaVanilla.Text);
                ShortcutCreator.CrearAccesoDirecto("ER - Seamless Co-op", "-launch_seamless", txtRutaSeamless.Text, iconCoop);

                if (interactivo) MessageBox.Show(
                    LocalizationManager.Get("MsgShortcutsCreatedSuccess"),
                    LocalizationManager.Get("MsgSuccess"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log(LocalizationManager.Get("MsgShortcutsCreated"));
            }
            catch (Exception ex)
            {
                Log(LocalizationManager.Get("MsgErrorCreatingShortcuts", ex.Message));
            }
        }

        // --- Gestión de Perfiles ---
        private void CargarPerfiles()
        {
            isUpdatingProfiles = true;
            cmbPerfiles.Items.Clear();
            string perfilesStr = ConfigHelper.GetSetting("Perfiles") ?? "Default";
            string[] perfiles = perfilesStr.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var p in perfiles) cmbPerfiles.Items.Add(p);
            
            string actual = ConfigHelper.GetSetting("PerfilActual") ?? "Default";
            if (cmbPerfiles.Items.Contains(actual)) cmbPerfiles.SelectedItem = actual;
            else if (cmbPerfiles.Items.Count > 0) cmbPerfiles.SelectedIndex = 0;
                
            isUpdatingProfiles = false;
        }

        private void cmbPerfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdatingProfiles || string.IsNullOrEmpty(txtRutaPartidas.Text)) return;
            string perfilNuevo = cmbPerfiles.SelectedItem.ToString();
            string perfilAnterior = ConfigHelper.GetSetting("PerfilActual");
            
            if (perfilNuevo != perfilAnterior)
            {
                try
                {
                    SaveFileManager.AplicarPerfil(txtRutaPartidas.Text, perfilAnterior, perfilNuevo);
                    GuardarConfiguracion("PerfilActual", perfilNuevo);
                    Log(LocalizationManager.Get("MsgProfileChanged", perfilAnterior, perfilNuevo));
                }
                catch (Exception ex) { MessageBox.Show(LocalizationManager.Get("MsgErrorRotatingProfiles", ex.Message)); }
            }
        }

        private void btnNuevoPerfil_Click(object sender, EventArgs e)
        {
            string input = MostrarInputBox(
                LocalizationManager.Get("MsgNewProfilePrompt"),
                LocalizationManager.Get("MsgNewProfileTitle"));
            if (!string.IsNullOrWhiteSpace(input) && !cmbPerfiles.Items.Contains(input))
            {
                string perfilesStr = ConfigHelper.GetSetting("Perfiles");
                string perfiles = string.IsNullOrEmpty(perfilesStr) ? input : perfilesStr + "|" + input;
                GuardarConfiguracion("Perfiles", perfiles);
                CargarPerfiles();
                cmbPerfiles.SelectedItem = input;
                Log(LocalizationManager.Get("MsgNewProfileCreated", input));
            }
        }

        private string MostrarInputBox(string texto, string caption)
        {
            Form prompt = new Form() { Width = 400, Height = 150, FormBorderStyle = FormBorderStyle.FixedDialog, Text = caption, StartPosition = FormStartPosition.CenterScreen };
            Label txt = new Label() { Left = 20, Top = 20, Width = 340, Text = texto };
            TextBox box = new TextBox() { Left = 20, Top = 50, Width = 340 };
            Button confirm = new Button() { Text = LocalizationManager.Get("BtnOk"), Left = 260, Top = 80, Width = 100, DialogResult = DialogResult.OK };
            prompt.Controls.Add(txt); prompt.Controls.Add(box); prompt.Controls.Add(confirm);
            prompt.AcceptButton = confirm;
            return prompt.ShowDialog() == DialogResult.OK ? box.Text : "";
        }

        // --- Instalador ---
        private void btnActualizador_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRutaVanilla.Text))
            {
                MessageBox.Show(
                    LocalizationManager.Get("MsgSelectVanillaFirst"),
                    LocalizationManager.Get("MsgPathRequired"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            btnActualizador.Enabled = false;

            try
            {
                // Direct local zip install — no network calls
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = LocalizationManager.Get("MsgZipFilter");
                    ofd.Title = LocalizationManager.Get("MsgSelectZipTitle");

                    if (ofd.ShowDialog() != DialogResult.OK) return;

                    Log(LocalizationManager.Get("MsgInstallingFromZip"));
                    string launcherPath = ModInstaller.InstalarDesdeZip(ofd.FileName, txtRutaVanilla.Text);
                    txtRutaSeamless.Text = launcherPath;
                    GuardarConfiguracion("RutaSeamless", launcherPath);

                    Log(LocalizationManager.Get("MsgCreatingShortcutsAuto"));
                    CrearAccesosMetodo(interactivo: false);

                    MessageBox.Show(
                        LocalizationManager.Get("MsgInstallComplete"),
                        LocalizationManager.Get("MsgCompleted"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Log(LocalizationManager.Get("MsgInstallError", ex.Message));
                MessageBox.Show(
                    LocalizationManager.Get("MsgInstallErrorDetail", ex.Message),
                    LocalizationManager.Get("MsgInstallErrorTitle"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { btnActualizador.Enabled = true; }
        }

        private void btnLimpiarCache_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRutaVanilla.Text)) return;
            if (MessageBox.Show(
                LocalizationManager.Get("MsgCleanCrashConfirm"),
                LocalizationManager.Get("MsgCleanTitle"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SaveFileManager.LimpiarTemporales(txtRutaVanilla.Text);
                Log(LocalizationManager.Get("MsgCleanDone"));
            }
        }

        // --- Mapeo Dinámico de INI ---
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabSeamlessConfig) CargarConfiguracionIni();
        }

        private void btnRecargarIni_Click(object sender, EventArgs e)
        {
            CargarConfiguracionIni();
            Log(LocalizationManager.Get("MsgIniReloaded"));
        }

        private void btnModoExperto_Click(object sender, EventArgs e)
        {
            txtSeamlessIni.Visible = !txtSeamlessIni.Visible;
            dgvConfig.Visible = !txtSeamlessIni.Visible;
            btnModoExperto.Text = txtSeamlessIni.Visible 
                ? LocalizationManager.Get("BtnToggleVisual") 
                : LocalizationManager.Get("BtnToggleExpert");
        }

        private void CargarConfiguracionIni()
        {
            string iniPath = CurrentIniPath;
            dgvConfig.Rows.Clear();
            txtSeamlessIni.Clear();

            if (string.IsNullOrEmpty(iniPath))
            {
                txtSeamlessIni.Text = LocalizationManager.Get("MsgVanillaPathNotSet");
                return;
            }

            if (!File.Exists(iniPath))
            {
                txtSeamlessIni.Text = LocalizationManager.Get("MsgIniNotFound");
                return;
            }

            // Smart Mapping
            string[] lineas = File.ReadAllLines(iniPath);
            txtSeamlessIni.Text = string.Join(Environment.NewLine, lineas);

            foreach (var l in lineas)
            {
                string linea = l.Trim();
                if (string.IsNullOrWhiteSpace(linea) || linea.StartsWith(";") || linea.StartsWith("#") || linea.StartsWith("["))
                    continue;

                int idx = linea.IndexOf('=');
                if (idx > 0)
                {
                    string k = linea.Substring(0, idx).Trim();
                    string v = linea.Substring(idx + 1).Trim();
                    dgvConfig.Rows.Add(k, v);
                }
            }
        }

        private void btnGuardarIni_Click(object sender, EventArgs e)
        {
            string iniPath = CurrentIniPath;
            if (string.IsNullOrEmpty(iniPath) || !File.Exists(iniPath)) return;

            try
            {
                if (txtSeamlessIni.Visible)
                {
                    File.WriteAllText(iniPath, txtSeamlessIni.Text);
                }
                else
                {
                    var gridValores = new Dictionary<string, string>();
                    foreach (DataGridViewRow row in dgvConfig.Rows)
                    {
                        if (row.Cells[0].Value != null)
                            gridValores[row.Cells[0].Value.ToString()] = row.Cells[1].Value?.ToString() ?? "";
                    }

                    string[] lineasOriginales = File.ReadAllLines(iniPath);
                    for (int i = 0; i < lineasOriginales.Length; i++)
                    {
                        string l = lineasOriginales[i].Trim();
                        if (string.IsNullOrWhiteSpace(l) || l.StartsWith(";") || l.StartsWith("#") || l.StartsWith("["))
                            continue;

                        int idx = l.IndexOf('=');
                        if (idx > 0)
                        {
                            string keyOriginal = l.Substring(0, idx).Trim();
                            if (gridValores.ContainsKey(keyOriginal))
                            {
                                lineasOriginales[i] = $"{keyOriginal} = {gridValores[keyOriginal]}";
                            }
                        }
                    }
                    File.WriteAllLines(iniPath, lineasOriginales);
                }
                
                MessageBox.Show(
                    LocalizationManager.Get("MsgIniSaved"),
                    LocalizationManager.Get("MsgSaved"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log(LocalizationManager.Get("MsgIniSavedLog"));
                
                CargarConfiguracionIni();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    LocalizationManager.Get("MsgIniSaveError", ex.Message),
                    LocalizationManager.Get("MsgIniSaveErrorTitle"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log(LocalizationManager.Get("MsgIniSaveError", ex.Message));
            }
        }

        // --- Utilidades ---
        private void ActualizarEstado()
        {
            var estado = SaveFileManager.DetectarEstadoActual(txtRutaPartidas.Text);
            
            switch (estado)
            {
                case EstadoPartida.Vanilla:
                    lblEstado.Text = LocalizationManager.Get("StatusVanilla");
                    lblEstado.ForeColor = System.Drawing.Color.DodgerBlue;
                    break;
                case EstadoPartida.Seamless:
                    lblEstado.Text = LocalizationManager.Get("StatusSeamless");
                    lblEstado.ForeColor = System.Drawing.Color.Goldenrod;
                    break;
                case EstadoPartida.Mixto:
                    lblEstado.Text = LocalizationManager.Get("StatusMixed");
                    lblEstado.ForeColor = System.Drawing.Color.Crimson;
                    break;
                case EstadoPartida.Desconocido:
                    lblEstado.Text = LocalizationManager.Get("StatusUnknown");
                    lblEstado.ForeColor = System.Drawing.Color.Gray;
                    break;
            }
        }

        private void GuardarConfiguracion(string key, string value)
        {
            ConfigHelper.SaveSetting(key, value);
        }

        private void Log(string mensaje)
        {
            Logger.Write($"[GUI] {mensaje}");
            if (txtLogs.InvokeRequired) { txtLogs.Invoke(new Action(() => Log(mensaje))); return; }
            txtLogs.AppendText($"[{DateTime.Now:HH:mm:ss}] {mensaje}{Environment.NewLine}");
        }
    }
}
