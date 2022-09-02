using Attune.Programs.Keyboard;
using AudioSwitcher.AudioApi.CoreAudio;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using static System.Windows.Forms.Control;

namespace AttuneLib;

public class KeyboardProgram : AttuneProgram
{

    private KeyboardControls keyboardControls;
    private KeyboardProgramData data;
    private InputSimulator ins = new InputSimulator();

    public KeyboardProgram(string name, Attune attune, AkaiFire fire) : base(name, attune, fire)
    { }

    public override void OnStart()
    {
        data = KeyboardProgramData.load();
        fire.WriteOnScreen(0, 0, "program: keyboard");
        fire.WriteOnAllPads(0);
        fire.LightAllButtons(0);

        updateLights();

        keyboardControls = new KeyboardControls(this);
    }

    private void updateLights()
    {
        for (int col = 0; col < 16; col++)
            for (int row = 0; row < 4; row++)
            {
                MacroData macro = getMacro(row * 16 + col);
                fire.SetPadColor(col, row, macro.getColor().ToArgb());
            }
    }

    public override UserControl GetControls()
    {
        return keyboardControls;
    }

    public override void OnPadPressed(int i, int x, int y)
    {
        try
        {
            MacroData macro = getMacro(i);
            if (!macro.macroString.StartsWith(":"))
            {
                VirtualKeyCode code = (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), macro.macroString);
                ins.Keyboard.KeyDown(code);
            }
        }
        catch (Exception ex)
        {
            Attune.Log(ex.Message);
        }
    }

    public override void OnPadReleased(int i, int x, int y)
    {
        try
        {
            MacroData macro = getMacro(i);
            if (!macro.macroString.StartsWith(":"))
            {
                VirtualKeyCode code = (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), macro.macroString);
                ins.Keyboard.KeyUp(code);
            }
            else if (macro.macroString.Length > 0)
                ins.Keyboard.TextEntry(macro.macroString.Substring(1));
        }
        catch (Exception ex)
        {
            Attune.Log(ex.Message);
        }
    }

    public override void OnKnob(AkaiFire.Knob knob, int d)
    {
        try
        {
            if (knob == AkaiFire.Knob.Knob1)
            {
                if (d < 0)
                    ins.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.F13);
                else
                    ins.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.F14);
            }

            else if (knob == AkaiFire.Knob.Knob2)
            {
                if (d < 0)
                    ins.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.F15);
                else
                    ins.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.F16);
            }

            else if (knob == AkaiFire.Knob.Knob3)
            {
                if (d < 0)
                    ins.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.F17);
                else
                    ins.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.F18);
            }

            else if (knob == AkaiFire.Knob.Knob4)
            {
                if (d < 0)
                    ins.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.F19);
                else
                    ins.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.F20);
                //SendKeys.SendWait("{F20}");
            }
        }
        catch (Exception ex)
        {
            Attune.Log(ex.Message);
        }
    }


    //
    //

    internal void setMacro(int padNr, string macroText, string colorString)
    {
        data.setMacro(padNr, macroText, colorString);
        data.save();
        updateLights();
    }

    internal MacroData getMacro(int padNr)
    {
        return data.getMacro(padNr);
    }

}
