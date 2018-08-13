using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using AudioLibrary.SoundFeatures;
using MathematicsLibrary.SignalProcessing;
using MathematicsLibrary.Vectors;
using MathematicsLibrary.Matrices;

namespace AudioLibrary
{
    #region Comments
    /// The WAVSound class is used for representing and
    /// manipulating uncompressed WAV sounds.
    /// It can handle both single channel (mono) and
    /// double channel (stereo) sounds. It is assumed
    /// that individual sound samples are stored with
    /// 16-bit accuracy
    #endregion

    [Serializable]
    public class WAVSound
    {
        #region Constants
        // A bit ugly, but since only 16-bit format is used here...
        private const int MINIMUM_SAMPLE = -32768;
        private const int MAXIMUM_SAMPLE = 32767;
        private const int BOTTOM_2S_COMPLEMENT = -65536;
        #endregion

        #region Fields
        private string name;
        private string soundName;
        private string description; // Optional - not saved during SaveToFile nor loaded during LoadFromFile
        private byte[] soundData;
        private string chunkID = "";
        private int chunkSize;
        private string riffType = "";
        private string formatSubChunkID = "";
        private int formatSubChunkSize;
        private Int16 audioFormat;
        private Int16 numberOfChannels;
        private int sampleRate;
        private int byteRate;
        private Int16 blockAlign;
        private Int16 bitsPerSample;
        private Int16 numberOfExtraFormatBytes;
        private string dataSubChunkID;
        private int dataSubChunkSize;
        private List<List<Int16>> samples;
        private ASCIIEncoding asciiEncoding;
        private MemoryStream wAVMemoryStream;

        #endregion

        #region Constructors

        public WAVSound()
        {
            asciiEncoding = new ASCIIEncoding();
        }

        #region Comments
        /// This method generates an empty sound, i.e. only a
        /// sound header, but no sound samples.
        #endregion
        public WAVSound(string _name, int _sampleRate, Int16 _numberOfChannels, Int16 _bitsPerSample)
        {
            asciiEncoding = new ASCIIEncoding();
            int currentPosition = 0;
            soundData = new byte[44]; // WAV file without data, and without a fact subchunk. No extra bytes in the format subchunk.
            name = _name;

            // Main chunk:
            chunkID = "RIFF";
            byte[] chunkIDAsBytes = asciiEncoding.GetBytes(chunkID);
            chunkIDAsBytes.CopyTo(soundData, currentPosition);
            currentPosition += 4;
            chunkSize = 36; // to be determined once the samples have been added... (soundData.Length - 8 = 36 for an empty sound).
            byte[] chunkSizeAsBytes = BitConverter.GetBytes(chunkSize);
            chunkSizeAsBytes.CopyTo(soundData, currentPosition);
            currentPosition += 4;
            riffType = "WAVE";
            byte[] riffTypeAsBytes = asciiEncoding.GetBytes(riffType);
            riffTypeAsBytes.CopyTo(soundData, currentPosition);
            currentPosition += 4;

            // Format subchunk:
            formatSubChunkID = "fmt ";
            byte[] formatSubChunkIDAsBytes = asciiEncoding.GetBytes(formatSubChunkID);
            formatSubChunkIDAsBytes.CopyTo(soundData, currentPosition);
            currentPosition += 4;
            formatSubChunkSize = 16;
            byte[] formatSubChunkSizeAsBytes = BitConverter.GetBytes(formatSubChunkSize);
            formatSubChunkSizeAsBytes.CopyTo(soundData, currentPosition);
            currentPosition += 4;
            audioFormat = 1;
            byte[] audioFormatAsBytes = BitConverter.GetBytes(audioFormat);
            audioFormatAsBytes.CopyTo(soundData, currentPosition);
            currentPosition += 2;
            numberOfChannels = _numberOfChannels;
            byte[] numberOfChannelsAsBytes = BitConverter.GetBytes(numberOfChannels);
            numberOfChannelsAsBytes.CopyTo(soundData, currentPosition);
            currentPosition += 2;
            sampleRate = _sampleRate;
            byte[] sampleRateAsBytes = BitConverter.GetBytes(sampleRate);
            sampleRateAsBytes.CopyTo(soundData, currentPosition);
            currentPosition += 4;
            byteRate = sampleRate * _bitsPerSample * numberOfChannels / 8;
            byte[] byteRateAsBytes = BitConverter.GetBytes(byteRate);
            byteRateAsBytes.CopyTo(soundData, currentPosition);
            currentPosition += 4;
            blockAlign = (Int16)(_bitsPerSample * numberOfChannels / 8);
            byte[] blockAlignAsBytes = BitConverter.GetBytes(blockAlign);
            blockAlignAsBytes.CopyTo(soundData, currentPosition);
            currentPosition += 2;
            bitsPerSample = _bitsPerSample;
            byte[] bitsPerSampleAsBytes = BitConverter.GetBytes(bitsPerSample);
            bitsPerSampleAsBytes.CopyTo(soundData, currentPosition);
            currentPosition += 2;

            // Data subchunk: (empty so far)
            dataSubChunkID = "data";
            byte[] dataSubChunkIDAsBytes = asciiEncoding.GetBytes(dataSubChunkID);
            dataSubChunkIDAsBytes.CopyTo(soundData, currentPosition);
            currentPosition += 4;
            dataSubChunkSize = 0;
            byte[] dataSubChunkSizeAsBytes = BitConverter.GetBytes(dataSubChunkSize);
            dataSubChunkSizeAsBytes.CopyTo(soundData, currentPosition);
            currentPosition += 4;

            // Do not include the (unnecessary) fact subchunk
            //    factSubChunkSize = 0;
        }
        #endregion

        #region Private methods
        #region Comments
        /// This method takes the raw sound data (which includes the header and the
        /// actual sample data, both stored in a single byte array (SoundData), and
        /// converts them to the WAV format. The method will include the fmt, 
        /// fact, and data subchunks (all that are needed for an uncompressed WAV
        /// sound), all other subchunks are unnecessary and are removed.
        #endregion
        public void ExtractInformation()
        {
            int currentPosition = 0;
            chunkID = asciiEncoding.GetString(soundData, 0, 4);
            if (chunkID != "RIFF")
            {
                try
                {
                    throw new Exception();
                }
                catch
                {
                    //         MessageBox.Show("Incorrect file format (not a WAV file).");
                    return;
                }
            }
            chunkSize = BitConverter.ToInt32(soundData, 4);
            riffType = asciiEncoding.GetString(soundData, 8, 4);
            if (riffType != "WAVE")
            {
                try
                {
                    throw new Exception();
                }
                catch
                {
                    //          MessageBox.Show("Incorrect file format (not a WAV file).");
                    return;
                }
            }
            currentPosition = 12;
            string subChunkID = asciiEncoding.GetString(soundData, currentPosition, 4);
            int subChunkSize = BitConverter.ToInt32(soundData, currentPosition + 4);
            while (currentPosition + subChunkSize < soundData.Length)
            {
                if (subChunkID == "fmt ")
                {
                    ExtractFormatSubchunk(subChunkSize, currentPosition);
                }
                else if (subChunkID == "data")
                {
                    ExtractDataSubChunk(subChunkSize, currentPosition);
                }
                else
                {
                    // Unnecessary subchunk - remove
                    int nBytesToCopy = soundData.Length - subChunkSize - 8;
                    byte[] tmpData = new byte[nBytesToCopy];
                    Array.Copy(soundData, 0, tmpData, 0, currentPosition);
                    int nBytesAfterSubchunk = soundData.Length - (8 + subChunkSize) - currentPosition;
                    Array.Copy(soundData, currentPosition + (8 + subChunkSize), tmpData, currentPosition, nBytesAfterSubchunk);
                    soundData = new byte[nBytesToCopy];
                    tmpData.CopyTo(soundData, 0);
                    currentPosition -= (subChunkSize + 8); // Needed here, since this number is added to currentPosition below. 
                }
                currentPosition += subChunkSize + 8;
                if (currentPosition < soundData.Length - 8)
                {
                    subChunkID = asciiEncoding.GetString(soundData, currentPosition, 4);
                    subChunkSize = BitConverter.ToInt32(soundData, currentPosition + 4);
                }
            }
        }

        #region Comments
        /// This method extracts the data subchunk, i.e. the part of the WAV sound that
        /// contains the actual sound samples, given the subchunkSize and the startByte
        /// (for the data subchunk). The samples are extracted by calling the
        /// GenerateSamples() method.
        #endregion
        private void ExtractDataSubChunk(int subChunkSize, int startByte)
        {
            dataSubChunkID = asciiEncoding.GetString(soundData, startByte, 4);
            dataSubChunkSize = BitConverter.ToInt32(soundData, startByte + 4);
            GenerateSamples();
        }

        #region Comments
        /// This method extracts the format subchunk (that specifies the audioformat), given the SoundData, 
        /// the size of the subchunk, and its starting byte within the SoundData. Since the library deals
        /// with uncompressed sounds, any extra format bytes are ignored.
        #endregion
        private void ExtractFormatSubchunk(int subChunkSize, int startByte)
        {
            formatSubChunkID = asciiEncoding.GetString(soundData, startByte, 4);
            formatSubChunkSize = BitConverter.ToInt32(soundData, startByte + 4);
            audioFormat = BitConverter.ToInt16(soundData, startByte + 8);  // Should be = 1 (uncompressed sound)
            numberOfChannels = BitConverter.ToInt16(soundData, startByte + 10);
            sampleRate = BitConverter.ToInt32(soundData, startByte + 12);
            byteRate = BitConverter.ToInt32(soundData, startByte + 16);
            blockAlign = BitConverter.ToInt16(soundData, startByte + 20);
            bitsPerSample = BitConverter.ToInt16(soundData, startByte + 22);
            if (subChunkSize >= 18)
            {
                numberOfExtraFormatBytes = BitConverter.ToInt16(soundData, startByte + 24);
            }
            // Ignore the extra bytes, if any, i.e. do not read them: Not used.
        }

        #region Comments
        /// This method converts the raw sound data (in soundData) into samples (in the
        /// range [-32768,32767]). 
        /// Note: In the AudioLibrary, it is assumed that 16 bits per sample is used.
        /// The method can handle both single channel (mono) and stereo sound.
        #endregion
        private void GenerateSamples()
        {
            int headerSize = GetHeaderSize();
            if (numberOfChannels == 1)
            {
                samples = new List<List<short>>();
                byte[] rawSamples = new byte[dataSubChunkSize];
                for (int ii = 0; ii < dataSubChunkSize; ii++)
                {
                    rawSamples[ii] = soundData[headerSize + ii];
                }
                int counter = 0;
                List<Int16> monoSamples = new List<Int16>();
                Int16 tmp = 0;
                while (counter < rawSamples.Length)
                {
                    tmp = (Int16)(rawSamples[counter + 1] * (Int16)256 + rawSamples[counter]);
                    if (tmp <= MAXIMUM_SAMPLE)
                    {
                        monoSamples.Add(tmp);
                    }
                    else
                    {
                        monoSamples.Add((Int16)(BOTTOM_2S_COMPLEMENT + tmp));
                    }
                    counter++;
                    counter++;
                }
                samples.Add(monoSamples);
            }
            else if (numberOfChannels == 2)
            {
                samples = new List<List<short>>();
                byte[] rawSamples = new byte[dataSubChunkSize];
                for (int ii = 0; ii < dataSubChunkSize; ii++)
                {
                    rawSamples[ii] = soundData[headerSize + ii];
                }
                int counter = 0;
                List<Int16> leftSamples = new List<Int16>();
                List<Int16> rightSamples = new List<Int16>();
                Int16 tmp = 0;
                while (counter < rawSamples.Length)
                {
                    tmp = (Int16)(rawSamples[counter + 1] * (Int16)256 + rawSamples[counter]);
                    if (tmp <= MAXIMUM_SAMPLE)
                    {
                        leftSamples.Add(tmp);
                    }
                    else
                    {
                        leftSamples.Add((Int16)(BOTTOM_2S_COMPLEMENT + tmp));
                    }
                    tmp = (Int16)(rawSamples[counter + 3] * (Int16)256 + rawSamples[counter + 2]);
                    if (tmp <= MAXIMUM_SAMPLE)
                    {
                        rightSamples.Add(tmp);
                    }
                    else
                    {
                        rightSamples.Add((Int16)(BOTTOM_2S_COMPLEMENT + tmp));
                    }
                    counter += 4;
                }
                samples.Add(leftSamples);
                samples.Add(rightSamples);
            }
        }
        #endregion

        #region Public methods
        public void SetDataSubchunkSizeInHeader(int size)
        {
            int currentPosition = 12; // Start at the first subchunk after the "RIFF" and "WAVE" information.
            string subChunkID = asciiEncoding.GetString(soundData, currentPosition, 4);
            int subChunkSize = BitConverter.ToInt32(soundData, currentPosition + 4);
            while (currentPosition + subChunkSize < soundData.Length)
            {
                if (subChunkID == "data") { break; } // In this case, the subChunkSize should be 0 here, since it has not been set yet.
                else
                {
                    currentPosition += subChunkSize + 8;
                    if (currentPosition < soundData.Length - 8)
                    {
                        subChunkID = asciiEncoding.GetString(soundData, currentPosition, 4);
                        subChunkSize = BitConverter.ToInt32(soundData, currentPosition + 4);
                    }
                }
            }

            // Data subchunk found: Now set the size:

        }

        public int GetNumberOfZeroCrossings(int channel)
        {
            int numberOfZeroCrossings = 0;
            for (int ii = 1; ii < samples[channel].Count; ii++)
            {
                int s0 = 0;
                if (ii > 1) s0 = Math.Sign(samples[channel][ii - 2]);
                int s1 = Math.Sign(samples[channel][ii - 1]);
                int s2 = Math.Sign(samples[channel][ii]);
                if (s1 * s2 == -1)
                {

                    numberOfZeroCrossings++;
                }
                else if (s1 == 0)
                {
                    if (s0 * s2 == -1)
                    {
                        numberOfZeroCrossings++;
                    }
                }
            }
            return numberOfZeroCrossings;
        }

        public double GetRelativeNumberOfZeroCrossings(int channel)
        {
            int numberOfZeroCrossings = GetNumberOfZeroCrossings(channel);
            double relativeNumberOfZeroCrossings = numberOfZeroCrossings / (double)(samples[0].Count - 1);
            return relativeNumberOfZeroCrossings;
        }

        public List<int> GetNumberOfZeroCrossings()
        {
            List<int> zeroCrossingsList = new List<int>();
            for (int ii = 0; ii < this.numberOfChannels; ii++)
            {
                int channelZeroCrossings = GetNumberOfZeroCrossings(ii);
                zeroCrossingsList.Add(channelZeroCrossings);
            }
            return zeroCrossingsList;
        }

        public void SubtractMean(int channel)
        {
            double averageSample = 0;
            for (int ii = 0; ii < samples[channel].Count; ii++)
            {
                averageSample += samples[channel][ii];
            }
            averageSample /= samples[channel].Count;
            Int16 shortAverageSample = (Int16)Math.Round(averageSample);
            for (int ii = 0; ii < samples[channel].Count; ii++)
            {
                int tmpSample = (int)samples[channel][ii] - (int)shortAverageSample;
                if (tmpSample > MAXIMUM_SAMPLE) tmpSample = MAXIMUM_SAMPLE;
                else if (tmpSample < MINIMUM_SAMPLE) tmpSample = MINIMUM_SAMPLE;
                samples[channel][ii] = (Int16)tmpSample;
            }
            GenerateSoundDataFromSamples();  // Important, since the Copy() method used the sound data! NOTE!
        }

        public void SubtractMean()
        {
            for (int ii = 0; ii < this.numberOfChannels; ii++)
            {
                SubtractMean(ii);
            }
        }

        #region Comments
        ///
        ///
        #endregion
        public double GetFirstTimeAboveThreshold(int channel, int numberOfSamplesInAverage, double threshold)
        {
            double firstTimeAboveThreshold = -1; // Returned if NO value above threshold is found.
            if (samples[channel].Count > numberOfSamplesInAverage)
            {
                double sumThreshold = numberOfSamplesInAverage * threshold;
                double sampleModulusSum = 0;
                for (int ii = 0; ii < numberOfSamplesInAverage; ii++)
                {
                    sampleModulusSum += Math.Abs((double)samples[channel][ii]);
                }
                if (sampleModulusSum >= sumThreshold)
                {
                    firstTimeAboveThreshold = GetTimeAtSampleIndex(0);
                }
                else
                {
                    int index = 1;
                    while (index < (samples[channel].Count - 1 - numberOfSamplesInAverage))
                    {
                        int sampleIndexToRemove = index - 1;
                        int sampleIndexToAdd = index + numberOfSamplesInAverage - 1;
                        sampleModulusSum += (-Math.Abs((double)samples[channel][sampleIndexToRemove]) + Math.Abs((double)samples[channel][sampleIndexToAdd]));
                        if (sampleModulusSum >= sumThreshold) { break; }
                        else { index++; }
                    }
                    if (sampleModulusSum >= sumThreshold)
                    {
                        firstTimeAboveThreshold = GetTimeAtSampleIndex(index);
                    }
                }
            }
            return firstTimeAboveThreshold;
        }

        #region Comments
        ///
        ///
        #endregion
        public double GetLastTimeAboveThreshold(int channel, int numberOfSamplesInAverage, double threshold)
        {
            double lastTimeAboveThreshold = -1; // Returned if NO value above threshold is found.
            double sumThreshold = numberOfSamplesInAverage * threshold;
            double sampleModulusSum = 0;
            for (int ii = samples[channel].Count - 1; ii >= (samples[channel].Count - numberOfSamplesInAverage); ii--)
            {
                sampleModulusSum += Math.Abs((double)samples[channel][ii]);
            }
            if (sampleModulusSum >= sumThreshold)
            {
                lastTimeAboveThreshold = GetTimeAtSampleIndex(samples[channel].Count - 1);
            }
            else
            {
                int index = samples[channel].Count - 2;
                while (index >= numberOfSamplesInAverage) // 0)
                {
                    int sampleIndexToRemove = index + 1;
                    int sampleIndexToAdd = index - numberOfSamplesInAverage + 1;
                    sampleModulusSum += (-Math.Abs((double)samples[channel][sampleIndexToRemove]) + Math.Abs((double)samples[channel][sampleIndexToAdd]));
                    if (sampleModulusSum >= sumThreshold) { break; }
                    else { index--; }
                }
                if (sampleModulusSum >= sumThreshold)
                {
                    lastTimeAboveThreshold = GetTimeAtSampleIndex(index);
                }
            }
            return lastTimeAboveThreshold;
        }

        #region Comments
        /// This method returns an exact copy of the sound.
        #endregion
        public WAVSound Copy()
        {
            WAVSound w = new WAVSound();
            w.Name = this.Name;
            w.Description = this.Description;
            w.SoundName = this.SoundName; // ?
            w.SoundData = new byte[this.SoundData.Length];
            for (int ii = 0; ii < this.SoundData.Length; ii++)
            {
                w.SoundData[ii] = this.SoundData[ii];
            }
            w.ExtractInformation();
            return w;
        }

        #region Comments
        /// This method return the header size, i.e. the size of
        /// of the file minus the size of the data samples.
        #endregion
        public int GetHeaderSize()
        {
            int numberOfHeaderBytes = 12 + (8 + formatSubChunkSize) + 8;
            return numberOfHeaderBytes;
        }

        #region Comments
        /// This method returns the data size, i.e. the size (number
        /// of bytes) of the data. Note that this number is generally
        /// NOT equal to the number of samples: Each sample typically
        /// takes up 2 bytes (16-bit mono sound) or 4 bytes
        /// (16-bit stereo sound, 2 bytes for the left sample and 2 for the right).
        #endregion
        public int GetDataSize()
        {
            int headerSize = GetHeaderSize();
            int dataSize = soundData.Length - headerSize;
            return dataSize;
        }

        #region Comments
        /// This method returns (as a double) the duration of the sound, computed
        /// as the number of samples (left or right) divided by the sample rate.
        #endregion
        public double GetDuration()
        {
            return samples[0].Count / (double)(sampleRate);
        }

        #region Comments
        /// Reads a sound from an (uncompressed) WAV file. 
        /// Note that, before calling this method, one must call the basic constructor WAVSound(),
        /// to generate an empty sound.
        /// Note also that the Name property is set to the file name (without the suffix), and
        /// the SoundName property is set to the name but without the identifying ID number, which
        /// must follow the "_". Valid file names are:
        /// (1) [SoundName].wav (in which case the SoundName equals the Name)
        /// (2) [SoundName]_[ID].wav (where [ID] is an integer)
        /// (3) [SoundName]_[ID]_[Description].wav (in which case the description field is set as well).
        #endregion
        public void LoadFromFile(string fileName)
        {
            name = Path.GetFileNameWithoutExtension(fileName);
            if (name.Contains('_'))
            {
                char[] splitParameters = new char[] { '_' };
                string[] nameSplit = name.Split(splitParameters, StringSplitOptions.RemoveEmptyEntries);
                soundName = nameSplit[0];
                if (nameSplit.Count() == 3) // description available
                {
                    description = nameSplit[2];
                }
            }
            else
            {
                soundName = name;
            }
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            soundData = new byte[fs.Length];
            fs.Position = 0;
            fs.Read(soundData, 0, soundData.Length);
            fs.Close();
            ExtractInformation();
        }

        #region Comments
        /// This method saves a sound (in WAV format) to a file.
        #endregion
        public void SaveToFile(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            fs.Position = 0;
            fs.Write(soundData, 0, soundData.Length);
            fs.Close();
        }

        // This method generates a sound from a sequence of samples. It is assumed that
        // the header (without the data subchunk size specification) has been generated
        // using the constructor WAVSound(string name, int _sampleRate, Int16 _numberOfChannels, Int16 _bitsPerSample).
        // The method generates the soundData corresponding to the data subchunk, and then sets
        // the size of data subchunk as well as the total data size.
        //
        // NOTE: This method assumes that the header ONLY contains the mandatory subchunks, i.e. it assumes that
        // there is no fact subchunk and no list (or other) subchunk.
        //
        public void GenerateFromSamples(List<List<Int16>> sampleList)
        {
            // Append the samples:
            AppendSamples(sampleList);

            // Set the size of the soundData byte array:
            int addedSoundDataSize = 0;
            if (numberOfChannels == 1) { addedSoundDataSize = sampleList[0].Count * bitsPerSample / 8; }
            else if (numberOfChannels == 2) { addedSoundDataSize = sampleList[0].Count * 2 * bitsPerSample / 8; }
            int headerSize = GetHeaderSize();
            byte[] tmpData = new byte[headerSize];
            soundData.CopyTo(tmpData, 0);
            soundData = new byte[headerSize + addedSoundDataSize];
            tmpData.CopyTo(soundData, 0);

            // Generate the sound data:
            GenerateSoundDataFromSamples(); // Adds the sound data after the header, but sets neither the data subchunk size nor the total size in the header.            

            // Assign the size bytes (data subchunk and total size):
            byte[] chunkSizeAsBytes = BitConverter.GetBytes(soundData.Length - 8);
            chunkSizeAsBytes.CopyTo(soundData, 4);
            int dataSize = GetDataSize();
            byte[] dataSubChunkSizeAsBytes = BitConverter.GetBytes(dataSize);
            dataSubChunkSizeAsBytes.CopyTo(soundData, 40 + (formatSubChunkSize - 16));

            // Extract information, i.e. regenerate the samples and the header information.
            ExtractInformation();
        }

        // NOTE: This method is mainly used when modifying sounds. It assumes that the
        // soundData size is given.
        private void GenerateSoundDataFromSamples()
        {
            int counter = GetHeaderSize();
            if (numberOfChannels == 1)
            {
                for (int ii = 0; ii < samples[0].Count; ii++)
                {
                    byte[] monoBytes = BitConverter.GetBytes(samples[0][ii]);
                    soundData[counter] = monoBytes[0];
                    soundData[counter + 1] = monoBytes[1];
                    counter++;
                    counter++;
                }
            }
            else if (numberOfChannels == 2)
            {
                for (int ii = 0; ii < samples[0].Count; ii++)
                {
                    byte[] leftBytes = BitConverter.GetBytes(samples[0][ii]);
                    byte[] rightBytes = BitConverter.GetBytes(samples[1][ii]);
                    soundData[counter] = leftBytes[0];
                    soundData[counter + 1] = leftBytes[1];
                    soundData[counter + 2] = rightBytes[0];
                    soundData[counter + 3] = rightBytes[1];
                    counter += 4;
                }
            }
        }

        #region Comments
        /// Generates a (multiline) text string containing
        /// the header of the WAV sound.
        #endregion
        public string HeaderAsString()
        {
            string s = "";
            s = "Chunk ID:             " + chunkID + "\r\n";
            s += "Chunk size:           " + chunkSize.ToString() + "\r\n";
            s += "RIFF type:            " + riffType.ToString() + "\r\n";
            s += "Format subchunk ID:   " + formatSubChunkID + "\r\n";
            s += "Format subchunk size: " + formatSubChunkSize.ToString() + "\r\n";
            s += "Audio format:         " + audioFormat.ToString() + "\r\n";
            s += "Number of channels:   " + numberOfChannels.ToString() + "\r\n";
            s += "Sample rate:          " + sampleRate.ToString() + "\r\n";
            s += "Byte rate:            " + byteRate.ToString() + "\r\n";
            s += "Block align:          " + blockAlign.ToString() + "\r\n";
            s += "Bits per sample:      " + bitsPerSample.ToString() + "\r\n";
            if (numberOfExtraFormatBytes > 0)
            {
                s += "Number of extra format bytes: " + numberOfExtraFormatBytes.ToString() + "\r\n";
            }
            /*   if (factSubChunkID != "")
               {
                   s += "Fact subchunk ID: " + factSubChunkID + "\r\n";
                   s += "Fact subchunk size: " + factSubChunkSize.ToString() + "\r\n";
               }  */
            s += "Data subchunk ID:     " + dataSubChunkID + "\r\n";
            s += "Data subchunk size:   " + dataSubChunkSize.ToString() + "\r\n";
            return s;
        }

        public void GenerateMemoryStream()
        {
            wAVMemoryStream = new MemoryStream(soundData);
            wAVMemoryStream.Position = 0;
            wAVMemoryStream.Read(soundData, 0, soundData.Length);
            wAVMemoryStream.Position = 0; // Must be reset to 0 before playing the sound.
        }

        public int GetDataByteIndexAtTime(double time)
        {
            int sampleIndex = (int)(time * sampleRate);
            return sampleIndex * blockAlign;
        }

        public double GetTimeAtSampleIndex(int sampleIndex)
        {
            double time = (double)sampleIndex / (double)(sampleRate);
            return time;
        }

        public int GetSampleIndexAtTime(double time)
        {
            int sampleIndex = (int)Math.Round(time * sampleRate);
            if (sampleIndex >= samples[0].Count)
            {
                sampleIndex = samples[0].Count - 1;
            }
            return sampleIndex;
        }

        public WAVSound Extract(double startTime, double EndTime)
        {
            WAVSound extractedSound = new WAVSound();
            int firstByte = GetDataByteIndexAtTime(startTime);
            int lastByte = GetDataByteIndexAtTime(EndTime);
            if (firstByte > lastByte)
            {
                return null;
            }
            if (firstByte < 0) { firstByte = 0; }
            if (lastByte > this.SoundData.Count() - 1) { lastByte = this.SoundData.Count() - 1; }
            if (lastByte < 0) { return null; }  // => endTime < 0 (no sound available)
            int headerSize = GetHeaderSize();
            extractedSound.SoundData = new byte[headerSize + (lastByte - firstByte)];
            for (int ii = 0; ii < headerSize; ii++)
            {
                extractedSound.SoundData[ii] = soundData[ii];
            }
            for (int ii = firstByte; ii < lastByte; ii++)
            {
                int index = (ii - firstByte) + headerSize;
                extractedSound.SoundData[index] = soundData[ii + headerSize];
            }
            byte[] chunkSizeAsBytes = BitConverter.GetBytes(extractedSound.SoundData.Length - 1);
            chunkSizeAsBytes.CopyTo(extractedSound.SoundData, 4);
            byte[] dataChunkSizeAsBytes = BitConverter.GetBytes(lastByte - firstByte);
            dataChunkSizeAsBytes.CopyTo(extractedSound.SoundData, 40 + (formatSubChunkSize - 16));
            extractedSound.ExtractInformation();
            return extractedSound;
        }

        public void RemoveToEnd(double removalStartTime)
        {
            int firstByteToRemove = GetDataByteIndexAtTime(removalStartTime);
            if (firstByteToRemove > GetDataSize())
            {
                return;
            }
            byte[] newSoundData = new byte[GetHeaderSize() + firstByteToRemove];
            for (int ii = 0; ii < newSoundData.Length; ii++)
            {
                newSoundData[ii] = soundData[ii];
            }
            soundData = new byte[newSoundData.Length];
            newSoundData.CopyTo(soundData, 0);
            byte[] chunkSizeAsBytes = BitConverter.GetBytes(soundData.Length - 8);
            chunkSizeAsBytes.CopyTo(soundData, 4);
            byte[] dataSubChunkSizeAsBytes = BitConverter.GetBytes(firstByteToRemove);
            dataSubChunkSizeAsBytes.CopyTo(soundData, 40 + (formatSubChunkSize - 16));
            ExtractInformation();
        }

        public void RemoveToTime(double removalEndTime)
        {
            int lastByteToRemove = GetDataByteIndexAtTime(removalEndTime);
            if (lastByteToRemove > GetDataSize())
            {
                return;
            }
            int remainingNumberofDataBytes = GetDataSize() - lastByteToRemove;
            byte[] newSoundData = new byte[GetHeaderSize() + remainingNumberofDataBytes];
            for (int ii = 0; ii < GetHeaderSize(); ii++)
            {
                newSoundData[ii] = soundData[ii];
            }
            for (int ii = GetHeaderSize(); ii < newSoundData.Length; ii++)
            {
                newSoundData[ii] = soundData[ii + lastByteToRemove];
            }
            soundData = new byte[newSoundData.Length];
            newSoundData.CopyTo(soundData, 0);
            byte[] chunkSizeAsBytes = BitConverter.GetBytes(soundData.Length - 8);
            chunkSizeAsBytes.CopyTo(soundData, 4);
            byte[] dataSubChunkSizeAsBytes = BitConverter.GetBytes(remainingNumberofDataBytes);
            dataSubChunkSizeAsBytes.CopyTo(soundData, 40 + (formatSubChunkSize - 16));
            ExtractInformation();

        }

        public void Shorten(double startTime, double endTime)
        {
            RemoveToEnd(endTime);
            RemoveToTime(startTime);
        }

        public void Append(WAVSound appendedSound)
        {
            int totalDataSize = GetDataSize() + appendedSound.GetDataSize();
            int headerSize = GetHeaderSize();
            int appendedSoundHeaderSize = appendedSound.GetHeaderSize();
            byte[] newSoundData = new byte[headerSize + totalDataSize];
            for (int ii = 0; ii < soundData.Length; ii++)
            {
                newSoundData[ii] = soundData[ii];
            }
            int appendedSoundDataSize = appendedSound.GetDataSize();
            for (int ii = 0; ii < appendedSoundDataSize; ii++)
            {
                newSoundData[ii + soundData.Length] = appendedSound.SoundData[appendedSoundHeaderSize + ii];
            }
            soundData = new byte[headerSize + totalDataSize];
            for (int ii = 0; ii < newSoundData.Length; ii++)
            {
                soundData[ii] = newSoundData[ii];
            }
            // Finalize header (set the total chunk size and the data subchunk size)
            byte[] chunkSizeAsBytes = BitConverter.GetBytes(soundData.Length - 8);
            chunkSizeAsBytes.CopyTo(soundData, 4);
            byte[] dataSubChunkSizeAsBytes = BitConverter.GetBytes(totalDataSize);
            dataSubChunkSizeAsBytes.CopyTo(soundData, 40 + (formatSubChunkSize - 16)); // the position of the bytes determining the data subchunk size.
            ExtractInformation(); // Needed in order to set all the header variables correctly for the new sound.
        }

        public void AppendSamplesAsBytes(byte[] appendedSamples)
        {
            int currentLength = soundData.Length;
            byte[] newSoundData = new byte[currentLength + appendedSamples.Length];
            for (int ii = 0; ii < soundData.Length; ii++)
            {
                newSoundData[ii] = soundData[ii];
            }
            for (int ii = 0; ii < appendedSamples.Length; ii++)
            {
                newSoundData[ii + currentLength] = appendedSamples[ii];
            }
            soundData = new byte[currentLength + appendedSamples.Length];
            for (int ii = 0; ii < newSoundData.Length; ii++)
            {
                soundData[ii] = newSoundData[ii];
            }
            // Finalize header (set the total chunk size and the data subchunk size)
            byte[] chunkSizeAsBytes = BitConverter.GetBytes(soundData.Length - 8);
            chunkSizeAsBytes.CopyTo(soundData, 4);
            byte[] dataSubChunkSizeAsBytes = BitConverter.GetBytes(appendedSamples.Length);
            dataSubChunkSizeAsBytes.CopyTo(soundData, 40 + (formatSubChunkSize - 16)); // the position of the bytes determining the data subchunk size.
            ExtractInformation();
        }

        public void AppendSamples(List<List<Int16>> _samples)
        {
            samples = new List<List<short>>();
            for (int jj = 0; jj < numberOfChannels; jj++)
            {
                List<Int16> channelSamples = new List<short>();
                for (int ii = 0; ii < _samples[jj].Count; ii++)
                {
                    channelSamples.Add(_samples[jj][ii]);
                }
                samples.Add(channelSamples);
            }
        }

        public void AddPause(double duration)
        {
            int addedDataSize = (int)Math.Round(sampleRate * duration * numberOfChannels * bitsPerSample / 8);
            while ((addedDataSize % blockAlign) != 0) // Make sure that the number of added samples is correct, based on the number of channels and the bits per sample.
            {
                addedDataSize++;
            }
            int totalDataSize = GetDataSize() + addedDataSize;
            int headerSize = GetHeaderSize();
            byte[] newSoundData = new byte[headerSize + totalDataSize];
            for (int ii = 0; ii < soundData.Length; ii++)
            {
                newSoundData[ii] = soundData[ii];
            }
            for (int ii = 0; ii < addedDataSize; ii++)
            {
                newSoundData[ii + soundData.Length] = 0; // Silence...
            }
            soundData = new byte[headerSize + totalDataSize];
            for (int ii = 0; ii < newSoundData.Length; ii++)
            {
                soundData[ii] = newSoundData[ii];
            }
            // Finalize header (set the total chunk size and the data subchunk size)
            byte[] chunkSizeAsBytes = BitConverter.GetBytes(soundData.Length - 8);
            chunkSizeAsBytes.CopyTo(soundData, 4);
            byte[] dataSubChunkSizeAsBytes = BitConverter.GetBytes(totalDataSize);
            dataSubChunkSizeAsBytes.CopyTo(soundData, 40 + (formatSubChunkSize - 16)); // the position of the bytes determining the data subchunk size.
            ExtractInformation(); // Needed in order to set all the header variables correctly for the new sound.
        }

        // NOTE: This method assumes that the header ONLY contains the mandatory subchunks, i.e. it assumes that
        // there is no fact subchunk and no list (or other) subchunk.
        public static WAVSound Join(List<WAVSound> soundList, List<double> pauseList)
        {
            if (soundList.Count == 0) { return null; } // 20160915
            List<WAVSound> modifiedSoundList = new List<WAVSound>();
            for (int ii = 0; ii < soundList.Count; ii++)
            {
                modifiedSoundList.Add(soundList[ii].Copy());
            }
            int totalDataSize = 0;
            if (pauseList != null)
            {
                for (int ii = 0; ii < pauseList.Count; ii++)
                {
                    modifiedSoundList[ii].AddPause(pauseList[ii]);
                }
            }
            foreach (WAVSound sound in modifiedSoundList)
            {
                totalDataSize += sound.GetDataSize();
            }
            WAVSound baseSound = modifiedSoundList[0];
            int baseHeaderSize = baseSound.GetHeaderSize(); // Use the first sound as the base sound.
            byte[] newSoundData = new byte[baseHeaderSize + totalDataSize];
            for (int ii = 0; ii < baseSound.SoundData.Length; ii++)
            {
                newSoundData[ii] = baseSound.SoundData[ii];
            }
            int iStart = baseSound.SoundData.Length;
            for (int kk = 1; kk < modifiedSoundList.Count; kk++)
            {
                int dataSize = modifiedSoundList[kk].GetDataSize();
                int headerSize = modifiedSoundList[kk].GetHeaderSize();
                for (int ii = 0; ii < dataSize; ii++)
                {
                    newSoundData[ii + iStart] = modifiedSoundList[kk].SoundData[headerSize + ii];
                }
                iStart += dataSize;
            }
            baseSound.SoundData = new byte[baseHeaderSize + totalDataSize];
            for (int ii = 0; ii < newSoundData.Length; ii++)
            {
                baseSound.SoundData[ii] = newSoundData[ii];
            }
            // Finalize header (set the total chunk size and the data subchunk size)
            byte[] chunkSizeAsBytes = BitConverter.GetBytes(baseSound.SoundData.Length - 8);
            chunkSizeAsBytes.CopyTo(baseSound.SoundData, 4);
            byte[] dataSubChunkSizeAsBytes = BitConverter.GetBytes(totalDataSize);
            dataSubChunkSizeAsBytes.CopyTo(baseSound.SoundData, 40 + (baseSound.formatSubChunkSize - 16)); // the position of the bytes determining the data subchunk size.
            baseSound.ExtractInformation(); // Needed in order to set all the header variables correctly for the new sound.
            return baseSound;
        }

        #region Comments
        /// Sets the average volume (sample average) of the sound.
        /// Note: The average volume is specified as a double in the range [0,1],
        /// where 1 corresponds to a sample average of 32767 (= the theoretical maximum).
        /// Typical values ~0.03-0.10.
        #endregion
        public void SetVolume(double volume)
        {
            if ((volume < 0.0) || (volume > 1.0))
            {
                return;
            }
            else
            {
                for (int ii = 0; ii < samples.Count; ii++)
                {
                    double intAverage = 0;
                    for (int jj = 0; jj < samples[ii].Count; jj++)
                    {
                        intAverage += Math.Abs((int)samples[ii][jj]);
                    }
                    double average = intAverage / ((double)samples[ii].Count);
                    double desiredAverage = volume * 32767; // 16-bit sound assumed
                    for (int jj = 0; jj < samples[ii].Count; jj++)
                    {
                        int tmpSample = (int)Math.Round(samples[ii][jj] * desiredAverage / average);
                        if (tmpSample > MAXIMUM_SAMPLE)
                        {
                            tmpSample = MAXIMUM_SAMPLE;
                        }
                        else if (tmpSample < MINIMUM_SAMPLE)
                        {
                            tmpSample = MINIMUM_SAMPLE;
                        }
                        samples[ii][jj] = (Int16)tmpSample;
                    }
                }
                GenerateSoundDataFromSamples(); // New sound data must be generated when the samples have been modified.
            }
        }

        // NOTE: Here, the user must be a bit careful. If the volume is set too high,
        // some samples might be clipped!
        public void SetRelativeVolume(double relativeVolume)
        {
            for (int iChannel = 0; iChannel < samples.Count; iChannel++)
            {
                for (int jj = 0; jj < samples[iChannel].Count; jj++)
                {
                    double newDoubleSample = Math.Truncate(relativeVolume * samples[iChannel][jj]);
                    if (newDoubleSample > MAXIMUM_SAMPLE) { newDoubleSample = MAXIMUM_SAMPLE; }
                    else if (newDoubleSample < MINIMUM_SAMPLE) { newDoubleSample = MINIMUM_SAMPLE; }
                    samples[iChannel][jj] = (Int16)Math.Round(newDoubleSample);
                }
            }
            GenerateSoundDataFromSamples(); // New sound data must be generated when the samples have been modified.
        }

        // MW Changed 20160915, so that the method handles the
        // case where the maximum amplitude is (-)32768, in
        // which case one cannot use Math.Abs() (Since the
        // maximum allowed value = 32767).
        public Int16 GetMaximumAmplitude()
        {
            double maximumAmplitude = 0;
            for (int ii = 0; ii < samples.Count; ii++)
            {
                for (int jj = 0; jj < samples[ii].Count; jj++)
                {
                    if (Math.Abs((double)samples[ii][jj]) > maximumAmplitude)
                    {
                        maximumAmplitude = (double)samples[ii][jj];
                    }
                }
            }
            if (maximumAmplitude > MAXIMUM_SAMPLE) { maximumAmplitude = MAXIMUM_SAMPLE; }
            return (Int16)maximumAmplitude;
        }

        public void SetMaximumNonClippingVolume()
        {
            Int16 maximumAmplitude = GetMaximumAmplitude();
            double volumeRatio = MAXIMUM_SAMPLE / (double)maximumAmplitude;
            for (int ii = 0; ii < samples.Count; ii++)
            {
                for (int jj = 0; jj < samples[ii].Count; jj++)
                {
                    samples[ii][jj] = (Int16)Math.Truncate(volumeRatio * samples[ii][jj]);
                }
            }
            GenerateSoundDataFromSamples(); // New sound data must be generated when the samples have been modified.
        }

        public void LowPassFilter(double cutoffFrequency)
        {
            double alpha = 2 * Math.PI * (cutoffFrequency / (double)SampleRate) /
                (2 * Math.PI * (cutoffFrequency / (double)SampleRate) + 1);
            for (int iChannel = 0; iChannel < numberOfChannels; iChannel++)
            {
                List<Int16> newSampleList = new List<Int16>();
                newSampleList.Add((Int16)Math.Round(alpha * samples[iChannel][0]));
                for (int ii = 1; ii < this.samples[iChannel].Count; ii++)
                {
                    Int16 newSample = (Int16)Math.Round(alpha * samples[iChannel][ii] +
                        (1 - alpha) * newSampleList[ii - 1]);
                    newSampleList.Add(newSample);
                }
                for (int ii = 0; ii < this.samples[iChannel].Count; ii++)
                {
                    this.samples[iChannel][ii] = newSampleList[ii];
                }
            }
            GenerateSoundDataFromSamples(); // New sound data must be generated when the samples have been modified.
        }

        public void HighPassFilter(double cutoffFrequency)
        {
            double alpha = 1 / (2 * Math.PI * (cutoffFrequency / (double)SampleRate) + 1);
            for (int iChannel = 0; iChannel < numberOfChannels; iChannel++)
            {
                List<Int16> newSampleList = new List<Int16>();
                newSampleList.Add((Int16)Math.Round(alpha * samples[iChannel][0]));
                for (int ii = 1; ii < this.samples[iChannel].Count; ii++)
                {
                    Int16 newSample = (Int16)Math.Round(alpha * newSampleList[ii - 1] +
                        alpha * (samples[iChannel][ii] - samples[iChannel][ii - 1]));
                    newSampleList.Add(newSample);
                }
                for (int ii = 0; ii < this.samples[iChannel].Count; ii++)
                {
                    this.samples[iChannel][ii] = newSampleList[ii];
                }
            }
            GenerateSoundDataFromSamples(); // New sound data must be generated when the samples have been modified.
        }

        //  20160811
        public void PreEmphasize(double cutoffFrequency)
        {
            double alpha = Math.Exp(-2 * Math.PI * cutoffFrequency / sampleRate);
            List<double> tmpSamples = new List<double>();
            for (int iChannel = 0; iChannel < numberOfChannels; iChannel++)
            {
                tmpSamples.Add(samples[iChannel][0]);
                for (int ii = 1; ii < samples[iChannel].Count; ii++)
                {
                    tmpSamples.Add(samples[iChannel][ii] - alpha * samples[iChannel][ii - 1]);
                }
                for (int ii = 0; ii < samples[iChannel].Count; ii++)
                {
                    int tmpSample = (int)Math.Round(tmpSamples[ii]);
                    if (tmpSample > MAXIMUM_SAMPLE) { tmpSample = MAXIMUM_SAMPLE; }
                    else if (tmpSample < MINIMUM_SAMPLE) { tmpSample = MINIMUM_SAMPLE; }
                    samples[iChannel][ii] = (Int16)tmpSample;
                }
            }
            GenerateSoundDataFromSamples();
        }

        /// Applies a generalized Hamming window, i.e. transforms the samples
        /// according to S_new(k) = [(1-alpha) - alpha*cos(2*pi*k/(n-1))]S_old(k), where
        /// n is the number of samples (sample indices are 0, 1, 2, ..., n-1).
        public void ApplyHammingWindow(double alpha)
        {
            for (int iChannel = 0; iChannel < this.samples.Count; iChannel++) // Loop over channels, usually 1.
            {
                for (int ii = 0; ii < samples[iChannel].Count; ii++)
                {
                    double tmpSample = ((1 - alpha) - alpha * Math.Cos(2 * Math.PI * ii / (samples[iChannel].Count - 1))) * samples[iChannel][ii];
                    Int16 newSample = (Int16)Math.Round(tmpSample);
                    samples[iChannel][ii] = newSample;
                }
            }
            GenerateSoundDataFromSamples();
        }

        // Needed? Computes AutoCorrelation for all possible shifts
        public List<double> ComputeAutoCorrelation(int channel)
        {
            List<double> autoCorrelationList = new List<double>();
            for (int shift = 0; shift < samples[channel].Count; shift++)
            {
                double autoCorrelation = AutoCorrelation.Compute(samples[channel], shift);
                autoCorrelationList.Add(autoCorrelation);
            }
            return autoCorrelationList;
        }

        // 20160811
        // Note: This method assumes that the sound has been recorded in mono (or, equivalently, that
        //       both channels are identical.
        public List<double> ComputeNormalizedAutoCorrelationCoefficients(int minimumOrder, int maximumOrder)
        {
            List<double> autoCorrelationCoefficients = new List<double>();
            for (int ii = minimumOrder; ii <= maximumOrder; ii++)
            {
                double autoCorrelation = AutoCorrelation.ComputeNormalized(samples[0], ii);
                autoCorrelationCoefficients.Add(autoCorrelation);
            }
            return autoCorrelationCoefficients;
        }

        // 20160812
        // Note: This method assumes that the sound has been recorded in mono (or, equivalently, that
        //       both channels are identical.
        public List<double> ComputeLPCCoefficients(int lpcOrder)
        {
            Vector correlationVector = new Vector();
            for (int ii = 0; ii <= lpcOrder; ii++)
            {
                double autoCorrelation = AutoCorrelation.Compute(samples[0], ii); // non-normalized correlation (covariance)
                correlationVector.Elements.Add(autoCorrelation);
            }
            Vector lpcCoefficientVector = LevinsonDurbinRecursion.Solve(correlationVector);
            List<double> lpcCoefficients = new List<double>(lpcCoefficientVector.Elements);  // OK for value types 
            return lpcCoefficients;
        }

        // 20160812
        // 
        // Note: This method assumes that the sound has been recorded in mono (or, equivalently, that
        //       both channels are identical.
        // References
        // http://practicalcryptography.com/miscellaneous/machine-learning/tutorial-cepstrum-and-lpccs/
        // http://msr.uwaterloo.ca/msr2005/papers/5.pdf
        public List<double> ComputeCepstralCoefficients(List<double> lpcCoefficientList, int cepstralOrder)
        {
            List<double> cepstralCoefficientList = new List<double>();
            double c0 = AutoCorrelation.Compute(samples[0], 0);
            cepstralCoefficientList.Add(c0);
            double c1 = lpcCoefficientList[0]; // a1.
            cepstralCoefficientList.Add(c1);
            int lpcOrder = lpcCoefficientList.Count;
            for (int mm = 2; mm <= lpcOrder; mm++)
            {
                double c = lpcCoefficientList[mm - 1];
                for (int kk = 1; kk < mm; kk++)
                {
                    c += ((double)kk / (double)mm) * cepstralCoefficientList[kk] * lpcCoefficientList[mm - kk - 1]; // -1 since lpcCoefficientList[0] = a1 etc.
                }
                cepstralCoefficientList.Add(c);
            }
            for (int mm = lpcOrder + 1; mm <= cepstralOrder; mm++)
            {
                double c = 0;
                for (int kk = mm - lpcOrder; kk < mm; kk++)
                {
                    c += ((double)kk / (double)mm) * cepstralCoefficientList[kk] * lpcCoefficientList[mm - kk - 1]; // -1 since lpcCoefficientList[0] = a1 etc.
                }
                cepstralCoefficientList.Add(c);
            }
            return cepstralCoefficientList;
        }

        // 20160916
        public Boolean IsSilence(double silenceThreshold)
        {
            Int16 maximumAmplitude = GetMaximumAmplitude();
            if (maximumAmplitude <= silenceThreshold) { return true; }
            else { return false; }
        }

        // 20160916 
        public List<List<double>> FindSplitPoints(double deltaTime, double silenceThreshold)
        {
            double startTime = 0;
            double endTime = deltaTime;
            double duration = GetDuration();
            Boolean newSound = true;
            List<List<double>> splitTimeList = new List<List<double>>();
            while (endTime <= duration)
            {
                WAVSound snippet = this.Extract(startTime, endTime);
                if (snippet.IsSilence(silenceThreshold))
                {
                    newSound = true;
                    if (splitTimeList.Count > 0)
                    {
                        if (splitTimeList.Last()[1] < double.Epsilon)
                        {
                            splitTimeList.Last()[1] = startTime; // = endTime of previous snippet
                        }
                    }
                }
                else
                {
                    if (newSound)
                    {
                        splitTimeList.Add(new List<double>() { startTime, 0 });
                        newSound = false;
                    }
                }
                startTime += deltaTime;
                endTime += deltaTime;
            }
            if (splitTimeList.Count > 0)
            {
                if (splitTimeList.Last()[1] < double.Epsilon)
                {
                    splitTimeList.Last()[1] = duration;
                }
            }
            return splitTimeList;
        }

        // 20160916
        public List<WAVSound> Split(double deltaTime, double silenceThreshold)
        {
            double startTime = 0;
            double endTime = deltaTime;
            double duration = GetDuration();
            Boolean newSound = true;
            WAVSound currentSound = null;
            List<WAVSound> splitList = new List<WAVSound>();
            while (endTime <= duration)
            {
                WAVSound snippet = this.Extract(startTime, endTime);
                if (snippet.IsSilence(silenceThreshold))
                {
                    newSound = true;
                    if (currentSound != null)
                    {
                        splitList.Add(currentSound); // No Copy() needed here - new instance generated below;
                        currentSound = null;
                    }
                }
                else
                {
                    if (newSound)
                    {
                        currentSound = snippet.Copy();
                        newSound = false;
                    }
                    else
                    {
                        currentSound.Append(snippet.Copy());
                    }

                }
                startTime += deltaTime;
                endTime += deltaTime;
            }
            if (currentSound != null) // Not yet added - occurs if the entire sound does not end with silence
            {
                splitList.Add(currentSound);
            }
            return splitList;
        }

        // 20170802
        #region Comments
        /// This method computes the (average) energy of a sound signal.
        #endregion
        public double GetAverageEnergy(int channel)
        {
            double energy = 0;
            for (int ii = 0; ii < samples[channel].Count; ii++)
            {
                energy += (samples[channel][ii] * samples[channel][ii]);
            }
            energy /= samples[channel].Count;
            return energy;
        }

        // 20170803: This method is primarily used in connection with pitch period determination
        //           for speech synthesis, but might have other uses as well.
        public double GetShiftedSquareDifference(int startIndex, int shift, int windowIndexDuration)
        {
            List<short> windowSampleList = samples[0].GetRange(startIndex, windowIndexDuration + shift);
            double shiftedSquareDifference = 0;
            for (int ii = 0; ii < windowIndexDuration; ii++)
            {
                shiftedSquareDifference += (windowSampleList[ii] - windowSampleList[ii + shift]) *
                                           (windowSampleList[ii] - windowSampleList[ii + shift]);
            }
            return shiftedSquareDifference;
        }

        // 20170803: This method is primarily used in connection with pitch period determination
        //           for speech synthesis, but might have other uses as well.
        public double GetAbsoluteMagnitudeDifference(int startIndex, int shift, int windowIndexDuration)
        {
            List<short> windowSampleList = samples[0].GetRange(startIndex, windowIndexDuration + shift);
            double absoluteMagnitudeDifference = 0;
            for (int ii = 0; ii < windowIndexDuration; ii++)
            {
                absoluteMagnitudeDifference += Math.Abs(windowSampleList[ii] - windowSampleList[ii + shift]);
            }
            return absoluteMagnitudeDifference;
        }

        // 20170804
        public List<int> GetAbsoluteSamples(int channel)
        {
            List<int> absoluteSampleList = new List<int>();
            foreach (short sample in this.samples[channel])
            {
                if (sample < 0)
                {
                    absoluteSampleList.Add(-(int)sample);
                }
                else
                {
                    absoluteSampleList.Add((int)sample);
                }
            }
            return absoluteSampleList;
        }

        // 20170804
        public int GetIndexOfAbsoluteMaximum(int startIndex, int endIndex)
        {
            int absoluteMaximum = 0;
            int indexOfAbsoluteMaximum = startIndex;
            for (int ii = startIndex; ii <= endIndex; ii++)
            {
                short sample = samples[0][ii];
                int absoluteSample;
                if (sample < 0) { absoluteSample = -(int)sample; }
                else { absoluteSample = (int)sample; }
                if (absoluteSample > absoluteMaximum)
                {
                    indexOfAbsoluteMaximum = ii;
                    absoluteMaximum = absoluteSample;
                }
            }
            return indexOfAbsoluteMaximum;
        }

        // 20170804
        public double GetTimeOfAbsoluteMaximum(double startTime, double endTime)
        {
            int sampleIndexAtStart = GetSampleIndexAtTime(startTime);
            int sampleIndexAtEnd = GetSampleIndexAtTime(endTime);
            int absoluteMaximum = 0;
            int indexOfAbsoluteMaximum = 0;
            for (int ii = sampleIndexAtStart; ii <= sampleIndexAtEnd; ii++)
            {
                short sample = samples[0][ii];
                int absoluteSample;
                if (sample < 0) { absoluteSample = -(int)sample; }
                else { absoluteSample = (int)sample; }
                if (absoluteSample > absoluteMaximum)
                {
                    indexOfAbsoluteMaximum = ii;
                    absoluteMaximum = absoluteSample;
                }
            }
            double timeOfAbsoluteMaximum = GetTimeAtSampleIndex(indexOfAbsoluteMaximum);
            return timeOfAbsoluteMaximum;
        }

        // 20170804
        public int GetAbsoluteMaximum(double startTime, double endTime)
        {
            int sampleIndexAtStart = GetSampleIndexAtTime(startTime);
            int sampleIndexAtEnd = GetSampleIndexAtTime(endTime);
            int absoluteMaximum = 0;
            for (int ii = sampleIndexAtStart; ii <= sampleIndexAtEnd; ii++)
            {
                short sample = samples[0][ii];
                int absoluteSample;
                if (sample < 0) { absoluteSample = -(int)sample; }
                else { absoluteSample = (int)sample; }
                if (absoluteSample > absoluteMaximum)
                {
                    absoluteMaximum = absoluteSample;
                }
            }
            return absoluteMaximum;
        }

        // 20170804
        public List<int> GetIndicesOfLocalExtremaAboveThreshold(int index, int backwardSearchIndexRange, int forwardSearchIndexRange, int threshold)
        {
            int startIndex = index - backwardSearchIndexRange;
            if (startIndex < 0) { startIndex = 0; }
            int endIndex = index + forwardSearchIndexRange;
            if (endIndex >= this.samples[0].Count) { endIndex = this.Samples[0].Count - 1; }
            List<int> zeroCrossingIndexList = new List<int>();
            for (int ii = startIndex+1; ii <= (endIndex-1); ii++)
            {
                double previousSample = samples[0][ii - 1];
                double currentSample = samples[0][ii];
                double nextSample = samples[0][ii + 1];
                if (previousSample * currentSample < 0) { zeroCrossingIndexList.Add(ii); }
                else if ((currentSample == 0) && (previousSample * nextSample < 0)) { zeroCrossingIndexList.Add(ii); }
            }
            List<int> localExtremaIndicesList = new List<int>();
            for (int iZCIndex = 0; iZCIndex < (zeroCrossingIndexList.Count-1); iZCIndex++)
            {
                int firstZeroCrossingIndex = zeroCrossingIndexList[iZCIndex];
                int secondZeroCrossingIndex = zeroCrossingIndexList[iZCIndex + 1];
                int extremum = 0;
                int localExtremumIndex = firstZeroCrossingIndex;
                for (int jj = firstZeroCrossingIndex; jj <= secondZeroCrossingIndex; jj++)
                {
                    int absoluteSample = Math.Abs((int)samples[0][jj]);
                    if (absoluteSample > extremum)
                    {
                        extremum = absoluteSample;
                        localExtremumIndex = jj;
                    }
                }
                if (extremum >= threshold)
                {
                    localExtremaIndicesList.Add(localExtremumIndex);
                }
            }
            return localExtremaIndicesList;
        }

        // 20170804
        public double GetLocalEnergy(int startIndex, int endIndex)
        {
            double localEnergy = 0;
            for (int ii = startIndex; ii <= endIndex; ii++)
            {
                localEnergy += samples[0][ii] * samples[0][ii];
            }
            return localEnergy;
        }

        // 20170815
        public void MedianFilter(int filterLength)
        {
            int halfFilterLength = (filterLength - 1) / 2;
            for (int iChannel = 0; iChannel < samples.Count; iChannel++)
            {
                List<short> newSampleList = new List<short>();
                for (int ii = filterLength; ii < samples[iChannel].Count - filterLength; ii++)
                {
                    List<short> sampleList = samples[iChannel].GetRange(ii - halfFilterLength, filterLength);
                    sampleList.Sort();
                    short newSample = sampleList[halfFilterLength];
                    newSampleList.Add(newSample);
                }
                samples[iChannel] = newSampleList;
            }
            GenerateSoundDataFromSamples();
        }
        #endregion

        #region Properties

        /// The entire sound, including the header, as a byte array.
        public byte[] SoundData
        {
            get { return soundData; }
            set { soundData = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string SoundName
        {
            get { return soundName; }
            set { soundName = value; }
        }

        #region Comments
        /// An optional field that can be used for tagging or otherwise describing the
        /// contents of a sound. Note: This field is not loaded during LoadFromFile nor
        /// saved during SaveToFile.
        #endregion
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string ChunkID
        {
            get { return chunkID; }
        }

        public int ChunkSize
        {
            get { return chunkSize; }
            set { chunkSize = value; }
        }

        public int DataSubChunkSize
        {
            get { return dataSubChunkSize; }
            set { dataSubChunkSize = value; }
        }

        public int SampleRate
        {
            get { return sampleRate; }
        }

        public Int16 BitsPerSample
        {
            get { return bitsPerSample; }
        }

        public Int16 NumberOfChannels
        {
            get { return numberOfChannels; }
        }

        #region Comments
        /// If numberOfChannels = 1 (mono), then Samples contains a vector
        /// of sound samples. If, instead, numberOfChannels = 2 (stereo), 
        /// Samples[0] contains the left samples and Samples[1] the
        /// right samples.
        #endregion
        public List<List<Int16>> Samples
        {
            get { return samples; }
            set { samples = value; }
        }

        public List<double> SampleTimeList
        {
            get
            {
                List<double> sampleTimeList = new List<double>();
                for (int ii = 0; ii < samples[0].Count; ii++)
                {
                    double time = GetTimeAtSampleIndex(ii);
                    sampleTimeList.Add(time);
                }
                return sampleTimeList;
            }
        }

        public MemoryStream WAVMemoryStream
        {
            get { return wAVMemoryStream; }
        }

        #endregion
    }
}
