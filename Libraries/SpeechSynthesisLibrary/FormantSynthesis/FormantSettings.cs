using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using MathematicsLibrary.Functions;

namespace SpeechSynthesisLibrary.FormantSynthesis
{
    [DataContract]
    public class FormantSettings
    {
        public const double DEFAULT_DURATION = 0.1;
        public const double DEFAULT_VOICED_FRACTION = 1.0;
        public const double DEFAULT_TRANSITION_START = 1.0; // => no transition
        public const double DEFAULT_FREQUENCY = 400;
        public const double DEFAULT_AMPLITUDE = 0.2;
        public const double DEFAULT_BANDWIDTH = 100;
        public const double MINIMUM_RANDOMIZED_FREQUENCY = 20;
        public const double MINIMUM_RANDOMIZED_BANDWIDTH = 10;
        public const double MINIMUM_RANDOMIZED_DURATION = 0.02;
        public const double MINIMUM_RANDOMIZED_AMPLITUDE = -2;
        public const double MAXIMUM_RANDOMIZED_FREQUENCY = 5000; // 4000;
        public const double MAXIMUM_RANDOMIZED_BANDWIDTH = 500; // 1000;
        public const double MAXIMUM_RANDOMIZED_DURATION = 0.50;
        public const double MAXIMUM_RANDOMIZED_AMPLITUDE = 2; // 0.5;

        public const double MINIMUM_RELATIVE_PITCH = 0.5;
        public const double MAXIMUM_RELATIVE_PITCH = 2.0;
        public const double MINIMUM_RELATIVE_AMPLITUDE = 0.0;
        public const double MAXIMUM_RELATIVE_AMPLITUDE = 1.0;
        public const double DEFAULT_RELATIVE_PITCH_MINIMUM_CONSTANT_COEFFICIENT = 0.8;
        public const double DEFAULT_RELATIVE_PITCH_MAXIMUM_CONSTANT_COEFFICIENT = 1.2;
        public const double DEFAULT_RELATIVE_PITCH_LINEAR_COEFFICIENT_RANGE = 0.3;
        public const double DEFAULT_RELATIVE_PITCH_QUADRATIC_COEFFICIENT_RANGE = 0.3;

        private double duration;
        private double voicedFraction;
        private double transitionStart; // The relative time (in sound j) in which the transition to sound j+1 starts
        private List<double> amplitudeList;  // For the sinusoids
        private List<double> frequencyList;  // For the sinusoids
        private List<double> bandwidthList;  // For the sinusoids
        private List<double> relativePitchVariationParameterList; // Relative pitch (relative to the fundamental pitch of the voice)
        private List<double> relativeAmplitudeVariationParameterList; // Relative, overall amplitude

        private double relativePitchMinimumConstantCoefficient = DEFAULT_RELATIVE_PITCH_MINIMUM_CONSTANT_COEFFICIENT;
        private double relativePitchMaximumConstantCoefficient = DEFAULT_RELATIVE_PITCH_MAXIMUM_CONSTANT_COEFFICIENT;
        private double relativePitchLinearCoefficientRange = DEFAULT_RELATIVE_PITCH_LINEAR_COEFFICIENT_RANGE; 
        private double relativePitchQuadraticCoefficientRange = DEFAULT_RELATIVE_PITCH_QUADRATIC_COEFFICIENT_RANGE; 

        // Generates formant settings with default parameter specifications: voiced sound, 3 sinusoids,
        // no variation in pitch or amplitude
        public FormantSettings()
        {
            duration = DEFAULT_DURATION;
            voicedFraction = DEFAULT_VOICED_FRACTION;
            transitionStart = DEFAULT_TRANSITION_START;
            amplitudeList = new List<double>() { DEFAULT_AMPLITUDE, DEFAULT_AMPLITUDE, DEFAULT_AMPLITUDE, DEFAULT_AMPLITUDE, DEFAULT_AMPLITUDE  };
            frequencyList = new List<double>() { DEFAULT_FREQUENCY, DEFAULT_FREQUENCY, DEFAULT_FREQUENCY, DEFAULT_FREQUENCY, DEFAULT_FREQUENCY };
            bandwidthList = new List<double>() { DEFAULT_BANDWIDTH, DEFAULT_BANDWIDTH, DEFAULT_BANDWIDTH, DEFAULT_BANDWIDTH, DEFAULT_BANDWIDTH };
            relativePitchVariationParameterList = new List<double>() { 1.0, 0.0, 0.0 };
            relativeAmplitudeVariationParameterList = new List<double>() { 1.0, 0.0, 0.0 };
        }

        public FormantSettings Copy()
        {
            FormantSettings copiedFormantSettings = new FormantSettings();
            copiedFormantSettings.Duration = this.duration;
            copiedFormantSettings.VoicedFraction = this.voicedFraction;
            copiedFormantSettings.TransitionStart = this.transitionStart;
            copiedFormantSettings.FrequencyList = new List<double>();
            foreach (double frequency in this.frequencyList) { copiedFormantSettings.FrequencyList.Add(frequency); }
            copiedFormantSettings.AmplitudeList = new List<double>();
            foreach (double amplitude in this.amplitudeList) { copiedFormantSettings.AmplitudeList.Add(amplitude); }
            copiedFormantSettings.BandwidthList = new List<double>();
            foreach (double bandwidth in this.bandwidthList) { copiedFormantSettings.BandwidthList.Add(bandwidth); }
            copiedFormantSettings.RelativePitchVariationParameterList = new List<double>();
            foreach (double pitchVariationParameter in this.relativePitchVariationParameterList)
            { copiedFormantSettings.RelativePitchVariationParameterList.Add(pitchVariationParameter); }
            copiedFormantSettings.RelativeAmplitudeVariationParameterList = new List<double>();
            foreach (double amplitudeVariationParameter in this.relativeAmplitudeVariationParameterList)
            { copiedFormantSettings.RelativeAmplitudeVariationParameterList.Add(amplitudeVariationParameter); }
            return copiedFormantSettings;
        }

        public void Randomize(Random randomNumberGenerator)
        {
            for (int iSinusoid = 0; iSinusoid < frequencyList.Count; iSinusoid++)
            {
                frequencyList[iSinusoid] = MINIMUM_RANDOMIZED_FREQUENCY + randomNumberGenerator.NextDouble() * (MAXIMUM_RANDOMIZED_FREQUENCY - MINIMUM_RANDOMIZED_FREQUENCY);
                amplitudeList[iSinusoid] = MINIMUM_RANDOMIZED_AMPLITUDE + randomNumberGenerator.NextDouble() * (MAXIMUM_RANDOMIZED_AMPLITUDE - MINIMUM_RANDOMIZED_AMPLITUDE); //  randomNumberGenerator.NextDouble();
                bandwidthList[iSinusoid] = MINIMUM_RANDOMIZED_BANDWIDTH + randomNumberGenerator.NextDouble() * (MAXIMUM_RANDOMIZED_BANDWIDTH - MINIMUM_RANDOMIZED_BANDWIDTH);
            }
            voicedFraction = 1; // randomNumberGenerator.NextDouble();
            duration = MINIMUM_RANDOMIZED_DURATION + randomNumberGenerator.NextDouble() * (MAXIMUM_RANDOMIZED_DURATION - MINIMUM_RANDOMIZED_DURATION);
            // Relative amplitude (first parameter: relative time of extremum):
            relativeAmplitudeVariationParameterList[0] = randomNumberGenerator.NextDouble();
            relativeAmplitudeVariationParameterList[1] = MINIMUM_RELATIVE_AMPLITUDE + randomNumberGenerator.NextDouble() * (MAXIMUM_RELATIVE_AMPLITUDE - MINIMUM_RELATIVE_AMPLITUDE);
            relativeAmplitudeVariationParameterList[2] = MINIMUM_RELATIVE_AMPLITUDE + randomNumberGenerator.NextDouble() * (MAXIMUM_RELATIVE_AMPLITUDE - MINIMUM_RELATIVE_AMPLITUDE);
            // Relative pitch (first parameter: relative time of extremum):
            relativePitchVariationParameterList[0] = relativePitchMinimumConstantCoefficient +
                randomNumberGenerator.NextDouble() * (relativePitchMaximumConstantCoefficient - relativePitchMinimumConstantCoefficient);
            relativePitchVariationParameterList[1] = -relativePitchLinearCoefficientRange + 2 * relativePitchLinearCoefficientRange * randomNumberGenerator.NextDouble();
            relativePitchVariationParameterList[2] = -relativePitchQuadraticCoefficientRange + 2 * relativePitchQuadraticCoefficientRange * randomNumberGenerator.NextDouble();



        /*    relativeAmplitudeVariationParameterList = QuadraticFunction.GetRandomCoefficients(0, 1, 0, 1, randomNumberGenerator, linearCoefficientRange, quadraticCoefficientRange);
            // Default range for relative pitch: [0.5,2.0]
            relativePitchVariationParameterList = 
                QuadraticFunction.GetRandomCoefficients(0, 1, minimumRelativePitch, maximumRelativePitch, randomNumberGenerator, linearCoefficientRange, quadraticCoefficientRange);  */
        }

        public void Modify(Random randomNumberGenerator, double relativeModificationRange, Boolean modifySinusoids,
                   Boolean modifyVoicedFraction, Boolean modifyDuration, Boolean modifyTransitionStart, Boolean modifyAmplitudeVariation, 
                   Boolean modifyPitchVariation)
        {
            if (modifyAmplitudeVariation)
            {
                relativeAmplitudeVariationParameterList[0] += (-relativeModificationRange + 2 * randomNumberGenerator.NextDouble() * relativeModificationRange);
                if (relativeAmplitudeVariationParameterList[0] < 0) { relativeAmplitudeVariationParameterList[0] = 0; }
                else if (relativeAmplitudeVariationParameterList[0]> 1) { relativeAmplitudeVariationParameterList[0] = 1; }
                relativeAmplitudeVariationParameterList[1] += (-relativeModificationRange + 2 * randomNumberGenerator.NextDouble() * relativeModificationRange);
                if (relativeAmplitudeVariationParameterList[1] < 0) { relativeAmplitudeVariationParameterList[1] = 0; }
                else if (relativeAmplitudeVariationParameterList[1] > 1) { relativeAmplitudeVariationParameterList[1] = 1; }
                relativeAmplitudeVariationParameterList[2] += (-relativeModificationRange + 2 * randomNumberGenerator.NextDouble() * relativeModificationRange);
                if (relativeAmplitudeVariationParameterList[2] < 0) { relativeAmplitudeVariationParameterList[2] = 0; }
                else if (relativeAmplitudeVariationParameterList[2] > 1) { relativeAmplitudeVariationParameterList[2] = 1; }
                //   relativeAmplitudeVariationParameterList = QuadraticFunction.GetRandomCoefficients(0, 1, 0, 1, randomNumberGenerator, linearCoefficientRange, quadraticCoefficientRange);
            }
            if (modifyPitchVariation)
            {
                relativePitchVariationParameterList[0] += (-relativeModificationRange + 2 * randomNumberGenerator.NextDouble() * relativeModificationRange);
                if (relativePitchVariationParameterList[0] < relativePitchMinimumConstantCoefficient) { relativePitchVariationParameterList[0] = relativePitchMinimumConstantCoefficient; }
                else if (relativePitchVariationParameterList[0] > relativePitchMaximumConstantCoefficient) { relativePitchVariationParameterList[0] = relativePitchMaximumConstantCoefficient; }
                relativePitchVariationParameterList[1] += (-relativeModificationRange + 2 * randomNumberGenerator.NextDouble() * relativeModificationRange);
                if (relativePitchVariationParameterList[1] < -relativePitchLinearCoefficientRange) { relativePitchVariationParameterList[1] = -relativePitchLinearCoefficientRange; }
                else if (relativePitchVariationParameterList[1] > relativePitchLinearCoefficientRange) { relativePitchVariationParameterList[1] = relativePitchLinearCoefficientRange; }
                relativePitchVariationParameterList[2] += (-relativeModificationRange + 2 * randomNumberGenerator.NextDouble() * relativeModificationRange);
                if (relativePitchVariationParameterList[2] < -relativePitchQuadraticCoefficientRange) { relativePitchVariationParameterList[2] = -relativePitchQuadraticCoefficientRange; }
                else if (relativePitchVariationParameterList[2] > relativePitchQuadraticCoefficientRange) { relativePitchVariationParameterList[2] = relativePitchQuadraticCoefficientRange; }
                //   relativeAmplitudeVariationParameterList = QuadraticFunction.GetRandomCoefficients(0, 1, 0, 1, randomNumberGenerator, linearCoefficientRange, quadraticCoefficientRange);
            }
            if (modifySinusoids)
            {
                for (int ii = 0; ii < amplitudeList.Count; ii++)
                {
                    amplitudeList[ii] += (-relativeModificationRange + 2 * randomNumberGenerator.NextDouble() * relativeModificationRange) * amplitudeList[ii];
                    if (amplitudeList[ii] > MAXIMUM_RANDOMIZED_AMPLITUDE) { amplitudeList[ii] = MAXIMUM_RANDOMIZED_AMPLITUDE; }
                    else if (amplitudeList[ii] < MINIMUM_RANDOMIZED_AMPLITUDE) { amplitudeList[ii] = MINIMUM_RANDOMIZED_AMPLITUDE; }
                    frequencyList[ii] += (-relativeModificationRange + 2 * randomNumberGenerator.NextDouble() * relativeModificationRange) * frequencyList[ii];
                    if (frequencyList[ii] < MINIMUM_RANDOMIZED_FREQUENCY) { frequencyList[ii] = MINIMUM_RANDOMIZED_FREQUENCY; }
                    else if (frequencyList[ii] > MAXIMUM_RANDOMIZED_FREQUENCY) { frequencyList[ii] = MAXIMUM_RANDOMIZED_FREQUENCY; }
                    bandwidthList[ii] += (-relativeModificationRange + 2 * randomNumberGenerator.NextDouble() * relativeModificationRange) * bandwidthList[ii];
                    if (bandwidthList[ii] > MAXIMUM_RANDOMIZED_BANDWIDTH) { bandwidthList[ii] = MAXIMUM_RANDOMIZED_BANDWIDTH; }
                    else if (bandwidthList[ii] < MINIMUM_RANDOMIZED_BANDWIDTH) { bandwidthList[ii] = MINIMUM_RANDOMIZED_BANDWIDTH; }
                }
            }
            if (modifyDuration)
            {
                Duration += (-relativeModificationRange + 2 * randomNumberGenerator.NextDouble() * relativeModificationRange) * Duration;
                if (Duration < MINIMUM_RANDOMIZED_DURATION) { Duration = MINIMUM_RANDOMIZED_DURATION; }
                else if (Duration > MAXIMUM_RANDOMIZED_DURATION) { Duration = MAXIMUM_RANDOMIZED_DURATION; }
            }
            if (modifyVoicedFraction)
            {
                VoicedFraction += (-relativeModificationRange + 2 * randomNumberGenerator.NextDouble() * relativeModificationRange);
                if (VoicedFraction < 0) { VoicedFraction = 0; }
                else if (VoicedFraction > 1) { VoicedFraction = 1; }
            }
            if (modifyTransitionStart)
            {
                TransitionStart += (-relativeModificationRange + 2 * randomNumberGenerator.NextDouble() * relativeModificationRange);
                if (TransitionStart < 0) { TransitionStart = 0; }
                else if (TransitionStart > 1) { TransitionStart = 1; }
            }
        }

        // The relative amplitude varies as aR = a0 + (1-a0)*t/tE for t < tE and
        // aR = 1 + (a1-1)(t-tE)/(1-tE), where t is the relative time (0 -> 1) over
        // the sound, and tE is the time of the extremum.
        public double GetRelativeAmplitude (double time)
        {
            double relativeTime = time / duration;

            double tE = relativeAmplitudeVariationParameterList[0];
            double a0 = relativeAmplitudeVariationParameterList[1];
            double a1 = relativeAmplitudeVariationParameterList[2];

            double relativeAmplitude = 1;
            if (relativeTime < tE)
            {
                relativeAmplitude = a0 + (1 - a0) * (relativeTime / tE);
            }
            else
            {
                relativeAmplitude = 1 + (a1 - 1) * (relativeTime - tE)/(1 - tE);
            }
            return relativeAmplitude;

         /*   double relativeAmplitude = relativeAmplitudeVariationParameterList[0] + relativeAmplitudeVariationParameterList[1] * relativeTime +
                relativeAmplitudeVariationParameterList[2] * relativeTime * relativeTime;
            if (relativeAmplitude < minimumRelativeAmplitude) { relativeAmplitude = minimumRelativeAmplitude; }
            else if (relativeAmplitude > maximumRelativeAmplitude) { relativeAmplitude = maximumRelativeAmplitude; }
            return relativeAmplitude;  */
        }

        // The relative pitch varies as pR = p0 + (1-p0)*t/tE for t < tE and
        // pR = 1 + (p1-1)(t-tE)/(1-tE), where t is the relative time (0 -> 1) over
        // the sound, and tE is the time of the extremum.
        public double GetRelativePitch(double time)
        {
            double relativeTime = time / duration;

            double p0 = relativePitchVariationParameterList[0];
            double p1 = relativePitchVariationParameterList[1];
            double p2 = relativePitchVariationParameterList[2];

            double relativePitch = p0 + p1 * relativeTime + p2 * relativeTime * relativeTime;
            if (relativePitch < MINIMUM_RELATIVE_PITCH) { relativePitch = MINIMUM_RELATIVE_PITCH; }
            else if (relativePitch > MAXIMUM_RELATIVE_PITCH) { relativePitch = MAXIMUM_RELATIVE_PITCH; }


         /*   double relativePitch = 1;
            if (relativeTime < tE)
            {
                relativePitch = p0 + (1 - p0) * (relativeTime / tE);
            }
            else
            {
                relativePitch = 1 + (p1 - 1) * (relativeTime - tE) / (1 - tE);
            }  */
            return relativePitch;

            /*  double relativePitch = relativePitchVariationParameterList[0] + relativePitchVariationParameterList[1] * relativeTime +
                  relativePitchVariationParameterList[2] * relativeTime * relativeTime;
              if (relativePitch < minimumRelativePitch) { relativePitch = minimumRelativePitch; }
              else if (relativePitch > maximumRelativePitch) { relativePitch = maximumRelativePitch; }
              return relativePitch; */
        }

        public static FormantSettings Interpolate(FormantSettings settings1, FormantSettings settings2, double alpha)
        {
            FormantSettings interpolatedSettings = settings1.Copy(); // Easy way to get the correct number of sinusoids etc.
            // The Duration and TransitionStart parameters should NOT be interpolated, and neither should
            // the amplitude and pitch variation parameters (which are interpolated separately, see the FormantSpecification class.
            // ...but the remaining parameters should:
            interpolatedSettings.VoicedFraction = (1 - alpha) * settings1.VoicedFraction + alpha * settings2.VoicedFraction;
            for (int iSinusoid = 0; iSinusoid < settings1.AmplitudeList.Count; iSinusoid++)
            {
                interpolatedSettings.AmplitudeList[iSinusoid] = (1 - alpha) * settings1.AmplitudeList[iSinusoid] +
                                                                alpha * settings2.AmplitudeList[iSinusoid];
                interpolatedSettings.FrequencyList[iSinusoid] = (1 - alpha) * settings1.FrequencyList[iSinusoid] +
                                                                alpha * settings2.FrequencyList[iSinusoid];
                interpolatedSettings.BandwidthList[iSinusoid] = (1 - alpha) * settings1.BandwidthList[iSinusoid] +
                                                                alpha * settings2.BandwidthList[iSinusoid];
            }
            return interpolatedSettings;
        }

        public void SetSilence(double duration)
        {
            this.duration = duration;
            transitionStart = 1; // Default value for silence: directly start interpolating towards the first non-silence.
            for (int iSinusoid = 0; iSinusoid < frequencyList.Count; iSinusoid++)
            {
                amplitudeList[iSinusoid] = 0;
            }
            relativePitchVariationParameterList = new List<double>() { 1.0, 0.0, 0.0 };
            relativeAmplitudeVariationParameterList = new List<double>() { 1.0, 0.0, 0.0 };  
        }

        [DataMember]
        public double Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        [DataMember]
        public double VoicedFraction
        {
            get { return voicedFraction; }
            set { voicedFraction = value; }
        }

        [DataMember]
        public double TransitionStart
        {
            get { return transitionStart; }
            set { transitionStart = value; }
        }

        [DataMember]
        public List<double> FrequencyList
        {
            get { return frequencyList; }
            set { frequencyList = value; }
        }

        [DataMember]
        public List<double> AmplitudeList
        {
            get { return amplitudeList; }
            set { amplitudeList = value; }
        }

        [DataMember]
        public List<double> BandwidthList
        {
            get { return bandwidthList; }
            set { bandwidthList = value; }
        }

        [DataMember]
        public List<double> RelativePitchVariationParameterList
        {
            get { return relativePitchVariationParameterList; }
            set { relativePitchVariationParameterList = value; }
        }

        [DataMember]
        public List<double> RelativeAmplitudeVariationParameterList
        {
            get { return relativeAmplitudeVariationParameterList; }
            set { relativeAmplitudeVariationParameterList = value; }
        }
    }
}
