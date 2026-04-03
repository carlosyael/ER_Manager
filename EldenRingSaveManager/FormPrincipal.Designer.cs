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
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            
            // groupBox1
            this.groupBox1.Controls.Add(this.btnNuevoPerfil);
            this.groupBox1.Controls.Add(this.cmbPerfiles);
            this.groupBox1.Controls.Add(this.lblPerfil);
            this.groupBox1.Controls.Add(this.btnAutodetectar);
            this.groupBox1.Controls.Add(this.btnSeleccionarPartidas);
            this.groupBox1.Controls.Add(this.txtRutaPartidas);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(600, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ruta de Partidas Guardadas y Perfiles";
            
            // txtRutaPartidas
            this.txtRutaPartidas.Location = new System.Drawing.Point(15, 27);
            this.txtRutaPartidas.Name = "txtRutaPartidas";
            this.txtRutaPartidas.ReadOnly = true;
            this.txtRutaPartidas.Size = new System.Drawing.Size(378, 23);
            this.txtRutaPartidas.TabIndex = 0;
            
            // btnSeleccionarPartidas
            this.btnSeleccionarPartidas.Location = new System.Drawing.Point(399, 25);
            this.btnSeleccionarPartidas.Name = "btnSeleccionarPartidas";
            this.btnSeleccionarPartidas.Size = new System.Drawing.Size(90, 26);
            this.btnSeleccionarPartidas.TabIndex = 1;
            this.btnSeleccionarPartidas.Text = "Seleccionar...";
            this.btnSeleccionarPartidas.UseVisualStyleBackColor = true;
            this.btnSeleccionarPartidas.Click += new System.EventHandler(this.btnSeleccionarPartidas_Click);
            
            // btnAutodetectar
            this.btnAutodetectar.Location = new System.Drawing.Point(495, 25);
            this.btnAutodetectar.Name = "btnAutodetectar";
            this.btnAutodetectar.Size = new System.Drawing.Size(90, 26);
            this.btnAutodetectar.TabIndex = 2;
            this.btnAutodetectar.Text = "Autodetectar";
            this.btnAutodetectar.UseVisualStyleBackColor = true;
            this.btnAutodetectar.Click += new System.EventHandler(this.btnAutodetectar_Click);
            
            // lblPerfil
            this.lblPerfil.AutoSize = true;
            this.lblPerfil.Location = new System.Drawing.Point(15, 65);
            this.lblPerfil.Name = "lblPerfil";
            this.lblPerfil.Size = new System.Drawing.Size(81, 15);
            this.lblPerfil.TabIndex = 3;
            this.lblPerfil.Text = "Perfil Activo:";
            
            // cmbPerfiles
            this.cmbPerfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPerfiles.FormattingEnabled = true;
            this.cmbPerfiles.Location = new System.Drawing.Point(100, 62);
            this.cmbPerfiles.Name = "cmbPerfiles";
            this.cmbPerfiles.Size = new System.Drawing.Size(293, 23);
            this.cmbPerfiles.TabIndex = 4;
            this.cmbPerfiles.SelectedIndexChanged += new System.EventHandler(this.cmbPerfiles_SelectedIndexChanged);
            
            // btnNuevoPerfil
            this.btnNuevoPerfil.Location = new System.Drawing.Point(399, 60);
            this.btnNuevoPerfil.Name = "btnNuevoPerfil";
            this.btnNuevoPerfil.Size = new System.Drawing.Size(186, 26);
            this.btnNuevoPerfil.TabIndex = 5;
            this.btnNuevoPerfil.Text = "+ Nuevo Perfil Co-op";
            this.btnNuevoPerfil.UseVisualStyleBackColor = true;
            this.btnNuevoPerfil.Click += new System.EventHandler(this.btnNuevoPerfil_Click);
            
            // groupBox2
            this.groupBox2.Controls.Add(this.btnSeleccionarSeamless);
            this.groupBox2.Controls.Add(this.txtRutaSeamless);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnSeleccionarVanilla);
            this.groupBox2.Controls.Add(this.txtRutaVanilla);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(600, 95);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rutas de Ejecutables";
            
            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vanilla:";
            
            // txtRutaVanilla
            this.txtRutaVanilla.Location = new System.Drawing.Point(135, 25);
            this.txtRutaVanilla.Name = "txtRutaVanilla";
            this.txtRutaVanilla.ReadOnly = true;
            this.txtRutaVanilla.Size = new System.Drawing.Size(354, 23);
            this.txtRutaVanilla.TabIndex = 1;
            
            // btnSeleccionarVanilla
            this.btnSeleccionarVanilla.Location = new System.Drawing.Point(495, 23);
            this.btnSeleccionarVanilla.Name = "btnSeleccionarVanilla";
            this.btnSeleccionarVanilla.Size = new System.Drawing.Size(90, 26);
            this.btnSeleccionarVanilla.TabIndex = 2;
            this.btnSeleccionarVanilla.Text = "Seleccionar...";
            this.btnSeleccionarVanilla.UseVisualStyleBackColor = true;
            this.btnSeleccionarVanilla.Click += new System.EventHandler(this.btnSeleccionarVanilla_Click);
            
            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Seamless Co-op:";
            
            // txtRutaSeamless
            this.txtRutaSeamless.Location = new System.Drawing.Point(135, 57);
            this.txtRutaSeamless.Name = "txtRutaSeamless";
            this.txtRutaSeamless.ReadOnly = true;
            this.txtRutaSeamless.Size = new System.Drawing.Size(354, 23);
            this.txtRutaSeamless.TabIndex = 4;
            
            // btnSeleccionarSeamless
            this.btnSeleccionarSeamless.Location = new System.Drawing.Point(495, 55);
            this.btnSeleccionarSeamless.Name = "btnSeleccionarSeamless";
            this.btnSeleccionarSeamless.Size = new System.Drawing.Size(90, 26);
            this.btnSeleccionarSeamless.TabIndex = 5;
            this.btnSeleccionarSeamless.Text = "Seleccionar...";
            this.btnSeleccionarSeamless.UseVisualStyleBackColor = true;
            this.btnSeleccionarSeamless.Click += new System.EventHandler(this.btnSeleccionarSeamless_Click);
            
            // groupBox4
            this.groupBox4.Controls.Add(this.btnLimpiarCache);
            this.groupBox4.Controls.Add(this.btnActualizador);
            this.groupBox4.Location = new System.Drawing.Point(12, 225);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(260, 95);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Mantenimiento Extra";
            
            // btnActualizador
            this.btnActualizador.Location = new System.Drawing.Point(15, 23);
            this.btnActualizador.Name = "btnActualizador";
            this.btnActualizador.Size = new System.Drawing.Size(230, 26);
            this.btnActualizador.TabIndex = 0;
            this.btnActualizador.Text = "Buscar Act. Seamless Co-op (Web)";
            this.btnActualizador.UseVisualStyleBackColor = true;
            this.btnActualizador.Click += new System.EventHandler(this.btnActualizador_Click);
            
            // btnLimpiarCache
            this.btnLimpiarCache.Location = new System.Drawing.Point(15, 55);
            this.btnLimpiarCache.Name = "btnLimpiarCache";
            this.btnLimpiarCache.Size = new System.Drawing.Size(230, 26);
            this.btnLimpiarCache.TabIndex = 1;
            this.btnLimpiarCache.Text = "Limpiar Game Crash Dumps";
            this.btnLimpiarCache.UseVisualStyleBackColor = true;
            this.btnLimpiarCache.Click += new System.EventHandler(this.btnLimpiarCache_Click);
            
            // lblEstado
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblEstado.Location = new System.Drawing.Point(300, 235);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(217, 21);
            this.lblEstado.TabIndex = 2;
            this.lblEstado.Text = "Estado actual: Desconocido";
            
            // btnCrearAccesos
            this.btnCrearAccesos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCrearAccesos.Location = new System.Drawing.Point(290, 270);
            this.btnCrearAccesos.Name = "btnCrearAccesos";
            this.btnCrearAccesos.Size = new System.Drawing.Size(322, 38);
            this.btnCrearAccesos.TabIndex = 3;
            this.btnCrearAccesos.Text = "Crear Accesos Directos";
            this.btnCrearAccesos.UseVisualStyleBackColor = true;
            this.btnCrearAccesos.Click += new System.EventHandler(this.btnCrearAccesos_Click);
            
            // groupBox3
            this.groupBox3.Controls.Add(this.txtLogs);
            this.groupBox3.Location = new System.Drawing.Point(12, 330);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(600, 160);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Logs de Actividad";
            
            // txtLogs
            this.txtLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogs.Location = new System.Drawing.Point(3, 19);
            this.txtLogs.Multiline = true;
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.ReadOnly = true;
            this.txtLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogs.Size = new System.Drawing.Size(594, 138);
            this.txtLogs.TabIndex = 0;
            
            // FormPrincipal
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 501);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnCrearAccesos);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elden Ring: Seamless Co-op Save Manager v2.0";
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

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
    }
}
