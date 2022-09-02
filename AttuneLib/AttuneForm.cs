using SkiaSharp.Views.Desktop;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace AttuneLib;

public partial class AttuneForm : System.Windows.Forms.Form
{

    private readonly Attune attuneObj;

    public AttuneForm(Attune attune)
    {
        InitializeComponent();
        this.attuneObj = attune;

        CurrentModeDropbox.DropDownStyle = ComboBoxStyle.DropDownList;
        CurrentModeDropbox.DataSource = Programs.Values;
        CurrentModeDropbox.SelectedIndex = 0;
        // because the modes list is a list of tuples, Item1 and Item2 are the tuple parts (name, create function)
        CurrentModeDropbox.DisplayMember = "Item1";
        CurrentModeDropbox.ValueMember = "Item2";
    }

    private void OnStart(object sender, EventArgs e)
    {
        attuneObj.onFormConnect(CurrentModeDropbox.SelectedValue);
    }

    public void OnFormClosing(object? sender, EventArgs e)
    {
        attuneObj.onFormClosed();
    }

    private void OnFormModeChanged(object sender, EventArgs e)
    {
        if (attuneObj != null)
            attuneObj.OnFormModeChanged(CurrentModeDropbox.SelectedValue);
    }

    private void OnClear(object sender, EventArgs e)
    {
        textbox.Text = "";
    }

    // OUT OF THREAD
    public void AddText(string text)
    {
        textbox.Invoke((MethodInvoker)(
            () =>
            {
                textbox.Text += text;
                textbox.SelectionStart = textbox.Text.Length;
                textbox.ScrollToCaret();
            }
        ));
    }

    internal void setProgramControls(UserControl userControl)
    {
        modeControlsWrapper.Controls.Clear();
        if (userControl != null)
            modeControlsWrapper.Controls.Add(userControl);
    }

    private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        Show();
        this.WindowState = FormWindowState.Normal;
        notifyIcon.Visible = false;
    }

    private void AttuneForm_Resize(object sender, EventArgs e)
    {
        if (this.WindowState == FormWindowState.Minimized)
        {
            Hide();
            notifyIcon.Visible = true;
        }
    }

}

