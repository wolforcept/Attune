using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttuneLib;

internal class Phrase : Entry {

    private static int id;
    public readonly string Name;
    public int Length => entries.Count;
    public int Current { get; private set; } = 0;

    private List<Entry> entries = new();

    public Phrase() {
        Name = "Phrase " + (id < 10 ? "00" + id : (id < 100 ? "0" + id : id));
        id++;
        Set(3, EmptyEntry);
    }
    internal override bool IsFinished => Current == Length - 1;

    //public override bool HasNext() {
    //    return true;
    //}

    public void Set(int n, Entry entry) {
        if (n >= entries.Count) {
            for (int i = entries.Count; i < n; i++) {
                entries.Add(Entry.EmptyEntry);
            }
            entries.Add(entry);
        }
        entries[n] = entry;
    }

    public override void Reset() {
        Current = 0;
        foreach (Entry entry in entries) {
            entry.Reset();
        }
    }

    public override void Play() {
        if (entries.Count == 0) return;

        entries[Current].Play();
        if (entries[Current].IsFinished)

            if (Current < entries.Count - 1)
                Current++;
            else
                Current = 0;

    }

    public Entry this[int i] {
        get { return entries[i]; }
        private set { entries[i] = value; }
    }

}
