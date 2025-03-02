﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WinOptimize
{
    public sealed partial class StartupPreviewForm : Form
    {
        string _token = string.Empty;

        public StartupPreviewForm(List<BackupStartupItem> items)
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;
            OptionsHelper.ApplyTheme(this);

            // translate UI elements
            if (OptionsHelper.CurrentOptions.LanguageCode != LanguageCode.EN) Translate();

            foreach (BackupStartupItem x in items)
            {
                if (File.Exists(SanitizePath(x.FileLocation)))
                {
                    _token = "[✓] ";
                }
                else
                {
                    _token = "[⚠] ";
                }

                listPreview.Items.Add(_token + x.Name + " - " + x.FileLocation);
            }
        }

        private void Translate()
        {
            this.Text = OptionsHelper.TranslationList["StartupPreviewForm"];

            Dictionary<string, string> translationList = OptionsHelper.TranslationList.ToObject<Dictionary<string, string>>();

            Control element;

            foreach (var x in translationList)
            {
                if (x.Key == null || x.Key == string.Empty) continue;
                element = this.Controls.Find(x.Key, true).FirstOrDefault();

                if (element == null) continue;

                element.Text = x.Value;
            }
        }

        private void StartupPreviewForm_Load(object sender, EventArgs e)
        {
            this.Focus();
        }

        private string SanitizePath(string s)
        {
            s = s.Replace("\"", string.Empty);
            int i;

            while (s.Contains("/"))
            {
                i = s.LastIndexOf("/");
                s = s.Substring(0, i);
            }

            i = s.IndexOf(".exe");
            s = s.Substring(0, i + 4);

            return s.Trim();
        }
    }
}
