using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
// using Microsoft.Speech.Recognition; // NOTE: In order to use Microsoft.Speech, you must add a reference to the Microsoft.Speech DLL. See also Question Qss1 in the FAQ (web page)
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AudioLibrary;

namespace SpeechRecognitionApplication
{
    public partial class SpeechRecognitionMainForm : Form
    {
        private WAVRecorder wavRecorder = null;
        private SpeechRecognitionEngine speechRecognitionEngine = null;

        public SpeechRecognitionMainForm()
        {
            InitializeComponent();
            speechRecognitionEngine = new SpeechRecognitionEngine();
            LoadGrammar();
        }

        private void startRecordingButton_Click(object sender, EventArgs e)
        {
            if (wavRecorder == null)
            {
                wavRecorder = new WAVRecorder();
                wavRecorder.SampleRate = 16000;
                wavRecorder.StorageDuration = 10;
            }
            if (!wavRecorder.IsRecording)
            {
                recognizeButton.Enabled = false;
                recordingButton.Text = "Stop recording";
                wavRecorder.Start();              
            }
            else
            {
                wavRecorder.Stop();
                byte[] recordedBytes = wavRecorder.GetAllRecordedBytes();
                if (recordedBytes != null)
                {
                    if (recordedBytes.Length > 0)
                    {
                        WAVSound sound = new WAVSound("", wavRecorder.SampleRate, wavRecorder.NumberOfChannels, wavRecorder.NumberOfBitsPerSample);
                        sound.AppendSamplesAsBytes(recordedBytes);
                        sound.SetMaximumNonClippingVolume();
                        soundVisualizer.SetSound(sound);
                    }
                }
                recordingButton.Text = "Start recording";
                recognizeButton.Enabled = true;
            }
        }

        private Boolean IsPhraseAlreadyPresent(string phrase)
        {
            for (int ii = 0; ii < grammarPhraseListBox.Items.Count;ii++)
            {
                string grammarPhrase = grammarPhraseListBox.Items[ii].ToString();
                if (grammarPhrase.ToLower() == phrase.ToLower()) { return true; }
            }
            return false;
        }

        private void LoadGrammar()
        {
            Choices choices = new Choices();
            List<string> phraseList = new List<string>();
            for (int ii = 0; ii < grammarPhraseListBox.Items.Count; ii++)
            {
                string phrase = grammarPhraseListBox.Items[ii].ToString();
                phraseList.Add(phrase);
            }
            choices.Add(phraseList.ToArray());
            if (phraseList.Count > 0)
            {
                GrammarBuilder grammarBuilder = new GrammarBuilder();
                CultureInfo currentCulture = new CultureInfo("en-US");
                grammarBuilder.Culture = currentCulture;
                grammarBuilder.Append(choices);
                Grammar grammar = new Grammar(grammarBuilder);
                speechRecognitionEngine.LoadGrammar(grammar);
            }
        }

        private void recognizeButton_Click(object sender, EventArgs e)
        {
            soundVisualizer.Sound.GenerateMemoryStream();
            speechRecognitionEngine.SetInputToWaveStream(soundVisualizer.Sound.WAVMemoryStream);
            RecognitionResult r = speechRecognitionEngine.Recognize();
            if (r != null)
            {
                recognitionResultTextBox.Text = r.Text;
                recognitionResultTextBox.BackColor = Color.Lime;
            }
            else
            {
                recognitionResultTextBox.Text = "";
                recognitionResultTextBox.BackColor = Color.Red;
            }
        }

        private void grammarPhraseListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int selectedIndex = grammarPhraseListBox.SelectedIndex;
            if (selectedIndex >= 0)
            {
                grammarPhraseListBox.Items.RemoveAt(selectedIndex);
                LoadGrammar();
            }
        }

        private void addGrammarPhraseButton_Click(object sender, EventArgs e)
        {
            string phrase = grammarPhraseTextBox.Text;
            if (phrase != "")
            {
                if (!IsPhraseAlreadyPresent(phrase))
                {
                    grammarPhraseListBox.Items.Add(phrase);
                    LoadGrammar();
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
