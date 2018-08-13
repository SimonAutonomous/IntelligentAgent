using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AudioLibrary.SoundFeatures;
using MathematicsLibrary.SignalProcessing;

namespace AudioLibrary
{
    public class WAVFrameSet
    {
        private List<WAVSound> frameList;
        private double frameDuration;
        private double frameShift;
        private List<double> startTimeList;

        public WAVFrameSet(WAVSound sound, double frameDuration, double frameShift)
        {
            double soundDuration = sound.GetDuration();
            this.frameDuration = frameDuration;
            this.frameShift = frameShift;
            this.frameList = new List<WAVSound>();
            int numberOfFrames = (int)Math.Truncate((soundDuration - frameDuration + frameShift) / frameShift);
            this.startTimeList = new List<double>();
            for (int ii = 0; ii < numberOfFrames; ii++)
            {
                double startTime = ii * frameShift;
                double endTime = startTime + frameDuration;
                WAVSound frame = sound.Extract(startTime, endTime);
                frameList.Add(frame);
                startTimeList.Add(startTime);
            }
        }

        public List<SoundFeature> GetAutoCorrelationSeries(string namePrefix, int autoCorrelationOrder)
        {
            List<SoundFeature> soundFeatureList = new List<SoundFeature>();
            for (int ii = 0; ii < autoCorrelationOrder; ii++)
            {
                SoundFeature soundFeature = new SoundFeature();
                soundFeature.Name = namePrefix + (ii+1).ToString(); // No reason to compute order 0 (=1).
                soundFeature.SetSize(frameList.Count);
                soundFeatureList.Add(soundFeature);
            }
            for (int iFrame = 0; iFrame < frameList.Count; iFrame++)
            {
                WAVSound frame = frameList[iFrame];
                List<double> autoCorrelationCoefficients = frame.ComputeNormalizedAutoCorrelationCoefficients(1, autoCorrelationOrder);
                for (int ii = 0; ii < autoCorrelationOrder; ii++)
                {
                    soundFeatureList[ii].ValueList[iFrame] = autoCorrelationCoefficients[ii];
                }
            }
            return soundFeatureList;
        }

        public List<SoundFeature> GetLPCAndCepstralSeries(string lpcNamePrefix, int lpcOrder, string cepstralNamePrefix, int cepstralOrder)
        {
            List<SoundFeature> lpcSoundFeatureList = new List<SoundFeature>();
            for (int ii = 0; ii < lpcOrder; ii++)
            {
                SoundFeature soundFeature = new SoundFeature();
                soundFeature.Name = lpcNamePrefix + (ii+1).ToString(); // LPCs enumerated from 1.
                soundFeature.SetSize(frameList.Count);
                lpcSoundFeatureList.Add(soundFeature);
            }
            List<SoundFeature> cepstralSoundFeatureList = new List<SoundFeature>();
            for (int ii = 0; ii <= cepstralOrder; ii++)
            {
                SoundFeature soundFeature = new SoundFeature();
                soundFeature.Name = cepstralNamePrefix + ii.ToString();
                soundFeature.SetSize(frameList.Count);
                cepstralSoundFeatureList.Add(soundFeature);
            }
            List<double> lpcCoefficients = null;
            for (int iFrame = 0; iFrame < frameList.Count; iFrame++)
            {
                WAVSound frame = frameList[iFrame];
                lpcCoefficients = frame.ComputeLPCCoefficients(lpcOrder);
                for (int ii = 0; ii < lpcOrder; ii++)
                {
                    lpcSoundFeatureList[ii].ValueList[iFrame] = lpcCoefficients[ii];
                }
                List<double> cepstralCoefficients = frame.ComputeCepstralCoefficients(lpcCoefficients, cepstralOrder);
                for (int ii = 0; ii <= cepstralOrder; ii++)
                {
                    cepstralSoundFeatureList[ii].ValueList[iFrame] = cepstralCoefficients[ii];
                }
            }
            //
            // The first cepstral coefficient (c0) is equal to the (non-normalized) autocorrelation,
            // and can usually be removed in speech recognition (the autocorrelation is typically computed
            // elsewhere, as a separate feature).
            //
            // Moreover the second cepstral coefficient (c1) is identical to the first LPC coefficient,
            // and so can also be removed.
            //
            cepstralSoundFeatureList.RemoveAt(0); // Remove c0
            cepstralSoundFeatureList.RemoveAt(0); // Remove c1
            List<SoundFeature> soundFeatureList = new List<SoundFeature>();
            soundFeatureList.AddRange(lpcSoundFeatureList);
            soundFeatureList.AddRange(cepstralSoundFeatureList);
            return soundFeatureList;  
        }

        //
        public SoundFeature GetRelativeNumberOfZeroCrossingsSeries(string name)
        {
            SoundFeature rnzcFeature = new SoundFeature();
            rnzcFeature.Name = name;
            rnzcFeature.SetSize(frameList.Count);
            for (int iFrame = 0; iFrame < frameList.Count; iFrame++)
            {
                WAVSound frame = frameList[iFrame];
                double relativeNumberOfZeroCrossings = frame.GetRelativeNumberOfZeroCrossings(0);  // 0 = channel index (again assuming mono).
                rnzcFeature.ValueList[iFrame] = relativeNumberOfZeroCrossings;
            }
            return rnzcFeature;
        }

        public void ApplyHammingWindows(double alpha)
        {
            foreach (WAVSound frame in frameList)
            {
                frame.ApplyHammingWindow(alpha);
            }
        }

        public List<WAVSound> FrameList
        {
            get { return frameList; }
        }

        public List<double> StartTimeList
        {
            get { return startTimeList; }
        }
    }
}
