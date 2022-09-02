using AttuneLib;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using NAudio.Dsp;
using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttuneLib;

public static class Notes
{

    private const int octaves = 20;
    private readonly static string[] baseNames = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
    private readonly static int[] notes = new int[baseNames.Length * octaves];
    private readonly static double[] freqs = new double[notes.Length];
    private readonly static string[] names = new string[notes.Length];
    private readonly static Dictionary<string, int> nameFinder = new();
    private readonly static Dictionary<double, int> freqFinder = new();

    static Notes()
    {
        for (int n = 0; n < notes.Length; n++)
        {
            notes[n] = n;
            freqs[n] = (440 * Math.Pow(2, (n - 69) / 12f));
            names[n] = baseNames[(n + 3) % 12] + (int)(n / 12);
            nameFinder[names[n]] = n;
            freqFinder[freqs[n]] = n;
        }
    }

    public static int Length => notes.Length;

    public static double GetFrequency(string note)
    {
        return freqs[nameFinder[note]];
    }

    public static double GetFrequency(int note)
    {
        if (note < 0 || note >= notes.Length)
            return 0;
        return freqs[note];
    }

    internal static int GetNoteClosestToFreq(double freq)
    {
        return notes.Aggregate((n1, n2) => Math.Abs(freqs[n1] - freq) < Math.Abs(freqs[n2] - freq) ? n1 : n2);
    }

    internal static int GetNote(double freq)
    {
        return freqFinder.GetValueOrDefault(freq);
    }

    internal static int GetNote(string name)
    {
        return nameFinder.GetValueOrDefault(name);
    }

    internal static string getName(int i)
    {
        return names[i];
    }
}
