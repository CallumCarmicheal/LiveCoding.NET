using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_ImplementingAGUI.Forms.SplashScreens {
    public partial class Splashscreen : Form {
        public Splashscreen() {
            InitializeComponent();

            this.CenterToScreen();

            this.MouseDown += evtMouseDown;
            pictureBox1.MouseDown += evtMouseDown;
            flatProgressbar1.MouseDown += evtMouseDown;
            label2.MouseDown += evtMouseDown;

            flatProgressbar1.Style = ProgressBarStyle.Marquee;
            flatProgressbar1.MarqueeAnimationSpeed = 100;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void evtMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public void SetText(string txt) {
            if (label2.InvokeRequired) {
                label2.Invoke(new MethodInvoker(() => {
                    SetText(txt);
                })); return;
            }

            label2.Text = txt;
            Application.DoEvents();

            System.Diagnostics.Debugger.Log(0, "SplashScreen - Text", txt);
        }

        private void Splashscreen_Shown(object sender, EventArgs e) {
            this.CenterToScreen();
        }

        private void flatProgressbar1_Click(object sender, EventArgs e) {

        }

        private void label2_Click(object sender, EventArgs e) {

        }
    }
}
