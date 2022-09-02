using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NAudio.Wave;

namespace AttuneLib;

internal abstract class PlayerSubProgram : AttuneProgram
{

    protected PlayerProgram baseProgram { get; }

    internal PlayerSubProgram(string name, Attune attune, AkaiFire fire, PlayerProgram player) : base(name, attune, fire)
    {
        this.baseProgram = player;
    }

    internal abstract void Pause();
    internal abstract void Resume();
    internal abstract void Refresh();
    internal abstract void Reset();
    internal abstract void Stop();
    internal abstract void Start();
    internal abstract void PlayStep();
}

class PlayerProgram : AttuneProgram
{

    // DATA
    internal readonly List<Instrument> Instruments;
    internal readonly List<Phrase> Phrases;

    // IO
    internal WaveOutEvent Output { get; } = new WaveOutEvent();

    // CONTROL
    private Clock clock;
    private bool IsPlaying => clock != null && clock.IsRunning;
    private bool IsRecording = false;
    private PlayerSubProgram SubMode => child is PlayerSubProgram s ? s : throw new ArgumentException("No Sub Mode Defined!");

    private readonly PhraseMode phraseMode;
    //private NoteMode noteMode = new NoteMode();
    private InstrumentMode instrumentMode;
    private int bpm = 100;

    public PlayerProgram(string name, Attune attune, AkaiFire fire) : base(name, attune, fire)
    {

        Instruments = new();
        Phrases = new();

        instrumentMode = new InstrumentMode("Instrument Mode", attune, fire, this);

        phraseMode = new PhraseMode("Instrument Mode", attune, fire, this);

        SynthInstrument startInstrument = new();
        Instruments.Add(startInstrument);

        Phrase startPhrase = new();
        startPhrase.Set(0, new Note(startInstrument, "C6", 100));
        startPhrase.Set(2, new Note(startInstrument, "E6", 100));
        startPhrase.Set(4, new Note(startInstrument, "F6", 100));
        startPhrase.Set(6, new Note(startInstrument, "G6", 100));
        startPhrase.Set(7, Entry.EmptyEntry);
        Phrases.Add(startPhrase);

        instrumentMode.SetInstrument(startInstrument);
        phraseMode.SetPhrase(startPhrase);


        this.child = phraseMode;
        SubMode.Resume();
    }

    protected void SetSubMode(AttuneProgram mode)
    {
        SubMode.Pause();
        this.child = mode;
        SubMode.Resume();
    }

    public override void OnStart()
    {
        init();
    }

    public override void OnButtonReleased(AkaiFire.Button button)
    {

        if (button == AkaiFire.Button.STEP)
            SetSubMode(phraseMode);

        //else if (button == AkaiFire.Button.STEP)
        //    this.child = noteMode;

        else if (button == AkaiFire.Button.DRUM)
            SetSubMode(instrumentMode);

        else if (button == AkaiFire.Button.RECORD)
            toggleRecording();

        else if (button == AkaiFire.Button.PLAY)
            togglePlaying();

        else if (button == AkaiFire.Button.STOP)
            stop();

        else
            base.OnButtonReleased(button);

        Refresh();
    }

    public override void OnPadPressed(int i, int x, int y)
    {
        base.OnPadPressed(i, x, y);
    }

    //
    //
    //

    private void init()
    {
        foreach (AkaiFire.Button button in Enum.GetValues(typeof(AkaiFire.Button)))
        {
            fire.LightButton(button, 0);
        }
        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                fire.SetPadColor(x, y, 0);
            }
        }
        Refresh();
    }

    private void togglePlaying()
    {
        if (IsPlaying)
            pause();
        else
            play();
        Refresh();
    }

    private void toggleRecording()
    {
        IsRecording = !IsRecording;
        Refresh();
    }

    private void play()
    {
        if (clock != null)
        {
            clock.Start();
            return;
        }
        clock = new Clock() { Interval = bpm };
        foreach (var instrument in Instruments)
            instrument.Init(Output);
        SubMode.Start();
        clock.Elapsed += (o, e) =>
        {
            SubMode.PlayStep();
        };
        clock.Start();
    }

    private void pause()
    {
        if (clock is not null)
        {
            if (clock.IsRunning)
                clock.Stop();
            Output.Stop();
            SubMode.Stop();
            foreach (var instrument in Instruments)
            {
                instrument.Stop(Output);
            }
        }
    }

    private void stop()
    {
        if (clock is not null)
        {
            if (clock.IsRunning)
                clock.Stop();
            clock.Dispose();
            Output.Stop();
            SubMode.Reset();
            clock = null;
        }
        Refresh();
    }

    private void Refresh()
    {
        fire.LightButton(AkaiFire.Button.PLAY, IsPlaying ? 3 : 2);
        fire.LightButton(AkaiFire.Button.STOP, IsPlaying ? 1 : 0);
        fire.LightButton(AkaiFire.Button.RECORD, IsRecording ? 3 : 2);
        SubMode.Refresh();
    }

}
