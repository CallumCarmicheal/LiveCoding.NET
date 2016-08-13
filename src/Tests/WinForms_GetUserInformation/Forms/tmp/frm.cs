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
    public partial class frm : Form {
        public frmUserViewer() {
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
            pictureBox1.MouseDown += evtMouseDown;
            pictureBox2.MouseDown += evtMouseDown;
        }

        #region Form Code

        private const int cGrip = 16;      // Grip size
        private const int cCaption = 32;   // Caption bar height;

        protected override void OnPaint(PaintEventArgs e) {
            Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
            rc = new Rectangle(0, 0, this.ClientSize.Width, cCaption);
            e.Graphics.FillRectangle(new SolidBrush(BackColor), rc);
        }

        protected override void WndProc(ref Message m) {
            if (m.Msg == 0x84) {  // Trap WM_NCHITTEST
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption) {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip) {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }
            }
            base.WndProc(ref m);
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
        #endregion

        private void flatButton1_Click(object sender, EventArgs e) {
            string username = textBox1.Text;
            var user = Program.apiEngine.getCurrentUser();

            if (user != null && user.Valid) {
                pictureBox1.ImageLocation = user.Avatar;
                label1.Text = user.Username;

                Type t = user.GetType();
                PropertyInfo[] pi = t.GetProperties();
                
                foreach (var p in pi) {
                    var txt = p.Name.PadRight(30);
                    object value = p.GetValue(user, null);

                    if (value == null) {
                        txt += "NULL";
                    } else if (value.GetType() == typeof(int)) {
                        txt += (int)p.GetValue(user);
                    } else if (value.GetType() == typeof(string)) {
                        txt += (string)p.GetValue(user);
                    } else {
                        txt += value.GetType().Name;
                    }

                    richTextBox1.AppendText(txt + Environment.NewLine);
                }
            } else {
                MessageBox.Show("User does not exist or there was as ERROR");
            }
        }

        private void flatButton2_Click(object sender, EventArgs e) {
            Program.mFrm.Show();
            this.Close();
        }
    }
}
