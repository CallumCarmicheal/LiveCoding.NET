using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveCoding.Net.GUI {
    public partial class MainGUI : Form {
        public MainGUI() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            new StreamsGetUser().Show();
        }
    }
}
