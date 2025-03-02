﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinOptimize
{
    public sealed partial class UpdateForm : Form
    {
        public UpdateForm(string message, bool newUpdate, string changelog, string latestVersion)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            OptionsHelper.ApplyTheme(this);

            txtMessage.Text = message;

            if (newUpdate)
            {
                this.Size = new Size(600, 545);
                btnOK.Text = OptionsHelper.TranslationList["btnYes"].ToString();
                btnNo.Text = OptionsHelper.TranslationList["btnNo"].ToString();
                btnNo.Visible = true;
                txtChanges.Text = OptionsHelper.TranslationList["btnChangelog"].ToString();
                txtVersions.Text = $"{Program.GetCurrentVersionTostring()} → {latestVersion}";
                txtVersions.Visible = true;

                btnOK.DialogResult = DialogResult.Yes;
                btnNo.DialogResult = DialogResult.No;

                txtInfo.Text = changelog;
                txtInfo.Visible = true;
                txtChanges.Visible = true;
            }
            else
            {
                this.Size = new Size(600, 188);
                btnOK.Text = OptionsHelper.TranslationList["btnAbout"].ToString();
                btnNo.Visible = false;
                txtVersions.Visible = false;

                btnOK.DialogResult = DialogResult.OK;

                txtInfo.Visible = false;
                txtChanges.Visible = false;
            }
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {

        }
    }
}
