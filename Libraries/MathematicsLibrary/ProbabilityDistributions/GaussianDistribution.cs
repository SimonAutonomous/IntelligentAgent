using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathematicsLibrary.ProbabilityDistributions
{
    public class GaussianDistribution
    {
        private double mean;
        private double variance;
        private double standardDeviation;
        private Random random;

        public GaussianDistribution(double mean, double variance, int randSeed)
        {
            if (randSeed < 0) {random = new Random();}
            else { random = new Random(randSeed);}
            this.mean = mean;
            this.variance = variance;
            this.standardDeviation = Math.Sqrt(variance);
        }

        public double GetSample()
        {
            double u1 = random.NextDouble();
            double u2 = random.NextDouble();
            double normalizedSample = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double sample = mean + standardDeviation * normalizedSample;
            return sample;
        }
    }
}
