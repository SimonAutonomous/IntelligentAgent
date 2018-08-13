using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeechSynthesisLibrary
{
    public class PulseSignal
    {
        private double pulseTime; // The time at which the pulse (duration = 1 sample) occurs.
        private double amplitude;

        public double PulseTime
        {
            get { return pulseTime; }
            set { pulseTime = value; }
        }

        public double Amplitude
        {
            get { return amplitude; }
            set { amplitude = value; }
        }
    }
}
