namespace Attune.Programs.Keyboard;

partial class KeyboardControls
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.buttonSave = new System.Windows.Forms.Button();
            this.inputColor = new System.Windows.Forms.TextBox();
            this.inputMacro = new System.Windows.Forms.TextBox();
            this.panelPads = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelPadNumber = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(927, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(25, 23);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "🖫";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.saveClicked);
            // 
            // inputColor
            // 
            this.inputColor.Location = new System.Drawing.Point(820, 3);
            this.inputColor.Name = "inputColor";
            this.inputColor.Size = new System.Drawing.Size(101, 23);
            this.inputColor.TabIndex = 2;
            // 
            // inputMacro
            // 
            this.inputMacro.Location = new System.Drawing.Point(78, 3);
            this.inputMacro.Name = "inputMacro";
            this.inputMacro.Size = new System.Drawing.Size(736, 23);
            this.inputMacro.TabIndex = 3;
            // 
            // panelPads
            // 
            this.panelPads.ColumnCount = 16;
            this.panelPads.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelPads.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelPads.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelPads.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelPads.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelPads.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelPads.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelPads.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelPads.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelPads.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelPads.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelPads.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelPads.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelPads.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelPads.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelPads.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelPads.Location = new System.Drawing.Point(3, 3);
            this.panelPads.Name = "panelPads";
            this.panelPads.RowCount = 4;
            this.panelPads.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelPads.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelPads.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelPads.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.panelPads.Size = new System.Drawing.Size(949, 390);
            this.panelPads.TabIndex = 4;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.labelPadNumber);
            this.flowLayoutPanel1.Controls.Add(this.inputMacro);
            this.flowLayoutPanel1.Controls.Add(this.inputColor);
            this.flowLayoutPanel1.Controls.Add(this.buttonSave);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 399);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(957, 29);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // labelPadNumber
            // 
            this.labelPadNumber.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelPadNumber.ForeColor = System.Drawing.Color.LightGray;
            this.labelPadNumber.Location = new System.Drawing.Point(0, 0);
            this.labelPadNumber.Margin = new System.Windows.Forms.Padding(0);
            this.labelPadNumber.Name = "labelPadNumber";
            this.labelPadNumber.Size = new System.Drawing.Size(75, 29);
            this.labelPadNumber.TabIndex = 4;
            // 
            // KeyboardControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panelPads);
            this.Name = "KeyboardControls";
            this.Size = new System.Drawing.Size(957, 428);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button buttonSave;
    private System.Windows.Forms.TextBox inputColor;
    private System.Windows.Forms.TextBox inputMacro;
    private System.Windows.Forms.TableLayoutPanel panelPads;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.Label labelPadNumber;
}

