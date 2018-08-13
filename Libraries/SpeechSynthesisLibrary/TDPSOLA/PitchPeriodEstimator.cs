using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using AudioLibrary;
using MathematicsLibrary.Interpolation;
using MathematicsLibrary.SignalProcessing;

namespace SpeechSynthesisLibrary.TDPSOLA
{
    [DataContract]
    public class PitchPeriodEstimator
    {
        #region Constants
        private const double DEFAULT_MINIMUM_PITCH_PERIOD = 0.0040;
        private const double DEFAULT_MAXIMUM_PITCH_PERIOD = 0.0120;
        private const double DEFAULT_FRAME_SHIFT = 0.0100;
        private const double DEFAULT_DELTA_TIME = 0.0050; // Used when interpolating the pitch periods
        private const Boolean DEFAULT_SET_UNVOICED_PITCH = true;
        #endregion

        #region Fields
        private double minimumPitchPeriod = DEFAULT_MINIMUM_PITCH_PERIOD;
        private double maximumPitchPeriod = DEFAULT_MAXIMUM_PITCH_PERIOD;
        private double frameShift = DEFAULT_FRAME_SHIFT;
        private double deltaTime = DEFAULT_DELTA_TIME;
        private Boolean setUnvoicedPitch = DEFAULT_SET_UNVOICED_PITCH;
        private PitchPeriodSpecification pitchPeriodSpecification;

        #endregion

        // 20170803
        #region Comments
        // This method uses Steps 2-4 of the YIN method (Cheveigne and Kawahara, 2002).
        // (Step 1 is only used (in the paper) for comparison, and Steps 5-6 only provide
        // minor absolute improvements.
        #endregion
    /*    public double GetPitchPeriod(WAVSound sound, double time, double maximumPitchPeriod, double threshold)
        {
            int sampleIndex = sound.GetSampleIndexAtTime(time);
            int maximumIndexDuration = (int)Math.Round(maximumPitchPeriod * sound.SampleRate);
            double minimum = double.MaxValue;
            double pitchPeriod = 0;
            List<double> periodList = new List<double>();
            List<double> shiftedSquareDifferenceList = new List<double>();
            List<double> normalizedShiftedSquaredDifferenceList = new List<double>();
            periodList.Add(0);
            shiftedSquareDifferenceList.Add(0);
            normalizedShiftedSquaredDifferenceList.Add(1);

            for (int ii = 1; ii <= maximumIndexDuration; ii++)
            {
                int indexDuration = ii;
                double shiftedSquareDifference = sound.GetShiftedSquareDifference(sampleIndex, ii, maximumIndexDuration);
                shiftedSquareDifferenceList.Add(shiftedSquareDifference);
                double period = indexDuration / (double)sound.SampleRate;
                periodList.Add(period);
                double average = shiftedSquareDifferenceList.Average();
                double normalizedShiftedSquareDifference = shiftedSquareDifference / average;
                if (normalizedShiftedSquareDifference < minimum)
                {
                    minimum = normalizedShiftedSquareDifference;
                    pitchPeriod = period;
                }
                normalizedShiftedSquaredDifferenceList.Add(normalizedShiftedSquareDifference);
            }
            int minimumIndex = FindFirstMinimum(normalizedShiftedSquaredDifferenceList, threshold);
            if (minimumIndex > 0)  // Otherwise use the global minimum, computed above.
            {
                pitchPeriod = minimumIndex / (double)sound.SampleRate;
            }
            return pitchPeriod;
        }  */

        public double ComputeFramePitchPeriod(WAVSound sound, double time)
            // , double minimumPitchPeriod, double maximumPitchPeriod)
        {
            int sampleIndex = sound.GetSampleIndexAtTime(time);
            int minimumIndexDuration = (int)Math.Round(minimumPitchPeriod * sound.SampleRate);
            int maximumIndexDuration = (int)Math.Round(maximumPitchPeriod * sound.SampleRate);
            double minimumAverageMagnitudeDifference = double.MaxValue;
            int indexDurationAtMinimum = 0;
            for (int ii = minimumIndexDuration; ii <= maximumIndexDuration; ii++)
            {
                double averageMagnitudeDifference = sound.GetAbsoluteMagnitudeDifference(sampleIndex, ii, maximumIndexDuration);
                if (averageMagnitudeDifference < minimumAverageMagnitudeDifference)
                {
                    minimumAverageMagnitudeDifference = averageMagnitudeDifference;
                    indexDurationAtMinimum = ii;
                }
            }
            double pitchPeriod = indexDurationAtMinimum / (double)sound.SampleRate;
            return pitchPeriod;
        }

        // First identifies the first index at which the data point is below the
        // threshold. Then finds the subsequent minimum.
   /*     public int FindFirstMinimum(List<double> dataList, double threshold)
        {
            int startIndex = dataList.FindIndex(d => d < threshold);
            if (startIndex < 0) { return -1; }
            else
            {
                double currentValue = dataList[startIndex];
                double minimum = dataList[startIndex];
                int minimumIndex = startIndex;
                int ii = minimumIndex + 1;
                while ((currentValue < threshold) && (ii < dataList.Count))
                {
                    currentValue = dataList[ii];
                    if (currentValue < minimum)
                    {
                        minimum = currentValue;
                        minimumIndex = ii;
                    }
                    ii++;
                }
                return minimumIndex;
            }
        }  */


        public void ComputePitchPeriods(WAVSound sound, double startTime, double endTime)
            // , double minimumPitchPeriod, double maximumPitchPeriod, 
             //                           double frameShift)  
        {
            pitchPeriodSpecification = new PitchPeriodSpecification();
            double time = startTime;
            double actualEndTime = endTime;
            double duration = sound.GetDuration();
            // At least to maximim pitch periods are required for the analysis
            if (actualEndTime > (duration - 2*maximumPitchPeriod))
            {
                actualEndTime = duration - 2 * maximumPitchPeriod;  
            }
            while (time <= actualEndTime)
            {
                double pitchPeriod = ComputeFramePitchPeriod(sound, time); // , minimumPitchPeriod, maximumPitchPeriod); //, threshold);
                Tuple<double, double> timePitchPeriodTuple = new Tuple<double, double>(time, pitchPeriod);
                pitchPeriodSpecification.TimePitchPeriodTupleList.Add(timePitchPeriodTuple);
                time += frameShift;
            }
        }

        public void AdjustAndInterpolate(SpeechTypeSpecification speechTypeSpecification) // , double deltaTime, Boolean setUnvoicedPitch)
        {
            // Carry out median filtering to remove single errors
            List<double> correctedPitchValues = new List<double>();
            correctedPitchValues.Add(pitchPeriodSpecification.TimePitchPeriodTupleList[0].Item2);
            for (int ii = 1; ii < pitchPeriodSpecification.TimePitchPeriodTupleList.Count - 1; ii++)
            {
                List<double> rawPitchValues = new List<double>()
                { pitchPeriodSpecification.TimePitchPeriodTupleList[ii-1].Item2,
                  pitchPeriodSpecification.TimePitchPeriodTupleList[ii].Item2,
                  pitchPeriodSpecification.TimePitchPeriodTupleList[ii+1].Item2 };
                rawPitchValues.Sort();
                correctedPitchValues.Add(rawPitchValues[1]); // Median
            }
            // Finally adjust the end points (which are not touched by the initial median filtering)
            if (pitchPeriodSpecification.TimePitchPeriodTupleList.Count > 2)
            {
                List<double> rawPitchValues = new List<double>()
                { pitchPeriodSpecification.TimePitchPeriodTupleList[0].Item2,
                  pitchPeriodSpecification.TimePitchPeriodTupleList[1].Item2,
                  pitchPeriodSpecification.TimePitchPeriodTupleList[2].Item2 };
                rawPitchValues.Sort();
                correctedPitchValues[0] = rawPitchValues[1];
                int lastIndex = pitchPeriodSpecification.TimePitchPeriodTupleList.Count-1;
                rawPitchValues = new List<double>()
                { pitchPeriodSpecification.TimePitchPeriodTupleList[lastIndex].Item2,
                  pitchPeriodSpecification.TimePitchPeriodTupleList[lastIndex-1].Item2,
                  pitchPeriodSpecification.TimePitchPeriodTupleList[lastIndex-2].Item2 };
                rawPitchValues.Sort();
                correctedPitchValues.Add(rawPitchValues[1]);
            }  
            for (int ii = 0; ii < pitchPeriodSpecification.TimePitchPeriodTupleList.Count - 1; ii++)
            {
                pitchPeriodSpecification.TimePitchPeriodTupleList[ii] =
                    new Tuple<double, double>(pitchPeriodSpecification.TimePitchPeriodTupleList[ii].Item1,
                    correctedPitchValues[ii]);
            }
            // Extend (extrapolate) the pitch period specification so that it runs to the end of the sound:
            double lastTime = speechTypeSpecification.TimeSpeechTypeTupleList.Last().Item1;
            int lastPitchIndex = pitchPeriodSpecification.TimePitchPeriodTupleList.Count - 1;
            double lastPitchTime = pitchPeriodSpecification.TimePitchPeriodTupleList[lastPitchIndex].Item1;
            if (lastTime > lastPitchTime) // Should always be the case, but just to be sure ...
            {
                double lastPitch = pitchPeriodSpecification.TimePitchPeriodTupleList[lastPitchIndex].Item2;
                pitchPeriodSpecification.TimePitchPeriodTupleList.Add(new Tuple<double, double>(lastTime, lastPitch));
            }

            // Next, resample (upsample) the pitch period specification
            List<double> timeList = new List<double>();
            List<double> pitchList = new List<double>();
            for (int ii = 0; ii < pitchPeriodSpecification.TimePitchPeriodTupleList.Count; ii++)
            {
                double time = pitchPeriodSpecification.TimePitchPeriodTupleList[ii].Item1;
                double pitch = pitchPeriodSpecification.TimePitchPeriodTupleList[ii].Item2;
                timeList.Add(time);
                pitchList.Add(pitch);
            }
            List<List<double>> timePitchList = new List<List<double>>() { timeList, pitchList };
            int numberOfPoints = (int)Math.Round(lastTime / deltaTime);
            List<List<double>> interpolatedTimePitchList = LinearInterpolation.Interpolate(timePitchList, numberOfPoints);

            pitchPeriodSpecification = new PitchPeriodSpecification();
            for (int ii = 0; ii < interpolatedTimePitchList[0].Count; ii++)
            {
                double time = interpolatedTimePitchList[0][ii];
                double pitch = interpolatedTimePitchList[1][ii];
                pitchPeriodSpecification.TimePitchPeriodTupleList.Add(new Tuple<double, double>(time, pitch));
            }

            // Optionally (usually true) hard-set the (anyway rather arbitrary) pitch period for
            // unvoiced parts of the sound, by extending the pitch period from surrounding
            // voiced parts. This might cause occasional jumps (in the middle of an unvoiced
            // section), but those jumps are reoved in the subsequent lowpass filtering

            if (setUnvoicedPitch)
            {
                double previousTime = pitchPeriodSpecification.TimePitchPeriodTupleList[0].Item1;
                SpeechType previousSpeechType = speechTypeSpecification.GetSpeechType(previousTime);
                int firstChangeIndex = 0; // Will be changed later - must initialize here.
                int lastChangeIndex = -1;
                double previousPitch; // Must define here for use after the loop as well.
                for (int ii = 1; ii < pitchPeriodSpecification.TimePitchPeriodTupleList.Count; ii++)
                {
                    double time = pitchPeriodSpecification.TimePitchPeriodTupleList[ii].Item1;
                    SpeechType speechType = speechTypeSpecification.GetSpeechType(time);
                    if ((previousSpeechType == SpeechType.Voiced) && (speechType != SpeechType.Voiced))
                    {
                        firstChangeIndex = ii;
                        lastChangeIndex = -1; // Not yet assigned. The value -1 is used for handling cases where the
                                              // sound remains not voiced until the end (see below).
                    }
                    else if ((previousSpeechType != SpeechType.Voiced) && (speechType == SpeechType.Voiced))
                    {
                        lastChangeIndex = ii - 1;
                        int middlexIndex = (firstChangeIndex + lastChangeIndex) / 2; // integer division
                                                                                     // assign the preceding pitch to the first half of the interval (unless firstChangeIndex = 0, meaning
                                                                                     // that the sound started with an unvoiced segment), and the  subsequent pitch to the second half of
                                                                                     // the interval:
                        double subsequentPitch = pitchPeriodSpecification.TimePitchPeriodTupleList[lastChangeIndex].Item2;
                        previousPitch = subsequentPitch;
                        if (firstChangeIndex > 0)
                        {
                            previousPitch = pitchPeriodSpecification.TimePitchPeriodTupleList[firstChangeIndex - 1].Item2;
                        }
                        for (int jj = firstChangeIndex; jj < middlexIndex; jj++)
                        {
                            time = pitchPeriodSpecification.TimePitchPeriodTupleList[jj].Item1;
                            pitchPeriodSpecification.TimePitchPeriodTupleList[jj] = new Tuple<double, double>(time, previousPitch);
                        }
                        for (int jj = middlexIndex; jj <= lastChangeIndex; jj++)
                        {
                            time = pitchPeriodSpecification.TimePitchPeriodTupleList[jj].Item1;
                            pitchPeriodSpecification.TimePitchPeriodTupleList[jj] = new Tuple<double, double>(time, subsequentPitch);
                        }
                    }
                    previousTime = time;
                    previousSpeechType = speechType;
                }
                // At the end, if lastChangeIndex = -1, then the sound remained not voiced from the latest
                // change until the end. Thus:
                if ((lastChangeIndex == -1) && (firstChangeIndex > 0))
                {
                    previousPitch = pitchPeriodSpecification.TimePitchPeriodTupleList[firstChangeIndex - 1].Item2;
                    for (int jj = firstChangeIndex; jj < pitchPeriodSpecification.TimePitchPeriodTupleList.Count; jj++)
                    {
                        double time = pitchPeriodSpecification.TimePitchPeriodTupleList[jj].Item1;
                        pitchPeriodSpecification.TimePitchPeriodTupleList[jj] = new Tuple<double, double>(time, previousPitch);
                    }
                }

                // Then, finally, low-pass filter the interpolated list, and assign the result:
                AveragingFilter averagingFilter = new AveragingFilter();
                List<double> inputList = new List<double>();
                for (int ii = 0; ii < pitchPeriodSpecification.TimePitchPeriodTupleList.Count; ii++)
                {
                    double input = pitchPeriodSpecification.TimePitchPeriodTupleList[ii].Item2;
                    inputList.Add(input);
                }
                List<double> outputList = averagingFilter.Run(inputList);
                for (int ii = 0; ii < pitchPeriodSpecification.TimePitchPeriodTupleList.Count; ii++)
                {
                    double filteredPitch = outputList[ii];
                    double time = pitchPeriodSpecification.TimePitchPeriodTupleList[ii].Item1;
                    pitchPeriodSpecification.TimePitchPeriodTupleList[ii] = new Tuple<double, double>(time, filteredPitch);
                }
           /*     FirstOrderLowPassFilter lowPassFilter = new FirstOrderLowPassFilter();
                lowPassFilter.SetAlpha(0.9); // To do: Parameterize.
                for (int ii = 0; ii < pitchPeriodSpecification.TimePitchPeriodTupleList.Count; ii++)
                {
                    double input = pitchPeriodSpecification.TimePitchPeriodTupleList[ii].Item2;
                    lowPassFilter.Step(input);
                }
                for (int ii = 0; ii < pitchPeriodSpecification.TimePitchPeriodTupleList.Count; ii++)
                {
                    double filteredPitch = lowPassFilter.OutputList[ii];
                    double time = pitchPeriodSpecification.TimePitchPeriodTupleList[ii].Item1;
                    pitchPeriodSpecification.TimePitchPeriodTupleList[ii] = new Tuple<double, double>(time, filteredPitch);
                }  */
            }
        }

        public PitchPeriodSpecification PitchPeriodSpecification
        {
            get { return pitchPeriodSpecification; }
        }

        [DataMember]
        public double MinimumPitchPeriod
        {
            get { return minimumPitchPeriod; }
            set { minimumPitchPeriod = value; }
        }

        [DataMember]
        public double MaximumPitchPeriod
        {
            get { return maximumPitchPeriod; }
            set { maximumPitchPeriod = value; }
        }

        [DataMember]
        public double FrameShift
        {
            get { return frameShift; }
            set { frameShift = value; }
        }

        [DataMember]
        public double DeltaTime
        {
            get { return deltaTime; }
            set { deltaTime = value; }
        }

        [DataMember]
        public Boolean SetUnvoicedPitch
        {
            get { return setUnvoicedPitch; }
            set { setUnvoicedPitch = value; }
        }
    }
}
