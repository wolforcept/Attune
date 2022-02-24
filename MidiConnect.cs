using NAudio.Midi;
using System;
using System.Collections.Generic;


namespace Attune {
    static class MidiConnect {

        public static Dictionary<string, int> listMidiIn() {
            var deviceList = new Dictionary<string, int>();
            for (int device = 0; device < MidiIn.NumberOfDevices; device++)
                deviceList[MidiIn.DeviceInfo(device).ProductName] = device;
            return deviceList;
        }

        internal static MidiIn HandleMidiIn(int deviceIndex,
            EventHandler<MidiInMessageEventArgs> messageHandler,
            EventHandler<MidiInMessageEventArgs> errorHandler,
            EventHandler<MidiInSysexMessageEventArgs> sysexHandler
        ) {
            var midiIn = new MidiIn(deviceIndex);
            midiIn.MessageReceived += messageHandler;
            midiIn.ErrorReceived += errorHandler;
            midiIn.SysexMessageReceived += sysexHandler;
            midiIn.Start();
            return midiIn;
        }

        public static Dictionary<string, int> listMidiOut() {
            var deviceList = new Dictionary<string, int>();
            for (int device = 0; device < MidiOut.NumberOfDevices; device++)
                deviceList[MidiOut.DeviceInfo(device).ProductName] = device;
            return deviceList;
        }

        internal static MidiOut GetMidiOut(int deviceIndex) {
            var midiOut = new MidiOut(deviceIndex);
            midiOut.Reset();
            return midiOut;
        }

    }
}