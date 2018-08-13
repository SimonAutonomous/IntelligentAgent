using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using AudioLibrary;

namespace SpeechSynthesisLibrary.TDPSOLA
{
    [DataContract]
    public class PitchMarkEstimator
    {
        #region Constants
        private const double DEFAULT_PEAK_SEARCH_TIME_RANGE = 0.0025;
        private const double DEFAULT_ADJUSTMENT_TIME_RANGE = 0.0025;
        private const double DEFAULT_RELATIVE_PEAK_THRESHOLD = 0.45;
        private const double DEFAULT_ENERGY_COMPUTATION_TIME_RANGE = 0.0020;
        private const double MINIMUM_UNVOICED_PITCHMARK_SPACING = 0.004;
        #endregion

        #region Fields
        private double peakSearchTimeRange = DEFAULT_PEAK_SEARCH_TIME_RANGE;
        private double adjustmentTimeRange = DEFAULT_ADJUSTMENT_TIME_RANGE;
        private double relativePeakThreshold = DEFAULT_RELATIVE_PEAK_THRESHOLD;
        private double energyComputationTimeRange = DEFAULT_ENERGY_COMPUTATION_TIME_RANGE;
        private List<double> pitchMarkTimeList;
        #endregion

        private int AdjustPitchMark(WAVSound sound, int nominalPitchMarkIndex, int adjustmentIndexRange) //, double relativePeakThreshold, double energyComputationTimeRange)
        {
            int absoluteMaximum = Math.Abs(sound.Samples[0][nominalPitchMarkIndex]);
            int threshold = (int)Math.Round(relativePeakThreshold * absoluteMaximum);
            List<int> indicesOfLocalMaximaAboveThreshold = sound.GetIndicesOfLocalExtremaAboveThreshold(nominalPitchMarkIndex, adjustmentIndexRange, adjustmentIndexRange, threshold);
            int adjustedPitchMarkIndex = nominalPitchMarkIndex; //  sound.GetSampleIndexAtTime(timeOfAbsoluteMaximum); // Fallback value.
            double minimumPreviousEnergy = double.MaxValue;
            int energyComputationIndexRange = (int)Math.Round(energyComputationTimeRange * sound.SampleRate);
            for (int ii = 0; ii < indicesOfLocalMaximaAboveThreshold.Count; ii++)
            {
                int indexOfLocalMaximumAboveThreshold = indicesOfLocalMaximaAboveThreshold[ii];
                int energyComputationEndIndex = indexOfLocalMaximumAboveThreshold - 1;
                if (energyComputationEndIndex >= sound.Samples[0].Count) { energyComputationEndIndex = sound.Samples[0].Count - 1; }
                int energyComputationStartIndex = indexOfLocalMaximumAboveThreshold - energyComputationIndexRange;
                if (energyComputationStartIndex < 0) { energyComputationStartIndex = 0; }
                double localEnergy = sound.GetLocalEnergy(energyComputationStartIndex, energyComputationEndIndex);
                if (localEnergy < minimumPreviousEnergy)
                {
                    minimumPreviousEnergy = localEnergy;
                    adjustedPitchMarkIndex = indexOfLocalMaximumAboveThreshold;
                }
            }
            return adjustedPitchMarkIndex;
        }

        public void FindPitchMarks(WAVSound sound, SpeechTypeSpecification speechTypeSpecification, PitchPeriodSpecification pitchPeriodSpecification) 
          //  , double peakSearchTimeRange,
          //  double adjustmentTimeRange, double relativePeakThreshold, double energyComputationTimeRange)
        {
            List<Tuple<int, int, SpeechType>> segmentTypeList = speechTypeSpecification.GetSegmentTypes();
            List<int> absoluteSampleList = sound.GetAbsoluteSamples(0);

            pitchMarkTimeList = new List<double>();
            for (int iSegment = 0; iSegment < segmentTypeList.Count; iSegment++)
            {
                SpeechType segmentType = segmentTypeList[iSegment].Item3;
                if (segmentType == SpeechType.Voiced)
                {
                    int startIndex = segmentTypeList[iSegment].Item1;
                    int endIndex = segmentTypeList[iSegment].Item2;
                    double startTime = speechTypeSpecification.TimeSpeechTypeTupleList[startIndex].Item1;
                    double endTime = speechTypeSpecification.TimeSpeechTypeTupleList[endIndex].Item1;
                    int startSearchIndex = sound.GetSampleIndexAtTime(startTime);
                    int endSearchIndex = sound.GetSampleIndexAtTime(endTime);
                    int peakIndexSearchRange = (int)Math.Round(peakSearchTimeRange * sound.SampleRate);
                    int adjustmentIndexRange = (int)Math.Round(adjustmentTimeRange * sound.SampleRate);
                    int indexOfAbsoluteMaximum = sound.GetIndexOfAbsoluteMaximum(startSearchIndex, endSearchIndex);

                    int adjustedMainPitchMarkIndex = AdjustPitchMark(sound, indexOfAbsoluteMaximum, adjustmentIndexRange); // , relativePeakThreshold, energyComputationTimeRange);
                    double adjustedMainPitchMarkTime = sound.GetTimeAtSampleIndex(adjustedMainPitchMarkIndex);
                    pitchMarkTimeList.Add(adjustedMainPitchMarkTime);
                    

                    Boolean inVoicedSegment = true;
                    double previousPitchMarkTime = adjustedMainPitchMarkTime;

                    // Next, move forward until the end of the voiced segment
                    while (inVoicedSegment)
                    {
                        double pitchPeriod = pitchPeriodSpecification.GetPitchPeriod(previousPitchMarkTime);
                        int deltaSample = (int)Math.Round(pitchPeriod * sound.SampleRate);
                        int previousSampleIndex = sound.GetSampleIndexAtTime(previousPitchMarkTime);
                        int pitchSampleIndex = previousSampleIndex + deltaSample;
                        if (pitchSampleIndex + 2*peakIndexSearchRange >= sound.Samples[0].Count) { break; }
                        int currentSampleIndex = sound.GetIndexOfAbsoluteMaximum(pitchSampleIndex - peakIndexSearchRange, pitchSampleIndex + peakIndexSearchRange);
                        double currentTime = sound.GetTimeAtSampleIndex(currentSampleIndex);
                        int adjustedSampleIndex = AdjustPitchMark(sound, currentSampleIndex, adjustmentIndexRange); //, relativePeakThreshold, energyComputationTimeRange);
                        if (adjustedSampleIndex <= previousSampleIndex)
                        {
                            adjustedSampleIndex = currentSampleIndex; // Emergency fallback in cases where the search gets stuck (can happen if the pitch period is too small relative to the search range)
                        }
                        double adjustedTime = sound.GetTimeAtSampleIndex(adjustedSampleIndex);
                        // Make an incursion into the non-voiced segment
                        pitchMarkTimeList.Add(adjustedTime);
                        previousPitchMarkTime = adjustedTime;
                        if (speechTypeSpecification.GetSpeechType(currentTime) != SpeechType.Voiced)
                        {
                            inVoicedSegment = false;
                        }
                        /*       if (speechTypeSpecification.GetSpeechType(currentTime) == SpeechType.Voiced)
                               {
                                   pitchMarkTimeList.Add(adjustedTime);
                                   previousPitchMarkTime = adjustedTime;
                               }
                               else { inVoicedSegment = false; }  */
                    }
                    double voicedEndTime = pitchMarkTimeList.Last();
                    // Then continue half-way through any non-voiced segment followed by another voiced segment,
                    // or until the end of the sound if no voiced segment follows:
                    if (iSegment < segmentTypeList.Count)
                    {
                        double subsequenceVoicedSegmentStartTime = 0;
                        Boolean hasSubsequentVoicedSegment = false;
                        if (iSegment + 1 < segmentTypeList.Count)
                        {
                            for (int kk = iSegment + 1; kk < segmentTypeList.Count; kk++)
                            {
                                if (segmentTypeList[kk].Item3 == SpeechType.Voiced)
                                {
                                    hasSubsequentVoicedSegment = true;
                                    int startSegmentIndex = segmentTypeList[kk].Item1;
                                    subsequenceVoicedSegmentStartTime = speechTypeSpecification.TimeSpeechTypeTupleList[startSegmentIndex].Item1;
                                    break;
                                }
                            }
                        }
                        if (!hasSubsequentVoicedSegment)
                        {
                            // No following voiced segment: Just continue to the end
                            Boolean endReached = false;
                            while (!endReached)
                            {
                                double pitchPeriod = pitchPeriodSpecification.GetPitchPeriod(previousPitchMarkTime);
                                int deltaSample = (int)Math.Round(pitchPeriod * sound.SampleRate);
                                int previousSampleIndex = sound.GetSampleIndexAtTime(previousPitchMarkTime);
                                int pitchSampleIndex = previousSampleIndex + deltaSample;
                                if (pitchSampleIndex + 2 * peakIndexSearchRange >= sound.Samples[0].Count)
                                {
                                    endReached = true;
                                    break;
                                }
                                int currentSampleIndex = sound.GetIndexOfAbsoluteMaximum(pitchSampleIndex - peakIndexSearchRange, pitchSampleIndex + peakIndexSearchRange);
                                double currentTime = sound.GetTimeAtSampleIndex(currentSampleIndex);
                                int adjustedSampleIndex = AdjustPitchMark(sound, currentSampleIndex, adjustmentIndexRange); //, relativePeakThreshold, energyComputationTimeRange);
                                if (adjustedSampleIndex <= previousSampleIndex)
                                {
                                    adjustedSampleIndex = currentSampleIndex; // Emergency fallback in cases where the search gets stuck (can happen if the pitch period is too small relative to the search range)
                                }
                                double adjustedTime = sound.GetTimeAtSampleIndex(adjustedSampleIndex);
                                pitchMarkTimeList.Add(adjustedTime);
                                previousPitchMarkTime = adjustedTime;
                            }
                        }
                        else  // Proceed to the half-way mark of the interval from the end of the current voice segment to the beginning of the next.
                        {
                            double stopTime = voicedEndTime + (subsequenceVoicedSegmentStartTime - voicedEndTime) / 2;
                            int stopTimeIndex = sound.GetSampleIndexAtTime(stopTime);
                            Boolean endReached = false;
                            while (!endReached)
                            {
                                double pitchPeriod = pitchPeriodSpecification.GetPitchPeriod(previousPitchMarkTime);
                                int deltaSample = (int)Math.Round(pitchPeriod * sound.SampleRate);
                                int previousSampleIndex = sound.GetSampleIndexAtTime(previousPitchMarkTime);
                                int pitchSampleIndex = previousSampleIndex + deltaSample;
                                if (pitchSampleIndex + 2 * peakIndexSearchRange >= stopTimeIndex)
                                {
                                    endReached = true;
                                    break;
                                }
                                int currentSampleIndex = sound.GetIndexOfAbsoluteMaximum(pitchSampleIndex - peakIndexSearchRange, pitchSampleIndex + peakIndexSearchRange);
                                double currentTime = sound.GetTimeAtSampleIndex(currentSampleIndex);
                                int adjustedSampleIndex = AdjustPitchMark(sound, currentSampleIndex, adjustmentIndexRange); // , relativePeakThreshold, energyComputationTimeRange);
                                if (adjustedSampleIndex <= previousSampleIndex)
                                {
                                    adjustedSampleIndex = currentSampleIndex; // Emergency fallback in cases where the search gets stuck (can happen if the pitch period is too small relative to the search range)
                                }
                                double adjustedTime = sound.GetTimeAtSampleIndex(adjustedSampleIndex);
                                pitchMarkTimeList.Add(adjustedTime);
                                previousPitchMarkTime = adjustedTime;
                            }
                        }
                    }

                    // Then move backward until the beginning of the voiced segment
                    inVoicedSegment = true;
                    previousPitchMarkTime = adjustedMainPitchMarkTime;
                    double voicedStartTime = 0;
                    while (inVoicedSegment)
                    {
                        double pitch = pitchPeriodSpecification.GetPitchPeriod(previousPitchMarkTime);
                        int deltaSample = -(int)Math.Round(pitch * sound.SampleRate);
                        int previousSampleIndex = sound.GetSampleIndexAtTime(previousPitchMarkTime);
                        int pitchSampleIndex = previousSampleIndex + deltaSample;
                        if (pitchSampleIndex - 2 * peakIndexSearchRange < 0) { break; }
                        int currentSampleIndex = sound.GetIndexOfAbsoluteMaximum(pitchSampleIndex - peakIndexSearchRange, pitchSampleIndex + peakIndexSearchRange);
                        double currentTime = sound.GetTimeAtSampleIndex(currentSampleIndex);
                        int adjustedSampleIndex = AdjustPitchMark(sound, currentSampleIndex, peakIndexSearchRange); // , relativePeakThreshold, energyComputationTimeRange);
                        if (adjustedSampleIndex <= previousSampleIndex)
                        {
                            adjustedSampleIndex = currentSampleIndex; // Emergency fallback in cases where the search gets stuck (can happen if the pitch period is too small relative to the search range)
                        }
                        double adjustedTime = sound.GetTimeAtSampleIndex(adjustedSampleIndex);
                        // Make an incursion into the non-voiced segment
                        pitchMarkTimeList.Add(adjustedTime);
                        previousPitchMarkTime = adjustedTime;
                        if (speechTypeSpecification.GetSpeechType(currentTime) != SpeechType.Voiced)
                        {
                            inVoicedSegment = false;
                            voicedStartTime = adjustedTime;
                        }
                    }

                    // Then continue half-way through any non-voiced segment preceded by another voiced segment,
                    // or until the beginning of the sound if no voiced segment follows:
                    if (iSegment > 0)
                    {
                        double priorVoicedSegmentEndTime = 0;
                        Boolean hasPriorVoicedSegment = false;
                        if (iSegment - 1 > 0)
                        {
                            for (int kk = iSegment - 1; kk >= 0; kk--)
                            {
                                if (segmentTypeList[kk].Item3 == SpeechType.Voiced)
                                {
                                    hasPriorVoicedSegment = true;
                                    int endSegmentIndex = segmentTypeList[kk].Item2;
                                    priorVoicedSegmentEndTime = speechTypeSpecification.TimeSpeechTypeTupleList[endSegmentIndex].Item1;
                                    break;
                                }
                            }
                        }
                        if (!hasPriorVoicedSegment)
                        {
                            // No following voiced segment: Just continue to the end
                            Boolean endReached = false;
                            while (!endReached)
                            {
                                double pitchPeriod = pitchPeriodSpecification.GetPitchPeriod(previousPitchMarkTime);
                                int deltaSample = -(int)Math.Round(pitchPeriod * sound.SampleRate);
                                int previousSampleIndex = sound.GetSampleIndexAtTime(previousPitchMarkTime);
                                int pitchSampleIndex = previousSampleIndex + deltaSample;
                                if (pitchSampleIndex - 2 * peakIndexSearchRange < 0)
                                {
                                    endReached = true;
                                    break;
                                }
                                int currentSampleIndex = sound.GetIndexOfAbsoluteMaximum(pitchSampleIndex - peakIndexSearchRange, pitchSampleIndex + peakIndexSearchRange);
                                double currentTime = sound.GetTimeAtSampleIndex(currentSampleIndex);
                                int adjustedSampleIndex = AdjustPitchMark(sound, currentSampleIndex, adjustmentIndexRange); // , relativePeakThreshold, energyComputationTimeRange);
                                if (adjustedSampleIndex <= previousSampleIndex)
                                {
                                    adjustedSampleIndex = currentSampleIndex; // Emergency fallback in cases where the search gets stuck (can happen if the pitch period is too small relative to the search range)
                                }
                                double adjustedTime = sound.GetTimeAtSampleIndex(adjustedSampleIndex);
                                pitchMarkTimeList.Add(adjustedTime);
                                previousPitchMarkTime = adjustedTime;
                            }
                        }
                        else  // Proceed to the half-way mark of the interval from the end of the current voice segment to the beginning of the next.
                        {
                            double stopTime = voicedStartTime - (voicedStartTime - priorVoicedSegmentEndTime) / 2;
                            int stopTimeIndex = sound.GetSampleIndexAtTime(stopTime);
                            Boolean endReached = false;
                            while (!endReached)
                            {
                                double pitchPeriod = pitchPeriodSpecification.GetPitchPeriod(previousPitchMarkTime);
                                int deltaSample = -(int)Math.Round(pitchPeriod * sound.SampleRate);
                                int previousSampleIndex = sound.GetSampleIndexAtTime(previousPitchMarkTime);
                                int pitchSampleIndex = previousSampleIndex + deltaSample;
                                if (pitchSampleIndex - 2 * peakIndexSearchRange <= stopTimeIndex)
                                {
                                    endReached = true;
                                    break;
                                }
                                int currentSampleIndex = sound.GetIndexOfAbsoluteMaximum(pitchSampleIndex - peakIndexSearchRange, pitchSampleIndex + peakIndexSearchRange);
                                double currentTime = sound.GetTimeAtSampleIndex(currentSampleIndex);
                                int adjustedSampleIndex = AdjustPitchMark(sound, currentSampleIndex, adjustmentIndexRange); // , relativePeakThreshold, energyComputationTimeRange);
                                if (adjustedSampleIndex <= previousSampleIndex)
                                {
                                    adjustedSampleIndex = currentSampleIndex; // Emergency fallback in cases where the search gets stuck (can happen if the pitch period is too small relative to the search range)
                                }
                                double adjustedTime = sound.GetTimeAtSampleIndex(adjustedSampleIndex);
                                pitchMarkTimeList.Add(adjustedTime);
                                previousPitchMarkTime = adjustedTime;
                            }
                        }
                    }


                }
            }
            pitchMarkTimeList.Sort();

            // Finally, remove any pitch marks that are too close (should only happen in non-voiced segments)
            double minimumPitchPeriod = pitchPeriodSpecification.GetMinimumPitchPeriod();
            int index = 1;
            while (index < pitchMarkTimeList.Count)
            {
                double previousTime = pitchMarkTimeList[index - 1];
                double currentTime = pitchMarkTimeList[index];
                SpeechType previousSpeechType = speechTypeSpecification.GetSpeechType(previousTime);
                SpeechType currentSpeechType = speechTypeSpecification.GetSpeechType(currentTime);
                if ((previousSpeechType != SpeechType.Voiced) && (currentSpeechType != SpeechType.Voiced))
                {
                    double deltaTime = currentTime - previousTime;
                    if (deltaTime < MINIMUM_UNVOICED_PITCHMARK_SPACING)
                    {
                        pitchMarkTimeList.RemoveAt(index);
                    }
                    else
                    {
                        index++;
                    }
                }
                else { index++; }
            }   
        }

        public List<double> PitchMarkTimeList
        {
            get { return pitchMarkTimeList; }
        }

        [DataMember]
        public double PeakSearchTimeRange
        {
            get { return peakSearchTimeRange; }
            set { peakSearchTimeRange = value; }
        }

        [DataMember]
        public double AdjustmentTimeRange
        {
            get { return adjustmentTimeRange; }
            set { adjustmentTimeRange = value; }
        }

        [DataMember]
        public double RelativePeakThreshold
        {
            get { return relativePeakThreshold; }
            set { relativePeakThreshold = value; }
        }

        [DataMember]
        public double EnergyComputationTimeRange
        {
            get { return energyComputationTimeRange; }
            set { energyComputationTimeRange = value; }
        }
    }
}
