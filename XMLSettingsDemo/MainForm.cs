using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace XMLSettingsDemo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            Dictionary<string, string> settings = new Dictionary<string, string>();

            settings.Add("BorderNone","True");
            settings.Add("BorderSingle","False");
            settings.Add("Border3D","False");
            settings.Add("PictureBoxBackColor","#F0F0F0");

            XMLSettings.AppSettingsFile = "Settings.xml";
            XMLSettings.InitializeSettings(settings);

            rbNone.Checked = bool.Parse(XMLSettings.GetSettingsValue("BorderNone"));
            rbBorderSingle.Checked = bool.Parse(XMLSettings.GetSettingsValue("BorderSingle"));
            rbBorder3D.Checked = bool.Parse(XMLSettings.GetSettingsValue("Border3D"));
            txtColor.Text = XMLSettings.GetSettingsValue("PictureBoxBackColor");
            pictureBox1.BackColor = ColorTranslator.FromHtml(txtColor.Text);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                XMLSettings.SetSettingsValue("BorderSingle", rbNone.Checked.ToString());
                XMLSettings.SetSettingsValue("BorderSingle", rbBorderSingle.Checked.ToString());
                XMLSettings.SetSettingsValue("Border3D", rbBorder3D.Checked.ToString());
                XMLSettings.SetSettingsValue("PictureBoxBackColor", txtColor.Text);

                MessageBox.Show("Settings have been saved", "XML Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "XML Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbNone_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.BorderStyle = BorderStyle.None;
        }

        private void rbFixedSingle_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
        }

        private void rbFixed3D_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.FullOpen = true;

            if (cd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = cd.Color;
                txtColor.Text = ColorTranslator.ToHtml(cd.Color);
            }
        }
    }
}
