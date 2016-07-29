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
    public partial class GenerateCurl : Form {
        public GenerateCurl() {
            InitializeComponent();
            txtGuid.Text = System.Guid.NewGuid().ToString();
        }

        private void button2_Click(object sender, EventArgs e) {
            txtGuid.Text = System.Guid.NewGuid().ToString();
        }

        private void updateCurlOutput(object sender, EventArgs e) {
            richTextBox1.Text = generateCurl();
        }

        private void GenerateCurl_Load(object sender, EventArgs e) {
            this.txtUrl.TextChanged += new System.EventHandler(this.updateCurlOutput);
            this.txtScope.TextChanged += new System.EventHandler(this.updateCurlOutput);
            this.txtGuid.TextChanged += new System.EventHandler(this.updateCurlOutput);
            this.txtRedirect.TextChanged += new System.EventHandler(this.updateCurlOutput);
            this.txtResponse.TextChanged += new System.EventHandler(this.updateCurlOutput);
            this.txtClientID.TextChanged += new System.EventHandler(this.updateCurlOutput);
        }

        string generateCurl() {
            string CurlTemplate = "{0}?scope={1}&state={2}&redirect_uri={3}&response_type={4}&client_id={5}";
            string CurlOutput = string.Format(
                CurlTemplate, txtUrl.Text, txtScope.Text, txtGuid.Text, 
                urlEncode(txtRedirect.Text), 
                txtResponse.Text, txtClientID.Text);
            return System.Web.HttpUtility.HtmlEncode(CurlOutput).Replace("&amp", "").Replace(";", "&") ;
        }

        string urlEncode(string str) {
            string basic = System.Web.HttpUtility.HtmlEncode(str);
            string output = basic;


            output = output.Replace(":", "%3A");
            output = output.Replace("/", "%2F");

            return output;
        }

        private void button1_Click(object sender, EventArgs e) {
            Clipboard.SetText(generateCurl());
        }

        private void button3_Click(object sender, EventArgs e) {
            Clipboard.SetText("curl " + generateCurl());
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e) {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}
