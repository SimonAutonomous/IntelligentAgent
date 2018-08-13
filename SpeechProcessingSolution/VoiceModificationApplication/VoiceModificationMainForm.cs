using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;  
using System.Speech.AudioFormat;
// using Microsoft.Speech.Synthesis;  // NOTE: In order to use Microsoft.Speech, you must add a reference to the Microsoft.Speech DLL. See also Question Qss1 in the FAQ (web page)
// using Microsoft.Speech.AudioFormat;
using AudioLibrary;
using AudioLibrary.Visualization;
using SpeechSynthesisLibrary.TDPSOLA;
using SpeechSynthesisLibrary.Visualization;

namespace VoiceModificationApplication
{
    public partial class VoiceModificationMainForm : Form
    {
        private SpeechSynthesizer speechSynthesizer;
        private List<InstalledVoice> voiceList;
        private WAVSound currentSound;
        private SpeechTypeSpecification speechTypeSpecification;
        private PitchPeriodSpecification pitchPeriodSpecification;
        private List<double> pitchMarkTimeList;
        private SpeechModifier speechModifier;

        public VoiceModificationMainForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            speechSynthesizer = new SpeechSynthesizer();
            voiceList = speechSynthesizer.GetInstalledVoices().ToList();
            foreach (InstalledVoice voice in voiceList)
            {
                voiceSelectionComboBox.Items.Add(voice.VoiceInfo.Name);
            }
            voiceSelectionComboBox.SelectedIndex = 0;
            adjustDurationComboBox.SelectedIndex = 0;
            speechModifier = new SpeechModifier();   
        }

        private void speakButton_Click(object sender, EventArgs e)
        {
            string sentence = sentenceTextBox.Text;
            if (sentence != "")
            {
                speechVisualizer.MarkerList = new List<SoundMarker>();
                speechVisualizer.SetPitchPeriodSpecification(null);

                string voiceName = voiceSelectionComboBox.SelectedItem.ToString();
                speechSynthesizer.SetOutputToWaveFile("./tmpOutput.wav", new SpeechAudioFormatInfo(16000, AudioBitsPerSample.Sixteen, AudioChannel.Mono));
                speechSynthesizer.Speak(sentence);
                speechSynthesizer.SetOutputToDefaultAudioDevice();
                speechSynthesizer.SelectVoice(voiceName);
                speechSynthesizer.Speak(sentence);
                currentSound = new WAVSound();
                currentSound.LoadFromFile("./tmpOutput.wav");

                double startTime = currentSound.GetFirstTimeAboveThreshold(0, 10, 20);
                double endTime = currentSound.GetLastTimeAboveThreshold(0, 10, 20);
                currentSound = currentSound.Extract(startTime, endTime);
                speechVisualizer.SetRange(0, currentSound.GetDuration(), -32768, 32768);
                speechVisualizer.SetSound(currentSound);
                speechVisualizer.Invalidate();

                soundTypeIdentificationButton.Enabled = true;
                playSoundButton.Enabled = true;
                modifySoundButton.Enabled = true;
                saveSoundToolStripMenuItem.Enabled = true;
            }    
        }

        private void ShowSpeechTypeVariation(SpeechVisualizer visualizer, SpeechTypeSpecification speechTypeSpecification, double deltaTime)
        {
            double time = 0;
            double lastTime = speechTypeSpecification.TimeSpeechTypeTupleList.Last().Item1;
            visualizer.MarkerList = new List<SoundMarker>();
            while (time < lastTime)
            {
                SoundMarker marker = new SoundMarker();
                marker.Type = SoundMarkerType.HorizontalLine;
                marker.Thickness = 4;
                marker.Start = (float)(time - 0.25 * deltaTime);
                marker.End = (float)(time + 0.25 * deltaTime);
                SpeechType speechType = speechTypeSpecification.GetSpeechType(time);
                if (speechType == SpeechType.Silence)
                {
                    marker.Color = Color.Yellow;
                    marker.Level = 28000;
                }
                else if (speechType == SpeechType.Voiced)
                {
                    marker.Color = Color.Lime;
                    marker.Level = 32000;
                }
                else
                {
                    marker.Color = Color.Red;
                    marker.Level = 30000;
                }
                visualizer.MarkerList.Add(marker);
                time += deltaTime;
            }
            visualizer.Invalidate();
        }

        private void ShowPitchMarks(SpeechVisualizer visualizer, List<double> pitchMarkTimeList)
        {
            foreach (double pitchMarkTime in pitchMarkTimeList)
            {
                SoundMarker marker = new SoundMarker();
                marker.Type = SoundMarkerType.VerticalLine;
                marker.Thickness = 2;
                marker.Start = -30000;
                marker.End = 30000;
                marker.Color = Color.Yellow;
                marker.Level = pitchMarkTime;
                visualizer.MarkerList.Add(marker);
            }
            visualizer.Invalidate();
        }

        private void soundTypeIdentificationButton_Click(object sender, EventArgs e)
        {
            soundTypeIdentificationButton.Enabled = false;
            SpeechTypeEstimator speechTypeEstimator = speechModifier.SpeechTypeEstimator;
            speechTypeEstimator.FindSpeechTypeVariation(currentSound); // , 0, 0.020, 0.010, 550, 0.02, 250000, 2500);
          //  speechTypeEstimator.Adjust(3);
          //  speechTypeEstimator.Adjust(3); // repeat the adjustment to remove double errors.
            ShowSpeechTypeVariation(speechVisualizer, speechTypeEstimator.SpeechTypeSpecification, 0.01);
            speechTypeSpecification = speechTypeEstimator.SpeechTypeSpecification;
            findPitchPeriodsButton.Enabled = true;
        }

        private void findPitchPeriodsButton_Click(object sender, EventArgs e)
        {
            findPitchPeriodsButton.Enabled = false;
            PitchPeriodEstimator pitchPeriodEstimator = speechModifier.PitchPeriodEstimator;
            pitchPeriodEstimator.ComputePitchPeriods(currentSound, 0.0, currentSound.GetDuration()); //, 0.0050, 0.0120, 0.01);
            pitchPeriodEstimator.AdjustAndInterpolate(speechTypeSpecification); // , 0.005, true);
            speechVisualizer.SetPitchPeriodSpecification(pitchPeriodEstimator.PitchPeriodSpecification);
            pitchPeriodSpecification = pitchPeriodEstimator.PitchPeriodSpecification;
            findPitchMarksButton.Enabled = true;
        }

        private void findPitchMarksButton_Click(object sender, EventArgs e)
        {
            findPitchMarksButton.Enabled = false;
            PitchMarkEstimator pitchMarkEstimator = speechModifier.PitchMarkEstimator;
            pitchMarkEstimator.FindPitchMarks(currentSound, speechTypeSpecification, pitchPeriodSpecification); // , 0.0025, 0.0025, 0.45, 0.002);
            pitchMarkTimeList = pitchMarkEstimator.PitchMarkTimeList;
            ShowPitchMarks(speechVisualizer, pitchMarkEstimator.PitchMarkTimeList);
        }

        private void modifySoundButton_Click(object sender, EventArgs e)
        {
            speechVisualizer.MarkerList = new List<SoundMarker>();

            speechModifier.TopFraction = double.Parse(topFractionTextBox.Text);
            double relativeStartPitch = double.Parse(relativeStartPitchTextBox.Text);
            double relativeEndPitch = double.Parse(relativeEndPitchTextBox.Text);
            Boolean adjustDuration = Boolean.Parse(adjustDurationComboBox.SelectedItem.ToString());
            double relativeDuration = double.Parse(relativeDurationTextBox.Text); // Only relevant if adjustDuration = true.

            WAVSound modifiedSound = speechModifier.Modify(currentSound, relativeStartPitch, relativeEndPitch, adjustDuration, relativeDuration);


         //   modifiedSound.MedianFilter(5);
          //  modifiedSound.LowPassFilter(1500);
          //  modifiedSound.SetMaximumNonClippingVolume();

            //   modifiedSound.SetMaximumNonClippingVolume();
            SoundPlayer soundPlayer = new SoundPlayer();
            modifiedSound.GenerateMemoryStream();
            modifiedSound.WAVMemoryStream.Position = 0; // Manually rewind stream 
            soundPlayer.Stream = null;
            soundPlayer.Stream = modifiedSound.WAVMemoryStream;
            soundPlayer.PlaySync();
            speechVisualizer.SetRange(0, modifiedSound.GetDuration(), -32768, 32768);
            speechVisualizer.SetPitchPeriodSpecification(null);
            speechVisualizer.SetSound(modifiedSound);

            currentSound = modifiedSound.Copy();

            soundTypeIdentificationButton.Enabled = true;
            findPitchPeriodsButton.Enabled = false;
            findPitchMarksButton.Enabled = false;
        }

        private void adjustDurationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Boolean adjustDuration = Boolean.Parse(adjustDurationComboBox.SelectedItem.ToString());
            relativeDurationLabel.Visible = adjustDuration;
            relativeDurationTextBox.Visible = adjustDuration;
        }

        private void parameterSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VoiceModificationParameterSettingsForm parameterSettingsForm = new VoiceModificationParameterSettingsForm();
            parameterSettingsForm.SetSpeechModifier(speechModifier);
            parameterSettingsForm.Show();
        }

    /*    private void loadSoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
        }  */

        private void saveSoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "WAV Files (*.wav)|*.wav";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    speechVisualizer.Sound.SaveToFile(saveFileDialog.FileName);
                }
            }
        }

        private void playSoundButton_Click(object sender, EventArgs e)
        {
            SoundPlayer soundPlayer = new SoundPlayer();
            WAVSound soundToPlay = speechVisualizer.Sound.Copy();
            soundToPlay.GenerateMemoryStream();
            soundToPlay.WAVMemoryStream.Position = 0; // Manually rewind stream 
            soundPlayer.Stream = null;
            soundPlayer.Stream = soundToPlay.WAVMemoryStream;
            soundPlayer.PlaySync();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
