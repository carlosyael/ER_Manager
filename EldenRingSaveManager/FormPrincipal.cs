using System;
using System.Configuration;
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
        private string CurrentIniPath => !string.IsNullOrEmpty(txtRutaVanilla.Text) 
            ? Path.Combine(Path.GetDirectoryName(txtRutaVanilla.Text), "SeamlessCoop", "ersc_settings.ini")
            : string.Empty;

        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            txtRutaPartidas.Text = ConfigHelper.GetSetting("RutaPartidas") ?? string.Empty;
            txtRutaVanilla.Text = ConfigHelper.GetSetting("RutaVanilla") ?? string.Empty;
            txtRutaSeamless.Text = ConfigHelper.GetSetting("RutaSeamless") ?? string.Empty;

            CargarPerfiles();
            ActualizarEstado();
            
            Logger.Write("[GUI] UI Iniciada correctamente. v4.0");
            Log("Aplicación iniciada. Bienvenido a Elden Ring Save Manager v4.0.");

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
                    CargarConfiguracionIni();
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

        // --- Accesos Directos ---
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
                            if (ofd.ShowDialog() == DialogResult.OK) iconCoop = ofd.FileName;
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
            string perfilesStr = ConfigHelper.GetSetting("Perfiles") ?? "Predeterminado";
            string[] perfiles = perfilesStr.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var p in perfiles) cmbPerfiles.Items.Add(p);
            
            string actual = ConfigHelper.GetSetting("PerfilActual") ?? "Predeterminado";
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
                    Log($"Perfil cambiado de {perfilAnterior} a {perfilNuevo}. Archivos rotados.");
                }
                catch (Exception ex) { MessageBox.Show($"Error al rotar perfiles: {ex.Message}"); }
            }
        }

        private void btnNuevoPerfil_Click(object sender, EventArgs e)
        {
            string input = MostrarInputBox("Nombre del nuevo perfil (ej: Coop Con Maria):", "Nuevo Perfil");
            if (!string.IsNullOrWhiteSpace(input) && !cmbPerfiles.Items.Contains(input))
            {
                string perfilesStr = ConfigHelper.GetSetting("Perfiles");
                string perfiles = string.IsNullOrEmpty(perfilesStr) ? input : perfilesStr + "|" + input;
                GuardarConfiguracion("Perfiles", perfiles);
                CargarPerfiles();
                cmbPerfiles.SelectedItem = input;
                Log($"Nuevo perfil '{input}' creado y seleccionado.");
            }
        }

        private string MostrarInputBox(string texto, string caption)
        {
            Form prompt = new Form() { Width = 400, Height = 150, FormBorderStyle = FormBorderStyle.FixedDialog, Text = caption, StartPosition = FormStartPosition.CenterScreen };
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
            finally { btnActualizador.Enabled = true; }
        }

        private void btnLimpiarCache_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRutaVanilla.Text)) return;
            if (MessageBox.Show("¿Limpiar archivos temporales de crash (.mdmp)?", "Limpieza", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SaveFileManager.LimpiarTemporales(txtRutaVanilla.Text);
                Log("Limpieza de crash dumps ejecutada.");
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
            Log("Mapeo de INI recargado.");
        }

        private void btnModoExperto_Click(object sender, EventArgs e)
        {
            txtSeamlessIni.Visible = !txtSeamlessIni.Visible;
            dgvConfig.Visible = !txtSeamlessIni.Visible;
            btnModoExperto.Text = txtSeamlessIni.Visible ? "Toggle Formulario (Visual)" : "Toggle Modo Experto (Texto)";
        }

        private void CargarConfiguracionIni()
        {
            string iniPath = CurrentIniPath;
            dgvConfig.Rows.Clear();
            txtSeamlessIni.Clear();

            if (string.IsNullOrEmpty(iniPath))
            {
                txtSeamlessIni.Text = "Ruta Base Vanilla no fijada.";
                return;
            }

            if (!File.Exists(iniPath))
            {
                txtSeamlessIni.Text = "No se encontró el archivo ersc_settings.ini.";
                return;
            }

            // Mapeo Inteligente
            string[] lineas = File.ReadAllLines(iniPath);
            txtSeamlessIni.Text = string.Join(Environment.NewLine, lineas); // Respaldo para la caja de texto avanzado

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
                    // Estamos en modo experto, guardar todo crudo.
                    File.WriteAllText(iniPath, txtSeamlessIni.Text);
                }
                else
                {
                    // Guardado inteligente: Extraer el datagridview y reemplazar solo los valores que coincidan sin romper lineas.
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
                                // Respetamos los espacios usando la llave original pero incrustando el nuevo valor del DataGridView
                                // Intentaremos reconstruir de manera simple
                                lineasOriginales[i] = $"{keyOriginal} = {gridValores[keyOriginal]}";
                            }
                        }
                    }
                    File.WriteAllLines(iniPath, lineasOriginales);
                }
                
                MessageBox.Show("Variables de Seamless modificadas con éxito.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log("Archivo INI estructurado fue guardado cuidando sus comentarios originales.");
                
                // Recargar para sincronizar vista experta y tabla
                CargarConfiguracionIni();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el INI: {ex.Message}", "Fallo de I/O", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log($"Fallo modificando INI: {ex.Message}");
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
