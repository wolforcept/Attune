using AttuneLib;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttuneLib;

internal class Note : Entry {

    private int note, time;
    private Instrument instrument;

    public Note(Instrument instrument, string noteName, int time)
        : this(instrument, Notes.GetNote(noteName), time) { }

    public Note(Instrument instrument, double noteFreq, int time)
        : this(instrument, Notes.GetNote(noteFreq), time) { }

    public Note(Instrument instrument, int note, int time) {
        this.note = note;
        this.instrument = instrument;
        this.time = time;
    }

    internal override bool IsFinished => true;

    public override void Play() {
        this.instrument.NoteOn(note, time);
    }

    public override void Reset() {
    }
}
