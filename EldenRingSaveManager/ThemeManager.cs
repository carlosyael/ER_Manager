using System.Drawing;
using System.Windows.Forms;

namespace EldenRingSaveManager
{
    public static class ThemeManager
    {
        // --- Dark Theme Palette (Elden Ring inspired) ---
        private static readonly Color DarkBackground    = Color.FromArgb(30, 30, 42);      // #1E1E2A
        private static readonly Color DarkSurface       = Color.FromArgb(40, 40, 56);      // #282838
        private static readonly Color DarkControl       = Color.FromArgb(50, 50, 68);      // #323244
        private static readonly Color DarkBorder        = Color.FromArgb(70, 70, 90);      // #46465A
        private static readonly Color DarkText          = Color.FromArgb(224, 224, 230);    // #E0E0E6
        private static readonly Color DarkTextDim       = Color.FromArgb(160, 160, 175);    // #A0A0AF
        private static readonly Color DarkAccent        = Color.FromArgb(200, 168, 80);     // #C8A850 (gold)
        private static readonly Color DarkButtonFace    = Color.FromArgb(55, 55, 75);       // #37374B
        private static readonly Color DarkButtonHover   = Color.FromArgb(65, 65, 88);       // #414158
        private static readonly Color DarkGridAltRow    = Color.FromArgb(36, 36, 50);       // #242432
        private static readonly Color DarkGridHeader    = Color.FromArgb(45, 45, 62);       // #2D2D3E

        // --- Light Theme Palette ---
        private static readonly Color LightBackground   = SystemColors.Control;
        private static readonly Color LightSurface      = SystemColors.Window;
        private static readonly Color LightText         = SystemColors.ControlText;
        private static readonly Color LightButtonFace   = SystemColors.ButtonFace;

        public static bool IsDarkMode { get; private set; } = false;

        public static void Initialize()
        {
            string saved = ConfigHelper.GetSetting("Theme");
            IsDarkMode = saved == "Dark";
        }

        public static void SetTheme(string theme)
        {
            IsDarkMode = theme == "Dark";
            ConfigHelper.SaveSetting("Theme", theme);
        }

        public static void ApplyTheme(Form form)
        {
            if (IsDarkMode)
                ApplyDark(form);
            else
                ApplyLight(form);
        }

        // ============================
        //  DARK THEME
        // ============================
        private static void ApplyDark(Control parent)
        {
            parent.BackColor = DarkBackground;
            parent.ForeColor = DarkText;

            foreach (Control ctrl in parent.Controls)
            {
                ApplyDarkToControl(ctrl);
            }
        }

        private static void ApplyDarkToControl(Control ctrl)
        {
            switch (ctrl)
            {
                case TabControl tab:
                    tab.BackColor = DarkBackground;
                    tab.ForeColor = DarkText;
                    foreach (TabPage page in tab.TabPages)
                    {
                        page.BackColor = DarkBackground;
                        page.ForeColor = DarkText;
                        page.UseVisualStyleBackColor = false;
                        foreach (Control child in page.Controls)
                            ApplyDarkToControl(child);
                    }
                    break;

                case DataGridView dgv:
                    dgv.BackgroundColor = DarkSurface;
                    dgv.ForeColor = DarkText;
                    dgv.GridColor = DarkBorder;
                    dgv.DefaultCellStyle.BackColor = DarkSurface;
                    dgv.DefaultCellStyle.ForeColor = DarkText;
                    dgv.DefaultCellStyle.SelectionBackColor = DarkAccent;
                    dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = DarkGridAltRow;
                    dgv.AlternatingRowsDefaultCellStyle.ForeColor = DarkText;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = DarkGridHeader;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = DarkAccent;
                    dgv.ColumnHeadersDefaultCellStyle.Font = new Font(dgv.Font, FontStyle.Bold);
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.BorderStyle = BorderStyle.FixedSingle;
                    break;

                case GroupBox grp:
                    grp.BackColor = DarkBackground;
                    grp.ForeColor = DarkAccent;
                    foreach (Control child in grp.Controls)
                        ApplyDarkToControl(child);
                    break;

                case TextBox txt:
                    txt.BackColor = DarkControl;
                    txt.ForeColor = DarkText;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    break;

                case ComboBox cmb:
                    cmb.BackColor = DarkControl;
                    cmb.ForeColor = DarkText;
                    cmb.FlatStyle = FlatStyle.Flat;
                    break;

                case Button btn:
                    btn.BackColor = DarkButtonFace;
                    btn.ForeColor = DarkText;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = DarkBorder;
                    btn.FlatAppearance.BorderSize = 1;
                    btn.FlatAppearance.MouseOverBackColor = DarkButtonHover;
                    btn.FlatAppearance.MouseDownBackColor = DarkAccent;
                    break;

                case Label lbl:
                    lbl.BackColor = Color.Transparent;
                    // Don't override ForeColor for status labels that use custom colors
                    if (lbl.Name != "lblEstado" && lbl.Name != "lblUpdateStatus")
                        lbl.ForeColor = DarkText;
                    break;

                default:
                    ctrl.BackColor = DarkBackground;
                    ctrl.ForeColor = DarkText;
                    foreach (Control child in ctrl.Controls)
                        ApplyDarkToControl(child);
                    break;
            }
        }

        // ============================
        //  LIGHT THEME (reset to defaults)
        // ============================
        private static void ApplyLight(Control parent)
        {
            parent.BackColor = LightBackground;
            parent.ForeColor = LightText;

            foreach (Control ctrl in parent.Controls)
            {
                ApplyLightToControl(ctrl);
            }
        }

        private static void ApplyLightToControl(Control ctrl)
        {
            switch (ctrl)
            {
                case TabControl tab:
                    tab.BackColor = LightBackground;
                    tab.ForeColor = LightText;
                    foreach (TabPage page in tab.TabPages)
                    {
                        page.BackColor = LightBackground;
                        page.ForeColor = LightText;
                        page.UseVisualStyleBackColor = true;
                        foreach (Control child in page.Controls)
                            ApplyLightToControl(child);
                    }
                    break;

                case DataGridView dgv:
                    dgv.BackgroundColor = LightSurface;
                    dgv.ForeColor = LightText;
                    dgv.GridColor = SystemColors.ControlDark;
                    dgv.DefaultCellStyle.BackColor = LightSurface;
                    dgv.DefaultCellStyle.ForeColor = LightText;
                    dgv.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                    dgv.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
                    dgv.AlternatingRowsDefaultCellStyle.BackColor = LightSurface;
                    dgv.AlternatingRowsDefaultCellStyle.ForeColor = LightText;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = LightText;
                    dgv.ColumnHeadersDefaultCellStyle.Font = new Font(dgv.Font, FontStyle.Regular);
                    dgv.EnableHeadersVisualStyles = true;
                    dgv.BorderStyle = BorderStyle.FixedSingle;
                    break;

                case GroupBox grp:
                    grp.BackColor = LightBackground;
                    grp.ForeColor = LightText;
                    foreach (Control child in grp.Controls)
                        ApplyLightToControl(child);
                    break;

                case TextBox txt:
                    txt.BackColor = LightSurface;
                    txt.ForeColor = LightText;
                    txt.BorderStyle = BorderStyle.Fixed3D;
                    break;

                case ComboBox cmb:
                    cmb.BackColor = LightSurface;
                    cmb.ForeColor = LightText;
                    cmb.FlatStyle = FlatStyle.Standard;
                    break;

                case Button btn:
                    btn.BackColor = LightButtonFace;
                    btn.ForeColor = LightText;
                    btn.FlatStyle = FlatStyle.Standard;
                    btn.UseVisualStyleBackColor = true;
                    break;

                case Label lbl:
                    lbl.BackColor = Color.Transparent;
                    if (lbl.Name != "lblEstado" && lbl.Name != "lblUpdateStatus")
                        lbl.ForeColor = LightText;
                    break;

                default:
                    ctrl.BackColor = LightBackground;
                    ctrl.ForeColor = LightText;
                    foreach (Control child in ctrl.Controls)
                        ApplyLightToControl(child);
                    break;
            }
        }
    }
}
