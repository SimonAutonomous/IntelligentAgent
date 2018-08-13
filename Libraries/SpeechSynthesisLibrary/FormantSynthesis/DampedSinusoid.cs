using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeechSynthesisLibrary.FormantSynthesis
{
    public class DampedSinusoid
    {
        private double bandWidth;
        private double frequency;
        private double amplitude;
        private int samplingFrequency;

        private double sampleTime;
        private int index;
        private double y;
        private double yPrevious;
        private double yBeforePrevious;
        private double xPrevious;
        private double a1;
        private double a2;
        private double b1;

        private List<double> sampleList = null;

        public DampedSinusoid(int samplingFrequency)
        {
            this.samplingFrequency = samplingFrequency;
            index = 0;
            yPrevious = 0;
            yBeforePrevious = 0;
            xPrevious = 0;
            sampleTime = 1.0 / (double)samplingFrequency;
        }

        // Used in case the parameters are constant over an entire sound.
        public DampedSinusoid(double amplitude, double frequency, double bandWidth, int samplingFrequency)
        {
            this.amplitude = amplitude;
            this.bandWidth = bandWidth;
            this.frequency = frequency;
            this.samplingFrequency = samplingFrequency;
            index = 0;
            yPrevious = 0;
            yBeforePrevious = 0;
            xPrevious = 0;
            double sampleTime = 1.0 / (double)samplingFrequency;
            a1 = -2 * Math.Exp(-bandWidth * Math.PI * sampleTime) * Math.Cos(2 * Math.PI * frequency * sampleTime);
            a2 = Math.Exp(-2 * bandWidth * Math.PI * sampleTime);
            b1 = Math.Exp(-bandWidth * Math.PI * sampleTime) * Math.Sin(2 * Math.PI * frequency * sampleTime);
        }

        public void SetParameters(double amplitude, double frequency, double bandWidth)
        {
            this.amplitude = amplitude;
            this.bandWidth = bandWidth;
            this.frequency = frequency;
            a1 = -2 * Math.Exp(-bandWidth * Math.PI * sampleTime) * Math.Cos(2 * Math.PI * frequency * sampleTime);
            a2 = Math.Exp(-2 * bandWidth * Math.PI * sampleTime);
            b1 = Math.Exp(-bandWidth * Math.PI * sampleTime) * Math.Sin(2 * Math.PI * frequency * sampleTime);
        }

        public void GenerateExplicitly(double duration)
        {
            sampleList = new List<double>();
            int numberOfSamples = (int)Math.Round(samplingFrequency * duration);
            double sampleTime = 1.0/(double)samplingFrequency;
            for (int ii = 0; ii < numberOfSamples; ii++)
            {
                double time = ii * sampleTime;
                double sample = amplitude * Math.Exp(-this.bandWidth * Math.PI * time) * Math.Sin(2 * Math.PI * frequency * time);
                sampleList.Add(sample);
            }
        }

        public void Generate(double duration)
        {
            sampleList = new List<double>();
            int numberOfSamples = (int)Math.Round(samplingFrequency * duration);
            double sampleTime = 1.0 / (double)samplingFrequency;
            a1 = -2 * Math.Exp(-bandWidth * Math.PI * sampleTime) * Math.Cos(2 * Math.PI * frequency * sampleTime);
            a2 = Math.Exp(-2 * bandWidth * Math.PI * sampleTime);
            b1 = Math.Exp(-bandWidth * Math.PI * sampleTime) * Math.Sin(2 * Math.PI * frequency * sampleTime);
            double y2 = 0;
            double y1 = 0;
            double y = 0;
            sampleList.Add(y*amplitude);
            y = b1;
            sampleList.Add(y*amplitude);
            int ii = 2;
            while (ii < numberOfSamples)
            {
                y2 = y1;
                y1 = y;
                y = -a1 * y1 - a2 * y2;
                sampleList.Add(y*amplitude);
                ii++;
            }
        }

        public double Next(double x)
        {
            x *= amplitude;
            yBeforePrevious = yPrevious;
            yPrevious = y;
            y = b1 * xPrevious - a1 * yPrevious - a2 * yBeforePrevious;
            xPrevious = x;
            return y;
        }

        public List<double> SampleList
        {
            get { return sampleList; }
        }
    }
}
