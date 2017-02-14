namespace TestDesktopApplication.Forms {
    partial class XtraForm1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // styleController1
            // 
            this.styleController1.Appearance.BackColor = System.Drawing.Color.Olive;
            this.styleController1.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.styleController1.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.styleController1.Appearance.Font = new System.Drawing.Font("Sitka Heading", 8.25F);
            this.styleController1.Appearance.Options.UseBackColor = true;
            this.styleController1.Appearance.Options.UseBorderColor = true;
            this.styleController1.Appearance.Options.UseFont = true;
            this.styleController1.LookAndFeel.SkinName = "VS2010";
            this.styleController1.LookAndFeel.UseDefaultLookAndFeel = false;
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(51, 65);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(199, 20);
            this.textEdit1.TabIndex = 0;
            // 
            // XtraForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 311);
            this.Controls.Add(this.textEdit1);
            this.Name = "XtraForm1";
            this.Text = "XtraForm1";
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.StyleController styleController1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
    }
}