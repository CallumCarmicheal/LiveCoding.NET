using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using m = LiveEdu.API.Wrappers.Models;

namespace WinForms_ImplementingAGUI.Forms.Controls {
    public partial class Livestream : UserControl {

        m.Livestream ls;
        m.User user;
        Action<m.Livestream> cb;

        public Livestream() {
            InitializeComponent();

            var pos = this.PointToScreen(label1.Location);
            pos = pictureBox1.PointToClient(pos);
            label1.Parent = pictureBox1;
            label1.Location = pos;
            label1.BackColor = Color.Transparent;
        }

        public void LoadStream(m.Livestream livestream, Action<m.Livestream> callback) {
            cb = callback;
            ls = livestream;
            user = ls.GetStreamer();

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, pictureBox2.Width, pictureBox2.Height);
            pictureBox2.Region = new Region(path);


            pictureBox1.ImageLocation = ls.ThumbnailURL;
            pictureBox2.ImageLocation = user.Avatar;

            pictureBox1.Refresh();
            pictureBox2.Refresh();

            label1.Text = ls.Title;
        }
    }
}
