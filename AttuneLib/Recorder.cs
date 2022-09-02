//using NAudio.Wave;
//using NAudio.Wave.Asio;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace AttuneLib;

//class Recorder
//{

//    // Other inputs are also usable. Just look through the NAudio library.
//    // private IWaveIn waveIn;
//    // private static int fftLength = 8192;
//    // NAudio fft wants powers of two! 
//    // There might be a sample aggregator in NAudio somewhere but I made a variation for my needs
//    // private SampleAggregator sampleAggregator = new SampleAggregator(fftLength);
//    // public void Freq() {
//    //    sampleAggregator.FftCalculated += new EventHandler<FftEventArgs>(FftCalculated);
//    //    sampleAggregator.PerformFFT = true;
//    //    // Here you decide what you want to use as the waveIn. 
//    //    // There are many options in NAudio and you can use other streams/files. 
//    //    // Note that the code varies for each different source.
//    //    waveIn = new WasapiLoopbackCapture();
//    //    waveIn.DataAvailable += OnDataAvailable;
//    //    waveIn.StartRecording();
//    // }

//    int fftLength = (int)Math.Pow(2, 13); // NAudio fft wants powers of two!

//    private static AsioOut asioOut;

//    public event EventHandler<float[]> OnData;
//    //public event EventHandler<FftEventArgs> OnData;
//    public bool IsRecording = false;

//    //private SampleAggregator sampleAggregator;
//    private float[] sampleBuffer, fftBuffer;

//    internal static void init()
//    {
//        var names = AsioOut.GetDriverNames();
//        foreach (var name in names)
//        {
//            if (name.Contains("FL Studio"))
//            {
//                asioOut = new AsioOut(name);
//            }
//        }
//    }

//    public Recorder()
//    {
//        if (asioOut == null)
//            return;

//        sampleBuffer = new float[fftLength];
//        fftBuffer = new float[fftLength];

//        //sampleAggregator = new SampleAggregator(fftLength);
//        //sampleAggregator. += FftCalculated;
//        //sampleAggregator.PerformFFT = true;

//        asioOut.AutoStop = false;
//        asioOut.AudioAvailable += OnAsioOutAudioAvailable;
//        //asioOut.ShowControlPanel();
//    }

//    //void FftCalculated(object sender, FftEventArgs e) {
//    //    OnData.Invoke(sender, e);
//    //}

//    public void Start()
//    {
//        if (asioOut == null)
//            return;
//        var inputChannels = asioOut.DriverInputChannelCount;
//        asioOut.InputChannelOffset = 0;
//        var recordChannelCount = inputChannels;
//        var sampleRate = 44100;
//        asioOut.InitRecordAndPlayback(null, recordChannelCount, sampleRate);
//        asioOut.Play();
//        asioOut.PlaybackStopped += (sender, e) =>
//        {
//            asioOut.Dispose();
//            asioOut = null;
//        };
//        IsRecording = true;
//    }

//    public void Stop()
//    {
//        asioOut.Stop();
//        IsRecording = false;
//    }

//    AsioSampleType? asioType = null;
//    void OnAsioOutAudioAvailable(object sender, AsioAudioAvailableEventArgs e)
//    {
//        if (asioType == null)
//            asioType = e.AsioSampleType;

//        e.GetAsInterleavedSamples(sampleBuffer);
//        //foreach (var v in sampleBuffer)
//            //sampleAggregator.Add(v, v);
//        //sampleAggregator.GetFFTResults(fftBuffer);
//        OnData.Invoke(sender, fftBuffer);
//        //sampleAggregator.Clear();

//    }

//    //void OnDataAvailable(object sender, WaveInEventArgs e) {

//    //    byte[] buffer = e.Buffer;
//    //    int bytesRecorded = e.BytesRecorded;
//    //    int bufferIncrement = waveIn.WaveFormat.BlockAlign;

//    //    for (int index = 0; index < bytesRecorded; index += bufferIncrement) {
//    //        Attune.Log(index);

//    //        float sample32 = BitConverter.ToSingle(buffer, index);
//    //        Attune.Log(sample32);
//    //        if (sample32 != float.NaN)
//    //            sampleAggregator.Add(sample32);
//    //    }

//    //    //byte[] buffer = e.Buffer;
//    //    //int bytesRecorded = e.BytesRecorded;
//    //    //for (int index = 0; index < bytesRecorded; index += bufferIncrement) {
//    //    //    float sample32 = BitConverter.ToSingle(buffer, index);
//    //    //    OnData.Invoke(this, sample32);
//    //    //}
//    //}


//}
