using AttuneLib;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttuneLib;

internal abstract class Instrument {

    private int id = 0;
    public readonly string Name;

    public Instrument() {
        Name = "Instrument " + (id < 10 ? "00" + id : (id < 100 ? "0" + id : id));
        id++;
    }

    internal abstract void Init(WaveOutEvent output);
    internal abstract void OnKnob(AkaiFire.Knob i, int d);
    internal abstract void Play(WaveOutEvent output);
    internal abstract void Stop(WaveOutEvent output);
    internal abstract void NoteOn(int note, int time = 0);
    internal abstract void NoteOff(int note);
    internal abstract void Render(AkaiFire fire);

}

