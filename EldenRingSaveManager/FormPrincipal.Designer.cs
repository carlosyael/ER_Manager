namespace EldenRingSaveManager
{
    partial class FormPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNuevoPerfil = new System.Windows.Forms.Button();
            this.cmbPerfiles = new System.Windows.Forms.ComboBox();
            this.lblPerfil = new System.Windows.Forms.Label();
            this.btnAutodetectar = new System.Windows.Forms.Button();
            this.btnSeleccionarPartidas = new System.Windows.Forms.Button();
            this.txtRutaPartidas = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSeleccionarSeamless = new System.Windows.Forms.Button();
            this.txtRutaSeamless = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSeleccionarVanilla = new System.Windows.Forms.Button();
            this.txtRutaVanilla = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnLimpiarCache = new System.Windows.Forms.Button();
            this.btnActualizador = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();
            this.btnCrearAccesos = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtLogs = new System.Windows.Forms.TextBox();
            this.tabSeamlessConfig = new System.Windows.Forms.TabPage();
            this.dgvConfig = new System.Windows.Forms.DataGridView();
            this.colProperty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGuardarIni = new System.Windows.Forms.Button();
            this.btnRecargarIni = new System.Windows.Forms.Button();
            this.btnModoExperto = new System.Windows.Forms.Button();
            this.txtSeamlessIni = new System.Windows.Forms.TextBox();
            // Settings tab controls
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.grpLanguage = new System.Windows.Forms.GroupBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.grpTheme = new System.Windows.Forms.GroupBox();
            this.lblTheme = new System.Windows.Forms.Label();
            this.cmbTheme = new System.Windows.Forms.ComboBox();
            this.grpGameVersion = new System.Windows.Forms.GroupBox();
            this.lblGameVersion = new System.Windows.Forms.Label();
            this.cmbGameVersion = new System.Windows.Forms.ComboBox();
            this.grpAbout = new System.Windows.Forms.GroupBox();
            this.lblVersionInfo = new System.Windows.Forms.Label();
            this.lblUpdateStatus = new System.Windows.Forms.Label();
            this.btnCheckUpdates = new System.Windows.Forms.Button();
            
            this.tabControl1.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabSeamlessConfig.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.grpLanguage.SuspendLayout();
            this.grpTheme.SuspendLayout();
            this.grpGameVersion.SuspendLayout();
            this.grpAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfig)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabGeneral);
            this.tabControl1.Controls.Add(this.tabSeamlessConfig);
            this.tabControl1.Controls.Add(this.tabSettings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(624, 501);
            this.tabControl1.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.groupBox1);
            this.tabGeneral.Controls.Add(this.groupBox2);
            this.tabGeneral.Controls.Add(this.groupBox4);
            this.tabGeneral.Controls.Add(this.lblEstado);
            this.tabGeneral.Controls.Add(this.btnCrearAccesos);
            this.tabGeneral.Controls.Add(this.groupBox3);
            this.tabGeneral.Location = new System.Drawing.Point(4, 24);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(616, 473);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "Launcher & Profiles";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNuevoPerfil);
            this.groupBox1.Controls.Add(this.cmbPerfiles);
            this.groupBox1.Controls.Add(this.lblPerfil);
            this.groupBox1.Controls.Add(this.btnAutodetectar);
            this.groupBox1.Controls.Add(this.btnSeleccionarPartidas);
            this.groupBox1.Controls.Add(this.txtRutaPartidas);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(600, 95);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Save Files Path & Profiles";
            // 
            // txtRutaPartidas
            // 
            this.txtRutaPartidas.Location = new System.Drawing.Point(15, 27);
            this.txtRutaPartidas.Name = "txtRutaPartidas";
            this.txtRutaPartidas.ReadOnly = true;
            this.txtRutaPartidas.Size = new System.Drawing.Size(378, 23);
            this.txtRutaPartidas.TabIndex = 0;
            // 
            // btnSeleccionarPartidas
            // 
            this.btnSeleccionarPartidas.Location = new System.Drawing.Point(399, 25);
            this.btnSeleccionarPartidas.Name = "btnSeleccionarPartidas";
            this.btnSeleccionarPartidas.Size = new System.Drawing.Size(90, 26);
            this.btnSeleccionarPartidas.TabIndex = 1;
            this.btnSeleccionarPartidas.Text = "Select...";
            this.btnSeleccionarPartidas.UseVisualStyleBackColor = true;
            this.btnSeleccionarPartidas.Click += new System.EventHandler(this.btnSeleccionarPartidas_Click);
            // 
            // btnAutodetectar
            // 
            this.btnAutodetectar.Location = new System.Drawing.Point(495, 25);
            this.btnAutodetectar.Name = "btnAutodetectar";
            this.btnAutodetectar.Size = new System.Drawing.Size(90, 26);
            this.btnAutodetectar.TabIndex = 2;
            this.btnAutodetectar.Text = "Autodetect";
            this.btnAutodetectar.UseVisualStyleBackColor = true;
            this.btnAutodetectar.Click += new System.EventHandler(this.btnAutodetectar_Click);
            // 
            // lblPerfil
            // 
            this.lblPerfil.AutoSize = true;
            this.lblPerfil.Location = new System.Drawing.Point(15, 65);
            this.lblPerfil.Name = "lblPerfil";
            this.lblPerfil.Size = new System.Drawing.Size(81, 15);
            this.lblPerfil.TabIndex = 3;
            this.lblPerfil.Text = "Active Profile:";
            // 
            // cmbPerfiles
            // 
            this.cmbPerfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPerfiles.FormattingEnabled = true;
            this.cmbPerfiles.Location = new System.Drawing.Point(100, 62);
            this.cmbPerfiles.Name = "cmbPerfiles";
            this.cmbPerfiles.Size = new System.Drawing.Size(293, 23);
            this.cmbPerfiles.TabIndex = 4;
            this.cmbPerfiles.SelectedIndexChanged += new System.EventHandler(this.cmbPerfiles_SelectedIndexChanged);
            // 
            // btnNuevoPerfil
            // 
            this.btnNuevoPerfil.Location = new System.Drawing.Point(399, 60);
            this.btnNuevoPerfil.Name = "btnNuevoPerfil";
            this.btnNuevoPerfil.Size = new System.Drawing.Size(186, 26);
            this.btnNuevoPerfil.TabIndex = 5;
            this.btnNuevoPerfil.Text = "+ New Co-op Profile";
            this.btnNuevoPerfil.UseVisualStyleBackColor = true;
            this.btnNuevoPerfil.Click += new System.EventHandler(this.btnNuevoPerfil_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSeleccionarSeamless);
            this.groupBox2.Controls.Add(this.txtRutaSeamless);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnSeleccionarVanilla);
            this.groupBox2.Controls.Add(this.txtRutaVanilla);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(600, 95);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Executable Paths";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vanilla:";
            // 
            // txtRutaVanilla
            // 
            this.txtRutaVanilla.Location = new System.Drawing.Point(135, 25);
            this.txtRutaVanilla.Name = "txtRutaVanilla";
            this.txtRutaVanilla.ReadOnly = true;
            this.txtRutaVanilla.Size = new System.Drawing.Size(354, 23);
            this.txtRutaVanilla.TabIndex = 1;
            // 
            // btnSeleccionarVanilla
            // 
            this.btnSeleccionarVanilla.Location = new System.Drawing.Point(495, 23);
            this.btnSeleccionarVanilla.Name = "btnSeleccionarVanilla";
            this.btnSeleccionarVanilla.Size = new System.Drawing.Size(90, 26);
            this.btnSeleccionarVanilla.TabIndex = 2;
            this.btnSeleccionarVanilla.Text = "Select...";
            this.btnSeleccionarVanilla.UseVisualStyleBackColor = true;
            this.btnSeleccionarVanilla.Click += new System.EventHandler(this.btnSeleccionarVanilla_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Seamless Co-op:";
            // 
            // txtRutaSeamless
            // 
            this.txtRutaSeamless.Location = new System.Drawing.Point(135, 57);
            this.txtRutaSeamless.Name = "txtRutaSeamless";
            this.txtRutaSeamless.ReadOnly = true;
            this.txtRutaSeamless.Size = new System.Drawing.Size(354, 23);
            this.txtRutaSeamless.TabIndex = 4;
            // 
            // btnSeleccionarSeamless
            // 
            this.btnSeleccionarSeamless.Location = new System.Drawing.Point(495, 55);
            this.btnSeleccionarSeamless.Name = "btnSeleccionarSeamless";
            this.btnSeleccionarSeamless.Size = new System.Drawing.Size(90, 26);
            this.btnSeleccionarSeamless.TabIndex = 5;
            this.btnSeleccionarSeamless.Text = "Select...";
            this.btnSeleccionarSeamless.UseVisualStyleBackColor = true;
            this.btnSeleccionarSeamless.Click += new System.EventHandler(this.btnSeleccionarSeamless_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnLimpiarCache);
            this.groupBox4.Controls.Add(this.btnActualizador);
            this.groupBox4.Location = new System.Drawing.Point(6, 215);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(260, 95);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Extra Maintenance";
            // 
            // btnActualizador
            // 
            this.btnActualizador.Location = new System.Drawing.Point(15, 23);
            this.btnActualizador.Name = "btnActualizador";
            this.btnActualizador.Size = new System.Drawing.Size(230, 26);
            this.btnActualizador.TabIndex = 0;
            this.btnActualizador.Text = "Download/Update Seamless Co-op";
            this.btnActualizador.UseVisualStyleBackColor = true;
            this.btnActualizador.Click += new System.EventHandler(this.btnActualizador_Click);
            // 
            // btnLimpiarCache
            // 
            this.btnLimpiarCache.Location = new System.Drawing.Point(15, 55);
            this.btnLimpiarCache.Name = "btnLimpiarCache";
            this.btnLimpiarCache.Size = new System.Drawing.Size(230, 26);
            this.btnLimpiarCache.TabIndex = 1;
            this.btnLimpiarCache.Text = "Clean Crash Dumps";
            this.btnLimpiarCache.UseVisualStyleBackColor = true;
            this.btnLimpiarCache.Click += new System.EventHandler(this.btnLimpiarCache_Click);
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblEstado.Location = new System.Drawing.Point(280, 225);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(217, 21);
            this.lblEstado.TabIndex = 2;
            this.lblEstado.Text = "Current status: Unknown";
            // 
            // btnCrearAccesos
            // 
            this.btnCrearAccesos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCrearAccesos.Location = new System.Drawing.Point(280, 260);
            this.btnCrearAccesos.Name = "btnCrearAccesos";
            this.btnCrearAccesos.Size = new System.Drawing.Size(326, 38);
            this.btnCrearAccesos.TabIndex = 3;
            this.btnCrearAccesos.Text = "Create Desktop Shortcuts";
            this.btnCrearAccesos.UseVisualStyleBackColor = true;
            this.btnCrearAccesos.Click += new System.EventHandler(this.btnCrearAccesos_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtLogs);
            this.groupBox3.Location = new System.Drawing.Point(6, 320);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(600, 140);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Activity Logs";
            // 
            // txtLogs
            // 
            this.txtLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogs.Location = new System.Drawing.Point(3, 19);
            this.txtLogs.Multiline = true;
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.ReadOnly = true;
            this.txtLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogs.Size = new System.Drawing.Size(594, 118);
            this.txtLogs.TabIndex = 0;
            // 
            // tabSeamlessConfig
            // 
            this.tabSeamlessConfig.Controls.Add(this.btnModoExperto);
            this.tabSeamlessConfig.Controls.Add(this.btnGuardarIni);
            this.tabSeamlessConfig.Controls.Add(this.btnRecargarIni);
            this.tabSeamlessConfig.Controls.Add(this.dgvConfig);
            this.tabSeamlessConfig.Controls.Add(this.txtSeamlessIni);
            this.tabSeamlessConfig.Location = new System.Drawing.Point(4, 24);
            this.tabSeamlessConfig.Name = "tabSeamlessConfig";
            this.tabSeamlessConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabSeamlessConfig.Size = new System.Drawing.Size(616, 473);
            this.tabSeamlessConfig.TabIndex = 1;
            this.tabSeamlessConfig.Text = "Seamless Co-op Config";
            this.tabSeamlessConfig.UseVisualStyleBackColor = true;
            // 
            // dgvConfig
            // 
            this.dgvConfig.AllowUserToAddRows = false;
            this.dgvConfig.AllowUserToDeleteRows = false;
            this.dgvConfig.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConfig.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProperty,
            this.colValue});
            this.dgvConfig.Location = new System.Drawing.Point(8, 8);
            this.dgvConfig.Name = "dgvConfig";
            this.dgvConfig.RowHeadersVisible = false;
            this.dgvConfig.Size = new System.Drawing.Size(600, 420);
            this.dgvConfig.TabIndex = 0;
            // 
            // colProperty
            // 
            this.colProperty.HeaderText = "Setting";
            this.colProperty.Name = "colProperty";
            this.colProperty.ReadOnly = true;
            // 
            // colValue
            // 
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            // 
            // txtSeamlessIni
            // 
            this.txtSeamlessIni.Location = new System.Drawing.Point(8, 8);
            this.txtSeamlessIni.Multiline = true;
            this.txtSeamlessIni.Name = "txtSeamlessIni";
            this.txtSeamlessIni.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSeamlessIni.Size = new System.Drawing.Size(600, 420);
            this.txtSeamlessIni.TabIndex = 1;
            this.txtSeamlessIni.Visible = false;
            this.txtSeamlessIni.WordWrap = false;
            // 
            // btnRecargarIni
            // 
            this.btnRecargarIni.Location = new System.Drawing.Point(8, 435);
            this.btnRecargarIni.Name = "btnRecargarIni";
            this.btnRecargarIni.Size = new System.Drawing.Size(120, 30);
            this.btnRecargarIni.TabIndex = 2;
            this.btnRecargarIni.Text = "Reload File";
            this.btnRecargarIni.UseVisualStyleBackColor = true;
            this.btnRecargarIni.Click += new System.EventHandler(this.btnRecargarIni_Click);
            // 
            // btnGuardarIni
            // 
            this.btnGuardarIni.Location = new System.Drawing.Point(140, 435);
            this.btnGuardarIni.Name = "btnGuardarIni";
            this.btnGuardarIni.Size = new System.Drawing.Size(130, 30);
            this.btnGuardarIni.TabIndex = 3;
            this.btnGuardarIni.Text = "Save Changes";
            this.btnGuardarIni.UseVisualStyleBackColor = true;
            this.btnGuardarIni.Click += new System.EventHandler(this.btnGuardarIni_Click);
            // 
            // btnModoExperto
            // 
            this.btnModoExperto.Location = new System.Drawing.Point(408, 435);
            this.btnModoExperto.Name = "btnModoExperto";
            this.btnModoExperto.Size = new System.Drawing.Size(200, 30);
            this.btnModoExperto.TabIndex = 4;
            this.btnModoExperto.Text = "Toggle Expert Mode (Text)";
            this.btnModoExperto.UseVisualStyleBackColor = true;
            this.btnModoExperto.Click += new System.EventHandler(this.btnModoExperto_Click);
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.grpLanguage);
            this.tabSettings.Controls.Add(this.grpTheme);
            this.tabSettings.Controls.Add(this.grpGameVersion);
            this.tabSettings.Controls.Add(this.grpAbout);
            this.tabSettings.Location = new System.Drawing.Point(4, 24);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(616, 473);
            this.tabSettings.TabIndex = 2;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // grpLanguage
            // 
            this.grpLanguage.Controls.Add(this.lblLanguage);
            this.grpLanguage.Controls.Add(this.cmbLanguage);
            this.grpLanguage.Location = new System.Drawing.Point(6, 6);
            this.grpLanguage.Name = "grpLanguage";
            this.grpLanguage.Size = new System.Drawing.Size(600, 65);
            this.grpLanguage.TabIndex = 0;
            this.grpLanguage.TabStop = false;
            this.grpLanguage.Text = "Language";
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(15, 28);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(120, 15);
            this.lblLanguage.TabIndex = 0;
            this.lblLanguage.Text = "Interface Language:";
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Location = new System.Drawing.Point(200, 25);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(200, 23);
            this.cmbLanguage.TabIndex = 1;
            this.cmbLanguage.SelectedIndexChanged += new System.EventHandler(this.cmbLanguage_SelectedIndexChanged);
            // 
            // grpTheme
            // 
            this.grpTheme.Controls.Add(this.lblTheme);
            this.grpTheme.Controls.Add(this.cmbTheme);
            this.grpTheme.Location = new System.Drawing.Point(6, 80);
            this.grpTheme.Name = "grpTheme";
            this.grpTheme.Size = new System.Drawing.Size(600, 65);
            this.grpTheme.TabIndex = 1;
            this.grpTheme.TabStop = false;
            this.grpTheme.Text = "Theme";
            // 
            // lblTheme
            // 
            this.lblTheme.AutoSize = true;
            this.lblTheme.Location = new System.Drawing.Point(15, 28);
            this.lblTheme.Name = "lblTheme";
            this.lblTheme.Size = new System.Drawing.Size(120, 15);
            this.lblTheme.TabIndex = 0;
            this.lblTheme.Text = "Application Theme:";
            // 
            // cmbTheme
            // 
            this.cmbTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTheme.FormattingEnabled = true;
            this.cmbTheme.Location = new System.Drawing.Point(200, 25);
            this.cmbTheme.Name = "cmbTheme";
            this.cmbTheme.Size = new System.Drawing.Size(200, 23);
            this.cmbTheme.TabIndex = 1;
            this.cmbTheme.SelectedIndexChanged += new System.EventHandler(this.cmbTheme_SelectedIndexChanged);
            // 
            // grpGameVersion
            // 
            this.grpGameVersion.Controls.Add(this.lblGameVersion);
            this.grpGameVersion.Controls.Add(this.cmbGameVersion);
            this.grpGameVersion.Location = new System.Drawing.Point(6, 154);
            this.grpGameVersion.Name = "grpGameVersion";
            this.grpGameVersion.Size = new System.Drawing.Size(600, 65);
            this.grpGameVersion.TabIndex = 2;
            this.grpGameVersion.TabStop = false;
            this.grpGameVersion.Text = "Game Version";
            // 
            // lblGameVersion
            // 
            this.lblGameVersion.AutoSize = true;
            this.lblGameVersion.Location = new System.Drawing.Point(15, 28);
            this.lblGameVersion.Name = "lblGameVersion";
            this.lblGameVersion.Size = new System.Drawing.Size(80, 15);
            this.lblGameVersion.TabIndex = 0;
            this.lblGameVersion.Text = "Target Game:";
            // 
            // cmbGameVersion
            // 
            this.cmbGameVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGameVersion.FormattingEnabled = true;
            this.cmbGameVersion.Location = new System.Drawing.Point(200, 25);
            this.cmbGameVersion.Name = "cmbGameVersion";
            this.cmbGameVersion.Size = new System.Drawing.Size(300, 23);
            this.cmbGameVersion.TabIndex = 1;
            // 
            // grpAbout
            // 
            this.grpAbout.Controls.Add(this.lblVersionInfo);
            this.grpAbout.Controls.Add(this.lblUpdateStatus);
            this.grpAbout.Controls.Add(this.btnCheckUpdates);
            this.grpAbout.Location = new System.Drawing.Point(6, 228);
            this.grpAbout.Name = "grpAbout";
            this.grpAbout.Size = new System.Drawing.Size(600, 120);
            this.grpAbout.TabIndex = 3;
            this.grpAbout.TabStop = false;
            this.grpAbout.Text = "About & Updates";
            // 
            // lblVersionInfo
            // 
            this.lblVersionInfo.AutoSize = true;
            this.lblVersionInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblVersionInfo.Location = new System.Drawing.Point(15, 25);
            this.lblVersionInfo.Name = "lblVersionInfo";
            this.lblVersionInfo.Size = new System.Drawing.Size(150, 19);
            this.lblVersionInfo.TabIndex = 0;
            this.lblVersionInfo.Text = "Current Version: v5.0.0";
            // 
            // lblUpdateStatus
            // 
            this.lblUpdateStatus.AutoSize = true;
            this.lblUpdateStatus.Location = new System.Drawing.Point(15, 55);
            this.lblUpdateStatus.Name = "lblUpdateStatus";
            this.lblUpdateStatus.Size = new System.Drawing.Size(200, 15);
            this.lblUpdateStatus.TabIndex = 1;
            this.lblUpdateStatus.Text = "";
            // 
            // btnCheckUpdates
            // 
            this.btnCheckUpdates.Location = new System.Drawing.Point(15, 80);
            this.btnCheckUpdates.Name = "btnCheckUpdates";
            this.btnCheckUpdates.Size = new System.Drawing.Size(200, 30);
            this.btnCheckUpdates.TabIndex = 2;
            this.btnCheckUpdates.Text = "Check for Updates";
            this.btnCheckUpdates.UseVisualStyleBackColor = true;
            this.btnCheckUpdates.Click += new System.EventHandler(this.btnCheckUpdates_Click);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 501);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elden Ring: Seamless Co-op Save Manager v5.0";
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabSeamlessConfig.ResumeLayout(false);
            this.tabSeamlessConfig.PerformLayout();
            this.tabSettings.ResumeLayout(false);
            this.grpLanguage.ResumeLayout(false);
            this.grpLanguage.PerformLayout();
            this.grpTheme.ResumeLayout(false);
            this.grpTheme.PerformLayout();
            this.grpGameVersion.ResumeLayout(false);
            this.grpGameVersion.PerformLayout();
            this.grpAbout.ResumeLayout(false);
            this.grpAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfig)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabSeamlessConfig;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnNuevoPerfil;
        private System.Windows.Forms.ComboBox cmbPerfiles;
        private System.Windows.Forms.Label lblPerfil;
        private System.Windows.Forms.Button btnAutodetectar;
        private System.Windows.Forms.Button btnSeleccionarPartidas;
        private System.Windows.Forms.TextBox txtRutaPartidas;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSeleccionarSeamless;
        private System.Windows.Forms.TextBox txtRutaSeamless;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSeleccionarVanilla;
        private System.Windows.Forms.TextBox txtRutaVanilla;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnLimpiarCache;
        private System.Windows.Forms.Button btnActualizador;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Button btnCrearAccesos;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtLogs;
        
        private System.Windows.Forms.DataGridView dgvConfig;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProperty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        
        private System.Windows.Forms.TextBox txtSeamlessIni;
        private System.Windows.Forms.Button btnGuardarIni;
        private System.Windows.Forms.Button btnRecargarIni;
        private System.Windows.Forms.Button btnModoExperto;

        // Settings tab
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.GroupBox grpLanguage;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.GroupBox grpTheme;
        private System.Windows.Forms.Label lblTheme;
        private System.Windows.Forms.ComboBox cmbTheme;
        private System.Windows.Forms.GroupBox grpGameVersion;
        private System.Windows.Forms.Label lblGameVersion;
        private System.Windows.Forms.ComboBox cmbGameVersion;
        private System.Windows.Forms.GroupBox grpAbout;
        private System.Windows.Forms.Label lblVersionInfo;
        private System.Windows.Forms.Label lblUpdateStatus;
        private System.Windows.Forms.Button btnCheckUpdates;
    }
}
