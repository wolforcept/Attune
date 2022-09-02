using NAudio.Midi;
using System;
using System.Threading;
using System.Windows.Forms;
using static AttuneLib.AttuneForm;

namespace AttuneLib;
public class Attune
{

    private static AttuneForm form;

    public static Thread mainThread;

    [STAThread]
    static void Main()
    {
        //Recorde.init();
        mainThread = Thread.CurrentThread;
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        form = new AttuneForm(new Attune());
        Application.Run(form);
    }

    private bool IsConnected => fire != null;
    private bool IsDebug => currentMode is DebugProgram;
    private bool IsVerbose => (currentMode as DebugProgram)?.IsVerbose ?? false;

    private AttuneProgram currentMode = null;
    //private Recorder recorder = new Recorder();
    private AkaiFire fire = null;

    public void onFormConnect(object selectedProgram)
    {
        Log("Connecting...");
        if (!IsConnected)
        {
            string reason; (fire, reason) = AkaiFire.Create(this);
            if (fire is null)
            {
                Log("Failed to connect.");
                Log(reason);
                return;
            }
            Log("Successfully connected to " + fire);
        }
        else
            Log("Already Connected.");

        OnFormModeChanged(selectedProgram);
    }

    public void onFormClosed()
    {
        if (fire != null)
            fire.Close();
        fire = null;
        currentMode.OnClose();
    }

    internal void OnFormModeChanged(object selectedValue)
    {
        if (IsConnected)
            if (selectedValue is Func<Attune, AkaiFire, AttuneProgram> func)
                SetCurrentMode(func.Invoke(this, fire));
    }

    public void SetCurrentMode(AttuneProgram mode)
    {
        if (currentMode != null)
        {
            Log("Program will exit: " + currentMode.name);
            currentMode?.OnClose();
        }

        if (mode != null)
        {
            currentMode = mode;
            Log("Program will start: " + currentMode.name);
            currentMode.OnStart();
            form.setProgramControls(currentMode.GetControls());
        }
    }

    public bool IsButtonDown(AkaiFire.Button button)
    {
        return fire.IsButtonDown(button);
    }

    //public void StartRecording()
    //{
    //recorder.Start();
    //}

    //public void StopRecording()
    //{
    //recorder.Stop(); TODO
    //}

    //public bool IsRecording => true; //recorder.IsRecording;

    //public void Render(Image img) {
    //    form.Render(img);
    //}

    //
    //
    // EVENTS
    //

    internal void OnDataRecorded(object sender, float[] e)
    {
        if (currentMode != null)
            currentMode.OnDataRecorded(sender, e);
    }

    internal void OnButtonPressed(AkaiFire.Button button)
    {
        if (currentMode != null)
            currentMode.OnButtonPressed(button);
    }

    internal void OnButtonReleased(AkaiFire.Button button)
    {
        if (currentMode != null)
            currentMode.OnButtonReleased(button);
    }

    internal void OnKnob(AkaiFire.Knob knob, int d)
    {
        if (currentMode != null)
            currentMode.OnKnob(knob, d);
    }

    internal void OnPadPressed(int n, int x, int y)
    {
        if (currentMode != null)
            currentMode.OnPadPressed(n, x, y);
    }

    internal void OnPadReleased(int n, int x, int y)
    {
        if (currentMode != null)
            currentMode.OnPadReleased(n, x, y);
    }

    internal void OnError(object sender, MidiInMessageEventArgs args)
    {
        LogDebug(DateTime.Now + ": ", args);
        fire = null;
    }

    internal void OnSysex(object sender, MidiInSysexMessageEventArgs args)
    {
        LogDebugVerbose(DateTime.Now + ": ", args);
    }

    internal void LogDebug(params object[] objs)
    {
        if (IsDebug)
            Log(objs);
    }

    internal void LogDebugVerbose(params object[] objs)
    {
        if (IsDebug && IsVerbose)
            Log(objs);
    }

    public static void Log(params object[] objs)
    {
        if (form == null)
            return;
        if (objs == null)
            form.AddText("null");
        else
            foreach (object s in objs)
                form.AddText(s?.ToString() ?? "null");
        form.AddText("\n");
    }
}
