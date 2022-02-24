
namespace Attune {
    partial class Form {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.startButton = new System.Windows.Forms.Button();
            this.textbox = new System.Windows.Forms.RichTextBox();
            this.clearConsoleButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.startButton.Location = new System.Drawing.Point(451, 12);
            this.startButton.Name = "buttonStart";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.OnStart);
            // 
            // textbox
            // 
            this.textbox.BackColor = System.Drawing.Color.LightGray;
            this.textbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textbox.ForeColor = System.Drawing.SystemColors.MenuBar;
            this.textbox.Location = new System.Drawing.Point(12, 41);
            this.textbox.Name = "textbox";
            this.textbox.Size = new System.Drawing.Size(514, 234);
            this.textbox.TabIndex = 3;
            this.textbox.Text = "";
            // 
            // button1
            // 
            this.clearConsoleButton.Location = new System.Drawing.Point(370, 11);
            this.clearConsoleButton.Name = "clearConsoleButton";
            this.clearConsoleButton.Size = new System.Drawing.Size(75, 23);
            this.clearConsoleButton.TabIndex = 4;
            this.clearConsoleButton.Text = "Clear Console";
            this.clearConsoleButton.UseVisualStyleBackColor = true;
            this.clearConsoleButton.Click += new System.EventHandler(this.OnClear);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 287);
            this.Controls.Add(this.clearConsoleButton);
            this.Controls.Add(this.textbox);
            this.Controls.Add(this.startButton);
            this.Name = "Form";
            this.Text = "Attune";
            this.FormClosing += OnFormClosing;
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.RichTextBox textbox;
        private System.Windows.Forms.Button clearConsoleButton;
    }
}

