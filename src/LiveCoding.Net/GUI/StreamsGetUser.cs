using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCoding.Net.API.Wrappers.Livestreams;

namespace LiveCoding.Net.GUI {
    public partial class StreamsGetUser : Form {
        public StreamsGetUser() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            string user = Username.Text;
            ILiveStreamInfo lsi = Program.livecodingAPI.getAPIEngine().getLiveStreamUser(user);

            richTextBox1.AppendText("url: " + lsi.url + "\n");
            richTextBox1.AppendText("user: " + lsi.user + "\n");

            richTextBox1.AppendText("user__slug: " + lsi.user__slug + "\n");
            richTextBox1.AppendText("title: " + lsi.title + "\n");

            richTextBox1.AppendText("description: " + lsi.description + "\n");
            richTextBox1.AppendText("difficulty: " + lsi.difficulty + "\n");

            richTextBox1.AppendText("langauge: " + lsi.langauge + "\n");
            richTextBox1.AppendText("tags: " + lsi.tags + "\n");

            richTextBox1.AppendText("is_live: " + lsi.is_live + "\n");
            richTextBox1.AppendText("viewers_live: " + lsi.viewers_live + "\n");

            richTextBox1.AppendText("viewing_urls: " + lsi.viewing_urls + "\n");

        }
    }
}
