using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AgentApplication.AddedClasses;
using AgentLibrary;
using AgentLibrary.DialogueItems;
using AgentLibrary.Memories;
using AuxiliaryLibrary;
using CommunicationLibrary;
using CustomUserControlsLibrary;
using ObjectSerializerLibrary;

namespace AgentApplication
{
    public partial class AgentMainForm : Form
    {
        #region Constants
        private const string DATETIME_FORMAT = "yyyyMMdd HH:mm:ss";
        private const string DEFAULT_LONGTERM_MEMORY_RELATIVE_PATH = "..\\..\\..\\Data\\LongTermMemory.xml";
        #endregion

        #region Fields
        private Agent agent;
        private static object communicationLogLockObject = new object();
        #endregion

        public AgentMainForm()
        {
            InitializeComponent();
            memoryItemDataGridView.ItemChanged += new EventHandler(HandleLongTermMemoryItemChanged);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            dialogueListCheckedListBox.Enabled = false;

            TimerResolution.TimeBeginPeriod(1);  // Sets the timer resolution to 1 ms (default in Windows 7: 15.6 ms)
            startButton.Enabled = false;
            exitToolStripMenuItem.Enabled = false;

            // Check which processes should be enabled:
            for (int ii = 0; ii < dialogueListCheckedListBox.Items.Count; ii++)
            {
                string context = dialogueListCheckedListBox.Items[ii].ToString();
                int dialogueIndex = agent.DialogueList.FindIndex(d => d.Context == context);
                if (dialogueIndex > 0) // Not allowed to remove dialogue 0 (startup dialogue)
                {
                    if (dialogueListCheckedListBox.GetItemChecked(ii) == false)
                    {
                        agent.DialogueList.RemoveAt(dialogueIndex);
                    }
                }
            }

            agent.AutoArrangeWindows = autoarrangeWindowsToolStripMenuItem.Checked;
            Boolean startSuccessful = agent.StartProcesses();
            if (startSuccessful)
            {
                agent.Start(agent.DialogueList[0].Context);  // Start from the first dialogue in the list
                workingMemoryViewer.SetWorkingMemory(agent.WorkingMemory);
                workingMemoryViewer.ShowMemory();
                stopButton.Enabled = true;
            }
            else
            {
                startButton.Enabled = true;
                stopButton.Enabled = false;
            }
        }

        private void HandleAgentServerProgress(object sender, CommunicationProgressEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => InsertLogMessage(e)));
            }
            else { InsertLogMessage(e); }
        }

        private void HandleAgentServerError(object sender, CommunicationErrorEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => InsertErrorMessage(e)));
            }
            else { InsertErrorMessage(e); }
        }

        private void InsertLogMessage(CommunicationProgressEventArgs e)
        {
            Monitor.Enter(communicationLogLockObject);
            Color itemBackColor = communicationLogColorListBox.BackColor;
            Color ItemForeColor = communicationLogColorListBox.ForeColor;
            ColorListBoxItem item = new ColorListBoxItem(e.DateTime.ToString(DATETIME_FORMAT) + ": " + e.Message,
                itemBackColor, ItemForeColor);
            communicationLogColorListBox.Items.Insert(0, item);
            Monitor.Exit(communicationLogLockObject);
        }

        private void InsertErrorMessage(CommunicationErrorEventArgs e)
        {
            Monitor.Enter(communicationLogLockObject);
            Color itemBackColor = communicationLogColorListBox.BackColor;
            Color ItemForeColor = communicationLogColorListBox.ForeColor;
            ColorListBoxItem item = new ColorListBoxItem(e.DateTime.ToString(DATETIME_FORMAT) + ": " + e.Message,
                itemBackColor, ItemForeColor);
            communicationLogColorListBox.Items.Insert(0, item);
            Monitor.Exit(communicationLogLockObject);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            stopButton.Enabled = false;
            if (agent != null)
            {
                agent.Stop();
            }
            exitToolStripMenuItem.Enabled = true;
            generateAgentToolStripMenuItem.Enabled = true;
            dialogueListCheckedListBox.Enabled = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void longTermMemoryViewer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = longTermMemoryViewer.SelectedIndex;
            if (selectedIndex >= 0)
            {
                MemoryItem memoryItem = agent.LongTermMemory.ItemList[selectedIndex];
                memoryItemDataGridView.SetMemoryItem(memoryItem);
            }
        }

        private void HandleLongTermMemoryItemChanged(object sender, EventArgs e)
        {
            longTermMemoryViewer.UpdateView();
        }

        private void generateAgentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 20180112:
            this.Width = Screen.GetBounds(this).Width / 2;
            this.Location = new Point((Screen.GetBounds(this).Width - this.Width) / 2, 0); 
            generateAgentToolStripMenuItem.Enabled = false;

            // Generate the agent instance, and set the search paths to the constituent programs:
            agent = new Agent();
            agent.LongTermMemoryRelativePath = DEFAULT_LONGTERM_MEMORY_RELATIVE_PATH;
            agent.ClientProcessRelativePathList.Add("..\\..\\..\\FaceApplication\\bin\\Debug\\FaceApplication.exe");
            agent.ClientProcessRelativePathList.Add("..\\..\\..\\ListenerApplication\\bin\\Debug\\ListenerApplication.exe");
            agent.ClientProcessRelativePathList.Add("..\\..\\..\\SpeechApplication\\bin\\Debug\\SpeechApplication.exe");
            agent.ClientProcessRelativePathList.Add("..\\..\\..\\InternetDataAcquisitionApplication\\bin\\Debug\\InternetDataAcquisitionApplication.exe");
            agent.ClientProcessRelativePathList.Add("..\\..\\..\\VisionApplication\\bin\\Debug\\VisionApplication.exe");
            agent.Server.Progress += new EventHandler<CommunicationProgressEventArgs>(HandleAgentServerProgress);
            agent.Server.Error += new EventHandler<CommunicationErrorEventArgs>(HandleAgentServerError);


            // Generate new dialogues
            GenerateIntroductionDialogue();
            GenerateRatingDialogue();
            GenerateTasteProfilingDialogue();
            GenerateRecommendationDialogue();
            GenerateMovieInformationDialogue();
            
            // Generate existing dialogues
            GenerateWakeUpDialogue();
            GenerateTimeDialogue();
            GenerateAttentionDialogue();
            // GenerateNameDialogue();
            GenerateWhoIsDialogue();
            GenerateWhatIsDialogue();
            GenerateIntegerArithmeticDialogue();
            GenerateGreetingDialogue();

            ShowDialogueList();
            importMemoryItemsButton.Enabled = true;
            saveLongTermMemoryButton.Enabled = true;

            // Prepare for starting the agent: Load the long-term memory already here, so that it can be viewed and edited before starting the agent
            string longTermMemoryFilePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + agent.LongTermMemoryRelativePath;
            if (!File.Exists(longTermMemoryFilePath))
            {
                MessageBox.Show("No long-term memory found");
                agent.LongTermMemory = new Memory();
                generateAgentToolStripMenuItem.Enabled = true;
            }
            else
            {
                agent.LongTermMemory = (Memory)ObjectXmlSerializer.ObtainSerializedObject(longTermMemoryFilePath, typeof(Memory));
                longTermMemoryViewer.SetMemory(agent.LongTermMemory);
              //  dialogueListPropertyPanel.SetObject(agent.DialogueList);
              //  runButton.Enabled = true;
            }
            startButton.Enabled = true;
        }

        private void ShowDialogueList()
        {
            dialogueListCheckedListBox.Items.Clear();
            int index = 0;
            foreach (Dialogue dialogue in agent.DialogueList)
            {
                dialogueListCheckedListBox.Items.Add(dialogue.Context);
                dialogueListCheckedListBox.SetItemChecked(index, true);
                index++;
            }
            if (dialogueListCheckedListBox.Items.Count > 0)
            {
                dialogueListCheckedListBox.SetItemCheckState(0, CheckState.Indeterminate);
            }
        }

        private void dialogueListCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index == 0) { e.NewValue = CheckState.Indeterminate; }
        }

        #region New agent dialogues

        // User introduction
        private void GenerateIntroductionDialogue()
        {
            Boolean isAlwaysAvailable = true;
            double inputTimeoutInterval = double.MaxValue; 
            int inputMaximumRepetitionCount = int.MaxValue; 

            Dialogue introductionDialogue = new Dialogue("IntroductionDialogue", isAlwaysAvailable);

            // Item ID1: User introduces him-/herself
            InputItem itemID1 = new InputItem("ID1", new List<string>() { AgentConstants.QUERY_TAG_5 },
                inputTimeoutInterval, inputMaximumRepetitionCount, "", "");
            InputAction inputActionID1 = new InputAction(introductionDialogue.Context, "ID2");
            inputActionID1.PatternList.Add(new Pattern("My name is" + " " + AgentConstants.QUERY_TAG_5));
            inputActionID1.PatternList.Add(new Pattern("I am" + " " + AgentConstants.QUERY_TAG_5));
            itemID1.InputActionList.Add(inputActionID1);
            introductionDialogue.DialogueItemList.Add(itemID1);

            // Item ID2: Search for existing user in ultraManager
            UserIntroductionItem itemID2 = new UserIntroductionItem("ID2", new List<string>() { AgentConstants.QUERY_TAG_5 }, 
                introductionDialogue.Context, "ID3", introductionDialogue.Context, "ID4");
            introductionDialogue.DialogueItemList.Add(itemID2);

            // Item ID3: If new user: Agent greets new user and suggests to make a taste profile
            OutputItem itemID3 = new OutputItem("ID3", AgentConstants.SPEECH_OUTPUT_TAG, new List<string>() { AgentConstants.QUERY_TAG_5 }, false, 1);
            itemID3.OutputAction = new OutputAction("" , ""); 
            itemID3.OutputAction.PatternList.Add(new Pattern("Nice to meet you" + " " + AgentConstants.QUERY_TAG_5 + " " + "I would recommend you make a taste profile"));
            introductionDialogue.DialogueItemList.Add(itemID3);

            // Item ID4: If existing user: Agent checks if the user has an open rating from a previous recommendation
            OpenRatingItem itemID4 = new OpenRatingItem("ID4", new List<string>() { AgentConstants.QUERY_TAG_5 },
                AgentConstants.QUERY_TAG_1, introductionDialogue.Context, "ID6", introductionDialogue.Context, "ID5");
            introductionDialogue.DialogueItemList.Add(itemID4);

            // Item ID5: If existing user without open rating: Agent welcomes user back
            OutputItem itemID5 = new OutputItem("ID5", AgentConstants.SPEECH_OUTPUT_TAG, new List<string>() { AgentConstants.QUERY_TAG_5 }, false, 1);
            itemID5.OutputAction = new OutputAction("", "");
            itemID5.OutputAction.PatternList.Add(new Pattern("Welcome back" + " " + AgentConstants.QUERY_TAG_5));
            introductionDialogue.DialogueItemList.Add(itemID5);

            // Item ID6: If existing user that has an open rating: Agent asks user to rate the movie
            OutputItem itemID6 = new OutputItem("ID6", AgentConstants.SPEECH_OUTPUT_TAG, new List<string>() { AgentConstants.QUERY_TAG_1, AgentConstants.QUERY_TAG_5 }, false, 1);
            itemID6.OutputAction = new OutputAction(introductionDialogue.Context, "ID7");
            itemID6.OutputAction.PatternList.Add(new Pattern("Welcome back" + " " + AgentConstants.QUERY_TAG_5 + " " + "It seems you have an open rating for" + " " + 
                                                             AgentConstants.QUERY_TAG_1 + " " + " how would you rate it from 1 to 10"));
            introductionDialogue.DialogueItemList.Add(itemID6);

            // Item ID7: User is expected to leave a rating (rating 0 if user didn't watch the recommended movie from last time
            InputItem itemID7 = new InputItem("ID7", new List<string>() { AgentConstants.QUERY_TAG_2 },
                inputTimeoutInterval, inputMaximumRepetitionCount, "", "");
            InputAction inputActionID7 = new InputAction(introductionDialogue.Context, "ID8");
            inputActionID7.PatternList.Add(new Pattern("" + AgentConstants.QUERY_TAG_2));
            inputActionID7.PatternList.Add(new Pattern("I would [rate give] it" + " " + AgentConstants.QUERY_TAG_2));
            inputActionID7.PatternList.Add(new Pattern("I would [rate give] it [a an]" + " " + AgentConstants.QUERY_TAG_2));
            itemID7.InputActionList.Add(inputActionID7);
            introductionDialogue.DialogueItemList.Add(itemID7);

            // Item ID8: Insert rating into ultraManager ratingList
            RatingItem itemID8 = new RatingItem("ID8", new List<string>() { AgentConstants.QUERY_TAG_1, AgentConstants.QUERY_TAG_2, AgentConstants.QUERY_TAG_5 }, "", "", introductionDialogue.Context, "ID9");
            introductionDialogue.DialogueItemList.Add(itemID8);

            // Item ID9: If rating given by user is not a valid rating
            OutputItem itemID9 = new OutputItem("ID9", AgentConstants.SPEECH_OUTPUT_TAG, null, false, 10);
            itemID9.OutputAction = new OutputAction(introductionDialogue.Context, "ID7");
            itemID9.OutputAction.PatternList.Add(new Pattern("The given rating is not valid"));
            itemID9.OutputAction.PatternList.Add(new Pattern("Please try again"));
            itemID9.OutputAction.PatternList.Add(new Pattern("The rating should be a number between 0 and 10"));
            introductionDialogue.DialogueItemList.Add(itemID9);

            agent.DialogueList.Add(introductionDialogue);
        }

        // User wants to rate a movie
        private void GenerateRatingDialogue()
        {
            Boolean isAlwaysAvailable = true;
            double inputTimeoutInterval = double.MaxValue;
            int inputMaximumRepetitionCount = int.MaxValue;

            Dialogue ratingDialogue = new Dialogue("RatingDialogue", isAlwaysAvailable);

            // Item RD1: User suggests to rate a movie
            InputItem itemRD11 = new InputItem("RD11", new List<string>() { AgentConstants.QUERY_TAG_1, AgentConstants.QUERY_TAG_2, AgentConstants.QUERY_TAG_3 },
                inputTimeoutInterval, inputMaximumRepetitionCount, "", "");
            InputAction inputActionRD11 = new InputAction(ratingDialogue.Context, "RD12");
            inputActionRD11.PatternList.Add(new Pattern("I would like to rate" + " " + AgentConstants.QUERY_TAG_1)); //TODO: able to rate movies longer than 1 word?
            inputActionRD11.PatternList.Add(new Pattern("I would like to rate" + " " + AgentConstants.QUERY_TAG_1 + " " + AgentConstants.QUERY_TAG_2));
            inputActionRD11.PatternList.Add(new Pattern("I would like to rate" + " " + AgentConstants.QUERY_TAG_1 + " " + AgentConstants.QUERY_TAG_2 + " " + AgentConstants.QUERY_TAG_3));
            itemRD11.InputActionList.Add(inputActionRD11);
            ratingDialogue.DialogueItemList.Add(itemRD11);

            // Item RD12: Make sure that there is a current user --> introduction dialogue complete
            UserCheckItem itemRD12 = new UserCheckItem("RD12", new List<string>() { AgentConstants.QUERY_TAG_5 }, ratingDialogue.Context, "RD13", "", "");
            ratingDialogue.DialogueItemList.Add(itemRD12);

            // Item RD13: Takes <Q1> - <Q3> and the movie is only in one querry tag --> <Q4>
            MovieInputItem itemRD13 = new MovieInputItem("RD13", new List<string>()
                { AgentConstants.QUERY_TAG_1, AgentConstants.QUERY_TAG_2, AgentConstants.QUERY_TAG_3 }, AgentConstants.QUERY_TAG_4, ratingDialogue.Context, "RD2");
            ratingDialogue.DialogueItemList.Add(itemRD13);

            // Item RD2: Agent ask user for rating between 1 and 10 or 0 if user hasn't seen the movie
            OutputItem itemRD2 = new OutputItem("RD2", AgentConstants.SPEECH_OUTPUT_TAG, new List<string>() { AgentConstants.QUERY_TAG_4 }, false, 1);
            itemRD2.OutputAction = new OutputAction(ratingDialogue.Context, "RD3");
            itemRD2.OutputAction.PatternList.Add(new Pattern("How would you rate" + " " + AgentConstants.QUERY_TAG_4 + " " + "from 1 to 10"));
            ratingDialogue.DialogueItemList.Add(itemRD2);

            // Item RD3: User responds with a rating 
            InputItem itemRD3 = new InputItem("RD3", new List<string>() { AgentConstants.QUERY_TAG_2 },
                inputTimeoutInterval, inputMaximumRepetitionCount, "", "");
            InputAction inputActionRD3 = new InputAction(ratingDialogue.Context, "RD4");
            inputActionRD3.PatternList.Add(new Pattern("I would [rate give] it" + " " + AgentConstants.QUERY_TAG_2));
            inputActionRD3.PatternList.Add(new Pattern("I would [rate give] it [a an]" + " " + AgentConstants.QUERY_TAG_2));
            itemRD3.InputActionList.Add(inputActionRD3);
            ratingDialogue.DialogueItemList.Add(itemRD3);

            // Item RD4: Insert rating into ultraManager ratingList
            RatingItem itemRD4 = new RatingItem("RD4", new List<string>() { AgentConstants.QUERY_TAG_4, AgentConstants.QUERY_TAG_2, AgentConstants.QUERY_TAG_5 }, "", "", ratingDialogue.Context, "RD5");
            ratingDialogue.DialogueItemList.Add(itemRD4);

            // Item IRD5: If rating given by user is not a valid rating
            OutputItem itemRD5 = new OutputItem("RD5", AgentConstants.SPEECH_OUTPUT_TAG, null, false, 10);
            itemRD5.OutputAction = new OutputAction(ratingDialogue.Context, "RD3");
            itemRD5.OutputAction.PatternList.Add(new Pattern("The given rating is not valid"));
            itemRD5.OutputAction.PatternList.Add(new Pattern("Please try again"));
            itemRD5.OutputAction.PatternList.Add(new Pattern("The rating should be a number between 0 and 10"));
            ratingDialogue.DialogueItemList.Add(itemRD5);

            agent.DialogueList.Add(ratingDialogue);
        }

        // Generate taste profile of a user --> specially necessary if the current user is a new user, else the movie recommendation can't be tailored to specific taste
        private void GenerateTasteProfilingDialogue()
        {
            Boolean isAlwaysAvailable = true;
            double inputTimeoutInterval = double.MaxValue; 
            int inputMaximumRepetitionCount = 10; // repeat 10 times or until no more unseen movies are left

            Dialogue tasteProfilingDialogue = new Dialogue("TasteProfilingDialogue", isAlwaysAvailable);

            // Item TP11: User suggests to make a taste profile
            InputItem itemTP11 = new InputItem("TP11", null,
                inputTimeoutInterval, inputMaximumRepetitionCount, "", "");
            InputAction inputActionTP11 = new InputAction(tasteProfilingDialogue.Context, "TP12");
            inputActionTP11.PatternList.Add(new Pattern("Taste profiling"));
            inputActionTP11.PatternList.Add(new Pattern("Let me make a taste profile"));
            inputActionTP11.PatternList.Add(new Pattern("I would like to make a taste profile"));
            itemTP11.InputActionList.Add(inputActionTP11);
            tasteProfilingDialogue.DialogueItemList.Add(itemTP11);

            // Item TP12: Make sure that there is a current user --> introduction dialogue complete
            UserCheckItem itemTP12 = new UserCheckItem("TP12", new List<string>() { AgentConstants.QUERY_TAG_5 }, tasteProfilingDialogue.Context, "TP2", "", "");
            tasteProfilingDialogue.DialogueItemList.Add(itemTP12);

            // Item TP2: Agent describes rating procedure
            OutputItem itemTP2 = new OutputItem("TP2", AgentConstants.SPEECH_OUTPUT_TAG, null, false, 1);
            itemTP2.OutputAction = new OutputAction(tasteProfilingDialogue.Context, "TP3");
            itemTP2.OutputAction.PatternList.Add(new Pattern("I will tell you a movie and you will give a corresponding rating between 1 and 10. " +
                                                             "You can rate a movie as 0 if you have not seen it"));
            tasteProfilingDialogue.DialogueItemList.Add(itemTP2);

            // Item TP3: Agent gets random unseen movie to rate
            TasteProfilingItem itemTP3 = new TasteProfilingItem("TP3", new List<string>() { AgentConstants.QUERY_TAG_5 }, 
                inputMaximumRepetitionCount, AgentConstants.QUERY_TAG_1, tasteProfilingDialogue.Context, "TP4", tasteProfilingDialogue.Context, "TP8");
            tasteProfilingDialogue.DialogueItemList.Add(itemTP3);

            // Item TP4: Ask user for rating between 1 and 10 or 0 if not seen
            OutputItem itemTP4 = new OutputItem("TP4", AgentConstants.SPEECH_OUTPUT_TAG, new List<string>() { AgentConstants.QUERY_TAG_1 }, false, 10);
            itemTP4.OutputAction = new OutputAction(tasteProfilingDialogue.Context, "TP5");
            itemTP4.OutputAction.PatternList.Add(new Pattern("Rate" + " " + AgentConstants.QUERY_TAG_1));
            itemTP4.OutputAction.PatternList.Add(new Pattern("What about" + " " + AgentConstants.QUERY_TAG_1));
            tasteProfilingDialogue.DialogueItemList.Add(itemTP4);

            // Item TP5: User leaves a  rating
            InputItem itemTP5 = new InputItem("TP5", new List<string>() { AgentConstants.QUERY_TAG_2 },
                inputTimeoutInterval, inputMaximumRepetitionCount, "", "");
            InputAction inputActionTP5 = new InputAction(tasteProfilingDialogue.Context, "TP6");
            inputActionTP5.PatternList.Add(new Pattern("" + AgentConstants.QUERY_TAG_2));
            itemTP5.InputActionList.Add(inputActionTP5);
            tasteProfilingDialogue.DialogueItemList.Add(itemTP5);

            // Item RD4: Insert rating into ultraManager ratingList
            RatingItem itemTP6 = new RatingItem("TP6", new List<string>() { AgentConstants.QUERY_TAG_1, AgentConstants.QUERY_TAG_2, AgentConstants.QUERY_TAG_5 }, tasteProfilingDialogue.Context, "TP3", tasteProfilingDialogue.Context, "TP7");
            tasteProfilingDialogue.DialogueItemList.Add(itemTP6);

            // Item IRD5: If rating given by user is not a valid rating
            OutputItem itemTP7 = new OutputItem("TP7", AgentConstants.SPEECH_OUTPUT_TAG, null, false, 30);
            itemTP7.OutputAction = new OutputAction(tasteProfilingDialogue.Context, "TP5");
            itemTP7.OutputAction.PatternList.Add(new Pattern("The given rating is not valid"));
            itemTP7.OutputAction.PatternList.Add(new Pattern("Please try again"));
            itemTP7.OutputAction.PatternList.Add(new Pattern("The rating should be a number between 0 and 10"));
            tasteProfilingDialogue.DialogueItemList.Add(itemTP7);

            // Item TP7: Successfuly completed taste profiling
            OutputItem itemTP8 = new OutputItem("TP8", AgentConstants.SPEECH_OUTPUT_TAG, new List<string>() { AgentConstants.QUERY_TAG_1 }, false, 1);
            itemTP8.OutputAction = new OutputAction("", "");
            itemTP8.OutputAction.PatternList.Add(new Pattern("You have successfuly completed your taste profile"));
            itemTP8.OutputAction.PatternList.Add(new Pattern("Your taste profile is now complete"));
            tasteProfilingDialogue.DialogueItemList.Add(itemTP8);

            agent.DialogueList.Add(tasteProfilingDialogue);
        }

        // Recommend unseen movie tailored to specific taste of current user
        private void GenerateRecommendationDialogue()
        {
            Boolean isAlwaysAvailable = true;
            double inputTimeoutInterval = double.MaxValue; 
            int inputMaximumRepetitionCount = int.MaxValue; 

            Dialogue recommendationDialogue = new Dialogue("RecommendationDialogue", isAlwaysAvailable);

            // Item RC11: User demands a recommendation
            InputItem itemRC11 = new InputItem("RC11", null,
                inputTimeoutInterval, inputMaximumRepetitionCount, "", "");
            InputAction inputActionRC11 = new InputAction(recommendationDialogue.Context, "RC12");
            inputActionRC11.PatternList.Add(new Pattern("[Recommend Suggest] me a movie"));
            inputActionRC11.PatternList.Add(new Pattern("Make me a * [recommendation suggestion]"));
            itemRC11.InputActionList.Add(inputActionRC11);
            recommendationDialogue.DialogueItemList.Add(itemRC11);

            // Item RC12: Make sure that there is a current user --> introduction dialogue complete
            UserCheckItem itemRC12 = new UserCheckItem("RC12", new List<string>() { AgentConstants.QUERY_TAG_5 }, recommendationDialogue.Context, "RC2", "", "");
            recommendationDialogue.DialogueItemList.Add(itemRC12);

            // Item RC2:
            OutputItem itemRC2 = new OutputItem("RC2", AgentConstants.SPEECH_OUTPUT_TAG, null, false, 1);
            itemRC2.OutputAction = new OutputAction(recommendationDialogue.Context, "RC3");
            itemRC2.OutputAction.PatternList.Add(new Pattern("I will search for a suitable movie, just a moment"));
            itemRC2.OutputAction.PatternList.Add(new Pattern("Let me think what you might like"));
            recommendationDialogue.DialogueItemList.Add(itemRC2);

            // Item RC3: Get random unseen movie to rate
            RecommendationItem itemRC3 = new RecommendationItem("RC3", new List<string>() { AgentConstants.QUERY_TAG_5 }, AgentConstants.QUERY_TAG_1);
            itemRC3.OutputAction = new OutputAction(recommendationDialogue.Context, "RC4");
            recommendationDialogue.DialogueItemList.Add(itemRC3);

            // Item RC4: Recommend the movie
            OutputItem itemRC4 = new OutputItem("RC4", AgentConstants.SPEECH_OUTPUT_TAG, new List<string>() { AgentConstants.QUERY_TAG_1 }, false, 10);
            itemRC4.OutputAction = new OutputAction("", "");
            itemRC4.OutputAction.PatternList.Add(new Pattern("How about" + " " + AgentConstants.QUERY_TAG_1));
            recommendationDialogue.DialogueItemList.Add(itemRC4);

            agent.DialogueList.Add(recommendationDialogue);
        }

        // This dialogue informs the user about any movie the user demands 
        private void GenerateMovieInformationDialogue()
        {
            Boolean isAlwaysAvailable = true;
            double inputTimeoutInterval = double.MaxValue;
            int inputMaximumRepetitionCount = int.MaxValue;
            double searchWaitingTime = 5.0;

            Dialogue movieInformationDialogue = new Dialogue("MovieInformationDialogue", isAlwaysAvailable);

            // Item MI11: The user asks the agent about a specifiy movie
            InputItem itemMI11 = new InputItem("MI11", new List<string>() { AgentConstants.QUERY_TAG_1, AgentConstants.QUERY_TAG_2, AgentConstants.QUERY_TAG_3 },
                inputTimeoutInterval, inputMaximumRepetitionCount, "", "");
            InputAction inputActionMI11 = new InputAction(movieInformationDialogue.Context, "MI12");
            inputActionMI11.PatternList.Add(new Pattern("Tell me about" + " " + AgentConstants.QUERY_TAG_1));
            inputActionMI11.PatternList.Add(new Pattern("Tell me about" + " " + AgentConstants.QUERY_TAG_1 + " " + AgentConstants.QUERY_TAG_2));
            inputActionMI11.PatternList.Add(new Pattern("Tell me about" + " " + AgentConstants.QUERY_TAG_1 + " " + AgentConstants.QUERY_TAG_2 + " " + AgentConstants.QUERY_TAG_3));
            itemMI11.InputActionList.Add(inputActionMI11);
            movieInformationDialogue.DialogueItemList.Add(itemMI11);

            // Item RD13: Takes <Q1> - <Q3> and the movie is only in one querry tag --> <Q4>
            MovieInputItem itemMI12 = new MovieInputItem("MI12", new List<string>()
                { AgentConstants.QUERY_TAG_1, AgentConstants.QUERY_TAG_2, AgentConstants.QUERY_TAG_3 }, AgentConstants.QUERY_TAG_4, movieInformationDialogue.Context, "MI2");
            movieInformationDialogue.DialogueItemList.Add(itemMI12);

            // Item MI2: The agent searches the existing movies in ultraManager
            MovieInformationItem itemMI2 = new MovieInformationItem("MI2", new List<string>() { AgentConstants.QUERY_TAG_4 }, "", "", movieInformationDialogue.Context, "MI3");
            movieInformationDialogue.DialogueItemList.Add(itemMI2);

            // Item MI3: If the movie is not yet in the database --> ultraManager, the agent will start a internet search
            OutputItem itemMI3 = new OutputItem("MI3", AgentConstants.INTERNET_OUTPUT_TAG, new List<string>() { AgentConstants.QUERY_TAG_4 }, false, 1);
            itemMI3.OutputAction = new OutputAction(movieInformationDialogue.Context, "MI4");
            itemMI3.OutputAction.PatternList.Add(new Pattern("Imdb|" + " " + AgentConstants.QUERY_TAG_4));
            movieInformationDialogue.DialogueItemList.Add(itemMI3);

            // Item MI4: ...and await the results
            WaitItem itemMI4 = new WaitItem("MI4", searchWaitingTime);
            itemMI4.OutputAction = new OutputAction(movieInformationDialogue.Context, "MI5");
            movieInformationDialogue.DialogueItemList.Add(itemMI4);

            // Item MI5: The agent searches its working memory for the requested movie which should have been inserted by the internet data aquisition
            MemorySearchItem itemMI5 = new MemorySearchItem("MI5", AgentConstants.WORKING_MEMORY_NAME, new List<string>() { AgentConstants.QUERY_TAG_4 },
                new List<string>() { "movie" }, TagSearchMode.Or, "title", "infoToProcess", AgentConstants.QUERY_TAG_2, movieInformationDialogue.Context, "MI6",
                movieInformationDialogue.Context, "MI7");
            movieInformationDialogue.DialogueItemList.Add(itemMI5);

            // Item MI6: Insert Movie from working-memory into ultraManager
            UltraManagerInsertionItem itemMI6 = new UltraManagerInsertionItem("MI6",
                new List<string>() {AgentConstants.QUERY_TAG_2}, AgentConstants.QUERY_TAG_4, movieInformationDialogue.Context, "MI2");
            movieInformationDialogue.DialogueItemList.Add(itemMI6);

            // Item MI7: If the movie was not found in working memory --> internet data aquisition failed
            OutputItem itemMI7 = new OutputItem("MI7", AgentConstants.SPEECH_OUTPUT_TAG, new List<string>() { AgentConstants.QUERY_TAG_1 }, false, 10);
            itemMI7.OutputAction = new OutputAction("", "");
            itemMI7.OutputAction.PatternList.Add(new Pattern("I am sorry but no information regarding " + " " + AgentConstants.QUERY_TAG_1 + " could be found"));
            movieInformationDialogue.DialogueItemList.Add(itemMI7);

            agent.DialogueList.Add(movieInformationDialogue);
        }

        #endregion

        #region Existing agent dialogues

        // This dialogue is not really a dialogue - it simply opens the eyes of the agent
        private void GenerateWakeUpDialogue()
        {
            Boolean isAlwaysAvailable = true;
            Boolean useVerbatimString = true; // Important: set to true here, since the face process expects case-sensitive input ("OpenEyes", not "openeyes").

            Dialogue wakeUpDialogue = new Dialogue("WakeUpDialogue", isAlwaysAvailable);

            // Item WU1: The agent sends the "OpenEyes" command to the face process:
            OutputItem itemWU1 = new OutputItem("WU1", AgentConstants.FACE_EXPRESSION_OUTPUT_TAG, null, useVerbatimString, 1);
            itemWU1.OutputAction = new OutputAction("", ""); // Abandon this context after completing the wu1 item
            itemWU1.OutputAction.PatternList.Add(new Pattern("OpenEyes"));
            wakeUpDialogue.DialogueItemList.Add(itemWU1);

            agent.DialogueList.Add(wakeUpDialogue);
        }

        // This dialogue handles time requests
        private void GenerateTimeDialogue()
        {
            Boolean isAlwaysAvailable = true;
            double inputTimeoutInterval = double.MaxValue; // No reason to have a timeout here, since the dialogue is _activated_ upon receiving matching input.
            int inputMaximumRepetitionCount = int.MaxValue; // No reason to have a repetition count here, for the reason just mentioned.

            Dialogue timeDialogue = new Dialogue("TimeRequest", isAlwaysAvailable);

            // Item TR1: User requests the current time
            InputItem itemTR1 = new InputItem("TR1", null, inputTimeoutInterval, inputMaximumRepetitionCount, "", "");
            InputAction inputActionTR1 = new InputAction(timeDialogue.Context, "TR2");
            inputActionTR1.PatternList.Add(new Pattern("What time is it"));
            itemTR1.InputActionList.Add(inputActionTR1);
            timeDialogue.DialogueItemList.Add(itemTR1);

            // Item TR2: The agent responds
            TimeItem itemTR2 = new TimeItem("TR2");
            itemTR2.OutputAction = new OutputAction("", ""); // Abandon this context after completing the output
            itemTR2.OutputAction.PatternList.Add(new Pattern("It is "));
            itemTR2.OutputAction.PatternList.Add(new Pattern("The time is "));
            timeDialogue.DialogueItemList.Add(itemTR2);

            agent.DialogueList.Add(timeDialogue);
        }

        // This dialogue simply serves to show that the agent is attentive
        private void GenerateAttentionDialogue()
        {
            Boolean isAlwaysAvailable = true;
            double inputTimeoutInterval = double.MaxValue; // No reason to have a timeout here, since the dialogue is _activated_ upon receiving matching input.
            int inputMaximumRepetitionCount = int.MaxValue; // No reason to have a repetition count here, for the reason just mentioned.

            Dialogue attentionDialogue = new Dialogue("AttentionDialogue", isAlwaysAvailable);
            // Item AD1: The user requests the agent's attention
            InputItem itemAD1 = new InputItem("AD1", null, inputTimeoutInterval, inputMaximumRepetitionCount, "", "");
            InputAction inputActionAD1 = new InputAction(attentionDialogue.Context, "AD2");
            inputActionAD1.PatternList.Add(new Pattern("Hazel"));
            itemAD1.InputActionList.Add(inputActionAD1);
            attentionDialogue.DialogueItemList.Add(itemAD1);

            // Item TR2: The agent responds
            Boolean useVerbatimString = false;
            OutputItem itemAD2 = new OutputItem("AD2", AgentConstants.SPEECH_OUTPUT_TAG, null, useVerbatimString, 1);
            itemAD2.OutputAction = new OutputAction("", ""); // Abandon this context after completing the wu1 item
            itemAD2.OutputAction.PatternList.Add(new Pattern("How can I be of service?"));
            itemAD2.OutputAction.PatternList.Add(new Pattern("Yes?"));
            attentionDialogue.DialogueItemList.Add(itemAD2);

            agent.DialogueList.Add(attentionDialogue);

        }

        // In this dialogue, the user asks for the agent's name
        private void GenerateNameDialogue()
        {
            // TBW
        }

        // In this dialogue, the user asks for information regarding some (well-known) person.
        // The agent first checks if the information is available in its long-term memory.
        // If not, it runs an internet (Wikipedia) search to try to find an answer.
        private void GenerateWhoIsDialogue()
        {
            Boolean isAlwaysAvailable = true;
            double inputTimeoutInterval = double.MaxValue; // No reason to have a timeout here, since the dialogue is _activated_ upon receiving matching input.
            int inputMaximumRepetitionCount = int.MaxValue; // No reason to have a repetition count here, for the reason just mentioned.
            double searchWaitingTime = 5.0;

            Dialogue whoIsDialogue = new Dialogue("WhoIsDialogue", isAlwaysAvailable);

            // The user asks a question of the form "What is a <Q>"?
            InputItem itemWI1 = new InputItem("WI1", new List<string>() { AgentConstants.QUERY_TAG_1, AgentConstants.QUERY_TAG_2 },
                                              inputTimeoutInterval, inputMaximumRepetitionCount, "", "");
            InputAction inputActionWI1 = new InputAction(whoIsDialogue.Context, "WI2");
            inputActionWI1.PatternList.Add(new Pattern("Who is" + " " + AgentConstants.QUERY_TAG_1 + " " +
                AgentConstants.QUERY_TAG_2));
            itemWI1.InputActionList.Add(inputActionWI1);
            whoIsDialogue.DialogueItemList.Add(itemWI1);

            // The agent searches its long-term memory for (the description of) an object (tag = object) with the required name
            MemorySearchItem itemWI2 = new MemorySearchItem("WI2", AgentConstants.LONG_TERM_MEMORY_NAME, new List<string>() { AgentConstants.QUERY_TAG_1,
            AgentConstants.QUERY_TAG_2},
                new List<string>() { "person" }, TagSearchMode.Or, "name", "description", AgentConstants.QUERY_TAG_3, whoIsDialogue.Context, "WI7",
                whoIsDialogue.Context, "WI3");
            whoIsDialogue.DialogueItemList.Add(itemWI2);

            // If the information cannot be found in long-term memory
            OutputItem itemWI3 = new OutputItem("WI3", AgentConstants.SPEECH_OUTPUT_TAG, null, false, 1);
            itemWI3.OutputAction = new OutputAction(whoIsDialogue.Context, "WI4");
            itemWI3.OutputAction.PatternList.Add(new Pattern("Just a moment, let me check"));
            itemWI3.OutputAction.PatternList.Add(new Pattern("I don't know, but I will find out"));
            whoIsDialogue.DialogueItemList.Add(itemWI3);

            // Run a search...
            OutputItem itemWI4 = new OutputItem("WI4", AgentConstants.INTERNET_OUTPUT_TAG, new List<string>() { AgentConstants.QUERY_TAG_1,
            AgentConstants.QUERY_TAG_2}, false, 1);
            itemWI4.OutputAction = new OutputAction(whoIsDialogue.Context, "WI5");
            itemWI4.OutputAction.PatternList.Add(new Pattern("Wiki|Person|" + " " + AgentConstants.QUERY_TAG_1 + " " + 
                AgentConstants.QUERY_TAG_2));
            whoIsDialogue.DialogueItemList.Add(itemWI4);

            // ...and await the results (and the search memory again)
            WaitItem itemWI5 = new WaitItem("WI5", searchWaitingTime);
            itemWI5.OutputAction = new OutputAction(whoIsDialogue.Context, "WI2");
            whoIsDialogue.DialogueItemList.Add(itemWI5);

            // If the item IS found:
            OutputItem itemWI7 = new OutputItem("WI7", AgentConstants.SPEECH_OUTPUT_TAG, new List<string>() { AgentConstants.QUERY_TAG_3 }, false, 1);
            itemWI7.OutputAction = new OutputAction("", "");
            itemWI7.OutputAction.PatternList.Add(new Pattern(AgentConstants.QUERY_TAG_3));
            whoIsDialogue.DialogueItemList.Add(itemWI7);

            agent.DialogueList.Add(whoIsDialogue);
        }

        // In this dialogue, the user asks for information regarding some object,
        // using a question of the form "What is a <Q>", where <Q> is the query term.
        // The agent first checks if the information is available in its long-term memory.
        // If not, it runs an internet (Wikipedia) search to try to find an answer.
        private void GenerateWhatIsDialogue()
        {
            Boolean isAlwaysAvailable = true;
            double inputTimeoutInterval = double.MaxValue; // No reason to have a timeout here, since the dialogue is _activated_ upon receiving matching input.
            int inputMaximumRepetitionCount = int.MaxValue; // No reason to have a repetition count here, for the reason just mentioned.
            double searchWaitingTime = 5.0;

            Dialogue whatIsDialogue = new Dialogue("WhatIsDialogue", isAlwaysAvailable);

            // The user asks a question of the form "What is a <Q>"?
            InputItem itemWI1 = new InputItem("WI1", new List<string>() { AgentConstants.QUERY_TAG_1 },
                                              inputTimeoutInterval, inputMaximumRepetitionCount, "", "");
            InputAction inputActionWI1 = new InputAction(whatIsDialogue.Context, "WI2");
            inputActionWI1.PatternList.Add(new Pattern("What is [a an]" + " " + AgentConstants.QUERY_TAG_1));
            inputActionWI1.PatternList.Add(new Pattern("What is " + " " + AgentConstants.QUERY_TAG_1));
            itemWI1.InputActionList.Add(inputActionWI1);
            whatIsDialogue.DialogueItemList.Add(itemWI1);

            // The agent searches its long-term memory for (the description of) an object (tag = object) with the required name
            MemorySearchItem itemWI2 = new MemorySearchItem("WI2", AgentConstants.LONG_TERM_MEMORY_NAME, new List<string>() { AgentConstants.QUERY_TAG_1 },
                new List<string>() { "object" }, TagSearchMode.Or, "name", "description", AgentConstants.QUERY_TAG_3, whatIsDialogue.Context, "WI7",
                whatIsDialogue.Context, "WI3");
            whatIsDialogue.DialogueItemList.Add(itemWI2);

            // If the information cannot be found in long-term memory
            OutputItem itemWI3 = new OutputItem("WI3", AgentConstants.SPEECH_OUTPUT_TAG, null, false, 1);
            itemWI3.OutputAction = new OutputAction(whatIsDialogue.Context, "WI4");
            itemWI3.OutputAction.PatternList.Add(new Pattern("I don't know, but I will find out"));
            itemWI3.OutputAction.PatternList.Add(new Pattern("Just a moment, let me check"));
            whatIsDialogue.DialogueItemList.Add(itemWI3);

            // Run a search...
            OutputItem itemWI4 = new OutputItem("WI4", AgentConstants.INTERNET_OUTPUT_TAG, new List<string>() { AgentConstants.QUERY_TAG_1 }, false, 1);
            itemWI4.OutputAction = new OutputAction(whatIsDialogue.Context, "WI5");
            itemWI4.OutputAction.PatternList.Add(new Pattern("Wiki|Object|" + " " + AgentConstants.QUERY_TAG_1));
            whatIsDialogue.DialogueItemList.Add(itemWI4);

            // ...and await the results (and the search memory again)
            WaitItem itemWI5 = new WaitItem("WI5", searchWaitingTime);
            itemWI5.OutputAction = new OutputAction(whatIsDialogue.Context, "WI2");
            whatIsDialogue.DialogueItemList.Add(itemWI5);

            // If the item IS found:
            OutputItem itemWI7 = new OutputItem("WI7", AgentConstants.SPEECH_OUTPUT_TAG, new List<string>() { AgentConstants.QUERY_TAG_3 }, false, 1);
            itemWI7.OutputAction = new OutputAction("", "");
            itemWI7.OutputAction.PatternList.Add(new Pattern(AgentConstants.QUERY_TAG_3));
            whatIsDialogue.DialogueItemList.Add(itemWI7);

            agent.DialogueList.Add(whatIsDialogue);
        }

        // Here, the user can ask for basic mathematical calculations involving integers
        // (product, sum, difference)
        private void GenerateIntegerArithmeticDialogue()
        {
            Boolean isAlwaysAvailable = true;
            double timeoutInterval = double.MaxValue; // No reason to have a timeout here, since the dialogue is _activated_ upon receiving matching input.
            int maximumRepetitionCount = int.MaxValue; // No reason to have a repetition count here, for the reason just mentioned.

            Dialogue integerArithmeticDialogue = new Dialogue("IntegerArithmetic", isAlwaysAvailable);

            InputItem itemIA1 = new InputItem("IA1", new List<string>() { AgentConstants.QUERY_TAG_1, AgentConstants.QUERY_TAG_2 }, timeoutInterval,
                maximumRepetitionCount, "", "");
            InputAction inputActionIA11 = new InputAction(integerArithmeticDialogue.Context, "IA2");
            inputActionIA11.PatternList.Add(new Pattern("What is the product of " + AgentConstants.QUERY_TAG_1 + " and " + AgentConstants.QUERY_TAG_2));
            inputActionIA11.PatternList.Add(new Pattern("What is " + AgentConstants.QUERY_TAG_1 + " times " + AgentConstants.QUERY_TAG_2));
            itemIA1.InputActionList.Add(inputActionIA11);
            InputAction inputActionIA12 = new InputAction(integerArithmeticDialogue.Context, "IA4");
            inputActionIA12.PatternList.Add(new Pattern("What is the sum of " + AgentConstants.QUERY_TAG_1 + " and " + AgentConstants.QUERY_TAG_2));
            inputActionIA12.PatternList.Add(new Pattern("What is " + AgentConstants.QUERY_TAG_1 + " plus " + AgentConstants.QUERY_TAG_2));
            itemIA1.InputActionList.Add(inputActionIA12);
            InputAction inputActionIA13 = new InputAction(integerArithmeticDialogue.Context, "IA6");
            inputActionIA13.PatternList.Add(new Pattern("What is " + AgentConstants.QUERY_TAG_1 + " minus " + AgentConstants.QUERY_TAG_2));
            itemIA1.InputActionList.Add(inputActionIA13);

            integerArithmeticDialogue.DialogueItemList.Add(itemIA1);

            IntegerArithmeticItem itemIA2 = new IntegerArithmeticItem("IA2", "multiplication", new List<string>() { AgentConstants.QUERY_TAG_1, AgentConstants.QUERY_TAG_2 },
                AgentConstants.QUERY_TAG_3, integerArithmeticDialogue.Context, "IA3");
            integerArithmeticDialogue.DialogueItemList.Add(itemIA2);

            OutputItem itemIA3 = new OutputItem("IA3", AgentConstants.SPEECH_OUTPUT_TAG, new List<string>() { AgentConstants.QUERY_TAG_1, AgentConstants.QUERY_TAG_2,
            AgentConstants.QUERY_TAG_3}, false, maximumRepetitionCount);
            itemIA3.OutputAction = new OutputAction("", "");
            itemIA3.OutputAction.PatternList.Add(new Pattern("The product of " + AgentConstants.SPEECH_OUTPUT_INTEGER + " " + AgentConstants.QUERY_TAG_1 + " and " +
                AgentConstants.SPEECH_OUTPUT_INTEGER + " " + AgentConstants.QUERY_TAG_2 + " " + "[equals is]" + " " +
                AgentConstants.SPEECH_OUTPUT_INTEGER + " " + AgentConstants.QUERY_TAG_3));
            integerArithmeticDialogue.DialogueItemList.Add(itemIA3);

            IntegerArithmeticItem itemIA4 = new IntegerArithmeticItem("IA4", "addition", new List<string>() { AgentConstants.QUERY_TAG_1, AgentConstants.QUERY_TAG_2 },
                AgentConstants.QUERY_TAG_3, integerArithmeticDialogue.Context, "IA5");
            integerArithmeticDialogue.DialogueItemList.Add(itemIA4);

            OutputItem itemIA5 = new OutputItem("IA5", AgentConstants.SPEECH_OUTPUT_TAG, new List<string>() { AgentConstants.QUERY_TAG_1, AgentConstants.QUERY_TAG_2,
            AgentConstants.QUERY_TAG_3}, false, maximumRepetitionCount);
            itemIA5.OutputAction = new OutputAction("", "");
            itemIA5.OutputAction.PatternList.Add(new Pattern("The sum of " + AgentConstants.SPEECH_OUTPUT_INTEGER + " " + AgentConstants.QUERY_TAG_1 + " and " +
                AgentConstants.SPEECH_OUTPUT_INTEGER + " " + AgentConstants.QUERY_TAG_2 + " " + "[equals is]" + " " +
                AgentConstants.SPEECH_OUTPUT_INTEGER + " " + AgentConstants.QUERY_TAG_3));
            integerArithmeticDialogue.DialogueItemList.Add(itemIA5);

            IntegerArithmeticItem itemIA6 = new IntegerArithmeticItem("IA6", "subtraction", new List<string>() { AgentConstants.QUERY_TAG_1, AgentConstants.QUERY_TAG_2 },
                AgentConstants.QUERY_TAG_3, integerArithmeticDialogue.Context, "IA7");
            integerArithmeticDialogue.DialogueItemList.Add(itemIA6);

            OutputItem itemIA7 = new OutputItem("IA7", AgentConstants.SPEECH_OUTPUT_TAG, new List<string>() { AgentConstants.QUERY_TAG_1, AgentConstants.QUERY_TAG_2,
            AgentConstants.QUERY_TAG_3}, false, maximumRepetitionCount);
            itemIA7.OutputAction = new OutputAction("", "");
            itemIA7.OutputAction.PatternList.Add(new Pattern(AgentConstants.SPEECH_OUTPUT_INTEGER + " " + AgentConstants.QUERY_TAG_1 + " minus " +
                AgentConstants.SPEECH_OUTPUT_INTEGER + " " + AgentConstants.QUERY_TAG_2 + " " + "[equals is]" + " " +
                AgentConstants.SPEECH_OUTPUT_INTEGER + " " + AgentConstants.QUERY_TAG_3));
            integerArithmeticDialogue.DialogueItemList.Add(itemIA7);

            agent.DialogueList.Add(integerArithmeticDialogue);

        }

        // This dialogue greets the user (as a result of face recognition input
        // from the vision process.
        private void GenerateGreetingDialogue()
        {
            Boolean isAlwaysAvailable = true;
            double inputTimeoutInterval = double.MaxValue; // No reason to have a timeout here, since the dialogue is _activated_ upon receiving matching input.
            int inputMaximumRepetitionCount = 1; // Greet the user only once.

            Dialogue greetingDialogue = new Dialogue("GreetingDialogue", isAlwaysAvailable);

            // The agent checks if a face has just been detected. 
            // NOTE: This requires, of course, that the Vision process should send the
            // appropriate message, in this case "FaceDetected".
            InputItem itemGD1 = new InputItem("GD1", null, inputTimeoutInterval, inputMaximumRepetitionCount, "", "");
            itemGD1.AllowVisionInput = true; // Default value = false. Must set to true for this dialogue item.
            itemGD1.DoReset = false; // Make sure that the greeting runs only once.
            InputAction inputActionGD1 = new InputAction(greetingDialogue.Context, "GD2");
            inputActionGD1.RequiredSource = AgentConstants.VISION_INPUT_TAG;
            inputActionGD1.PatternList.Add(new Pattern("FaceDetected"));
            itemGD1.InputActionList.Add(inputActionGD1);
            greetingDialogue.DialogueItemList.Add(itemGD1);
            OutputItem itemGD2 = new OutputItem("GD2", AgentConstants.SPEECH_OUTPUT_TAG, null, false, 1);
            OutputAction outputActionGD2 = new OutputAction("", "");
            outputActionGD2.PatternList.Add(new Pattern("Hello user"));
            itemGD2.OutputAction = outputActionGD2;
            greetingDialogue.DialogueItemList.Add(itemGD2);

            agent.DialogueList.Add(greetingDialogue);
        }
        #endregion

        private void newLongtermMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            agent = new Agent();
            agent.LongTermMemory = new Memory();
            longTermMemoryViewer.SetMemory(agent.LongTermMemory);
        }

        private void saveLongtermMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "XML files (*.xml)|*.xml";
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Application.ExecutablePath) + "\\" + DEFAULT_LONGTERM_MEMORY_RELATIVE_PATH);
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ObjectXmlSerializer.SerializeObject(saveFileDialog.FileName, agent.LongTermMemory);
                }
            }
        }

        private void AgentMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (agent != null)
            {
                agent.Stop();
            }
        }

        private void importMemoryItemsButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                string longTermMemoryDirectoryPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + agent.LongTermMemoryRelativePath;
                openFileDialog.InitialDirectory = longTermMemoryDirectoryPath;
                openFileDialog.Filter = "text files (*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    List<MemoryItem> addedItemsList = new List<MemoryItem>();
                    StreamReader memoryItemReader = new StreamReader(openFileDialog.FileName);
                    string errorString = "Errors on lines: ";
                    Boolean errorsFound = false;
                    int lineNumber = 0;
                    while (!memoryItemReader.EndOfStream)
                    {
                        string information = memoryItemReader.ReadLine();
                        if (information != null)
                        {
                            Boolean itemOK = false;
                            List<string> tagsContentList = information.Split(new string[] { "[Tags]", "[Content]" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            if (tagsContentList.Count == 2)
                            {
                                if (information.IndexOf("Tags") < information.IndexOf("Content"))
                                {
                                    string tagInformation = tagsContentList[0];
                                    if ((!tagInformation.Contains("="))  && (!tagInformation.Contains(":"))) // Make sure that the user does not accidentally add a "=" or ":"
                                    {
                                        List<string> tagList = tagInformation.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                                        if (tagList.Count > 0)
                                        {
                                            string content = tagsContentList[1];
                                            if (content != null)
                                            {
                                                if (content != "")
                                                {
                                                    itemOK = true;
                                                    StringMemoryItem item = new StringMemoryItem();
                                                    item.TagList = tagList;
                                                    item.SetContent(content);
                                                    item.InsertionTime = DateTime.Now;
                                                    addedItemsList.Add(item);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (!itemOK)
                            {
                                errorsFound = true;
                                errorString += lineNumber.ToString() + ",";
                            }
                        }
                    }
                    if (addedItemsList.Count > 0)
                    {
                        agent.LongTermMemory.AddItems(addedItemsList);
                    }
                    if (errorsFound)
                    {
                        MessageBox.Show("Import errors", errorString);
                    }
                }
            }
        }

        private void saveLongTermMemoryButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Application.ExecutablePath) + "\\" + agent.LongTermMemoryRelativePath);
                saveFileDialog.Filter = "XML files (*.xml)|*.xml";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ObjectXmlSerializer.SerializeObject(saveFileDialog.FileName, agent.LongTermMemory);
                }
            }
        }
    }
}
