using System;
using System.Collections.Generic;

namespace AttuneLib;

public class FftProcessor : IDisposable
{
    readonly List<double> Values = new();
    readonly NAudio.Wave.WaveInEvent WaveIn;

    public FftProcessor(int device = 0)
    {
        WaveIn = new()
        {
            DeviceNumber = device,
            WaveFormat = new NAudio.Wave.WaveFormat(rate: 12_000, bits: 16, channels: 1),
            BufferMilliseconds = 10,
        };
        WaveIn.DataAvailable += WaveIn_DataAvailable; ;
        WaveIn.StartRecording();
    }

    public void Dispose()
    {
        WaveIn.DataAvailable -= WaveIn_DataAvailable;
        WaveIn.StopRecording();
        WaveIn.Dispose();
    }

    private void WaveIn_DataAvailable(object? sender, NAudio.Wave.WaveInEventArgs e)
    {
        for (int index = 0; index < e.BytesRecorded; index += 2)
        {
            double value = BitConverter.ToInt16(e.Buffer, index);
            Values.Add(value);
        }
    }

    public double[]? GetFft(int pow = 10, double stepFrac = 0.1)
    {
        int sampleCount = 1 << pow;
        if (Values.Count < sampleCount)
            return null;

        double[] values = new double[sampleCount];
        Values.CopyTo(Values.Count - sampleCount, values, 0, sampleCount);

        int pointsToKeep = (int)((1 - stepFrac) * sampleCount);
        Values.RemoveRange(0, Values.Count - pointsToKeep);

        double[] fft = FftSharp.Transform.FFTmagnitude(values);
        return fft;
    }
}


//// (c) Copyright Jacob Johnston.
//// This source is subject to Microsoft Public License (Ms-PL).
//// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
//// All other rights reserved.

//using System;
//using NAudio.Dsp;

//namespace AttuneLib {
//    public class SampleAggregator {
//        private float volumeLeftMaxValue;
//        private float volumeLeftMinValue;
//        private float volumeRightMaxValue;
//        private float volumeRightMinValue;
//        private Complex[] channelData;
//        private int bufferSize;
//        private int binaryExponentitation;
//        private int channelDataPosition;

//        public SampleAggregator(int bufferSize) {
//            this.bufferSize = bufferSize;
//            binaryExponentitation = (int)Math.Log(bufferSize, 2);
//            channelData = new Complex[bufferSize];
//        }

//        public void Clear() {
//            volumeLeftMaxValue = float.MinValue;
//            volumeRightMaxValue = float.MinValue;
//            volumeLeftMinValue = float.MaxValue;
//            volumeRightMinValue = float.MaxValue;
//            channelDataPosition = 0;
//        }

//        /// <summary>
//        /// Add a sample value to the aggregator.
//        /// </summary>
//        /// <param name="leftValue">The value of the left sample</param>
//        /// <param name="rightValue">The value of the right sample</param>
//        public void Add(float leftValue, float rightValue) {
//            if (channelDataPosition == 0) {
//                volumeLeftMaxValue = float.MinValue;
//                volumeRightMaxValue = float.MinValue;
//                volumeLeftMinValue = float.MaxValue;
//                volumeRightMinValue = float.MaxValue;
//            }

//            // Make stored channel data stereo by averaging left and right values.
//            channelData[channelDataPosition].X = (leftValue + rightValue) / 2.0f;
//            channelData[channelDataPosition].Y = 0;
//            channelDataPosition++;

//            volumeLeftMaxValue = Math.Max(volumeLeftMaxValue, leftValue);
//            volumeLeftMinValue = Math.Min(volumeLeftMinValue, leftValue);
//            volumeRightMaxValue = Math.Max(volumeRightMaxValue, rightValue);
//            volumeRightMinValue = Math.Min(volumeRightMinValue, rightValue);

//            if (channelDataPosition >= channelData.Length) {
//                channelDataPosition = 0;
//            }
//        }

//        // windows the data in samples with a Hamming window
//        //private void doWindow(Complex[] complex) {
//        //	double[] windowArray = CommonUtils.MathLib.FFT.FFTWindow.GetWindowFunction(CommonUtils.MathLib.FFT.FFTWindowType.HAMMING, complex.Length);

//        //	for (int i = 0; i < complex.Length; i++) {
//        //		complex[i].X *= (float)windowArray[i];
//        //	}
//        //}

//        /// <summary>
//        /// Performs an FFT calculation on the channel data upon request.
//        /// </summary>
//        /// <param name="fftBuffer">A buffer where the FFT data will be stored.</param>
//        public void GetFFTResults(float[] fftBuffer) {
//            var channelDataClone = new Complex[bufferSize];
//            channelData.CopyTo(channelDataClone, 0);

//            //doWindow(channelDataClone);

//            // Use NAUDIO FFT
//            FastFourierTransform.FFT(true, binaryExponentitation, channelDataClone);
//            for (int i = 0; i < channelDataClone.Length / 2; i++) {
//                // Calculate actual intensities for the FFT results.
//                fftBuffer[i] = (float)Math.Sqrt(channelDataClone[i].X * channelDataClone[i].X + channelDataClone[i].Y * channelDataClone[i].Y);
//            }
//        }

//        public float LeftMaxVolume {
//            get { return volumeLeftMaxValue; }
//        }

//        public float LeftMinVolume {
//            get { return volumeLeftMinValue; }
//        }

//        public float RightMaxVolume {
//            get { return volumeRightMaxValue; }
//        }

//        public float RightMinVolume {
//            get { return volumeRightMinValue; }
//        }
//    }
//}

////using System;
////using System.Collections.Generic;
////using System.Text;
////using System.Diagnostics;
////using NAudio.Dsp;

////namespace AttuneLib {
////    public class SampleAggregator {
////        // volume
////        public event EventHandler<MaxSampleEventArgs> MaximumCalculated;
////        private float maxValue;
////        private float minValue;
////        public int NotificationCount { get; set; }
////        int count;

////        // FFT
////        public event EventHandler<FftEventArgs> FftCalculated;
////        public bool PerformFFT { get; set; }
////        private Complex[] fftBuffer;
////        private FftEventArgs fftArgs;
////        private int fftPos;
////        private int fftLength;
////        private int m;

////        public SampleAggregator(int fftLength = 1024) {
////            if (!IsPowerOfTwo(fftLength)) {
////                throw new ArgumentException("FFT Length must be a power of two");
////            }
////            this.m = (int)Math.Log(fftLength, 2.0);
////            this.fftLength = fftLength;
////            this.fftBuffer = new Complex[fftLength];
////            this.fftArgs = new FftEventArgs(fftBuffer);
////        }

////        bool IsPowerOfTwo(int x) {
////            return (x & (x - 1)) == 0;
////        }

////        public void Reset() {
////            count = 0;
////            maxValue = minValue = 0;
////        }

////        public void Add(float value) {
////            if (PerformFFT && FftCalculated != null) {
////                fftBuffer[fftPos].X = (float)(value * FastFourierTransform.HammingWindow(fftPos, fftBuffer.Length));
////                fftBuffer[fftPos].Y = 0;
////                fftPos++;
////                if (fftPos >= fftBuffer.Length) {
////                    fftPos = 0;
////                    // 1024 = 2^10
////                    FastFourierTransform.FFT(true, m, fftBuffer);
////                    FftCalculated(this, fftArgs);
////                }
////            }

////            maxValue = Math.Max(maxValue, value);
////            minValue = Math.Min(minValue, value);
////            count++;
////            if (count >= NotificationCount && NotificationCount > 0) {
////                if (MaximumCalculated != null) {
////                    MaximumCalculated(this, new MaxSampleEventArgs(minValue, maxValue));
////                }
////                Reset();
////            }
////        }
////    }

////    public class MaxSampleEventArgs : EventArgs {
////        [DebuggerStepThrough]
////        public MaxSampleEventArgs(float minValue, float maxValue) {
////            this.MaxSample = maxValue;
////            this.MinSample = minValue;
////        }
////        public float MaxSample { get; private set; }
////        public float MinSample { get; private set; }
////    }

////    public class FftEventArgs : EventArgs {
////        [DebuggerStepThrough]
////        public FftEventArgs(Complex[] result) {
////            this.Result = result;
////        }
////        public Complex[] Result { get; private set; }
////    }
////}

//////using System;
//////using System.Collections.Generic;
//////using System.Text;

//////namespace Attune {
//////    using NAudio.Dsp; // The Complex and FFT are here!

//////    class SampleAggregator {
//////        // FFT
//////        public event EventHandler<FftEventArgs> FftCalculated;
//////        public bool PerformFFT { get; set; }

//////        // This Complex is NAudio's own! 
//////        private Complex[] fftBuffer;
//////        private FftEventArgs fftArgs;
//////        private int fftPos;
//////        private int fftLength;
//////        private int m;

//////        public SampleAggregator(int fftLength) {
//////            if (!IsPowerOfTwo(fftLength)) {
//////                throw new ArgumentException("FFT Length must be a power of two");
//////            }
//////            this.m = (int)Math.Log(fftLength, 2.0);
//////            this.fftLength = fftLength;
//////            this.fftBuffer = new Complex[fftLength];
//////            this.fftArgs = new FftEventArgs(fftBuffer);
//////        }

//////        bool IsPowerOfTwo(int x) {
//////            return (x & (x - 1)) == 0;
//////        }

//////        public void Add(float value) {
//////            if (PerformFFT && FftCalculated != null) {
//////                // Remember the window function! There are many others as well.
//////                fftBuffer[fftPos].X = (float)(value * FastFourierTransform.HammingWindow(fftPos, fftLength));
//////                fftBuffer[fftPos].Y = 0; // This is always zero with audio.
//////                fftPos++;
//////                if (fftPos >= fftLength) {
//////                    fftPos = 0;
//////                    FastFourierTransform.FFT(true, m, fftBuffer);
//////                    FftCalculated(this, fftArgs);
//////                }
//////            }
//////        }
//////    }

//////    public class FftEventArgs : EventArgs {
//////        public FftEventArgs(Complex[] result) {
//////            this.Result = result;
//////        }
//////        public Complex[] Result { get; private set; }
//////    }
//////}
