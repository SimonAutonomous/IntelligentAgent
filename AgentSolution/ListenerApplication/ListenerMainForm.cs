using global::System;
using global::System.Collections.Generic;
using global::System.ComponentModel;
using global::System.Data;
using global::System.Diagnostics;
using global::System.Drawing;
using global::System.Globalization;
using global::System.Linq;
using global::System.Speech.Recognition;
using global::System.Text;
using global::System.Threading;
using global::System.Threading.Tasks;
using global::System.Windows.Forms;
using global::AudioLibrary;
using global::AuxiliaryLibrary;
using global::CommunicationLibrary;
using global::CustomUserControlsLibrary;
using global::SpeechRecognitionLibrary;

namespace ListenerApplication
{
    public partial class ListenerMainForm : global::System.Windows.Forms.Form
    {
        private const string CLIENT_NAME = "Listener";
        private const string DEFAULT_IP_ADDRESS = "127.0.0.1";
        private const int DEFAULT_PORT = 7;
        private const string DATETIME_FORMAT = "yyyyMMdd HH:mm:ss";
        private const int DEFAULT_RECORDING_DEVICE_ID = 0;

        private string ipAddress = global::ListenerApplication.ListenerMainForm.DEFAULT_IP_ADDRESS;
        private int port = global::ListenerApplication.ListenerMainForm.DEFAULT_PORT;
        private global::CommunicationLibrary.Client client = null;

        private global::AudioLibrary.WAVRecorder wavRecorder = null;
        private global::System.Speech.Recognition.SpeechRecognitionEngine speechRecognitionEngine = null;
        private int recordingDeviceID = global::ListenerApplication.ListenerMainForm.DEFAULT_RECORDING_DEVICE_ID;

        private global::System.Threading.Thread clientThread = null;
        private global::System.Boolean clientBusy = false;

        public ListenerMainForm()
        {
            InitializeComponent();
            Initialize();
            SetUpSpeechRecognizer();
            Connect();
        }

        private void Initialize()
        {
            global::System.Drawing.Size screenSize = global::System.Windows.Forms.Screen.GetBounds(this).Size;
            this.Location = new global::System.Drawing.Point(screenSize.Width - this.Width, screenSize.Height - this.Height);
            mainTabControl.SelectedTab = inputTabPage;
            continuousSpeechRecognitionControl.SoundRecognized += new global::System.EventHandler<global::AuxiliaryLibrary.StringEventArgs>(HandleContinuousSoundRecognized);
            continuousSpeechRecognitionControl.SoundDetected += new global::System.EventHandler<global::AudioLibrary.WAVSoundEventArgs>(HandleContinuousSoundDetected);
            global::System.Collections.Generic.List<string> recordingDeviceNameList = global::AudioLibrary.WAVRecorder.GetDeviceNames();
            recordingDeviceComboBox.Items.Clear();
            foreach (string recordingDeviceName in recordingDeviceNameList)
            {
                recordingDeviceComboBox.Items.Add(recordingDeviceName);
            }
            if (recordingDeviceNameList.Count > 0)
            {
                recordingDeviceComboBox.SelectedIndex = recordingDeviceID;
            }
            clientBusy = false;
        }

        private void SetUpSpeechRecognizer()
        {
            speechRecognitionEngine = new global::System.Speech.Recognition.SpeechRecognitionEngine();
            LoadGrammar();
        }

        private void LoadGrammar()
        {
            global::System.Speech.Recognition.Choices choices = new global::System.Speech.Recognition.Choices();
            global::System.Collections.Generic.List<string> phraseList = new global::System.Collections.Generic.List<string>();
            for (int ii = 0; ii < grammarPhraseListBox.Items.Count; ii++)
            {
                string phrase = grammarPhraseListBox.Items[ii].ToString();
                phraseList.Add(phrase);
            }
            choices.Add(phraseList.ToArray());
            if (phraseList.Count > 0)
            {
                global::System.Speech.Recognition.GrammarBuilder grammarBuilder = new global::System.Speech.Recognition.GrammarBuilder();
                global::System.Globalization.CultureInfo currentCulture = new global::System.Globalization.CultureInfo("en-US");
                grammarBuilder.Culture = currentCulture;
                grammarBuilder.Append(choices);
                global::System.Speech.Recognition.Grammar grammar = new global::System.Speech.Recognition.Grammar(grammarBuilder);
                speechRecognitionEngine.LoadGrammar(grammar);
             //   speechRecognitionEngine.LoadGrammar(new DictationGrammar());
                recognizeButton.Enabled = true;
            }
            else
            {
                recognizeButton.Enabled = false;
            }
        }

        private void Connect()
        {
            client = new global::CommunicationLibrary.Client();
            client.Progress += new global::System.EventHandler<global::CommunicationLibrary.CommunicationProgressEventArgs>(HandleClientProgress);
            client.Error += new global::System.EventHandler<global::CommunicationLibrary.CommunicationErrorEventArgs>(HandleClientError); // 20171214
            client.ConnectionEstablished += new global::System.EventHandler(HandleConnectionEstablished);
            client.Name = global::ListenerApplication.ListenerMainForm.CLIENT_NAME;
            client.Connect(ipAddress, port);
        }

        private void HandleConnectionEstablished(object sender, global::System.EventArgs e)
        {
            // Start continuous recognition once a connection to the server has been established
            grammarPhraseTextBox.Enabled = false;
            grammarPhraseListBox.Enabled = false;
            addToGrammarButton.Enabled = false;
            speechRecognitionEngine = new global::System.Speech.Recognition.SpeechRecognitionEngine();
            LoadGrammar();
            recordingDeviceComboBox.Enabled = false;
            int detectionThreshold = int.Parse(detectionThresholdTextBox.Text);
            continuousSpeechRecognitionControl.DetectionThreshold = detectionThreshold;
            continuousSpeechRecognitionControl.RecordingDeviceID = recordingDeviceID;
            continuousSpeechRecognitionControl.SetSpeechRecognitionEngine(speechRecognitionEngine);
            continuousSpeechRecognitionControl.ShowSoundStream = showSoundStreamToolStripMenuItem.Checked;
            continuousSpeechRecognitionControl.ExtractDetectedSounds = extractDetectedSoundsToolStripMenuItem.Checked;
            continuousSpeechRecognitionControl.Start();
            continuousInputButton.Enabled = false;
            continuousInputStopButton.Enabled = true;
        }

        private void HandleClientProgress(object sender, global::CommunicationLibrary.CommunicationProgressEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new global::System.Windows.Forms.MethodInvoker(() => ShowProgress(e)));
            }
            else { ShowProgress(e); }
            clientBusy = false; // 20171214 The HandleClientProgress method is executed once a 
                                // message has been succesfully sent.
        }

        private void HandleClientError(object sender, global::CommunicationLibrary.CommunicationErrorEventArgs e)
        {
            clientBusy = false; // 20171214 The HandleClientError methods is executed if the
                                // client fails to send the message.
        }

        private void ShowProgress(global::CommunicationLibrary.CommunicationProgressEventArgs e)
        {
            global::CustomUserControlsLibrary.ColorListBoxItem item;
            item = new global::CustomUserControlsLibrary.ColorListBoxItem(e.Message, communicationLogColorListBox.BackColor, communicationLogColorListBox.ForeColor);
            communicationLogColorListBox.Items.Insert(0, item);
        }

        private void inputTextBox_KeyDown(object sender, global::System.Windows.Forms.KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == global::System.Windows.Forms.Keys.Enter)
            {
                string message = inputTextBox.Text;
                inputTextBox.Text = "";
                e.Handled = true;
                e.SuppressKeyPress = true;
                client.Send(message);
                global::CustomUserControlsLibrary.ColorListBoxItem item = new global::CustomUserControlsLibrary.ColorListBoxItem(global::System.DateTime.Now.ToString(global::ListenerApplication.ListenerMainForm.DATETIME_FORMAT) + ": " + message, inputMessageColorListBox.BackColor,
                    inputMessageColorListBox.ForeColor);
                inputMessageColorListBox.Items.Insert(0, item);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, global::System.EventArgs e)
        {
            continuousSpeechRecognitionControl.Stop();
            global::System.Windows.Forms.Application.Exit();
        }

        private void recordingButton_Click(object sender, global::System.EventArgs e)
        {
            if (wavRecorder == null)
            {
                wavRecorder = new global::AudioLibrary.WAVRecorder();
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
                        global::AudioLibrary.WAVSound recordedSound = new global::AudioLibrary.WAVSound("", wavRecorder.SampleRate, wavRecorder.NumberOfChannels, wavRecorder.NumberOfBitsPerSample);
                        recordedSound.AppendSamplesAsBytes(recordedBytes);
                        recordedSound.SetMaximumNonClippingVolume();
                        soundVisualizer.SetSound(recordedSound);
                    }
                }
                recordingButton.Text = "Start recording";
                recognizeButton.Enabled = true;
            }
        }

        private void recognizeButton_Click(object sender, global::System.EventArgs e)
        {
            if (soundVisualizer.Sound == null) { return; }
            soundVisualizer.Sound.GenerateMemoryStream();
            speechRecognitionEngine.SetInputToWaveStream(soundVisualizer.Sound.WAVMemoryStream);
            global::System.Speech.Recognition.RecognitionResult r = speechRecognitionEngine.Recognize();
            if (r != null)
            {
                inputTextBox.Text = r.Text;
                client.Send(r.Text);
                global::CustomUserControlsLibrary.ColorListBoxItem item = new global::CustomUserControlsLibrary.ColorListBoxItem(global::System.DateTime.Now.ToString(global::ListenerApplication.ListenerMainForm.DATETIME_FORMAT) + ": " + r.Text, inputMessageColorListBox.BackColor,
                    inputMessageColorListBox.ForeColor);
                inputMessageColorListBox.Items.Insert(0, item);
            }
        }

        private void continuousInputButton_Click(object sender, global::System.EventArgs e)
        {
            continuousInputButton.Enabled = false;
            grammarPhraseTextBox.Enabled = false;
            grammarPhraseListBox.Enabled = false;
            addToGrammarButton.Enabled = false;
            speechRecognitionEngine = new global::System.Speech.Recognition.SpeechRecognitionEngine();
            LoadGrammar();
            recordingDeviceComboBox.Enabled = false;
            recordingDeviceID = recordingDeviceComboBox.SelectedIndex; // 20171214
            int detectionThreshold = int.Parse(detectionThresholdTextBox.Text);
            continuousSpeechRecognitionControl.DetectionThreshold = detectionThreshold;
            continuousSpeechRecognitionControl.RecordingDeviceID = recordingDeviceID;
            continuousSpeechRecognitionControl.SetSpeechRecognitionEngine(speechRecognitionEngine);
            continuousSpeechRecognitionControl.ShowSoundStream = showSoundStreamToolStripMenuItem.Checked;
            continuousSpeechRecognitionControl.ExtractDetectedSounds = extractDetectedSoundsToolStripMenuItem.Checked;
            continuousSpeechRecognitionControl.Start();
            continuousInputStopButton.Enabled = true;
        }

        private void ShowContinuousRecognizedSound(string recognizedString)
        {
            global::CustomUserControlsLibrary.ColorListBoxItem item = new global::CustomUserControlsLibrary.ColorListBoxItem(global::System.DateTime.Now.ToString(global::ListenerApplication.ListenerMainForm.DATETIME_FORMAT) + ": " + recognizedString, continuousInputColorListBox.BackColor,
                    continuousInputColorListBox.ForeColor);
            continuousInputColorListBox.Items.Insert(0, item);
        }

        private void SendMessage(string message)
        {
            client.Send(message);
           // clientBusy = false;  // Should not be set here - let the event handlers (see above) handle this. 20171214
        } 

        private void HandleContinuousSoundRecognized(object sender, global::AuxiliaryLibrary.StringEventArgs e)
        {
            if (!clientBusy)
            {
                clientBusy = true;
                clientThread = new global::System.Threading.Thread(new global::System.Threading.ThreadStart(() => SendMessage(e.StringValue)));
                clientThread.Start();
            }
            //    client.Send(e.StringValue);
            if (continuousSpeechRecognitionControl.ExtractDetectedSounds == true)
            {
                 if (InvokeRequired) { this.BeginInvoke(new global::System.Windows.Forms.MethodInvoker(() => ShowContinuousRecognizedSound(e.StringValue))); }
                 else { ShowContinuousRecognizedSound(e.StringValue); }
            }
        }

        private void HandleContinuousSoundDetected(object sender, global::AudioLibrary.WAVSoundEventArgs e)
        {
            if (InvokeRequired) { this.BeginInvoke(new global::System.Windows.Forms.MethodInvoker(() => detectedSoundVisualizer.SetSound(e.Sound))); }
            else { detectedSoundVisualizer.SetSound(e.Sound); }
        }

        private void showSoundStreamToolStripMenuItem_Click(object sender, global::System.EventArgs e)
        {
            continuousSpeechRecognitionControl.ShowSoundStream = showSoundStreamToolStripMenuItem.Checked;
        }

        private void extractDetectedSoundsToolStripMenuItem_Click(object sender, global::System.EventArgs e)
        {
            continuousSpeechRecognitionControl.ExtractDetectedSounds = extractDetectedSoundsToolStripMenuItem.Checked;
        }

        private void addToGrammarButton_Click(object sender, global::System.EventArgs e)
        {
            if (grammarPhraseTextBox.Text != "")
            {
                if (!grammarPhraseListBox.Items.Contains(grammarPhraseTextBox.Text))
                {
                    grammarPhraseListBox.Items.Insert(0, grammarPhraseTextBox.Text);
                }
            }
        }

        private void grammarPhraseListBox_SelectedIndexChanged(object sender, global::System.EventArgs e)
        {
            int selectedIndex = grammarPhraseListBox.SelectedIndex;
            if (selectedIndex >= 0)
            {
                grammarPhraseListBox.Items.RemoveAt(selectedIndex);
            }
        }

        private void continuousInputStopButton_Click(object sender, global::System.EventArgs e)
        {
            continuousInputStopButton.Enabled = false;
            continuousSpeechRecognitionControl.Stop();
            continuousInputButton.Enabled = true;
            recordingDeviceComboBox.Enabled = true;
        }

        private void detectionThresholdTextBox_TextChanged(object sender, global::System.EventArgs e)
        {
            int detectionThreshold;
            global::System.Boolean ok = int.TryParse(detectionThresholdTextBox.Text, out detectionThreshold);
            if (ok)
            {
                if (detectionThreshold > 0) { continuousSpeechRecognitionControl.DetectionThreshold = detectionThreshold; }
            }
        }

        private void ListenerMainForm_FormClosing(object sender, global::System.Windows.Forms.FormClosingEventArgs e)
        {
            continuousSpeechRecognitionControl.Stop();
            global::System.Windows.Forms.Application.Exit();
        }
    }
}
