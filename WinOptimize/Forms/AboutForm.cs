using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WinOptimize
{
    public sealed partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            OptionsHelper.ApplyTheme(this);

            pictureBox1.BackColor = OptionsHelper.CurrentOptions.Theme;
        }

        private void About_Load(object sender, EventArgs e)
        {
            t1.Interval = 50;
            t2.Interval = 50;

            t1.Start();
        }

        private void t1_Tick(object sender, EventArgs e)
        {
            string s0 = "";
            string s1 = "W";
            string s2 = "Wi";
            string s3 = "Win";
            string s4 = "WinO";
            string s5 = "WinOp";
            string s6 = "WinOpt";
            string s7 = "WinOpti";
            string s8 = "WinOptim";
            string s9 = "WinOptimi";
            string s10 = "WinOptimiz";
            string s11 = "WinOptimize";

            switch (l1.Text)
            {
                case "":
                    l1.Text = s1;
                    break;
                case "W":
                    l1.Text = s2;
                    break;
                case "Wi":
                    l1.Text = s3;
                    break;
                case "Win":
                    l1.Text = s4;
                    break;
                case "WinO":
                    l1.Text = s5;
                    break;
                case "WinOp":
                    l1.Text = s6;
                    break;
                case "WinOpt":
                    l1.Text = s7;
                    break;
                case "WinOpti":
                    l1.Text = s8;
                    break;
                case "WinOptim":
                    l1.Text = s9;
                    break;
                case "WinOptimi":
                    l1.Text = s10;
                    break;
                case "WinOptimiz":
                    l1.Text = s11;
                    t1.Stop();
                    t2.Start();
                    break;
                case "WinOptimize":
                    l1.Text = s0;
                    break;
            }
        }

        private void t2_Tick(object sender, EventArgs e)
        {
            string s0 = "";
            string s1 = "E";
            string s2 = "Em";
            string s3 = "Emr";
            string s4 = "Emre";
            string s5 = "Emre3";
            string s6 = "Emre37";
            string s7 = "Emre37D";
            string s8 = "Emre37De";
            string s9 = "Emre37Des";
            string s10 = "Emre37Dest";
            string s11 = "Emre37Desta";
            string s12 = "Emre37Destan";
            string s13 = "Emre37Destan © ";
            string s14 = "Emre37Destan © ∞";

            switch (l2.Text)
            {
                case "":
                    l2.Text = s1;
                    break;
                case "E":
                    l2.Text = s2;
                    break;
                case "Em":
                    l2.Text = s3;
                    break;
                case "Emr":
                    l2.Text = s4;
                    break;
                case "Emre":
                    l2.Text = s5;
                    break;
                case "Emre3":
                    l2.Text = s6;
                    break;
                case "Emre37":
                    l2.Text = s7;
                    break;
                case "Emre37D":
                    l2.Text = s8;
                    break;
                case "Emre37De":
                    l2.Text = s9;
                    break;
                case "Emre37Des":
                    l2.Text = s10;
                    break;
                case "Emre37Dest":
                    l2.Text = s11;
                    break;
                case "Emre37Desta":
                    l2.Text = s12;
                    break;
                case "Emre37Destan":
                    l2.Text = s13;
                    break;
                case "Emre37Destan © ":
                    l2.Text = s14;
                    t2.Stop();
                    break;
                case "Emre37Destan © ∞":
                    l2.Text = s0;
                    break;
            }
        }

        private void l2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Emre37destan/WinOptimize");
        }
    }
}
