using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AttuneLib;
using NAudio.Wave;

namespace AttuneLib;

class InstrumentMode : PlayerSubProgram
{

    const int KEY_BLUE = 0x0066FF;
    const int KEY_WHITE = 0x99AAFF;
    private static readonly int? UU = null;
    private static readonly int[] PIANO_ROW1_COLORS = new int[] { 0, KEY_BLUE, KEY_BLUE, 0, KEY_BLUE, KEY_BLUE, KEY_BLUE, 0, KEY_BLUE, KEY_BLUE, 0, KEY_BLUE, KEY_BLUE, KEY_BLUE, 0, KEY_BLUE };
    private static readonly int?[] PIANO_ROW1_NOTES = new int?[] { UU, 02, 04, UU, 07, 09, 11, UU, 14, 16, UU, 19, 21, 23, UU, 26 };
    private static readonly int?[] PIANO_ROW2_NOTES = new int?[] { 01, 03, 05, 06, 08, 10, 12, 13, 15, 17, 18, 20, 22, 24, 25, 27 };

    private Instrument instrument;
    private int octave = 6;
    //private Note[] NotesRow1, NotesRow2;

    public InstrumentMode(string name, Attune attune, AkaiFire fire, PlayerProgram player) : base(name, attune, fire, player) { }

    public override void OnKnob(AkaiFire.Knob i, int d)
    {
        if (i == AkaiFire.Knob.Selector)
        {
            i += d;
        }
        instrument.OnKnob(i, d);
        Refresh();
    }

    public override void OnPadPressed(int i, int x, int y)
    {
        if (y == 2 && PIANO_ROW1_NOTES[x] != null)
            instrument.NoteOn(PIANO_ROW1_NOTES[x] ?? 0);
        if (y == 3)
            instrument.NoteOn(PIANO_ROW2_NOTES[x] ?? 0);
        Refresh();
    }

    public override void OnPadReleased(int i, int x, int y)
    {
        if (y == 2 && PIANO_ROW1_NOTES[x] != null)
            instrument.NoteOff(PIANO_ROW1_NOTES[x] ?? 0);
        if (y == 3)
            instrument.NoteOff(PIANO_ROW2_NOTES[x] ?? 0);
        Refresh();
    }

    internal void SetInstrument(Instrument instrument)
    {
        if (this.instrument != null)
            this.instrument.Stop(baseProgram.Output);
        if (instrument != null)
        {
            this.instrument = instrument;
            instrument.Init(baseProgram.Output);
        }
    }

    internal override void Resume()
    {
        instrument.Init(baseProgram.Output);
        //NotesRow1 = new Note[PIANO_ROW1_NOTES.Length];
        //NotesRow2 = new Note[PIANO_ROW1_NOTES.Length];
        //for (int i = 0; i < NotesRow1.Length; i++)
        //    NotesRow1[i] = new Note(instrument, octave * 12 + PIANO_ROW1_NOTES[i] ?? 0, 0);
        //for (int i = 0; i < NotesRow2.Length; i++)
        //    NotesRow2[i] = new Note(instrument, octave * 12 + PIANO_ROW2_NOTES[i] ?? 0, 0);
        Refresh();
    }

    internal override void Pause()
    {
        instrument.Stop(baseProgram.Output);
    }

    internal override void Refresh()
    {

        for (int i = 0; i < 16; i++)
            fire.SetPadColor(i, 2, PIANO_ROW1_COLORS[i]);
        for (int i = 0; i < 16; i++)
            fire.SetPadColor(i, 3, KEY_WHITE);

        fire.PrepImg();

        fire.WriteOnScreenBuffered(0, 0, instrument.Name);
        fire.WriteOnScreenBuffered(0, 10, "octave: " + octave);

        instrument.Render(fire);

        fire.PrepImgSend();
    }

    internal override void Reset()
    {
    }

    internal override void Stop()
    {
    }

    internal override void Start()
    {
    }

    internal override void PlayStep()
    {
    }
}
