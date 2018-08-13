using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SpeechSynthesisLibrary.FormantSynthesis
{
    [DataContract]
    public class FormantSpecification
    {
        private const int MINIMUM_PULSE_SPACING = 10;
        private const double DEFAULT_END_POINT_SILENCE_DURATION = 0.025;

        private string name = "Unnamed";
        private int samplingFrequency;
        private int fundamentalFrequency;
        private List<FormantSettings> formantSettingsList;
        private List<int> settingsIndexList; // The FormantSettings (index) to use for each time sample
        private List<double> deltaTimeList; // The time elapsed since the start of the current formant settings;

        public FormantSpecification(int fundamentalFrequency, int samplingFrequency)
        {
            this.fundamentalFrequency = fundamentalFrequency;
            this.samplingFrequency = samplingFrequency;
            formantSettingsList = new List<FormantSettings>();
        }

        public void Modify(Random random, List<int> modifiableFormatSettingsIndexList, double relativeModificationRange,
                   Boolean modifySinusoids, Boolean modifyVoicedFraction, Boolean modifyDuration,
                   Boolean modifyTransitionStart, Boolean modifyAmplitudeVariation, Boolean modifyPitchVariation)
        {
            foreach (int index in modifiableFormatSettingsIndexList)
            {
                formantSettingsList[index].Modify(random, relativeModificationRange,
                    modifySinusoids, modifyVoicedFraction, modifyDuration, modifyTransitionStart,
                    modifyAmplitudeVariation, modifyPitchVariation);
            }
        }

        public FormantSpecification Copy()
        {
            FormantSpecification copiedFormantSpecification = new FormantSpecification(this.fundamentalFrequency, this.samplingFrequency);
            foreach (FormantSettings formantSettings in this.formantSettingsList)
            {
                FormantSettings copiedFormantSettings = formantSettings.Copy();
                copiedFormantSpecification.FormantSettingsList.Add(copiedFormantSettings);
            }
            return copiedFormantSpecification;
        }

        public void GenerateSettingsSequence()
        {
            double sampleTime = 1 / (double)samplingFrequency;
            double duration = GetDuration();
            int numberOfSamples = (int)Math.Round(duration / sampleTime);
            double time = 0;
            int currentIndex = 0;
            settingsIndexList = new List<int>();
            deltaTimeList = new List<double>();
            FormantSettings currentFormantSettings = formantSettingsList[currentIndex];
            double currentIndexStartTime = time;
            for (int ii = 0; ii < numberOfSamples; ii++)
            {
                settingsIndexList.Add(currentIndex);
                time = ii * sampleTime;
                deltaTimeList.Add(time - currentIndexStartTime);
                if ((time - currentIndexStartTime) > currentFormantSettings.Duration)
                {
                    currentIndex++;
                    currentFormantSettings = formantSettingsList[currentIndex];
                    currentIndexStartTime = time;
                }
            }
        }

        public FormantSettings GetInterpolatedSettings(int sampleIndex)
        {
            int settingsIndex = settingsIndexList[sampleIndex];
            if ((formantSettingsList[settingsIndex].TransitionStart >= 1.0) || (formantSettingsList.Count == 1)) { return formantSettingsList[settingsIndex]; } // Common special case..
            else
            {
                double deltaTime = deltaTimeList[sampleIndex];
                double constantSettingsEndTime = formantSettingsList[settingsIndex].TransitionStart *
                                                 formantSettingsList[settingsIndex].Duration;
                if (deltaTime <= constantSettingsEndTime)  // Before transition onset
                {
                    return formantSettingsList[settingsIndex];
                }
                else // After transition onset
                {
                    int nextSettingsIndex = settingsIndex;
                    if (settingsIndex < (formantSettingsList.Count - 1))
                    {
                        nextSettingsIndex = settingsIndex + 1;
                    }
                    double alpha = (deltaTime - constantSettingsEndTime) / (formantSettingsList[settingsIndex].Duration - constantSettingsEndTime);
                    FormantSettings settings = FormantSettings.Interpolate(formantSettingsList[settingsIndex], formantSettingsList[nextSettingsIndex], alpha);
                    return settings;
                }
            }
        }

        public double GetDuration()
        {
            double duration = 0;
            foreach (FormantSettings formantSettings in formantSettingsList)
            {
                duration += formantSettings.Duration;
            }
            return duration;
        }

        // (Note: It is assumed that the number of sinusoids is the same for all formantsettings
        public int GetNumberOfSinusoids()
        {
            if (formantSettingsList == null) { return 0; }
            if (formantSettingsList.Count == 0) { return 0; }
            return formantSettingsList[0].AmplitudeList.Count;
        }

        // 20170405. For this method it is assumed that GenerateSettingsSequence has been
        // called first. See also GenerateWord() in (for example) FormantSpeechSynthesizer.
        public List<int> GeneratePulseSpacingList()
        {
            // First generate the implied pulse spacing at each sample:
            // (Actually, a bit unnecessary - could be done in a single pass,
            // but the code below makes the procedure clearer (runs fast enough anyway).
            List<int> pulseSpacingList = new List<int>();
            double sampleTime = 1.0 / (double)samplingFrequency;
            double duration = GetDuration();
            int numberOfSamples = (int)Math.Round(duration / sampleTime);
            for (int ii = 0; ii < numberOfSamples; ii++)
            {
                int settingsIndex = settingsIndexList[ii];
                double deltaTime = deltaTimeList[ii];
                double constantSettingsEndTime = formantSettingsList[settingsIndex].TransitionStart *
                                                 formantSettingsList[settingsIndex].Duration;
                if ((deltaTime <= constantSettingsEndTime) || (formantSettingsList.Count == 1))  // Before transition onset (no transition of there is only one formant settings)
                {
                    double relativePitch = formantSettingsList[settingsIndex].GetRelativePitch(deltaTime);
                    double pitch = fundamentalFrequency * relativePitch;
                    int pulseSpacing = (int)Math.Round(samplingFrequency / pitch);  // Measured in # of samples
                    if (pulseSpacing < MINIMUM_PULSE_SPACING) { pulseSpacing = MINIMUM_PULSE_SPACING; }
                    pulseSpacingList.Add(pulseSpacing);
                }
                else  // During transition
                {
                    int nextSettingsIndex = settingsIndex;
                    if (settingsIndex < (formantSettingsList.Count - 1))
                    {
                        nextSettingsIndex = settingsIndex + 1;
                    }
                    double alpha = 0;
                    if (Math.Abs(formantSettingsList[settingsIndex].Duration - constantSettingsEndTime) > double.Epsilon)
                    {
                        alpha = (deltaTime - constantSettingsEndTime) / (formantSettingsList[settingsIndex].Duration - constantSettingsEndTime);
                    }
                    //   double alpha = (deltaTime - constantSettingsEndTime) / (formantSettingsList[settingsIndex].Duration - constantSettingsEndTime);

                    double deltaTimeInNextSettings = deltaTime - constantSettingsEndTime; // Time incursion in next sound
                    double relativePitch1 = formantSettingsList[settingsIndex].GetRelativePitch(constantSettingsEndTime); // The pitch just before transition start
                    double relativePitch2 = formantSettingsList[nextSettingsIndex].GetRelativePitch(0);  // Work towards the relative pitch at the start of sound2
                    double relativePitch = (1 - alpha) * relativePitch1 + alpha * relativePitch2;
                    double pitch = fundamentalFrequency * relativePitch;
                    int pulseSpacing = (int)Math.Round(samplingFrequency / pitch); // Measured in # of samples
                    pulseSpacingList.Add(pulseSpacing);
                }
            }
            return pulseSpacingList;
        }

        public List<double> GenerateRelativeAmplitudeList()
        {
            List<double> relativeAmplitudeList = new List<double>();
            double sampleTime = 1.0 / (double)samplingFrequency;
            double duration = GetDuration();
            int numberOfSamples = (int)Math.Round(duration / sampleTime);
            for (int ii = 0; ii < numberOfSamples; ii++)
            {
                int settingsIndex = settingsIndexList[ii];
                double deltaTime = deltaTimeList[ii];
                double constantSettingsEndTime = formantSettingsList[settingsIndex].TransitionStart *
                                                 formantSettingsList[settingsIndex].Duration;
                if ((deltaTime <= constantSettingsEndTime) || (formantSettingsList.Count == 1) || (settingsIndex == formantSettingsList.Count-1))  // Before transition onset
                {
                    double relativeAmplitude = formantSettingsList[settingsIndex].GetRelativeAmplitude(deltaTime);
                    relativeAmplitudeList.Add(relativeAmplitude);
                }
                else  // During transition
                {
                    int nextSettingsIndex = settingsIndex;
                    if (settingsIndex < (formantSettingsList.Count - 1))
                    {
                        nextSettingsIndex = settingsIndex + 1;
                    }
                    double alpha = (deltaTime - constantSettingsEndTime) / (formantSettingsList[settingsIndex].Duration - constantSettingsEndTime);
                    double deltaTimeInNextSettings = deltaTime - constantSettingsEndTime; // Time incursion in next sound
                    double relativeAmplitude1 = formantSettingsList[settingsIndex].GetRelativeAmplitude(constantSettingsEndTime); // The amplitude just before transition start
                    double relativeAmplitude2 = formantSettingsList[nextSettingsIndex].GetRelativeAmplitude(0);  // Work towards the relative amplitude at the start of sound2
                    double relativeAmplitude = (1 - alpha) * relativeAmplitude1 + alpha * relativeAmplitude2;
                    relativeAmplitudeList.Add(relativeAmplitude);
                }
            
            }
            return relativeAmplitudeList;
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public int SamplingFrequency
        {
            get { return samplingFrequency; }
            set { samplingFrequency = value; }
        }

        [DataMember]
        public int FundamentalFrequency
        {
            get { return fundamentalFrequency; }
            set { fundamentalFrequency = value; }
        }

        [DataMember]
        public List<FormantSettings> FormantSettingsList
        {
            get { return formantSettingsList; }
            set { formantSettingsList = value; }
        }

        // Lists, for each sample, the time elapsed since the
        // start of the current formant settings.
        public List<double> DeltaTimeList
        {
            get { return deltaTimeList; }
        }
    }
}
