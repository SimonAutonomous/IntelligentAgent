using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AgentLibrary.Memories;

namespace AgentLibrary.DialogueItems
{
    [DataContract]
    public class InputItem: DialogueItem
    {
        private const double DEFAULT_TIMEOUT_INTERVAL = 60; // Really long default timeout (mostly for debugging...)

        private const int DEFAULT_MAXIMUM_REPETITION_COUNT = 3;

        private List<InputAction> inputActionList;
        private List<string> queryTagList; // Used in cases where one or several query terms should be added to working memory
        private double timeoutInterval = DEFAULT_TIMEOUT_INTERVAL; // in s.
        private List<Pattern> failureResponsePatternList;
        private int maximumRepetitionCount;
        private string finalFailureTargetContext;
        private string finalFailureTargetID;
        private Boolean allowVisionInput = false;  // Default value

        public override void Initialize(Agent ownerAgent)
        {
            base.Initialize(ownerAgent);
            foreach (InputAction inputAction in inputActionList)
            {
                foreach (Pattern pattern in inputAction.PatternList)
                {
                    pattern.ProcessDefinition();
               //     pattern.ProcessDefinitionList();
                }
                foreach (Pattern failurePattern in failureResponsePatternList)
                {
                    failurePattern.ProcessDefinition();
                }
            }
        }

        // Sets everything except (i) the inputActionList and (ii) the (optional) failureResponsePatternList
        public InputItem(string id, List<string> queryTagList, double timeoutInterval, int maximumRepetitionCount, string finalFailureTargetContext,
                         string finalFailureTargetID)
        {
            this.id = id;
            this.queryTagList = queryTagList;
            this.timeoutInterval = timeoutInterval;
            this.maximumRepetitionCount = maximumRepetitionCount;
            this.finalFailureTargetContext = finalFailureTargetContext;
            this.finalFailureTargetID = finalFailureTargetID;
            inputActionList = new List<InputAction>();
            failureResponsePatternList = new List<Pattern>();
            allowVisionInput = false; // Default value
            doReset = true; // Default value
        }

        public InputItem()
        {
            inputActionList = new List<InputAction>();
            queryTagList = new List<string>();
            failureResponsePatternList = new List<Pattern>();
            maximumRepetitionCount = DEFAULT_MAXIMUM_REPETITION_COUNT; 
            finalFailureTargetContext = ""; // Default value if the user gives repeated incomprehensible inputs.
        }

        private void AddQueryTermsToWorkingMemory(List<string> queryTerms)
        {
            List<MemoryItem> queryMemoryItemList = new List<MemoryItem>();
            for (int ii = 0; ii < queryTerms.Count; ii++)
            {
                string queryTerm = queryTerms[ii];
                string queryTag = queryTagList[ii];
                StringMemoryItem queryMemoryItem = new StringMemoryItem();
                queryMemoryItem.TagList.Add(queryTag);
                queryMemoryItem.SetContent(queryTerm);
                queryMemoryItemList.Add(queryMemoryItem);
            }
            ownerAgent.WorkingMemory.AddItems(queryMemoryItemList);


         /*   StringMemoryItem queryMemoryItem = new StringMemoryItem();
            foreach (string queryTag in queryTagList)
            {
                queryMemoryItem.TagList.Add(queryTag);
            }
            string queryTermsString = "";
            foreach (string queryTerm in queryTerms)
            {
                queryTermsString += queryTerm + ownerAgent.MemoryItemSeparationCharacter;
            }
            queryTermsString = queryTermsString.TrimEnd(new char[] { ownerAgent.MemoryItemSeparationCharacter });
            queryMemoryItem.SetContent(queryTermsString);
            ownerAgent.WorkingMemory.AddItem(queryMemoryItem);   */
        }

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            base.Run(parameterList, out targetContext, out targetID);
            if (repetitionCount > maximumRepetitionCount)  // Giving up after repeated incomprehensible inputs: Leave the dialogue
            {
                targetContext = finalFailureTargetContext;
                targetID = finalFailureTargetID;
                if (doReset)
                {
                    repetitionCount = 0;
                }
                return true;
            }
            else
            {
                string inputString = (string)parameterList[0];
                string tag = (string)parameterList[1]; // see Agent.HandleInput()
                Pattern matchingPattern = null;
                targetContext = null;
                targetID = null;
                foreach (InputAction inputAction in inputActionList)
                {
                    Boolean isMatching = inputAction.CheckMatch(inputString, tag, out matchingPattern);
                    if (isMatching)
                    {
                        targetContext = inputAction.TargetContext;
                        targetID = inputAction.TargetID;
                        if (doReset)
                        {
                            repetitionCount = 0; // 20171025. Once the agent moves to a different item, the repetition count should be reset.
                        }
                        List<string> queryTerms = matchingPattern.GetQueryTerms();
                        if (queryTerms.Count > 0) { AddQueryTermsToWorkingMemory(queryTerms); }
                        return true;
                    }
                    else { repetitionCount -= 1; }  // Does not count as a repetition if no match was found.
                }
            }
            return false;
        }

        public string GetFailureResponsePhrase(Random randomNumberGenerator)
        {
            int patternIndex = randomNumberGenerator.Next(0, failureResponsePatternList.Count);
            Pattern pattern = failureResponsePatternList[patternIndex];
            string outputString = pattern.GetString(randomNumberGenerator, null);
            return outputString;
        }

        [DataMember]
        public List<InputAction> InputActionList
        {
            get { return inputActionList; }
            set { inputActionList = value; }
        }

        [DataMember]
        public List<string> QueryTagList
        {
            get { return queryTagList; }
            set { queryTagList = value; }
        }

        [DataMember]
        public double TimeoutInterval
        {
            get { return timeoutInterval; }
            set { timeoutInterval = value; }
        }

        [DataMember]
        public List<Pattern> FailureResponsePatternList
        {
            get { return failureResponsePatternList; }
            set { failureResponsePatternList = value; }
        }

        [DataMember]
        public int MaximumRepetitionCount
        {
            get { return maximumRepetitionCount; }
            set { maximumRepetitionCount = value; }
        }

        [DataMember]
        public string FinalFailureTargetContext
        {
            get { return finalFailureTargetContext; }
            set { finalFailureTargetContext = value; }
        }

        [DataMember]
        public string FinalFailureTargetID
        {
            get { return finalFailureTargetID; }
            set { finalFailureTargetID = value; }
        }

        [DataMember]
        public Boolean AllowVisionInput
        {
            get { return allowVisionInput; }
            set { allowVisionInput = value; }
        }

        [DataMember] 
        public Boolean DoReset
        {
            get { return doReset; }
            set { doReset = value; }
        }
    }
}
