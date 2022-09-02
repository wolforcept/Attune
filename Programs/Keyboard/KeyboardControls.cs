using AttuneLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput.Native;

namespace Attune.Programs.Keyboard
{
    class ButtonWrapper : Button
    {
        public int padNr;
        public string macroText;
        public string colorString;

        public ButtonWrapper(int padNr)
        {
            this.padNr = padNr;
        }

        public void redraw(bool selected)
        {
            Text = selected ? "X" : "";
            if (colorString != null)
                BackColor = Color.FromArgb(int.Parse(colorString, System.Globalization.NumberStyles.HexNumber));

            Invalidate();
        }
    }

    public partial class KeyboardControls : UserControl
    {
        private KeyboardProgram keyboardProgram;
        private ButtonWrapper currentButton;

        public KeyboardControls(KeyboardProgram keyboardProgram)
        {
            InitializeComponent();
            this.keyboardProgram = keyboardProgram;

            for (int i = 0; i < 64; i++)
            {
                var macro = keyboardProgram.getMacro(i);
                var b = new ButtonWrapper(i)
                {
                    BackColor = macro.getColor(),
                    Width = 53,
                    Height = 80,
                };
                b.Click += (object? sender, EventArgs args) => padClicked(b);
                panelPads.Controls.Add(b);
            }

            AutoCompleteStringCollection acsc = new AutoCompleteStringCollection();
            inputMacro.AutoCompleteCustomSource = acsc;
            inputMacro.AutoCompleteMode = AutoCompleteMode.Suggest;
            inputMacro.AutoCompleteSource = AutoCompleteSource.CustomSource;
            foreach (var item in (VirtualKeyCode[])Enum.GetValues(typeof(VirtualKeyCode)))
                acsc.Add(item.ToString());

        }

        private void padClicked(ButtonWrapper button)
        {
            if (currentButton != null)
                currentButton.redraw(false);

            currentButton = button;

            labelPadNumber.Text = "Pad " + (currentButton.padNr + 1);
            inputColor.Text = currentButton.BackColor.ToArgb().ToString("X8");
            inputMacro.Text = currentButton.macroText;
            currentButton.redraw(true);
        }

        private void saveClicked(object sender, EventArgs e)
        {
            if (currentButton == null)
                return;

            currentButton.macroText = inputMacro.Text;
            currentButton.colorString = inputColor.Text;
            keyboardProgram.setMacro(currentButton.padNr, currentButton.macroText, currentButton.colorString);
            currentButton.redraw(true);
        }

    }
}
