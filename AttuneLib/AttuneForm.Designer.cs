
namespace AttuneLib
{
    partial class AttuneForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttuneForm));
            this.startButton = new System.Windows.Forms.Button();
            this.textbox = new System.Windows.Forms.RichTextBox();
            this.clearConsoleButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CurrentModeDropbox = new System.Windows.Forms.ComboBox();
            this.attuneBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.attuneBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.modeControlsWrapper = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.attuneBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attuneBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(3, 3);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Connect";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.OnStart);
            // 
            // textbox
            // 
            this.textbox.BackColor = System.Drawing.Color.DarkSlateGray;
            this.textbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textbox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textbox.ForeColor = System.Drawing.SystemColors.MenuBar;
            this.textbox.Location = new System.Drawing.Point(0, 432);
            this.textbox.Margin = new System.Windows.Forms.Padding(10);
            this.textbox.Name = "textbox";
            this.textbox.ReadOnly = true;
            this.textbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.textbox.Size = new System.Drawing.Size(963, 131);
            this.textbox.TabIndex = 3;
            this.textbox.Text = "";
            // 
            // clearConsoleButton
            // 
            this.clearConsoleButton.Location = new System.Drawing.Point(885, 4);
            this.clearConsoleButton.Name = "clearConsoleButton";
            this.clearConsoleButton.Size = new System.Drawing.Size(75, 23);
            this.clearConsoleButton.TabIndex = 4;
            this.clearConsoleButton.Text = "Clear Console";
            this.clearConsoleButton.UseVisualStyleBackColor = true;
            this.clearConsoleButton.Click += new System.EventHandler(this.OnClear);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CurrentModeDropbox);
            this.panel1.Controls.Add(this.startButton);
            this.panel1.Controls.Add(this.clearConsoleButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 563);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(963, 29);
            this.panel1.TabIndex = 6;
            // 
            // CurrentModeDropbox
            // 
            this.CurrentModeDropbox.FormattingEnabled = true;
            this.CurrentModeDropbox.Location = new System.Drawing.Point(84, 3);
            this.CurrentModeDropbox.Name = "CurrentModeDropbox";
            this.CurrentModeDropbox.Size = new System.Drawing.Size(108, 23);
            this.CurrentModeDropbox.TabIndex = 5;
            this.CurrentModeDropbox.SelectedIndexChanged += new System.EventHandler(this.OnFormModeChanged);
            // 
            // attuneBindingSource1
            // 
            this.attuneBindingSource1.DataSource = typeof(AttuneLib.Attune);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Attune is now minimized to tray. Double click to open it again.";
            this.notifyIcon.BalloonTipTitle = "Attune";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Attune";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // attuneBindingSource
            // 
            this.attuneBindingSource.DataSource = typeof(AttuneLib.Attune);
            // 
            // modeControlsWrapper
            // 
            this.modeControlsWrapper.BackColor = System.Drawing.Color.Black;
            this.modeControlsWrapper.Location = new System.Drawing.Point(3, 2);
            this.modeControlsWrapper.Name = "modeControlsWrapper";
            this.modeControlsWrapper.Size = new System.Drawing.Size(957, 428);
            this.modeControlsWrapper.TabIndex = 7;
            // 
            // AttuneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 592);
            this.Controls.Add(this.modeControlsWrapper);
            this.Controls.Add(this.textbox);
            this.Controls.Add(this.panel1);
            this.Name = "AttuneForm";
            this.Text = "Attune";
            this.Resize += new System.EventHandler(this.AttuneForm_Resize);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.attuneBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attuneBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.RichTextBox textbox;
        private System.Windows.Forms.Button clearConsoleButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ComboBox CurrentModeDropbox;
        private System.Windows.Forms.BindingSource attuneBindingSource1;
        private System.Windows.Forms.BindingSource attuneBindingSource;
        private System.Windows.Forms.Panel modeControlsWrapper;
    }
}

