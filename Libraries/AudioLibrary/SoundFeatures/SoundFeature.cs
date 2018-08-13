using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using MathematicsLibrary.Interpolation;
using MathematicsLibrary.Statistics;

namespace AudioLibrary.SoundFeatures
{
    [DataContract]
    public class SoundFeature
    {
        #region Fields
        private string name;
        private List<double> timeList;
        private List<double> valueList;
        private List<double> varianceList; // Values non-zero only in case the feature values represent averages over several sounds
        private int numberOfInstances; // Used only when the sound feature values represent averages of several sounds
        #endregion

        #region Constructor
        public SoundFeature()
        {
            name = "";
            timeList = new List<double>();
            valueList = new List<double>();
            varianceList = new List<double>();
            numberOfInstances = 1;
        }
        #endregion

        #region Public methods
        public void SetSize(int size)
        {
            timeList = new List<double>();
            valueList = new List<double>();
            varianceList = new List<double>();
            for (int ii = 0; ii < size; ii++)
            {
                timeList.Add(0);
                valueList.Add(0);
                varianceList.Add(0);
            }
            numberOfInstances = 1; // Default value
        }

        // Sets the time values of the feature to the range [0,1],
        // assuming that the frames are equally spaced in time (which is always
        // the case, by construction).
        public void SetNormalizedTime()
        {
            for (int ii = 0; ii < timeList.Count; ii++)
            {
                timeList[ii] = ii / (double)(timeList.Count - 1);
            }
        }

        // (Linearly) interpolates the feature, generating n (=numberOfValues) values
        // for the feature.
        public void Interpolate(int numberOfValues)
        {
            List<List<double>> timeValueList = new List<List<double>>() { timeList, valueList };
            List<List<double>> interpolatedTimeValueList = LinearInterpolation.Interpolate(timeValueList, numberOfValues);
            List<List<double>> timeVarianceList = new List<List<double>>() { timeList, varianceList };
            List<List<double>> interpolatedTimeVarianceList = LinearInterpolation.Interpolate(timeVarianceList, numberOfValues);
            timeList = interpolatedTimeValueList[0];
            valueList = interpolatedTimeValueList[1];
            varianceList = interpolatedTimeVarianceList[1];
        }

        // Generates the average (and variance) of the list of sound features, ASSUMING
        // that all sound features use the same time values
        public static SoundFeature GenerateAverage(List<SoundFeature> soundFeatureList)
        {
            SoundFeature averageSoundFeature = new SoundFeature();
            if (soundFeatureList.Count > 0)
            {
                averageSoundFeature.Name = soundFeatureList[0].Name;
                int size = soundFeatureList[0].TimeList.Count;
                averageSoundFeature.SetSize(size);
                for (int jj = 0; jj < size; jj++)
                {
                    averageSoundFeature.TimeList[jj] = soundFeatureList[0].TimeList[jj];
                }
                for (int jj = 0; jj < size; jj++)
                {
                    List<double> featureValueList = new List<double>();
                    for (int ii = 0; ii < soundFeatureList.Count; ii++)
                    {
                        featureValueList.Add(soundFeatureList[ii].ValueList[jj]);
                    }
                    double average = featureValueList.Average();
                    double variance = Variance.Compute(featureValueList);
                    averageSoundFeature.ValueList[jj] = average;
                    averageSoundFeature.VarianceList[jj] = variance;
                }
            }
            return averageSoundFeature;
        }
        #endregion

        #region Properties
        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public List<double> TimeList
        {
            get { return timeList; }
            set { timeList = value; }
        }

        [DataMember]
        public List<double> ValueList
        {
            get { return valueList; }
            set { valueList = value; }
        }

        [DataMember]
        public List<double> VarianceList
        {
            get { return varianceList; }
            set { varianceList = value; }
        }

        [DataMember]
        public int NumberOfInstances
        {
            get { return numberOfInstances; }
            set { numberOfInstances = value; }
        }
        #endregion
    }
}
