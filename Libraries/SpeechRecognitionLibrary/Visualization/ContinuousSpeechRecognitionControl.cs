using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AudioLibrary;
using AudioLibrary.Visualization;
using AuxiliaryLibrary;
using CustomUserControlsLibrary;

namespace SpeechRecognitionLibrary.Visualization
{
    public partial class ContinuousSpeechRecognitionControl : SoundVisualizer
    {
        #region Constants
        private const int DEFAULT_MILLISECOND_RECORDING_INTERVAL = 200; // 100;
        private const int DEFAULT_RECORDING_DEVICE_ID = 0;
        private const double DEFAULT_STORAGE_DURATION = 10.0;
        private const double DEFAULT_VIEWING_INTERVAL = 2.0;
        private const int DEFAULT_MOVING_AVERAGE_LENGTH = 10;
        private const int DEFAULT_DETECTION_THRESHOLD = 300;
        private const long TICKS_PER_SECOND = 10000000;
        private const double DEFAULT_DETECTION_SILENCE_INTERVAL = 0.25; // AFTER the last detection.
        private const double DEFAULT_EXTRACTION_MARGIN = 0.5; // BEFORE the first detection.
        #endregion

        private WAVRecorder wavRecorder = null;
        private SpeechRecognitionEngine speechRecognitionEngine = null;
        private Thread runThread = null;
        private Boolean running = false;
        private int millisecondRecordingInterval = DEFAULT_MILLISECOND_RECORDING_INTERVAL;
        private int recordingDeviceID = DEFAULT_RECORDING_DEVICE_ID;
        private double storageDuration = DEFAULT_STORAGE_DURATION;
        private double viewingInterval = DEFAULT_VIEWING_INTERVAL;
        private int movingAverageLength = DEFAULT_MOVING_AVERAGE_LENGTH;
        private int detectionThreshold = DEFAULT_DETECTION_THRESHOLD;
        private Boolean inUtterance = false;
        private double utteranceStartTime;
        private double utteranceEndTime;
        private Thread recognitionThread;
        private Boolean showSoundStream = true;
        private Boolean extractDetectedSounds = true;
        private double detectionSilenceInterval = DEFAULT_DETECTION_SILENCE_INTERVAL;
        private double extractionMargin = DEFAULT_EXTRACTION_MARGIN;

        public event EventHandler<StringEventArgs> SoundRecognized = null;
        public event EventHandler<WAVSoundEventArgs> SoundDetected = null;

        private Boolean displayBusy = false; // 20180112 A bit ugly - needed since BeginInvoke has no associated callback.

     //   private static object recognitionLockObject = new object();
        private Boolean recognizerBusy = false;

        public ContinuousSpeechRecognitionControl()
        {
            InitializeComponent();
            running = false;
        }

        private void OnSoundRecognized(RecognitionResult r)
        {
            if (SoundRecognized != null)
            {
                EventHandler<StringEventArgs> handler = SoundRecognized;
                StringEventArgs e = new StringEventArgs(r.Text);
                handler(this, e);
            }
        }

        private void OnSoundDetected(WAVSound detectedSound)
        {
            if (SoundRecognized != null)
            {
                EventHandler<WAVSoundEventArgs> handler = SoundDetected;
                WAVSoundEventArgs e = new WAVSoundEventArgs(detectedSound);
                handler(this, e);
            }
        }

        private void RunLoop()
        {
            Thread.Sleep(1);
            DateTime utteranceStartDateTime = DateTime.Now;// Just needed for initialization.
            DateTime utteranceEndDateTime = DateTime.MinValue; // Just needed for initialization.
            DateTime previousUtteranceStartDateTime = DateTime.MinValue;
            DateTime previousUtteranceEndDateTime = DateTime.MinValue;
            DateTime recordingStartDateTime;
            DateTime recordingEndDateTime;
            double utteranceStartTime = 0; // In seconds, measured from the start of the current recording.  (=0 just for initialization).
            double utteranceEndTime;
            while (running)
            {
                Thread.Sleep(millisecondRecordingInterval);
                byte[] soundData = wavRecorder.GetAllRecordedBytes(out recordingStartDateTime, out recordingEndDateTime);
                if (soundData != null)
                {
                    if (soundData.Length > 0)
                    {
                        WAVSound sound = new WAVSound("", wavRecorder.SampleRate, wavRecorder.NumberOfChannels, wavRecorder.NumberOfBitsPerSample);
                        sound.AppendSamplesAsBytes(soundData);
                        if (showSoundStream)
                        {
                            if (!displayBusy)
                            {
                                WAVSound soundToDisplay = sound.Copy(); // 20171207: Make a new copy here, since the code below may process the sound before visualization is completed.
                                if (InvokeRequired) { this.BeginInvoke(new MethodInvoker(() => ShowSound(soundToDisplay))); }
                                else { ShowSound(soundToDisplay); }
                            }
                        }

                        // Next, remove all parts of the sound that have already been recognized, if any:
                        if (previousUtteranceEndDateTime > recordingStartDateTime)
                        {
                            double extractionStartTime = (previousUtteranceEndDateTime - recordingStartDateTime).TotalSeconds;
                            double extractionEndTime = sound.GetDuration();
                            sound = sound.Extract(extractionStartTime, extractionEndTime);

                            // Debug code, remove
                            /*   if (sound == null)  // Should not happen, unless the recognition thread is stopped using a breakpoint.
                                {

                                }  */
                        }

                        if (!inUtterance)
                        {
                            utteranceStartTime = sound.GetFirstTimeAboveThreshold(0, movingAverageLength, detectionThreshold);
                            if (utteranceStartTime > 0)
                            {
                                double duration = sound.GetDuration();
                                double timeToEnd = duration - utteranceStartTime;
                                long ticksToEnd = TICKS_PER_SECOND * (long)(timeToEnd);
                                utteranceStartDateTime = recordingEndDateTime.Subtract(new TimeSpan(ticksToEnd));
                                if (utteranceStartDateTime > previousUtteranceEndDateTime) // True (by construction) the FIRST time.
                                {
                                    inUtterance = true;
                                    long utteranceStartTimeAsTicks = (long)(TICKS_PER_SECOND * utteranceStartTime);  // Corrected 20170907 (1000000 -> 10000000)
                                    utteranceStartDateTime = recordingStartDateTime.Add(new TimeSpan(utteranceStartTimeAsTicks));
                                }
                            }
                        }
                        else
                        {
                            double duration = sound.GetDuration();
                            WAVSound endOfSound = sound.Extract(duration - detectionSilenceInterval, duration);  
                            double startTimeInEndOfSound = endOfSound.GetFirstTimeAboveThreshold(0, movingAverageLength, detectionThreshold);
                            if (startTimeInEndOfSound < 0)  //  <=> silence at the end of the sound
                            {
                                inUtterance = false;
                                utteranceEndDateTime = recordingEndDateTime; // recordingStartDateTime.Add(new TimeSpan(utteranceEndTimeAsTicks));
                                previousUtteranceStartDateTime = utteranceStartDateTime;
                                previousUtteranceEndDateTime = utteranceEndDateTime;
                                //    Monitor.Enter(recognitionLockObject);
                                if (!recognizerBusy)
                                {
                                    recognizerBusy = true;
                                    WAVSound soundToRecognize = sound.Extract(utteranceStartTime - extractionMargin, duration).Copy();
                                    //    Monitor.Exit(recognitionLockObject);
                                    RunRecognizer(soundToRecognize);
                                }
                            }
                        }
                    }      
                }
            }
        }

        private void RecognitionLoop(WAVSound soundToRecognize)
        {
      //      Monitor.Enter(recognitionLockObject);
            try
            {
                soundToRecognize.GenerateMemoryStream();
                speechRecognitionEngine.SetInputToWaveStream(soundToRecognize.WAVMemoryStream);
                RecognitionResult r = speechRecognitionEngine.Recognize();
                if (r != null)
                {
                    OnSoundRecognized(r);
                }
                if (extractDetectedSounds)
                {
                    OnSoundDetected(soundToRecognize.Copy());
                }
                recognizerBusy = false;
            }
            catch
            {
                // Nothing to do here - try-catch needed to avoid (rare) crashes when the WAVE stream cannot be found.
            }
            finally
            {
                recognizerBusy = false; // Needed if the catch is triggered.
            }
         //   Monitor.Exit(recognitionLockObject);
        }

        private void RunRecognizer(WAVSound soundToRecognize)
        {
            recognitionThread = new Thread(new ThreadStart(() => RecognitionLoop(soundToRecognize)));
            recognitionThread.Start();
        }

        private void ShowSound(WAVSound sound)
        {
            ClearHistory();
            try  // This (using try-catch) is ugly, but appears to be necessary for some hardware configurations
            {
                double duration = sound.GetDuration();
                if (duration > viewingInterval)
                {
                    WAVSound visibleSound = sound.Extract(duration - viewingInterval, duration);
                    SetSound(visibleSound);
                }
                else
                {
                    SetSound(sound);
                }
            }
            catch
            {
                // Nothing to do here..
            }
            displayBusy = false;
        }

        public void Start()
        {
            if (running) { return; }

            wavRecorder = new WAVRecorder();
            wavRecorder.DeviceId = recordingDeviceID;
            wavRecorder.StorageDuration = storageDuration;
            wavRecorder.Start();

            runThread = new Thread(new ThreadStart(() => RunLoop()));
            running = true;
            runThread.Start();
        }

        public void Stop()
        {
            if (!running) { return; }
            wavRecorder.Stop();
            running = false;
        }

        public void SetSpeechRecognitionEngine(SpeechRecognitionEngine speechRecognitionEngine)
        {
            this.speechRecognitionEngine = speechRecognitionEngine;
        }

        public Boolean ShowSoundStream
        {
            get { return showSoundStream; }
            set { showSoundStream = value; }
        }

        public Boolean ExtractDetectedSounds
        {
            get { return extractDetectedSounds; }
            set { extractDetectedSounds = value; }
        }

        public int RecordingDeviceID
        {
            get { return recordingDeviceID; }
            set
            {
                if (!running)
                {
                    recordingDeviceID = value;
                }
            }
        }

        public int DetectionThreshold
        {
            get { return detectionThreshold; }
            set { detectionThreshold = value; }
        }
    }
}
