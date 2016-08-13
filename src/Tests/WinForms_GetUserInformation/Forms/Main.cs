using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WinForms_ImplementingAGUI.Forms {
    public partial class Main : Form {
        public Main() {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e) {
            // Exit the application
            Application.Exit();
        }

        private void Main_Load(object sender, EventArgs e) {
            this.MouseDown += evtMouseDown;
            pictureBox2.MouseDown += evtMouseDown;
        }

        #region Form Code
        
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
        #endregion

        private void flatButton1_Click(object sender, EventArgs e) {
            this.Hide();
            new frmUserViewer().Show();
        }

        private void flatButton4_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
        }

        private void flatButton5_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void flatButton8_Click(object sender, EventArgs e) {
            this.Hide();
            new frmLivestreamsOnAIR().Show();
        }
    }
}
