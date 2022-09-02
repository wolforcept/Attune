using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AttuneLib;
using NAudio.Wave;

namespace AttuneLib;

class PhraseMode : PlayerSubProgram
{

    private Phrase phrase;

    internal PhraseMode(String name, Attune attune, AkaiFire fire, PlayerProgram player) : base(name, attune, fire, player) { }

    internal void SetPhrase(Phrase phrase)
    {
        if (phrase == null)
            throw new ArgumentNullException("Phrase cannot be null!!");
        this.phrase = phrase;
        Refresh();
    }

    internal override void Pause()
    {
    }

    internal override void Resume()
    {
        Refresh();
    }

    internal override void Refresh()
    {
        refreshScreen();
        refreshPadColors();
    }

    private void refreshScreen()
    {
        fire.WriteOnScreen(0, 0, phrase.Name);
    }

    private void refreshPadColors()
    {
        for (int i = 0; i < phrase.Length; i++)
        {
            fire.SetPadColor(i, 0, i == phrase.Current ? 0xFFFFFF : phrase[i] == Entry.EmptyEntry ? 0 : 0x555555);
        }
    }

    internal override void Stop()
    {

    }

    internal override void Reset()
    {
        phrase.Reset();
    }

    internal override void Start()
    {
    }

    internal override void PlayStep()
    {
        phrase.Play();
        refreshPadColors();
    }
}
