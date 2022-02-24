using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attune {
    public partial class Form : System.Windows.Forms.Form {
        private readonly Attune attuneObj;

        public Form(Attune attune) {
            InitializeComponent();
            this.attuneObj = attune;
        }

        private void OnStart(object sender, EventArgs e) {
            attuneObj.Start();
        }

        public void OnFormClosing(object? sender, EventArgs e) {
            attuneObj.Close();
        }

        private void OnClear(object sender, EventArgs e) {
            textbox.Text = "";
        }

        // OUT OF THREAD
        public void AddText(string text) {
            textbox.Invoke((MethodInvoker)(
                () => {
                    textbox.Text += text;
                    textbox.SelectionStart = textbox.Text.Length;
                    textbox.ScrollToCaret();
                }
            ));
        }
    }
}
