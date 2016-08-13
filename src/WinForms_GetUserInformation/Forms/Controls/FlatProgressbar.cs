using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_ImplementingAGUI.Forms.SplashScreens {
    class FlatProgressbar : System.Windows.Forms.ProgressBar {
        Timer timer = new Timer();
        public FlatProgressbar() {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            timer.Interval = 1;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e) {
            if (this.Style == ProgressBarStyle.Marquee) {
                mqInt += MarqueeAnimationSpeed/100 + 0.11f;

                this.Refresh();
            }
        }

        private float mqInt = -5f;
        protected override void OnPaint(PaintEventArgs e) {
            Rectangle rect = this.ClientRectangle;
            Graphics g = e.Graphics;
            
            rect.Inflate(-3, -3);

            if (this.Style == ProgressBarStyle.Blocks) {
                if (this.Value > 0) {
                    Rectangle clip = new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)this.Value / this.Maximum) * rect.Width), rect.Height);

                    var brush = new SolidBrush(Color.FromArgb(255, 48, 53, 64));
                    g.FillRectangle(brush, clip);
                }
            } else {
                int width = (int)Math.Round(this.Width / 2.1f, 0);

                Rectangle clip = new Rectangle((int)Math.Round(mqInt, 0), rect.Y, width, rect.Height);

                if (mqInt >= this.Width + 5f)
                    mqInt  = 0 - width - 5f;

                var brush = new SolidBrush(Color.FromArgb(255, 48, 53, 64));
                g.FillRectangle(brush, clip);
            }
        }
    }
}
