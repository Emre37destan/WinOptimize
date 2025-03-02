﻿using System.Drawing;
using System.Windows.Forms;

namespace WinOptimize
{
    internal sealed class MoonMenuRenderer : ToolStripProfessionalRenderer
    {
        internal MoonMenuRenderer() : base(new MoonColors())
        {

        }

        //protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        //{
        //    var tsMenuItem = e.Item as ToolStripMenuItem;
        //    if (tsMenuItem != null)
        //        e.TextColor = Color.GhostWhite;
        //    base.OnRenderItemText(e);
        //}

        //protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        //{
        //    var tsMenuItem = e.Item as ToolStripMenuItem;
        //    if (tsMenuItem != null)
        //        e.Graphics.bru
        //    base.OnRenderSeparator(e);
        //}

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            var tsMenuItem = e.Item as ToolStripMenuItem;
            if (tsMenuItem != null)
                e.ArrowColor = Color.DimGray;
            base.OnRenderArrow(e);
        }
    }

    internal class MoonColors : ProfessionalColorTable
    {
        public override Color SeparatorLight
        {
            get { return Color.DimGray; }
        }

        public override Color SeparatorDark
        {
            get { return Color.DimGray; }
        }

        public override Color ToolStripDropDownBackground
        {
            get
            {
                return OptionsHelper.BackgroundColor;
            }
        }

        public override Color ImageMarginGradientBegin
        {
            get
            {
                return OptionsHelper.BackgroundColor;
            }
        }
        public override Color ImageMarginGradientMiddle
        {
            get
            {
                return OptionsHelper.BackgroundColor;
            }
        }
        public override Color ImageMarginGradientEnd
        {
            get
            {
                return OptionsHelper.BackgroundColor;
            }
        }

        public override Color ToolStripBorder
        {
            get
            {
                return OptionsHelper.BackgroundColor;
            }
        }

        public override Color MenuBorder
        {
            get
            {
                return OptionsHelper.BackAccentColor;
            }
        }

        public override Color MenuItemSelected
        {
            get
            {
                return OptionsHelper.BackAccentColor;
            }
        }

        public override Color MenuItemSelectedGradientBegin
        {
            get
            {
                return OptionsHelper.BackAccentColor;
            }
        }

        public override Color MenuItemSelectedGradientEnd
        {
            get
            {
                return OptionsHelper.BackAccentColor;
            }
        }

        public override Color MenuItemBorder
        {
            get
            {
                return OptionsHelper.BackAccentColor;
            }
        }
    }
}
