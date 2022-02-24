using NAudio.Midi;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Attune {
    public class Attune {

        const bool debug = false;
        static Form form;

        [STAThread]
        static void Main() {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new Form(new Attune());
            Application.Run(form);
        }

        private bool IsConnected => fire != null;
        private AkaiFire fire = null;

        internal void Start() {
            Log("Connecting...");
            if (!IsConnected) {
                string reason; (fire, reason) = AkaiFire.Create(this);
                Log(reason);
                onStart();
            } else
                Log("Already Connected.");
        }

        internal void Close() {
            fire.Close();
            fire = null;
        }

        internal void OnMessage(MidiEvent e) {
            if (debug) {
                Log(DateTime.Now + ": ", e.GetAsShortMessage());
            }

            if (e is NoteEvent note) {
                Log(note.CommandCode, note.NoteNumber);
                if (note.NoteNumber >= 54 && note.NoteNumber <= 117) {
                    int n = note.NoteNumber - 54;
                    if (note is NoteOnEvent)
                        OnPadPressed(n, n % 16, n / 16);
                    else
                        OnPadReleased(n, n % 16, n / 16);
                }
            }

        }

        internal void OnPadPressed(int n, int x, int y) {
            fire.SetPadColor(x, y, 20 + n);
        }

        internal void OnPadReleased(int n, int x, int y) {
            fire.SetPadColor(x, y, 0);
        }

        internal void OnError(object sender, MidiInMessageEventArgs args) {
            if (debug) {
                Log(DateTime.Now + ": ", args);
            }
            fire = null;
        }

        internal void OnSysex(object sender, MidiInSysexMessageEventArgs args) {
            if (debug) {
                Log(DateTime.Now + ": ", args);
            }
        }

        internal void Log(params object[] objs) {
            foreach (object s in objs)
                form.AddText(s.ToString());
            form.AddText("\n");
        }

        //
        // PRIVATE METHODS
        private void onStart() {
            new Thread(new ThreadStart(AwakeCheck)).Start();
        }

        private void AwakeCheck() {
            while (fire != null) {
                fire.WriteOnScreen();
                Thread.Sleep(1000);
            }
        }

    }
}
