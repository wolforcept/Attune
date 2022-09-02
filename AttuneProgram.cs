using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AttuneLib;

public abstract class AttuneProgram
{
    public readonly string name;
    internal Attune attune;
    internal AttuneProgram child;
    internal AkaiFire fire;

    public AttuneProgram(string name, Attune attune, AkaiFire fire)
    {
        this.name = name;
        this.attune = attune;
        this.fire = fire;
    }

    public virtual void OnStart()
    {
        child?.OnStart();
    }
    public virtual void OnClose()
    {
        child?.OnClose();
    }
    public virtual void OnDataRecorded(object sender, float[] e)
    {
        child?.OnDataRecorded(sender, e);
    }
    public virtual void OnButtonPressed(AkaiFire.Button button)
    {
        child?.OnButtonPressed(button);
    }
    public virtual void OnButtonReleased(AkaiFire.Button button)
    {
        child?.OnButtonReleased(button);
    }
    public virtual void OnPadPressed(int i, int x, int y)
    {
        child?.OnPadPressed(i, x, y);
    }
    public virtual void OnPadReleased(int i, int x, int y)
    {
        child?.OnPadReleased(i, x, y);
    }
    public virtual void OnKnob(AkaiFire.Knob i, int d)
    {
        child?.OnKnob(i, d);
    }

    public virtual UserControl GetControls()
    {
        return null;
    }

    public bool IsShiftDown => attune.IsButtonDown(AkaiFire.Button.SHIFT);
    public bool IsAltDown => attune.IsButtonDown(AkaiFire.Button.ALT);

}
