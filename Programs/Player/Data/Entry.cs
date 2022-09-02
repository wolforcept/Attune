using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttuneLib;

internal abstract class Entry
{
    private class _EmptyEntry : Entry
    {
        internal override bool IsFinished => true;

        public override void Play() { }
        public override void Reset() { }
    }

    public static Entry EmptyEntry = new _EmptyEntry();

    internal abstract bool IsFinished { get; }

    public abstract void Play();
    public abstract void Reset();

}
