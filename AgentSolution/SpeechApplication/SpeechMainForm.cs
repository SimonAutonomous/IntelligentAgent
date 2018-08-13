using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunicationLibrary;
using CustomUserControlsLibrary;
//using Microsoft.Speech.Synthesis;  // Note: In order to use Microsoft.Speech, you must add a reference to the Microsoft.Speech DLL. See also the FAQ (Question Qss1).
using System.Speech.Synthesis;

namespace SpeechApplication
{
    public partial class SpeechMainForm : Form
    {
        private const string CLIENT_NAME = "Speech";
        private const string DEFAULT_IP_ADDRESS = "127.0.0.1";
        private const int DEFAULT_PORT = 7;

        private string ipAddress = DEFAULT_IP_ADDRESS;
        private int port = DEFAULT_PORT;
        private Client client = null;
        private SpeechSynthesizer speechSynthesizer = null;

        private const int ONE_BILLION = 1000000000;
        private const int ONE_MILLION = 1000000;
        private const int ONE_THOUSAND = 1000;

        public SpeechMainForm()
        {
            InitializeComponent();
            Initialize();
            SetUpSpeechSynthesizer();
            Connect();
        }

        private void SetUpSpeechSynthesizer()
        {
            speechSynthesizer = new SpeechSynthesizer();
            speechSynthesizer.SetOutputToDefaultAudioDevice();

            var voices = speechSynthesizer.GetInstalledVoices();
            Boolean voiceSelected = false;
            foreach (InstalledVoice voice in voices)
            {
                if (voice.VoiceInfo.Name.ToString().Contains("Hazel"))
                {
                    speechSynthesizer.SelectVoice(voice.VoiceInfo.Name);
                    speechSynthesizer.Rate = 3; // The Hazel voice is a bit slow ...
                    voiceSelected = true;
                    break;
                }
            }
            if (!voiceSelected)
            {
                speechSynthesizer.SelectVoice(voices[0].VoiceInfo.Name);
            }
        }

        private void Initialize()
        {
            // Default location for the window
            Size screenSize = Screen.GetBounds(this).Size;
            this.Location = new Point(screenSize.Width - this.Width, screenSize.Height - this.Height);
            mainTabControl.SelectedTab = speechTabPage;
        }

        private string ProcessIntegerString(string integerString)
        {
            long integer;
            Boolean ok = long.TryParse(integerString, out integer);
            if (ok)
            {
                string integerWord = "";
                if (integer >= ONE_BILLION)
                {
                    int numberOfBillions =  (int)(integer / ONE_BILLION);
                    integerWord = numberOfBillions.ToString() + " " + "billion" + " ";
                    int remainder = (int)(integer - ONE_BILLION * numberOfBillions);
                    string remainderWord = ProcessIntegerString(remainder.ToString());
                    integerWord += remainderWord;
                    return integerWord;
                }
                if (integer >= ONE_MILLION)
                {
                    int numberOfMillions = (int)Math.Truncate(integer / (double)ONE_MILLION);
                    integerWord = numberOfMillions.ToString() + " " + "million" + " ";
                    long remainder = integer - ONE_MILLION * numberOfMillions;
                    string remainderWord = ProcessIntegerString(remainder.ToString());
                    integerWord += remainderWord;
                    return integerWord;
                }
                else if (integer >= ONE_THOUSAND)
                {
                    int numberOfThousands = (int)Math.Truncate(integer / (double)ONE_THOUSAND);
                    if (numberOfThousands > 0)
                    {
                        integerWord += numberOfThousands.ToString() + " " + "thousand" + " ";
                    }
                    long remainder = integer - ONE_THOUSAND * numberOfThousands;
                    if (remainder > 0)
                    {
                        integerWord += remainder;
                    }
                    return integerWord;
                }
                else
                {
                    return integer.ToString();
                }
            }
            else { return ""; }
        }

        private void Speak(DataPacket dataPacket)
        {
            string sentence = dataPacket.Message;
            List<string> sentenceSplit = sentence.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            string processedSentence = "";
            int index = 0;
            while (index < sentenceSplit.Count)
            {
                string word = sentenceSplit[index];
                if (word.StartsWith("#"))
                {
                    if (word == "#integer")  // Note: Speech output is sent in lower case
                    {
                        if (sentenceSplit.Count > (index + 1))
                        {
                            word = ProcessIntegerString(sentenceSplit[index + 1]);
                            index += 1;
                        }
                    }
                }
                processedSentence += word + " ";
                index++;
            }
            processedSentence = processedSentence.TrimEnd(new char[] { ' ' });
            speechSynthesizer.SpeakAsync(processedSentence);
            //   speechSynthesizer.SpeakAsync(sentence);
            ColorListBoxItem item = new ColorListBoxItem(dataPacket.TimeStamp.ToString("yyyyMMdd HH:mm:ss") + ": " + processedSentence, speechColorListBox.BackColor,
                speechColorListBox.ForeColor);
            speechColorListBox.Items.Insert(0, item);
        }

        private void Connect()
        {
            client = new Client();
            client.Received += new EventHandler<DataPacketEventArgs>(HandleClientReceived);
            client.Progress += new EventHandler<CommunicationProgressEventArgs>(HandleClientProgress);
            client.Name = CLIENT_NAME;
            client.Connect(ipAddress, port);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void HandleClientReceived(object sender, DataPacketEventArgs e)
        {
            if (InvokeRequired) { BeginInvoke(new MethodInvoker(() => Speak(e.DataPacket))); }
            else { Speak(e.DataPacket); }
        }

        private void HandleClientProgress(object sender, CommunicationProgressEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => ShowProgress(e)));
            }
            else { ShowProgress(e); }
        }

        private void ShowProgress(CommunicationProgressEventArgs e)
        {
            ColorListBoxItem item;
            item = new ColorListBoxItem(e.Message, communicationLogListBox.BackColor, communicationLogListBox.ForeColor);
            communicationLogListBox.Items.Insert(0, item);
        }

        private void SpeechMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
