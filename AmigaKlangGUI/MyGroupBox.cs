using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Diagnostics;
using System.Drawing.Text;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace AmigaKlangGUI
{


    public class MyGroupBox : GroupBox
    {
        private Color _bordercolor = Color.White;
        public Color BorderColor
        {
            get { return this._bordercolor; }
            set { this._bordercolor = value; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //get the textsize in GroupBox
            Size tSize = TextRenderer.MeasureText(this.Text, this.Font);

            Rectangle borderRect = e.ClipRectangle;
            borderRect.Y = (borderRect.Y + (tSize.Height / 2));
            borderRect.Height = (borderRect.Height - (tSize.Height / 2));
            ControlPaint.DrawBorder(e.Graphics, borderRect, this._bordercolor, ButtonBorderStyle.Solid);

            Rectangle textRect = e.ClipRectangle;
            textRect.X = (textRect.X + 6);
            textRect.Width = tSize.Width + 6;
            textRect.Height = tSize.Height;
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), textRect);
            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), textRect);
        }



    }



}


