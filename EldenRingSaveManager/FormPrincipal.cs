using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace EldenRingSaveManager
{
    public partial class FormPrincipal : Form
    {
        private bool isUpdatingProfiles = false;
        private string CurrentIniPath => !string.IsNullOrEmpty(txtRutaVanilla.Text) 
            ? Path.Combine(Path.GetDirectoryName(txtRutaVanilla.Text), "SeamlessCoop", "ersc_settings.ini")
            : string.Empty;

        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            txtRutaPartidas.Text = ConfigurationManager.AppSettings["RutaPartidas"] ?? string.Empty;
            txtRutaVanilla.Text = ConfigurationManager.AppSettings["RutaVanilla"] ?? string.Empty;
            txtRutaSeamless.Text = ConfigurationManager.AppSettings["RutaSeamless"] ?? string.Empty;

            CargarPerfiles();
            ActualizarEstado();
            
            Logger.Write("[GUI] UI Iniciada correctamente. v3.0");
            Log("Aplicación iniciada. Bienvenido a Elden Ring Save Manager v3.0.");

            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;
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
                    Log($"Ruta de partidas actualizada a: {txtRutaPartidas.Text}");
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
                Log($"Ruta autodectada y guardada.");
            }
        }

        private void btnSeleccionarVanilla_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Ejecutable|*.exe" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtRutaVanilla.Text = ofd.FileName;
                    GuardarConfiguracion("RutaVanilla", txtRutaVanilla.Text);
                    CargarConfiguracionIni(); // Intentar cargar config de seamless automáticamente
                }
            }
        }

        private void btnSeleccionarSeamless_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Lanzador|*.exe;*.lnk" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtRutaSeamless.Text = ofd.FileName;
                    GuardarConfiguracion("RutaSeamless", txtRutaSeamless.Text);
                }
            }
        }

        // --- Accesos Directos e Iconos Personalizados ---
        private void btnCrearAccesos_Click(object sender, EventArgs e)
        {
            CrearAccesosMetodo(interactivo: true);
        }

        private void CrearAccesosMetodo(bool interactivo)
        {
            if (string.IsNullOrEmpty(txtRutaPartidas.Text) || string.IsNullOrEmpty(txtRutaVanilla.Text) || string.IsNullOrEmpty(txtRutaSeamless.Text))
            {
                if (interactivo) MessageBox.Show("Faltan rutas por configurar antes de crear los accesos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string iconCoop = txtRutaSeamless.Text.EndsWith(".lnk", StringComparison.OrdinalIgnoreCase) ? txtRutaVanilla.Text : txtRutaSeamless.Text;
                
                if (interactivo)
                {
                    DialogResult customIcon = MessageBox.Show("¿Deseas elegir un icono personalizado para Seamless Co-op? (Si marcas No, usará el del juego)", 
                                                             "Icono Personalizado", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    
                    if (customIcon == DialogResult.Cancel) return;
                    
                    if (customIcon == DialogResult.Yes)
                    {
                        using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Icono (*.ico)|*.ico" })
                        {
                            if (ofd.ShowDialog() == DialogResult.OK)
                                iconCoop = ofd.FileName;
                        }
                    }
                }

                ShortcutCreator.CrearAccesoDirecto("ER - Vanilla", "-launch_vanilla", txtRutaVanilla.Text, txtRutaVanilla.Text);
                ShortcutCreator.CrearAccesoDirecto("ER - Seamless Co-op", "-launch_seamless", txtRutaSeamless.Text, iconCoop);

                if (interactivo) MessageBox.Show("Accesos directos creados.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log("Accesos directos creados con éxito.");
            }
            catch (Exception ex)
            {
                Log($"Error al crear accesos: {ex.Message}");
            }
        }

        // --- Gestión de Perfiles ---
        private void CargarPerfiles()
        {
            isUpdatingProfiles = true;
            cmbPerfiles.Items.Clear();
            
            string perfilesStr = ConfigurationManager.AppSettings["Perfiles"] ?? "Predeterminado";
            string[] perfiles = perfilesStr.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var p in perfiles) cmbPerfiles.Items.Add(p);
            
            string actual = ConfigurationManager.AppSettings["PerfilActual"] ?? "Predeterminado";
            if (cmbPerfiles.Items.Contains(actual))
                cmbPerfiles.SelectedItem = actual;
            else if (cmbPerfiles.Items.Count > 0)
                cmbPerfiles.SelectedIndex = 0;
                
            isUpdatingProfiles = false;
        }

        private void cmbPerfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdatingProfiles || string.IsNullOrEmpty(txtRutaPartidas.Text)) return;
            
            string perfilNuevo = cmbPerfiles.SelectedItem.ToString();
            string perfilAnterior = ConfigurationManager.AppSettings["PerfilActual"];
            
            if (perfilNuevo != perfilAnterior)
            {
                try
                {
                    SaveFileManager.AplicarPerfil(txtRutaPartidas.Text, perfilAnterior, perfilNuevo);
                    GuardarConfiguracion("PerfilActual", perfilNuevo);
                    Log($"Perfil cambiado de {perfilAnterior} a {perfilNuevo}. Archivos rotados.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al rotar perfiles: {ex.Message}");
                }
            }
        }

        private void btnNuevoPerfil_Click(object sender, EventArgs e)
        {
            string input = MostrarInputBox("Nombre del nuevo perfil (ej: Coop Con Maria):", "Nuevo Perfil");
            if (!string.IsNullOrWhiteSpace(input) && !cmbPerfiles.Items.Contains(input))
            {
                string perfilesStr = ConfigurationManager.AppSettings["Perfiles"];
                string perfiles = string.IsNullOrEmpty(perfilesStr) ? input : perfilesStr + "|" + input;
                GuardarConfiguracion("Perfiles", perfiles);
                CargarPerfiles();
                cmbPerfiles.SelectedItem = input;
                Log($"Nuevo perfil '{input}' creado y seleccionado.");
            }
        }

        private string MostrarInputBox(string texto, string caption)
        {
            Form prompt = new Form()
            {
                Width = 400, Height = 150, FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption, StartPosition = FormStartPosition.CenterScreen
            };
            Label txt = new Label() { Left = 20, Top = 20, Width = 340, Text = texto };
            TextBox box = new TextBox() { Left = 20, Top = 50, Width = 340 };
            Button confirm = new Button() { Text = "Aceptar", Left = 260, Top = 80, Width = 100, DialogResult = DialogResult.OK };
            prompt.Controls.Add(txt); prompt.Controls.Add(box); prompt.Controls.Add(confirm);
            prompt.AcceptButton = confirm;
            return prompt.ShowDialog() == DialogResult.OK ? box.Text : "";
        }

        // --- Instalador ---
        private async void btnActualizador_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRutaVanilla.Text))
            {
                MessageBox.Show("Por favor, selecciona primero tu eldenring.exe (Vanilla) para saber dónde instalar el mod.", "Ruta Necesaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult res = MessageBox.Show($"¿Deseas descargar de GitHub e instalar/actualizar Seamless Co-op automáticamente en {Path.GetDirectoryName(txtRutaVanilla.Text)}?",
                                               "Instalar Mod", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (res != DialogResult.Yes) return;

            Log("Iniciando instalación automática de Seamless Co-op...");
            btnActualizador.Enabled = false;

            try
            {
                string launcherPath = await ModInstaller.InstalarActualizacionAsync(txtRutaVanilla.Text);
                
                txtRutaSeamless.Text = launcherPath;
                GuardarConfiguracion("RutaSeamless", launcherPath);
                
                Log("Creando accesos directos actualizados automáticamente...");
                CrearAccesosMetodo(interactivo: false);

                MessageBox.Show("¡Instalación completada con éxito! Archivos descomprimidos y acceso directo actualizado.", "Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Log($"Error al instalar: {ex.Message}");
                MessageBox.Show($"Fallo conectando a GitHub o extrayendo: {ex.Message}", "Error de Instalación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnActualizador.Enabled = true;
            }
        }

        private void btnLimpiarCache_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRutaVanilla.Text)) return;
            DialogResult res = MessageBox.Show("¿Limpiar archivos temporales de crash (.mdmp)?", "Limpieza", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                SaveFileManager.LimpiarTemporales(txtRutaVanilla.Text);
                Log("Limpieza de crash dumps ejecutada.");
            }
        }

        // --- Editor de Configuración (Tab 2) ---
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabSeamlessConfig)
            {
                CargarConfiguracionIni();
            }
        }

        private void btnRecargarIni_Click(object sender, EventArgs e)
        {
            CargarConfiguracionIni();
            Log("Archivo .ini recargado manualmente.");
        }

        private void btnGuardarIni_Click(object sender, EventArgs e)
        {
            string iniPath = CurrentIniPath;
            if (string.IsNullOrEmpty(iniPath)) return;

            try
            {
                File.WriteAllText(iniPath, txtSeamlessIni.Text);
                MessageBox.Show("Configuración del mod guardada con éxito.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log("ersc_settings.ini modificado y guardado el disco.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Fallo de I/O", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log($"Fallo modificando INI: {ex.Message}");
            }
        }

        private void CargarConfiguracionIni()
        {
            string iniPath = CurrentIniPath;
            if (string.IsNullOrEmpty(iniPath))
            {
                txtSeamlessIni.Text = "Ruta de instalación Vanilla no configurada. No se sabe dónde buscar el mod.";
                return;
            }

            if (File.Exists(iniPath))
            {
                txtSeamlessIni.Text = File.ReadAllText(iniPath);
            }
            else
            {
                txtSeamlessIni.Text = "El archivo ersc_settings.ini no se encontró. Quizá el mod no se ha ejecutado por primera vez o no está instalado en esa ruta.";
            }
        }

        // --- Utilidades ---
        private void ActualizarEstado()
        {
            var estado = SaveFileManager.DetectarEstadoActual(txtRutaPartidas.Text);
            
            switch (estado)
            {
                case EstadoPartida.Vanilla: lblEstado.Text = "Estado actual: Vanilla (.sl2)"; lblEstado.ForeColor = System.Drawing.Color.DodgerBlue; break;
                case EstadoPartida.Seamless: lblEstado.Text = "Estado actual: Seamless (.co2)"; lblEstado.ForeColor = System.Drawing.Color.Goldenrod; break;
                case EstadoPartida.Mixto: lblEstado.Text = "Estado: Mixto (Riesgo pérdida)"; lblEstado.ForeColor = System.Drawing.Color.Crimson; break;
                case EstadoPartida.Desconocido: lblEstado.Text = "Estado actual: Desconocido"; lblEstado.ForeColor = System.Drawing.Color.Gray; break;
            }
        }

        private void GuardarConfiguracion(string key, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.AppSettings.Settings[key] != null) config.AppSettings.Settings[key].Value = value;
                else config.AppSettings.Settings.Add(key, value);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex) { Logger.Write($"Error config {key}: {ex.Message}"); }
        }

        private void Log(string mensaje)
        {
            Logger.Write($"[GUI] {mensaje}");
            if (txtLogs.InvokeRequired) { txtLogs.Invoke(new Action(() => Log(mensaje))); return; }
            txtLogs.AppendText($"[{DateTime.Now:HH:mm:ss}] {mensaje}{Environment.NewLine}");
        }
    }
}
