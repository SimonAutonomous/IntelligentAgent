using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AudioLibrary;

namespace SpeechSynthesisLibrary.TDPSOLA
{
    public class SpeechModifier
    {
        #region Constants
        private const double DEFAULT_TOP_FRACTION = 0.20;
        #endregion

        #region Fields
        private SpeechTypeEstimator speechTypeEstimator;
        private PitchPeriodEstimator pitchPeriodEstimator;
        private PitchMarkEstimator pitchMarkEstimator;
        #endregion

        private double topFraction = DEFAULT_TOP_FRACTION;

        private List<double> modifiedPitchMarkTimeList; // Generated whenever ChangePitch() is called. Used when changing duration (ChangeDuration()).

        public SpeechModifier()
        {
            speechTypeEstimator = new SpeechTypeEstimator();
            pitchPeriodEstimator = new PitchPeriodEstimator();
            pitchMarkEstimator = new PitchMarkEstimator();
        }

        // Note: it is assumed that both channels (left and right) are equal.
        public WAVSound ChangePitch(WAVSound sound, List<double> pitchMarkTimeList, double relativeStartPitch, double relativeEndPitch)
        {
            // First find the pitch mark indices in the original sound:
            List<int> originalPitchMarkIndexList = new List<int>();
            foreach (double pitchMarkTime in pitchMarkTimeList)
            {
                int originalPitchMarkIndex = sound.GetSampleIndexAtTime(pitchMarkTime);
                originalPitchMarkIndexList.Add(originalPitchMarkIndex);
            }

            // Next, compute the index spacings of the pitch marks in the modified sound:
            double originalSoundDuration = sound.GetDuration();
            List<int> modifiedPitchMarkIndexSpacingList = new List<int>();
            modifiedPitchMarkTimeList = new List<double>();
            double firstModifiedPitchMarkTime = pitchMarkTimeList[0]; // First pitch mark unchanged
            modifiedPitchMarkTimeList.Add(firstModifiedPitchMarkTime);
            for (int ii = 1; ii < originalPitchMarkIndexList.Count; ii++)
            {
                int originalPitchMarkSpacing = originalPitchMarkIndexList[ii] - originalPitchMarkIndexList[ii - 1];
                double relativePitch = relativeStartPitch + (pitchMarkTimeList[ii] / originalSoundDuration) * (relativeEndPitch - relativeStartPitch);
                int modifiedPitchMarkIndexSpacing = (int)Math.Round(originalPitchMarkSpacing / relativePitch);
                modifiedPitchMarkIndexSpacingList.Add(modifiedPitchMarkIndexSpacing);
                double modifiedPitchMarkTime = modifiedPitchMarkTimeList.Last() + (double)modifiedPitchMarkIndexSpacing / (double)sound.SampleRate;
                modifiedPitchMarkTimeList.Add(modifiedPitchMarkTime);
            }

            // Now build the sound, keeping the original sound data over a fraction (topFraction) of the pitch periods
            // and interpolating between pitch periods:
            List<short> newSamples = new List<short>();

            //  Special treatment of the first pitch period:
            int firstPitchMarkIndex = originalPitchMarkIndexList[0];  // Position of the first pitch mark in the original sound
            int firstModifiedPitchMarkIndexSpacing = modifiedPitchMarkIndexSpacingList[0]; // Spacing between the first and second pitch mark in the modified sound
            int firstTopEndIndex = firstPitchMarkIndex + (int)Math.Round(topFraction * firstModifiedPitchMarkIndexSpacing);
            for (int ii = 0; ii < firstTopEndIndex; ii++)
            {
                newSamples.Add(sound.Samples[0][ii]);
            }

            for (int iPitchMark = 1; iPitchMark < originalPitchMarkIndexList.Count; iPitchMark++)
            {
                // First add samples for the transition from the previous pitch period to the current one:
                int modifiedPitchMarkIndexSpacing = modifiedPitchMarkIndexSpacingList[iPitchMark - 1]; // -1 since there are n-1 spacings for n pitch marks                
                int transitionIndexDuration = (int)Math.Round((1 - 2 * topFraction) * modifiedPitchMarkIndexSpacing);
                int previousPitchMarkIndex = originalPitchMarkIndexList[iPitchMark - 1];
                int previousTopEndIndex = previousPitchMarkIndex + (int)Math.Round(topFraction * modifiedPitchMarkIndexSpacing);
                int startIndexPreviousPitchPeriod = previousTopEndIndex;
                int currentPitchMarkIndex = originalPitchMarkIndexList[iPitchMark];
                int currentTopStartIndex = currentPitchMarkIndex - (int)Math.Round(topFraction * modifiedPitchMarkIndexSpacing);
                for (int ii = 0; ii < transitionIndexDuration; ii++)
                {
                    double alpha = (double)ii / (double)(transitionIndexDuration-1);
                    int previousPitchPeriodSampleIndex = previousTopEndIndex + ii;
                    int currentPitchPeriodSampleIndex = currentTopStartIndex - transitionIndexDuration + ii;
                    short newSample = (short)Math.Round(((1 - alpha) * sound.Samples[0][previousPitchPeriodSampleIndex] +
                                                              alpha * sound.Samples[0][currentPitchPeriodSampleIndex]));
                    newSamples.Add(newSample);
                }
                // Next, add samples around the top of the current pitch period:
                if (iPitchMark < (originalPitchMarkIndexList.Count - 1))
                {
                    int nextModifiedPitchMarkIndexSpacing = modifiedPitchMarkIndexSpacingList[iPitchMark];
                    int currentTopEndIndex = currentPitchMarkIndex + (int)Math.Round(topFraction * nextModifiedPitchMarkIndexSpacing);
                    for (int ii = currentTopStartIndex; ii < currentTopEndIndex; ii++)
                    {
                        newSamples.Add(sound.Samples[0][ii]);
                    }
                }
                else // Special treatment of the final pitch period:
                {
                    int endIndex = sound.Samples[0].Count;
                    for (int ii = currentTopStartIndex; ii < endIndex; ii++)
                    {
                        newSamples.Add(sound.Samples[0][ii]);
                    }
                }  
            }

            // Finally, build the sound from the new samples:
            WAVSound newSound = new WAVSound(sound.Name, sound.SampleRate, sound.NumberOfChannels, sound.BitsPerSample);
            newSound.GenerateFromSamples(new List<List<short>>() { newSamples, newSamples });

            return newSound;
        }

        // This method makes an approximation, namely that the pitch mark interval is roughly constant.
        // Usually, this will give a duration accurate to a few per cent (sufficient!) relative to the desired duration.
        public WAVSound ChangeDuration(WAVSound sound, List<double> pitchMarkTimeList, double relativeDuration)
        {
            /*   List<double> pitchPeriodList = new List<double>();
               for (int ii = 1; ii < pitchMarkTimeList.Count; ii++)
               {
                   double pitchPeriod = pitchMarkTimeList[ii] - pitchMarkTimeList[ii - 1];
                   pitchPeriodList.Add(pitchPeriod);
               }
               double averagePitchPeriod = pitchPeriodList.Average();  */

            List<short> newSamples = new List<short>();
            int firstPitchMarkIndex = sound.GetSampleIndexAtTime(pitchMarkTimeList[0]);
            for (int ii = 0; ii < firstPitchMarkIndex; ii++)
            {
                newSamples.Add(sound.Samples[0][ii]);
            }
            if (relativeDuration <= 1)
            {
                int removalStepInterval = (int)Math.Round(1 / (1-relativeDuration));
                int pitchIndex = 1;
                while (pitchIndex < pitchMarkTimeList.Count)
                {
                    if ( (pitchIndex % removalStepInterval) == 0)
                    {
                        // Nothing to do here: Simply avoid adding these samples
                    }
                    else
                    {
                        int previousPitchMarkIndex = sound.GetSampleIndexAtTime(pitchMarkTimeList[pitchIndex - 1]);
                        int currentPitchMarkIndex = sound.GetSampleIndexAtTime(pitchMarkTimeList[pitchIndex]);
                        for (int ii = previousPitchMarkIndex; ii < currentPitchMarkIndex; ii++)
                        {
                            newSamples.Add(sound.Samples[0][ii]);
                        }
                    }
                    pitchIndex++;
                }
            }
            else if (relativeDuration > 1)
            {
                int additionStepInterval = (int)Math.Round(1 / (relativeDuration - 1));
                int pitchIndex = 1;
                while (pitchIndex < pitchMarkTimeList.Count)
                {
                    if ((pitchIndex % additionStepInterval) == 0)
                    {
                        // Insert the samples for this pitch period twice:
                        int previousPitchMarkIndex = sound.GetSampleIndexAtTime(pitchMarkTimeList[pitchIndex - 1]);
                        int currentPitchMarkIndex = sound.GetSampleIndexAtTime(pitchMarkTimeList[pitchIndex]);
                        for (int ii = previousPitchMarkIndex; ii < currentPitchMarkIndex; ii++)
                        {
                            newSamples.Add(sound.Samples[0][ii]);
                        }
                        for (int ii = previousPitchMarkIndex; ii < currentPitchMarkIndex; ii++)
                        {
                            newSamples.Add(sound.Samples[0][ii]);
                        }
                    }
                    else
                    {
                        int previousPitchMarkIndex = sound.GetSampleIndexAtTime(pitchMarkTimeList[pitchIndex - 1]);
                        int currentPitchMarkIndex = sound.GetSampleIndexAtTime(pitchMarkTimeList[pitchIndex]);
                        for (int ii = previousPitchMarkIndex; ii < currentPitchMarkIndex; ii++)
                        {
                            newSamples.Add(sound.Samples[0][ii]);
                        }
                    }
                    pitchIndex++;
                }
            }

            // Finally, build the sound from the new samples:
            WAVSound newSound = new WAVSound(sound.Name, sound.SampleRate, sound.NumberOfChannels, sound.BitsPerSample);
            newSound.GenerateFromSamples(new List<List<short>>() { newSamples, newSamples });

            return newSound;
        }

        public WAVSound Modify(WAVSound sound, double relativeStartPitch, double relativeEndPitch, Boolean adjustDuration, double relativeDuration)
        {
            // First, find the speech type variation:
            //  speechTypeEstimator = new SpeechTypeEstimator();
            speechTypeEstimator.FindSpeechTypeVariation(sound);

        /*    speechTypeEstimator.FindSpeechTypeVariation(sound, 0, frameDuration, frameShift, speechTypeLowPassCutoffFrequency, speechTypeLowPassRatioThreshold,
                speechTypeEnergyThreshold, speechTypeSilenceThreshold);
            speechTypeEstimator.Adjust(3);
            speechTypeEstimator.Adjust(3); // repeat the adjustment to remove double errors.  */
            SpeechTypeSpecification speechTypeSpecification = speechTypeEstimator.SpeechTypeSpecification;
            
            // Next, find the pitch periods:
            PitchPeriodEstimator pitchPeriodEstimator = new PitchPeriodEstimator();
            pitchPeriodEstimator.ComputePitchPeriods(sound, 0.0, sound.GetDuration()); //, minimumPitchPeriod, maximumPitchPeriod, frameShift); // 0.0120, 0.01, 0.03);
            pitchPeriodEstimator.AdjustAndInterpolate(speechTypeSpecification); //, pitchPeriodDeltaTime, setUnvoicedPitch); // 0.005, true);
            PitchPeriodSpecification pitchPeriodSpecification = pitchPeriodEstimator.PitchPeriodSpecification;

            // Then, find the pitch marks:
            pitchMarkEstimator = new PitchMarkEstimator();
            pitchMarkEstimator.FindPitchMarks(sound, speechTypeSpecification, pitchPeriodSpecification); // , 0.0025, 0.0025, 0.45, 0.002);
            List<double> pitchMarkTimeList = pitchMarkEstimator.PitchMarkTimeList;

            // Then, change the pitch of the sound
            double originalDuration = sound.GetDuration();
            double desiredDuration = originalDuration * relativeDuration;
            double actualRelativeDuration = relativeDuration; // Valid if the pitch is unchanged ...
            WAVSound pitchChangedSound;
            if ((Math.Abs(relativeStartPitch - 1) > double.Epsilon) || (Math.Abs(relativeEndPitch - 1) > double.Epsilon)) // To save some time, if only duration is to be changed..
            {
                pitchChangedSound = ChangePitch(sound, pitchMarkTimeList, relativeStartPitch, relativeEndPitch);
                // The pitch change also changes the duration of the sound:
                double newDuration = pitchChangedSound.GetDuration();
                actualRelativeDuration = desiredDuration / newDuration; // ...but if the pitch is changed, the duration changes too.
            }
            else
            {
                pitchChangedSound = sound;  // No copying needed here, a reference is sufficient.
                modifiedPitchMarkTimeList = pitchMarkTimeList; // No pitch change => use original pitch marks.
            }

            // If the adjustDuration is true, change the duration, using the stored pitchmark time list (to avoid repeating the three steps above):
            if (adjustDuration)
            {
                WAVSound durationChangedSound = ChangeDuration(pitchChangedSound, modifiedPitchMarkTimeList, actualRelativeDuration);
                return durationChangedSound;
            }
            else { return pitchChangedSound; }
        }

        #region Properties
        public double TopFraction
        {
            get { return topFraction; }
            set { topFraction = value; }
        }

        public SpeechTypeEstimator SpeechTypeEstimator
        {
            get { return speechTypeEstimator; }
            set { speechTypeEstimator = value; }
        }

        public PitchPeriodEstimator PitchPeriodEstimator
        {
            get { return pitchPeriodEstimator; }
            set { pitchPeriodEstimator = value; }
        }

        public PitchMarkEstimator PitchMarkEstimator
        {
            get { return pitchMarkEstimator; }
            set { pitchMarkEstimator = value; }
        }
        #endregion
    }
}
