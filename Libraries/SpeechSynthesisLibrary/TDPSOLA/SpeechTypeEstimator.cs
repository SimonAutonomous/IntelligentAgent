using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using AudioLibrary;

namespace SpeechSynthesisLibrary.TDPSOLA
{
    [DataContract]
    public class SpeechTypeEstimator
    {
        #region Constants
        private const int DEFAULT_CHANNEL = 0;
        private const double DEFAULT_FRAME_DURATION = 0.020;
        private const double DEFAULT_FRAME_SHIFT = 0.010;
        private const double DEFAULT_LOW_PASS_CUTOFF_FREQUENCY = 550;
        private const double DEFAULT_LOW_PASS_RATIO_THRESHOLD = 0.02;
        private const double DEFAULT_ENERGY_THRESHOLD = 250000;
        private const double DEFAULT_SILENCE_THRESHOLD = 2500;
        private const double DEFAULT_ZERO_CROSSING_RATE_THRESHOLD = 0.30;
        private const int DEFAULT_NUMBER_OF_ADJUSTMENT_STEPS = 2;
        private const int DEFAULT_ADJUSTMENT_MINIMUM_INDEX_DURATION = 3;
        #endregion

        #region Fields
        private int channel = DEFAULT_CHANNEL;
        private double frameDuration = DEFAULT_FRAME_DURATION;
        private double frameShift = DEFAULT_FRAME_SHIFT;
        private double lowPassCutoffFrequency = DEFAULT_LOW_PASS_CUTOFF_FREQUENCY;
        private double lowPassRatioThreshold = DEFAULT_LOW_PASS_RATIO_THRESHOLD;
        private double energyThreshold = DEFAULT_ENERGY_THRESHOLD;
        private double silenceThreshold = DEFAULT_SILENCE_THRESHOLD;
        private double zeroCrossingRateThreshold = DEFAULT_ZERO_CROSSING_RATE_THRESHOLD;
        private int numberOfAdjustmentSteps = DEFAULT_NUMBER_OF_ADJUSTMENT_STEPS;
        private int adjustmentMinimumIndexDuration = DEFAULT_ADJUSTMENT_MINIMUM_INDEX_DURATION;

        private SpeechTypeSpecification speechTypeSpecification;

        #endregion

        public virtual SpeechType GetFrameSpeechType(WAVSound frame) 
            // , int channel, double lowPassCutoffFrequency, double lowPassRatioThreshold,
            //                                 double energyThreshold, double silenceThreshold)
        {
            double zeroCrossingRate = frame.GetRelativeNumberOfZeroCrossings(channel);
            double averageEnergy = frame.GetAverageEnergy(channel);
            if (averageEnergy < silenceThreshold) { return SpeechType.Silence; }
            else
            {
                WAVSound lowPassFilteredFrame = frame.Copy();
                lowPassFilteredFrame.LowPassFilter(lowPassCutoffFrequency);
                double lowPassFilteredAverageEnergy = lowPassFilteredFrame.GetAverageEnergy(channel);
                double energyRatio = lowPassFilteredAverageEnergy / averageEnergy;
                if ((energyRatio > lowPassRatioThreshold) && (averageEnergy > energyThreshold) && (zeroCrossingRate < zeroCrossingRateThreshold))
                {
                    return SpeechType.Voiced;
                }
                else if (zeroCrossingRate < zeroCrossingRateThreshold)
                {
                    return SpeechType.Voiced;
                }
                else
                {
                    return SpeechType.Unvoiced;
                }
            }
        }

        public void FindSpeechTypeVariation(WAVSound sound)
            // , int channel, double frameDuration, double frameShift,
            //                      double lowPassCutoffFrequency, double lowPassRatioThreshold, double energyThreshold, double silenceThreshold)
        {
            WAVFrameSet frameSet = new WAVFrameSet(sound, frameDuration, frameShift);
            speechTypeSpecification = new SpeechTypeSpecification();
            double time = 0;
            for (int ii = 0; ii < frameSet.FrameList.Count; ii++)
            {
                WAVSound frame = frameSet.FrameList[ii];
                SpeechType speechType = this.GetFrameSpeechType(frame); //
               // SpeechType speechType = this.GetFrameSpeechType(frame, channel, lowPassCutoffFrequency, lowPassRatioThreshold, energyThreshold, silenceThreshold);
                time = frameSet.StartTimeList[ii] + frameDuration / 2; // The speech type is assigned to the center of the frame
                speechTypeSpecification.TimeSpeechTypeTupleList.Add(new Tuple<double, SpeechType>(time, speechType));
            }
            // Finally, to make sure that the speech type can be interpolated over the entire sound, set the
            // end values:
            SpeechType firstSpeechType = speechTypeSpecification.TimeSpeechTypeTupleList[0].Item2;
            speechTypeSpecification.TimeSpeechTypeTupleList.Insert(0, new Tuple<double, SpeechType>(0, firstSpeechType));
            SpeechType lastSpeechType = speechTypeSpecification.TimeSpeechTypeTupleList.Last().Item2;
            double duration = sound.GetDuration();
            if (speechTypeSpecification.TimeSpeechTypeTupleList.Last().Item1 < duration) // Will ALMOST always be the case, unless the duration is an exact multiple of the frame shift
            {
                speechTypeSpecification.TimeSpeechTypeTupleList.Add(new Tuple<double, SpeechType>(duration, lastSpeechType));
            }
            for (int jj = 0; jj < numberOfAdjustmentSteps; jj++)
            {
                Adjust();
            }
        }

        private void Adjust() // int adjustmentMinimumIndexDuration)
        {
            if (speechTypeSpecification.TimeSpeechTypeTupleList.Count == 0) { return; }

            // Divide the speech types into segments

            int index = 0;
            List<SpeechTypeSpecification> segmentSpecificationList = new List<SpeechTypeSpecification>();
            SpeechTypeSpecification currentSegmentSpecification = new SpeechTypeSpecification();
            SpeechType currentSpeechType = speechTypeSpecification.TimeSpeechTypeTupleList[index].Item2;
            double currentTime = speechTypeSpecification.TimeSpeechTypeTupleList[index].Item1;
            currentSegmentSpecification.TimeSpeechTypeTupleList.Add(new Tuple<double, SpeechType>(currentTime, currentSpeechType));
            while (index < speechTypeSpecification.TimeSpeechTypeTupleList.Count)
            {
                index++;
                if (index >= speechTypeSpecification.TimeSpeechTypeTupleList.Count) { break; }
                SpeechType speechType = speechTypeSpecification.TimeSpeechTypeTupleList[index].Item2;
                double time = speechTypeSpecification.TimeSpeechTypeTupleList[index].Item1;
                if (speechType == currentSpeechType)
                {
                    currentSegmentSpecification.TimeSpeechTypeTupleList.Add(new Tuple<double, SpeechType>(time, speechType));
                }
                else
                {
                    segmentSpecificationList.Add(currentSegmentSpecification);
                    currentSegmentSpecification = new SpeechTypeSpecification();
                    currentSpeechType = speechTypeSpecification.TimeSpeechTypeTupleList[index].Item2;
                    currentTime = speechTypeSpecification.TimeSpeechTypeTupleList[index].Item1;
                    currentSegmentSpecification.TimeSpeechTypeTupleList.Add(new Tuple<double, SpeechType>(currentTime, currentSpeechType));
                }
            }

            // Adjustment:

            if (currentSegmentSpecification.TimeSpeechTypeTupleList.Count > 0)  // Make sure to add the last segment
            {
                segmentSpecificationList.Add(currentSegmentSpecification);
            }
            if (segmentSpecificationList.Count == 2)
            {
                SpeechTypeSpecification firstSegmentSpecification = segmentSpecificationList[0];
                SpeechTypeSpecification secondSegmentSpecification = segmentSpecificationList[1];
                if ((firstSegmentSpecification.TimeSpeechTypeTupleList.Count < adjustmentMinimumIndexDuration) && 
                    (secondSegmentSpecification.TimeSpeechTypeTupleList.Count >= adjustmentMinimumIndexDuration))
                {
                    SpeechType speechType = secondSegmentSpecification.TimeSpeechTypeTupleList[0].Item2;
                    firstSegmentSpecification.SetUniformSpeechType(speechType);
                } 
                else if ((firstSegmentSpecification.TimeSpeechTypeTupleList.Count >= adjustmentMinimumIndexDuration) &&
                    (secondSegmentSpecification.TimeSpeechTypeTupleList.Count < adjustmentMinimumIndexDuration))
                {
                    SpeechType speechType = firstSegmentSpecification.TimeSpeechTypeTupleList[0].Item2;
                    secondSegmentSpecification.SetUniformSpeechType(speechType);
                }
            }
            else if (segmentSpecificationList.Count > 2)
            {
                // Now remove any segments shorter than the required minimum length
                SpeechTypeSpecification firstSegmentSpecification = segmentSpecificationList[0];
                SpeechTypeSpecification secondSegmentSpecification = segmentSpecificationList[1];
                if ((firstSegmentSpecification.TimeSpeechTypeTupleList.Count < adjustmentMinimumIndexDuration) &&
                    (secondSegmentSpecification.TimeSpeechTypeTupleList.Count >= adjustmentMinimumIndexDuration))
                {
                    SpeechType speechType = secondSegmentSpecification.TimeSpeechTypeTupleList[0].Item2;
                    firstSegmentSpecification.SetUniformSpeechType(speechType);
                }
                
                for (int ii = 1; ii < segmentSpecificationList.Count-1; ii++)
                {
                    SpeechTypeSpecification segmentSpecification = segmentSpecificationList[ii];
                    if (segmentSpecification.TimeSpeechTypeTupleList.Count < adjustmentMinimumIndexDuration)
                    {
                        SpeechTypeSpecification previousSegmentSpecification = segmentSpecificationList[ii - 1];
                        SpeechTypeSpecification nextSegmentSpecification = segmentSpecificationList[ii + 1];
                        if (previousSegmentSpecification.TimeSpeechTypeTupleList.Count > nextSegmentSpecification.TimeSpeechTypeTupleList.Count)
                        {
                            SpeechType previousSpeechType = previousSegmentSpecification.TimeSpeechTypeTupleList[0].Item2;
                            segmentSpecification.SetUniformSpeechType(previousSpeechType);
                        }
                        else
                        {
                            SpeechType nextSpeechType = nextSegmentSpecification.TimeSpeechTypeTupleList[0].Item2;
                            segmentSpecification.SetUniformSpeechType(nextSpeechType);
                        }
                    }
                }

                SpeechTypeSpecification lastSegmentSpecification = segmentSpecificationList.Last();
                SpeechTypeSpecification penultimateSegmentSpecification = segmentSpecificationList[segmentSpecificationList.Count - 2];
                if ((lastSegmentSpecification.TimeSpeechTypeTupleList.Count < adjustmentMinimumIndexDuration) &&
                    (penultimateSegmentSpecification.TimeSpeechTypeTupleList.Count >= adjustmentMinimumIndexDuration))
                {
                    SpeechType speechType = penultimateSegmentSpecification.TimeSpeechTypeTupleList[0].Item2;
                    lastSegmentSpecification.SetUniformSpeechType(speechType);
                }
            }

            // Finally, assign the modified speech types to the overall speech type specification:
            index = 0;
            foreach (SpeechTypeSpecification segmentSpecification in segmentSpecificationList)
            {
                for (int jj = 0; jj < segmentSpecification.TimeSpeechTypeTupleList.Count; jj++)
                {
                    SpeechType speechType = segmentSpecification.TimeSpeechTypeTupleList[jj].Item2;
                    speechTypeSpecification.SetSpeechType(index, speechType);
                    index++;
                }
            }
        }

        public SpeechTypeSpecification SpeechTypeSpecification
        {
            get { return speechTypeSpecification; }
        }

        [DataMember]
        public double FrameDuration
        {
            get { return frameDuration; }
            set { frameDuration = value; }
        }

        [DataMember]
        public double FrameShift
        {
            get { return frameShift; }
            set { frameShift = value; }
        }

        [DataMember]
        public double LowPassCutoffFrequency
        {
            get { return lowPassCutoffFrequency; }
            set { lowPassCutoffFrequency = value; }
        }

        [DataMember]
        public double LowPassRatioThreshold
        {
            get { return lowPassRatioThreshold; }
            set { lowPassRatioThreshold = value; }
        }

        [DataMember]
        public double EnergyThreshold
        {
            get { return energyThreshold; }
            set { energyThreshold = value; }
        }

        [DataMember]
        public double SilenceThreshold
        {
            get { return silenceThreshold; }
            set { silenceThreshold = value; }
        }

        [DataMember]
        public double ZeroCrossingRateThreshold
        {
            get { return zeroCrossingRateThreshold; }
            set { zeroCrossingRateThreshold = value; }
        }

        [DataMember]
        public int NumberOfAdjustmentSteps
        {
            get { return numberOfAdjustmentSteps; }
            set { numberOfAdjustmentSteps = value; }
        }

        [DataMember]
        public int AdjustmentMinimumIndexDuration
        {
            get { return adjustmentMinimumIndexDuration; }
            set { adjustmentMinimumIndexDuration = value; }
        }
    }
}
