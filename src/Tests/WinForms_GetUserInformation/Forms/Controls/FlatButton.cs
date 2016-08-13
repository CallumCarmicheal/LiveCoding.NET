using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_ImplementingAGUI.Forms.Controls {
    public class FlatButton : Button {
        private static Color _back          = System.Drawing.Color.FromArgb(40, 44, 52); // .FromArgb(100, 100, 100)
        private static Color _border        = System.Drawing.Color.FromArgb(100,100,100);
        private static Color _activeBorder  = System.Drawing.Color.White;
        private static Color _fore          = System.Drawing.Color.White;
        
        private static Padding _padding     = new System.Windows.Forms.Padding(0);

        private bool _active;

        public FlatButton() : base() {
            base.BackColor                  = _back;
            base.ForeColor                  = _fore;
            base.FlatAppearance.BorderColor = _border;
            base.FlatStyle                  = System.Windows.Forms.FlatStyle.Flat;
            base.Padding                    = _padding;
        }

        protected override void OnControlAdded(ControlEventArgs e) {
            base.OnControlAdded(e);
            UseVisualStyleBackColor = false;
        }

        protected override void OnMouseEnter(System.EventArgs e) {
            base.OnMouseEnter(e);
            if (!_active)
                base.FlatAppearance.BorderColor = _activeBorder;
        }

        protected override void OnMouseLeave(System.EventArgs e) {
            base.OnMouseLeave(e);
            if (!_active)
                base.FlatAppearance.BorderColor = _border;
        }

        public void SetStateActive() {
            _active = true;
            base.FlatAppearance.BorderColor = _activeBorder;
        }

        public void SetStateNormal() {
            _active = false;
            base.FlatAppearance.BorderColor = _border;
        }
    }
}
