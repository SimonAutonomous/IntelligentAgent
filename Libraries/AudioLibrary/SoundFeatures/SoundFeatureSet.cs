using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AudioLibrary.SoundFeatures
{
    [DataContract]
    public class SoundFeatureSet
    {
        #region Fields
        private string information;
        private List<SoundFeature> featureList;
        #endregion

        #region Constructor
        public SoundFeatureSet()
        {
            information = "";
            featureList = new List<SoundFeature>();
        }
        #endregion

        #region Public methods
        // Sets the time values of the constituent features to the range [0,1],
        // assuming that the frames are equally spaced in time (which is always
        // the case, by construction).
        public void SetNormalizedTime()
        {
            foreach (SoundFeature soundFeature in featureList)
            {
                soundFeature.SetNormalizedTime();
            }
        }

        // (Linearly) interpolates each feature, generating n (=numberOfValues) values
        // for the feature.
        public void Interpolate(int numberOfValues)
        {
            foreach (SoundFeature soundFeature in featureList)
            {
                soundFeature.Interpolate(numberOfValues);
            }
        }

        // Generates the average sound feature set from a list of sound feature sets (one for
        // each instance of a sound). It is assumed that all sound feature sets contain the
        // same features, in the same order.
        public static SoundFeatureSet GenerateAverage(List<SoundFeatureSet> soundFeatureSetList)
        {
            SoundFeatureSet averageSoundFeatureSet = new SoundFeatureSet();
            int numberOfFeatures = soundFeatureSetList[0].FeatureList.Count;
            for (int iFeature = 0; iFeature < numberOfFeatures; iFeature++)
            {
                List<SoundFeature> soundFeatureList = new List<SoundFeature>();
                for (int iInstance = 0; iInstance < soundFeatureSetList.Count; iInstance++)
                {
                    soundFeatureList.Add(soundFeatureSetList[iInstance].FeatureList[iFeature]);
                }
                SoundFeature averageFeature = SoundFeature.GenerateAverage(soundFeatureList);
                averageFeature.NumberOfInstances = soundFeatureSetList.Count; // Number of instances used when forming the average.
                averageSoundFeatureSet.FeatureList.Add(averageFeature);
            }
            return averageSoundFeatureSet;
        }

        public static double GetDeviation(SoundFeatureSet soundFeatureSet1, SoundFeatureSet soundFeatureSet2, List<double> weightList)
        {
            if (soundFeatureSet1.FeatureList.Count != soundFeatureSet2.FeatureList.Count) { return double.MaxValue; }
            else
            {
                double deviation = 0;
                int numberOfNonZeroWeights = 0;
                for (int iFeature = 0; iFeature < soundFeatureSet1.FeatureList.Count; iFeature++)
                {
                    double weight = weightList[iFeature];
                    if (weight > double.Epsilon)
                    {
                        numberOfNonZeroWeights++;
                        double featureDeviation = 0;
                        SoundFeature feature1 = soundFeatureSet1.FeatureList[iFeature];
                        SoundFeature feature2 = soundFeatureSet2.FeatureList[iFeature];
                        if (feature1.TimeList.Count != feature2.TimeList.Count)
                        {
                            return double.MaxValue;
                        }
                        else
                        {
                            for (int jj = 0; jj < feature1.TimeList.Count; jj++)
                            {
                                featureDeviation += (feature1.ValueList[jj] - feature2.ValueList[jj]) *
                                                    (feature1.ValueList[jj] - feature2.ValueList[jj]);
                            }
                        }
                        deviation += weight*Math.Sqrt(featureDeviation / (double)feature1.ValueList.Count);
                    }
                }
                deviation /= (double)soundFeatureSet1.FeatureList.Count;
                deviation /= (double)numberOfNonZeroWeights;
                return deviation;
            }
        }
        #endregion

        #region Properties
        [DataMember]
        public string Information
        {
            get { return information; }
            set { information = value; }
        }

        [DataMember]
        public List<SoundFeature> FeatureList
        {
            get { return featureList; }
            set { featureList = value; }
        }
        #endregion
    }
}
