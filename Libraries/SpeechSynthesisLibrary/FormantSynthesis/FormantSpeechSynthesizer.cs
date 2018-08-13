using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using AudioLibrary;
using MathematicsLibrary.ProbabilityDistributions;

namespace SpeechSynthesisLibrary.FormantSynthesis
{
    [DataContract]
    public class FormantSpeechSynthesizer: SpeechSynthesizer
    {
        #region Constants
        private const int DEFAULT_FUNDAMENTAL_FREQUENCY = 120;
        private const double DEFAULT_VOLUME = 0.5;
        private const double DEFAULT_MINIMUM_VOICED_FRACTION_FOR_PITCH = 0.5;
        private const int DEFAULT_SAMPLING_FREQUENCY = 16000;
        private const double WHITE_NOISE_LEVEL = 0.10;
        #endregion

        #region Fields
        private int fundamentalFrequency = DEFAULT_FUNDAMENTAL_FREQUENCY; // 20161024;
        private int samplingFrequency = DEFAULT_SAMPLING_FREQUENCY;
        private List<FormantSpecification> specificationList = null;
        private List<WordToSoundMapping> wordToSoundMappingList = null;

        private double volume = DEFAULT_VOLUME; // An overall volume parameter, ranging from 0 to 1.
        private Random randomNumberGenerator = null; // For generating unvoiced pulse trains
        private GaussianDistribution gaussian = null; // For generating unvoiced pulse trains

        private Boolean storePitch = false; // Set to true in order to store the pitch variation over the most recently generated sound
   //     private List<double> pitchList = null;
        private List<List<double>> timePitchPeriodList = null;
        private double minimumVoicedFractionForPitch = DEFAULT_MINIMUM_VOICED_FRACTION_FOR_PITCH; // The minimum voiced fraction for which pitch is computed (if storePitch = true).
        #endregion

        #region Constructors
        public FormantSpeechSynthesizer()
        {
            fundamentalFrequency = DEFAULT_FUNDAMENTAL_FREQUENCY;
            samplingFrequency = DEFAULT_SAMPLING_FREQUENCY;
            specificationList = new List<FormantSpecification>();
            wordToSoundMappingList = new List<WordToSoundMapping>();
            volume = DEFAULT_VOLUME;
        }
        #endregion

        #region Public methods
        public WAVSound GenerateSound(FormantSpecification formantSpecification)
        {
            double duration = formantSpecification.GetDuration();
            int samplingFrequency = formantSpecification.SamplingFrequency;
            double sampleTime = 1.0 / (double)samplingFrequency;
            double nominalPulsePeriod = 1 / (double)formantSpecification.FundamentalFrequency;
            int numberOfSamples = (int)Math.Round(duration / sampleTime);

            // Generate voiced pulse train: This requires running through the sequence
            // of formantSettings in the formantSpecification, in order to determine
            // the pitch (and its inverse, the pulse period) at each time:
            List<int> voicedPulseSpacingList = formantSpecification.GeneratePulseSpacingList();
            List<double> voicedPulseTrain = new List<double>();
            for (int ii = 0; ii < numberOfSamples; ii++) { voicedPulseTrain.Add(0); } // To be adjusted below.
            int sampleIndex = 0;
            while (sampleIndex < voicedPulseTrain.Count)
            {
                voicedPulseTrain[sampleIndex] = 1.0;
                sampleIndex += voicedPulseSpacingList[sampleIndex];
            }

            // Generate unvoiced pulse train:
            if (randomNumberGenerator == null) { randomNumberGenerator = new Random(); }
       //     if (gaussian == null) { gaussian = new GaussianDistribution(0, 0.05, -1); }
            List<double> unvoicedPulseTrain = new List<double>();
            for (int ii = 0; ii < numberOfSamples; ii++)
            {
                unvoicedPulseTrain.Add(0);
                if (randomNumberGenerator.NextDouble() < 0.5)
                {
                    unvoicedPulseTrain[ii] = -WHITE_NOISE_LEVEL + 2* WHITE_NOISE_LEVEL * randomNumberGenerator.NextDouble(); // gaussian.GetSample();
                }
            }

            // Set up sinusoids:
            int numberOfSinusoids = formantSpecification.GetNumberOfSinusoids();
            List<DampedSinusoid> sinusoidList = new List<DampedSinusoid>();
            for (int iSinusoid = 0; iSinusoid < numberOfSinusoids; iSinusoid++)
            {
                DampedSinusoid sinusoid = new DampedSinusoid(samplingFrequency);
                sinusoidList.Add(sinusoid);
            }

            // Prepare for storing pitch:
            if (storePitch)
            {
        //        pitchList = new List<double>();
                timePitchPeriodList = new List<List<double>>();
            }

            // Generate the relative amplitude list: Must be done separately, to handle
            // transitions:
            List<double> relativeAmplitudeList = formantSpecification.GenerateRelativeAmplitudeList();

            // Generate the unscaled samples:
            List<double> unscaledSampleList = new List<double>();
            double time = 0;
            sampleIndex = 0;
            while (sampleIndex < numberOfSamples)
            {
                time = sampleIndex * sampleTime;
                FormantSettings formantSettings = formantSpecification.GetInterpolatedSettings(sampleIndex);
                for (int iSinusoid = 0; iSinusoid < sinusoidList.Count; iSinusoid++)
                {
                    sinusoidList[iSinusoid].SetParameters(formantSettings.AmplitudeList[iSinusoid],
                                                          formantSettings.FrequencyList[iSinusoid],
                                                          formantSettings.BandwidthList[iSinusoid]);
                }
                double x = formantSettings.VoicedFraction * voicedPulseTrain[sampleIndex] +
                           (1 - formantSettings.VoicedFraction) * unvoicedPulseTrain[sampleIndex];

                // 20170407
                if (storePitch)
                {
                    if (formantSettings.VoicedFraction > minimumVoicedFractionForPitch)
                    {
                        if (voicedPulseTrain[sampleIndex] != 0)  // Define the pitch only at pulse spikes
                        {
                            double pitch = samplingFrequency / voicedPulseSpacingList[sampleIndex];
                        //    pitchList.Add(pitch);
                            double pitchPeriod = voicedPulseSpacingList[sampleIndex] * sampleTime;
                            timePitchPeriodList.Add(new List<double>() { time, pitchPeriod });
                        }
                 /*       else { pitchList.Add(-1); }
                    }
                    else
                    {
                        pitchList.Add(-1); // < 0 => pitch not defined  */
                    }  
                }

                double deltaTime = formantSpecification.DeltaTimeList[sampleIndex];
                double relativeAmplitude = relativeAmplitudeList[sampleIndex]; //  formantSettings.GetRelativeAmplitude(deltaTime);
                double sample = 0;
                for (int iSinusoid = 0; iSinusoid < sinusoidList.Count; iSinusoid++)
                {
                    sample += sinusoidList[iSinusoid].Next(x);
                }
                sample *= relativeAmplitude*volume;
                unscaledSampleList.Add(sample);
                sampleIndex++;
            }
            
            // Next generate the scaled samples
            List<Int16> sampleList = new List<Int16>();
            for (int ii = 0; ii < numberOfSamples; ii++)
            {
                if (unscaledSampleList[ii] > 1) { unscaledSampleList[ii] = 1; }
                else if (unscaledSampleList[ii] < -1) { unscaledSampleList[ii] = -1; }
                Int16 sample = (Int16)Math.Round(32767 *  unscaledSampleList[ii]);  // Some ugly hard-coding here...
                sampleList.Add(sample);
            }
            List<List<Int16>> twoChannelSampleList = new List<List<Int16>>();
            twoChannelSampleList.Add(sampleList);
            WAVSound wavSound = new WAVSound("Test", samplingFrequency, 1, 16);  // Some ugly hard-coding here...
            wavSound.GenerateFromSamples(twoChannelSampleList);
            return wavSound;
        }

        public override WAVSound GenerateWord(string word)
        {
            int wordIndex = wordToSoundMappingList.FindIndex(m => m.Word == word);
            WAVSound wordSound = null;
            if (wordIndex >= 0)
            {
                WordToSoundMapping wordToSoundMapping = wordToSoundMappingList[wordIndex];
                List<WAVSound> wavSoundList = new List<WAVSound>();
                foreach (string soundName in wordToSoundMapping.SoundNameList)
                {
                    FormantSpecification formantSpecification = SpecificationList.Find(s => s.Name == soundName);
                    if (formantSpecification != null)
                    {
                        formantSpecification.GenerateSettingsSequence();
                        WAVSound sound = GenerateSound(formantSpecification);
                        wavSoundList.Add(sound);
                    }
                }
                if (wavSoundList.Count > 0)
                {
                    wordSound = WAVSound.Join(wavSoundList, null);
                }
            }
            return wordSound;
        }

        public override WAVSound GenerateWordSequence(List<string> wordList, List<double> silenceList)
        {
            List<WAVSound> wordSoundList = new List<WAVSound>();
            foreach (string word in wordList)
            {
                WAVSound wordSound = GenerateWord(word);
                if (wordSound != null)
                {
                    wordSoundList.Add(wordSound);
                }
            }
            if (silenceList.Count > wordList.Count)
            {
                while (silenceList.Count > wordList.Count) { silenceList.RemoveAt(silenceList.Count - 1); }  // The silence list must not be longer than the wordlist.
            }
            WAVSound wordSequenceSound = null;
            if (wordSoundList.Count > 0) 
            {
                wordSequenceSound = WAVSound.Join(wordSoundList, silenceList);
            }

            //    WAVSound wordSequenceSound = WAVSound.Join(wordSoundList, silenceList);
            return wordSequenceSound;
        }
        #endregion

        #region Properties
        public Boolean StorePitch
        {
            get { return storePitch; }
            set { storePitch = value; }
        }

    /*    public List<double> PitchList
        {
            get { return pitchList; }
        }  */
         
        public List<List<double>> TimePitchPeriodList
        {
            get { return timePitchPeriodList; }
        }

        [DataMember]
        public double Volume
        {
            get { return volume; }
            set { volume = value; }
        }

        [DataMember]
        public int SamplingFrequency
        {
            get { return samplingFrequency; }
            set
            {
                samplingFrequency = value;
                if (specificationList != null)
                {
                    foreach (FormantSpecification formantSpecification in specificationList)
                    {
                        formantSpecification.SamplingFrequency = samplingFrequency;
                    }
                }
            }
        }

        [DataMember]
        public int FundamentalFrequency
        {
            get { return fundamentalFrequency; }
            set
            {
                fundamentalFrequency = value;
                if (specificationList != null)
                {
                    foreach (FormantSpecification formantSpecification in specificationList)
                    {
                        formantSpecification.FundamentalFrequency = fundamentalFrequency;
                    }
                }
            }
        }

        [DataMember]
        public List<FormantSpecification> SpecificationList
        {
            get { return specificationList; }
            set { specificationList = value; }
        }

        [DataMember]
        public List<WordToSoundMapping> WordToSoundMappingList
        {
            get { return wordToSoundMappingList; }
            set { wordToSoundMappingList = value; }
        }
        #endregion
    }
}
