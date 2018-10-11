using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunicationLibrary;
using AgentLibrary.DialogueItems;
using AgentLibrary.Memories;

namespace AgentLibrary
{
    [DataContract]
    public class Agent
    {
        #region Constants
        private const int DEFAULT_WINDOW_MOVE_MILLISECOND_SLEEP_TIME = 250;
        private const int DEFAULT_START_DELAY_MILLISECOND_SLEEP_TIME = 250;
        #endregion

        #region Fields
        protected string name = AgentConstants.DEFAULT_NAME;

        // Agent brain-related fields:
        protected List<Dialogue> dialogueList;
        protected WorkingMemory workingMemory;
        protected Memory longtermMemory;

        protected string longTermMemoryRelativePath = "";

        protected DateTime timeOfLastListenerInput;
        protected DateTime timeOfLastSpeechOutput;
        protected DateTime timeOfLastFaceExpressionOutput;
        protected DateTime timeOfLastInternetRequest;
        protected DateTime timeOfLastVisionInput;
        protected DateTime timeOfLastInternetInput;

        protected string listenerInputTag = AgentConstants.LISTENER_INPUT_TAG;
        protected string speechOutputTag = AgentConstants.SPEECH_OUTPUT_TAG;
        protected string visionInputTag = AgentConstants.VISION_INPUT_TAG;
        protected string internetOutputTag = AgentConstants.INTERNET_OUTPUT_TAG;
        protected string internetInputTag = AgentConstants.INTERNET_INPUT_TAG;
        protected string faceExpressionOutputTag = AgentConstants.FACE_EXPRESSION_OUTPUT_TAG;
     //   protected string queryTag = AgentConstants.DEFAULT_QUERY_TAG;

        protected Random randomNumberGenerator;

        private Boolean busy = false;

        protected char memoryItemSeparationCharacter = AgentConstants.MEMORY_ITEM_SEPARATION_CHARACTER;

        private DialogueItemTimer dialogueItemTimer;

        private List<Pattern> failureResponsePatternList = null;

        private int windowMoveMillisecondSleepTime = DEFAULT_WINDOW_MOVE_MILLISECOND_SLEEP_TIME;
        private int startDelayMillisecondSleepTime = DEFAULT_START_DELAY_MILLISECOND_SLEEP_TIME;

        // Communication-related fields:
        private string ipAddress = AgentConstants.DEFAULT_IP_ADDRESS;
        private int communicationPort = AgentConstants.DEFAULT_COMMUNICATION_PORT;
        private List<string> clientProcessRelativePathList = null;
        private List<Process> clientProcessList = null; // Pointers to all (other) agent processes.
        private Server server = null;
        private Boolean running = false;

        private Boolean autoArrangeWindows = false;
        #endregion

        #region External methods
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetForegroundWindow();

        internal struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowRect(IntPtr hWnd, ref Rect rect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;
        #endregion

        #region Constructors
        public Agent()
        {
            clientProcessRelativePathList = new List<string>();
            running = false;
            server = new Server();
            server.Name = AgentConstants.AGENT_PROCESS_NAME;
            dialogueList = new List<Dialogue>();
            randomNumberGenerator = new Random();
         //   longtermMemory = new Memory();   // 20171120: Do not generate (or clear) the long-term memory here - it will be loaded from file before
                                               //           the agent is started.
        }
        #endregion

        #region Private methods
        private void HandleServerReceived(object sender, DataPacketEventArgs e)
        {
            // Add the input to working memory
            string senderName = e.DataPacket.SenderName;
            StringMemoryItem inputItem = null;
            if (senderName == AgentConstants.LISTENER_PROCESS_NAME)
            {
                inputItem = new StringMemoryItem();
                inputItem.TagList.Add(listenerInputTag);
                inputItem.SetContent(e.DataPacket.Message);
                workingMemory.AddItem(inputItem);
                timeOfLastListenerInput = inputItem.InsertionTime;
                if (!busy) { HandleUserInput(inputItem); }
            }
            else if (senderName == AgentConstants.VISION_PROCESS_NAME)
            {
                inputItem = new StringMemoryItem();
                inputItem.TagList.Add(visionInputTag);
                inputItem.SetContent(e.DataPacket.Message);
                workingMemory.AddItem(inputItem);
                timeOfLastVisionInput = inputItem.InsertionTime;
                if (!busy) { HandleUserInput(inputItem); }
            }
            else if (senderName == AgentConstants.INTERNET_PROCESS_NAME)
            {
                ProcessInternetInput(e.DataPacket.Message);
            }    
        }

        // The message should be formatted as [<dest>][{tag1, ..., tagN}][<content>]
        // where <dest> is either "WorkingMemory" or "LongTermMemory". The tag list is optional for items
        // inserted into working memory, but must be present for long-term memory items. If tags are included,
        // they are added to the tag list of the memory item. Then the content (specified in <content>, without
        // the < >) is added as well.
        // Note that the message must have PRECISELY the shape defined above!
        private void ProcessInternetInput(string message)
        {
            StringMemoryItem internetInputItem = new StringMemoryItem();
            char leftSeparationCharacter = AgentConstants.INTERNET_INPUT_LEFT_SEPARATION_CHARACTER;
            char rightSeparationCharacter = AgentConstants.INTERNET_INPUT_RIGHT_SEPARATION_CHARACTER;
            List<string> messageSplit = message.Split(new char[] { leftSeparationCharacter, rightSeparationCharacter }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (messageSplit.Count == 2)  // No tags
            {
                if (messageSplit[0] == AgentConstants.WORKING_MEMORY_NAME)
                {
                    string content = messageSplit[1];
                    if (content != null)
                    {
                        if (content.Length > 0)
                        {
                            internetInputItem.SetContent(content);
                            internetInputItem.TagList.Add(internetInputTag);
                            workingMemory.AddItem(internetInputItem);
                            timeOfLastInternetInput = internetInputItem.InsertionTime;
                   //         if (!busy) { HandleInput(internetInputItem); }
                        }
                    }
                }
            }
            else if (messageSplit.Count == 3)
            {
                if (messageSplit[0] == AgentConstants.WORKING_MEMORY_NAME)
                {
                    string content = messageSplit[2];
                    if (content != null)
                    {
                        if (content.Length > 0)
                        {
                            internetInputItem.SetContent(content);
                            internetInputItem.TagList.Add(internetInputTag);
                            char leftListCharacter = AgentConstants.INTERNET_INPUT_TAG_LIST_LEFT_CHARACTER;
                            char rightListCharacter = AgentConstants.INTERNET_INPUT_TAG_LIST_RIGHT_CHARACTER;
                            char separator = AgentConstants.INTERNET_INPUT_TAG_LIST_SEPARATOR_CHARACTER;
                            List<string> tagListSplit = messageSplit[1].Split(new char[] { leftListCharacter, rightListCharacter, separator },
                                StringSplitOptions.RemoveEmptyEntries).ToList();
                            if (tagListSplit != null)
                            {
                                if (tagListSplit.Count > 0)
                                {
                                    foreach (string tag in tagListSplit) { internetInputItem.TagList.Add(tag); }
                                }
                            }
                            workingMemory.AddItem(internetInputItem);
                            timeOfLastInternetInput = internetInputItem.InsertionTime;
                      //      if (!busy) { HandleInput(internetInputItem); }
                        }
                    }
                }
                else if (messageSplit[0] == AgentConstants.LONG_TERM_MEMORY_NAME)
                {
                    // Insertions on long-term memory do not trigger actions by the agent, but the contents
                    // will of course be available for later use.
                    // Also, for long-term memory items, tags are _required_. If no tags are present, the
                    // input is simply ignored.
                    char leftListCharacter = AgentConstants.INTERNET_INPUT_TAG_LIST_LEFT_CHARACTER;
                    char rightListCharacter = AgentConstants.INTERNET_INPUT_TAG_LIST_RIGHT_CHARACTER;
                    char separator = AgentConstants.INTERNET_INPUT_TAG_LIST_SEPARATOR_CHARACTER;
                    List<string> tagListSplit = messageSplit[1].Split(new char[] { leftListCharacter, rightListCharacter, separator },
                        StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (tagListSplit != null)
                    {
                        if (tagListSplit.Count > 0)
                        {
                            foreach (string tag in tagListSplit) { internetInputItem.TagList.Add(tag); }
                            string content = messageSplit[2];
                            if (content != null)
                            {
                                if (content.Length > 0)
                                {
                                    internetInputItem.SetContent(content);
                                    longtermMemory.AddItem(internetInputItem);
                                    timeOfLastInternetInput = internetInputItem.InsertionTime;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void HandleUserInput(MemoryItem inputItem)
        {
            busy = true;
            dialogueItemTimer.Stop(); // In case the time was running, make sure that it is stopped here, to avoid repeating the agent's previous statement.
            string inputString = (string)inputItem.GetContent();
            string sourceTag = inputItem.TagList[0]; // Note: input items have precisely one tag; see HandleServerReceived().
            List<object> parameterList = new List<object>() { inputString, sourceTag };
            string targetContext = workingMemory.CurrentContext;
            string targetID = workingMemory.CurrentID;
            DialogueItem currentDialogueItem = FindCurrentDialogueItem();
            Boolean itemProcessed = false;           
            if (currentDialogueItem != null)
            {
                if (currentDialogueItem is InputItem) // OK with type comparison here (InputItem is the only possibility).
                {
                    Boolean allowVisionInput = ((InputItem)currentDialogueItem).AllowVisionInput;
                    if ((sourceTag == AgentConstants.LISTENER_INPUT_TAG) ||
                        ((sourceTag == AgentConstants.VISION_INPUT_TAG) && allowVisionInput))
                    {
                        // Run the timeout timer only for those items that have the corresponding property:
                        itemProcessed = currentDialogueItem.Run(parameterList, out targetContext, out targetID);
                        if (itemProcessed)
                        {
                            busy = false;
                            if (targetContext != workingMemory.CurrentContext) { workingMemory.CurrentContext = targetContext; } // Triggers the ContextChangedEvent in working memory
                            else { workingMemory.CurrentID = targetID; } // Triggers the IDChangedEvent in working memory
                            return;
                        }
                    }
                }
            }
            if (!itemProcessed)  // No dialogue item found OR no dialogue matching the context found
            {
                List<Dialogue> alwaysAvailableDialogueList = GetAlwaysAvailableDialogues();
                foreach (Dialogue dialogue in alwaysAvailableDialogueList)
                {
                    DialogueItem dialogueItem = dialogue.DialogueItemList[0];
                    if (dialogueItem is InputItem) // Should be the case, but just to be sure:
                    {
                        Boolean allowVisionInput = ((InputItem)dialogueItem).AllowVisionInput;
                        if ((sourceTag == AgentConstants.LISTENER_INPUT_TAG) ||
                            ((sourceTag == AgentConstants.VISION_INPUT_TAG) && allowVisionInput))
                        {
                            itemProcessed = dialogueItem.Run(parameterList, out targetContext, out targetID);
                            if (itemProcessed)
                            {
                                workingMemory.CurrentContext = dialogue.Context; // Set the context here.
                                busy = false;
                                if (targetContext != workingMemory.CurrentContext) { workingMemory.CurrentContext = targetContext; } // Triggers the ContextChangedEvent in working memory
                                else { workingMemory.CurrentID = targetID; } // Triggers the IDChangedEvent in working memory
                                return;
                            }
                        }
                    }
                }
            }
            // In case no match has been found, either for the current dialogue or for any of the always available dialogues:
            // if the dialogue item has a customized error phrase, use it. Otherwise, use one of the default error phrases

            if (sourceTag == AgentConstants.VISION_INPUT_TAG)  // Do not trigger errors based on vision input
            {
                busy = false;
                return;
            }
            else  // Listener input
            {

                string failureResponsePhrase = GetFailureResponsePhrase();
                if (currentDialogueItem != null)
                {
                    if (currentDialogueItem is InputItem)  // A bit ugly with a typecast, but OK...
                    {
                        InputItem currentInputItem = (InputItem)currentDialogueItem;
                        if (currentInputItem.FailureResponsePatternList != null)
                        {
                            if (currentInputItem.FailureResponsePatternList.Count > 0)
                            {
                                failureResponsePhrase = currentInputItem.GetFailureResponsePhrase(randomNumberGenerator);
                            }
                        }
                    }
                }
                SendSpeechOutput(failureResponsePhrase);
                busy = false;  // to be removed here (see above).
            }
        }

        private string GetFailureResponsePhrase()
        {
            int patternIndex = randomNumberGenerator.Next(0, failureResponsePatternList.Count);
            Pattern pattern = failureResponsePatternList[patternIndex];
            string outputString = pattern.GetString(randomNumberGenerator, null);
            return outputString;
        }

        public void SendSpeechOutput(string outputString)
        {
            StringMemoryItem outputItem = new StringMemoryItem();
            outputItem.TagList.Add(speechOutputTag);
            outputItem.SetContent(outputString);
            workingMemory.AddItem(outputItem);
            timeOfLastSpeechOutput = outputItem.InsertionTime;
            string clientID = server.GetFirstClientID(AgentConstants.SPEECH_PROCESS_NAME);
            if (clientID != null)
            {
                server.Send(clientID, outputString);
            }
        }

        public void SendExpression(string expression)
        {
            StringMemoryItem expressionItem = new StringMemoryItem();
            expressionItem.TagList.Add(faceExpressionOutputTag);
            expressionItem.SetContent(expression);
            workingMemory.AddItem(expressionItem);
            timeOfLastFaceExpressionOutput = expressionItem.InsertionTime;
            string clientID = server.GetFirstClientID(AgentConstants.FACE_PROCESS_NAME);
            if (clientID != null)
            {
                server.Send(clientID, expression);
            }
        }

        public void SendInternetRequest(string request)
        {
            StringMemoryItem requestItem = new StringMemoryItem();
            requestItem.TagList.Add(internetOutputTag);
            requestItem.SetContent(request);
            workingMemory.AddItem(requestItem);
            timeOfLastInternetRequest = requestItem.InsertionTime;
            string clientID = server.GetFirstClientID(AgentConstants.INTERNET_PROCESS_NAME);
            if (clientID != null)
            {
                server.Send(clientID, request);
            }
        }

        // A bit brutal - kills all the client processes
        private void StopClients()
        {
            if (clientProcessList != null)
            {
                foreach (Process process in clientProcessList)
                {
                    if (!process.HasExited)
                    {
                        process.Kill();
                    }
                }
                clientProcessList = null;
            }
        }

        private Boolean StartServer(out string errorMessage)
        {
            // Start server
            Boolean ok = true;
            errorMessage = "";
            timeOfLastSpeechOutput = DateTime.MinValue;
            server.Connect(ipAddress, communicationPort);
            if (server.Connected)
            {
                server.AcceptClients();
                if (clientProcessRelativePathList != null)
                {
                    foreach (string processRelativeFilePath in clientProcessRelativePathList)
                    {
                        string processFilePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + processRelativeFilePath;
                        try
                        {
                            Process process = Process.Start(processFilePath);
                            Thread.Sleep(windowMoveMillisecondSleepTime);
                            if (clientProcessList == null) { clientProcessList = new List<Process>(); }
                            clientProcessList.Add(process);
                        }
                        catch (Exception ex)
                        {
                            errorMessage += Path.GetFileName(processFilePath) + " Failed to start:" + ex.Message + "\r\n";
                            ok = false;
                        }
                    }
                }
            }
            if (!ok) { StopClients(); }
            return ok;
        }

        private Dialogue FindCurrentDialogue()
        {
            Dialogue currentDialogue = dialogueList.Find(d => d.Context == workingMemory.CurrentContext);
            return currentDialogue;
        }

        private DialogueItem FindCurrentDialogueItem()
        {
            Dialogue currentDialogue = FindCurrentDialogue();
            if (currentDialogue != null)
            {
                DialogueItem currentDialogueItem = currentDialogue.DialogueItemList.Find(d => d.ID == workingMemory.CurrentID);
                return currentDialogueItem;
            }
            else { return null; }
        }

        private List<Dialogue> GetAlwaysAvailableDialogues()
        {
            List<Dialogue> alwaysAvailableDialogueList = new List<Dialogue>();
            foreach (Dialogue dialogue in dialogueList)
            {
                if (dialogue.IsAlwaysAvailable) { alwaysAvailableDialogueList.Add(dialogue); }
            }
            return alwaysAvailableDialogueList;
        }

        private void HandleCurrentIDChanged(object sender, EventArgs e)
        {
            DialogueItem currentDialogueItem = FindCurrentDialogueItem();
            if (currentDialogueItem != null)
            {
              //  currentDialogueItem.RepetitionCount++;
                // Run the timeout timer only for those items that have the corresponding property:
                PropertyInfo timeoutProperty = currentDialogueItem.GetType().GetProperty(AgentConstants.TIMEOUT_INTERVAL_PROPERTY_NAME);
                if (timeoutProperty != null)
                {
                    dialogueItemTimer.Stop(); // Just in case it was running previously.
                    double timeoutInterval = (double)timeoutProperty.GetValue(currentDialogueItem);
                    if (timeoutInterval > 0)
                    {
                        dialogueItemTimer.Run(timeoutInterval);
                    }
                }
                if (!(currentDialogueItem is InputItem)) // If this is NOT true, then just await the input; see above.
                {
                    if (currentDialogueItem is AsynchronousDialogueItem)
                    {
                        ((AsynchronousDialogueItem)currentDialogueItem).RunAsynchronously(null);
                    }
                    else
                    {
                        string targetContext = null;
                        string targetID = null;
                        currentDialogueItem.Run(null, out targetContext, out targetID);
                        if (targetContext != workingMemory.CurrentContext) { workingMemory.CurrentContext = targetContext; } // Triggers the ContextChangedEvent in working memory
                        else { workingMemory.CurrentID = targetID; } // Triggers the IDChangedEvent in working memory
                    }
                }
               // else // But perhaps add a timeout, then repeat, then set context = "".
              //  { }
            }
        }

        public void HandleAsynchronousDialogueItemCompleted(object sender, AsynchronousDialogueItemEventArgs e)
        {
            string targetContext = e.TargetContext;
            string targetID = e.TargetID;
            // Here, set the targetID ONLY if 
            //
            // (i)  the agent will change its context anyway upon completion of the asynchronous dialogue item, i.e. if 
            //      the targetContext is different from the original context, or
            //
            // (ii) the agent aims to stay in the same context (upon completion of the asynchronous dialogue item) but 
            //      has been forced to change context for some other reason (e.g. user action). In this case, the
            //      agent will ignore the targedID suggested by the asynchronous dialogue item.
            //
            //
            if (e.TargetContext != e.OriginalContext)
            {
                workingMemory.CurrentContext = e.TargetContext;
            }
            else
            {
                if (targetContext == workingMemory.CurrentContext)
                {
                    workingMemory.CurrentID = targetID;
                }
            }
        }

        private void HandleCurrentContextChanged(object sender, EventArgs e)
        {
            Dialogue currentDialogue = FindCurrentDialogue();
            // Reset repetitions count for dialogues, unless actively prevented:
            if (currentDialogue != null)
            {
                currentDialogue.ResetRepetitionCount();
                if (workingMemory.CurrentID == "") // If no ID has been set, then just start from the beginning.
                {
                    workingMemory.CurrentID = currentDialogue.DialogueItemList[0].ID;
                }
                else
                {
                    DialogueItem currentDialogueItem = FindCurrentDialogueItem();
                    PropertyInfo timeoutProperty = currentDialogueItem.GetType().GetProperty(AgentConstants.TIMEOUT_INTERVAL_PROPERTY_NAME);
                    if (timeoutProperty != null)
                    {
                        dialogueItemTimer.Stop(); // Just in case it was running previously.
                        double timeoutInterval = (double)timeoutProperty.GetValue(currentDialogueItem);
                        dialogueItemTimer.Run(timeoutInterval);
                    }
                }
            }
        }

        private void HandleTimeoutReached(object sender, EventArgs e)
        {
            if (workingMemory.PreviousID != "")
            {
                workingMemory.CurrentID = workingMemory.PreviousID;
            }
        }
        #endregion

        #region Public methods
        public Boolean StartProcesses()
        {
            if (running) { return false; }
            IntPtr agentWindowHandle = IntPtr.Zero;
            if (autoArrangeWindows)
            {
                agentWindowHandle = GetForegroundWindow();
            }
            server.Received -= new EventHandler<DataPacketEventArgs>(HandleServerReceived);
            server.Received += new EventHandler<DataPacketEventArgs>(HandleServerReceived);
            string errorMessage;
            Boolean ok = StartServer(out errorMessage);
            if (ok)
            {
                if (autoArrangeWindows)
                {
                    if (agentWindowHandle != IntPtr.Zero)
                    {
                        SetAgentCenteredWindowPositions(agentWindowHandle);
                    }
                }
                // Make sure that all clients sign in before actually starting the agent.
                while (clientProcessList.Count < clientProcessRelativePathList.Count)
                {
                    Thread.Sleep(windowMoveMillisecondSleepTime);
                }
            }
            return ok;
        }

        public void Start(string startContext)
        {
            randomNumberGenerator = new Random();
            timeOfLastSpeechOutput = DateTime.MinValue;
            timeOfLastListenerInput = DateTime.MinValue;
            workingMemory = new WorkingMemory();

            if (failureResponsePatternList == null)
            {
                failureResponsePatternList = new List<Pattern>();
                Pattern failureResponsePattern1 = new Pattern();
                failureResponsePattern1.Definition = AgentConstants.DEFAULT_FAILURE_RESPONSE_1;
                failureResponsePattern1.ProcessDefinition();
                failureResponsePatternList.Add(failureResponsePattern1);
                Pattern failureResponsePattern2 = new Pattern();
                failureResponsePattern2.Definition = AgentConstants.DEFAULT_FAILURE_RESPONSE_2;
                failureResponsePattern2.ProcessDefinition();
                failureResponsePatternList.Add(failureResponsePattern2);
            }

            foreach (Dialogue dialogue in dialogueList) { dialogue.Initialize(this); }
            dialogueItemTimer = new DialogueItemTimer();
            dialogueItemTimer.TimeoutReached += new EventHandler(HandleTimeoutReached);

            Thread.Sleep(startDelayMillisecondSleepTime);
            workingMemory.CurrentContextChanged += new EventHandler(HandleCurrentContextChanged);
            workingMemory.CurrentIDChanged += new EventHandler(HandleCurrentIDChanged);
            workingMemory.CurrentContext = startContext;
        }

        public void Stop()
        {
            running = false;
            if (dialogueItemTimer != null)
            {
                dialogueItemTimer.Stop();
            }
            StopClients();
            server.Disconnect();
        }

        public void SetAgentCenteredWindowPositions(IntPtr agentWindowHandle)
        {
         //   IntPtr agentWindowHandle = Process.GetCurrentProcess().MainWindowHandle; // GetForegroundWindow();
            Rect windowRect = new Rect();
            GetWindowRect(agentWindowHandle, ref windowRect);
            MoveWindow(agentWindowHandle, windowRect.Left, 0, windowRect.Right - windowRect.Left, Screen.PrimaryScreen.WorkingArea.Height, true);
            Thread.Sleep(windowMoveMillisecondSleepTime); // This is UGLY, but may be needed on some hardware (slow laptops).
            GetWindowRect(agentWindowHandle, ref windowRect);  // Get the new windowRect after moving the window

            int currentRightRelativeTop = windowRect.Top;
            int currentLeftRelativeTop = windowRect.Top;        
            Rect processWindowRect = new Rect();
            Boolean rightSide = true;
            int divisor = clientProcessList.Count / 2;
            if (clientProcessList.Count % 2 != 0) { divisor += 1; }
            int windowHeight = Screen.PrimaryScreen.WorkingArea.Height / divisor;

            foreach (Process process in clientProcessList)
            {
                IntPtr processWindowHandle = process.MainWindowHandle;
                Boolean handleFound = GetWindowRect(processWindowHandle, ref processWindowRect);
                if (handleFound)
                {
                    int processWindowHeight = processWindowRect.Bottom - processWindowRect.Top; // Note: Bottom > Top
                    if (rightSide)
                    {
                        int processWindowWidth = Screen.PrimaryScreen.WorkingArea.Width - windowRect.Right - 1; //  
                        MoveWindow(processWindowHandle, windowRect.Right, currentRightRelativeTop, processWindowWidth, windowHeight, true);
                        Thread.Sleep(windowMoveMillisecondSleepTime);
                     //   SetWindowPos(processWindowHandle, windowRect.Right, currentLeftRelativeTop, )
                        currentRightRelativeTop += windowHeight;
                        rightSide = false;
                    }
                    else
                    {
                        int processWindowWidth = windowRect.Left - 1;
                        MoveWindow(processWindowHandle, windowRect.Left - processWindowWidth, currentLeftRelativeTop, processWindowWidth, windowHeight, true);
                        Thread.Sleep(windowMoveMillisecondSleepTime);
                        currentLeftRelativeTop += windowHeight;
                        rightSide = true;
                    }
                }
            }
            SetForegroundWindow(agentWindowHandle);
        }
        #endregion

        #region DataMember properties
        [DataMember]
        public List<string> ClientProcessRelativePathList
        {
            get { return clientProcessRelativePathList; }
            set { clientProcessRelativePathList = value; }
        }

        [DataMember]
        public List<Dialogue> DialogueList
        {
            get { return dialogueList; }
            set { dialogueList = value; }
        }

        [DataMember]
        public Memory LongTermMemory
        {
            get { return longtermMemory; }
            set { longtermMemory = value; }
        }

        [DataMember]
        public string LongTermMemoryRelativePath
        {
            get { return longTermMemoryRelativePath; }
            set { longTermMemoryRelativePath = value; }
        }

        [DataMember]
        public char MemoryItemSeparationCharacter
        {
            get { return memoryItemSeparationCharacter; }
            set { memoryItemSeparationCharacter = value; }
        }

        [DataMember]
        public List<Pattern> FailureResponsePatternList
        {
            get { return failureResponsePatternList; }
            set { failureResponsePatternList = value; }
        }
        #endregion

        #region Other properties
        public Server Server
        {
            get { return server; }
        }

        public Random RandomNumberGenerator
        {
            get { return randomNumberGenerator; }
        }

        public WorkingMemory WorkingMemory
        {
            get { return workingMemory; }
        }

        public int WindowMoveMillisecondSleepTime
        {
            get { return windowMoveMillisecondSleepTime; }
            set { windowMoveMillisecondSleepTime = value; }
        }

        public Boolean AutoArrangeWindows
        {
            get { return autoArrangeWindows; }
            set { autoArrangeWindows = value; }
        }

        #endregion
    }
}
