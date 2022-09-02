using System;
using System.Linq;
using NAudio_Synth;
using NAudio.Wave;
using AttuneLib;

namespace AttuneLib;

internal class SynthInstrument : Instrument
{

    //private readonly SignalGenerator gen;
    private SynthEngine synthEngine;

    public SynthInstrument()
    {
        //gen = new SignalGenerator() {
        //    Gain = 0.2,
        //    Type = SignalGeneratorType.Sin
        //};
    }

    internal override void Init(WaveOutEvent output)
    {

        synthEngine = new SynthEngine(48000, 64) { FilterCutoff = 20000, FilterLevel = 1 };
        output.Init(synthEngine);
    }

    internal override void Play(WaveOutEvent output)
    {
        output.Play();
    }

    internal override void OnKnob(AkaiFire.Knob i, int d)
    {

        if (i == AkaiFire.Knob.Knob1)
            synthEngine.Attack = Math.Max(0, synthEngine.Attack + .001f * d * d * d);

        else if (i == AkaiFire.Knob.Knob2)
            synthEngine.Decay = Math.Max(0, synthEngine.Attack + .001f * d * d * d);

        else if (i == AkaiFire.Knob.Knob3)
            synthEngine.Sustain = Math.Max(0, synthEngine.Attack + .001f * d * d * d);

        else if (i == AkaiFire.Knob.Knob4)
            synthEngine.Release = Math.Max(0, synthEngine.Attack + .001f * d * d * d);

    }

    internal override void NoteOn(int note, int time = 0)
    {
        //gen.Frequency = freq;
        synthEngine.NoteOn(note);
        if (time > 0)
            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.Sleep(time);
                NoteOff(note);
            }).Start();
    }

    internal override void NoteOff(int note)
    {
        synthEngine.NoteOff(note);
    }

    internal override void Stop(WaveOutEvent output)
    {
        output.Stop();
    }

    internal override void Render(AkaiFire fire)
    {
        fire.WriteOnScreenBuffered(10, 20, "A: " + ((int)(synthEngine.Attack * 100f)) / 100f);
        fire.WriteOnScreenBuffered(10, 30, "D: " + ((int)(synthEngine.Decay * 100f)) / 100f);
        fire.WriteOnScreenBuffered(10, 40, "S: " + ((int)(synthEngine.Sustain * 100f)) / 100f);
        fire.WriteOnScreenBuffered(10, 50, "R: " + ((int)(synthEngine.Release * 100f)) / 100f);
    }

}
