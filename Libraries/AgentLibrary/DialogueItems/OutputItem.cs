using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AgentLibrary.Memories;

namespace AgentLibrary.DialogueItems
{
    [DataContract]
    public class OutputItem : DialogueItem
    {
        private const int DEFAULT_MAXIMUM_REPETITION_COUNT = 3;

        private OutputAction outputAction;
        private string target;
     //   private string expression;
        private List<string> queryTagList;
        private Boolean useVerbatimString = false;
        private int maximumRepetitionCount = DEFAULT_MAXIMUM_REPETITION_COUNT;

        public OutputItem(string id, string target, List<string> queryTagList, Boolean useVerbatimString, int maximumRepetitionCount)
        {
            this.id = id;
            this.target = target;
            this.queryTagList = queryTagList;
            this.useVerbatimString = useVerbatimString;
            this.maximumRepetitionCount = maximumRepetitionCount;
        }

        public OutputItem()
        {
            queryTagList = new List<string>();
            target = AgentConstants.SPEECH_OUTPUT_TAG;
        }

        public override void Initialize(Agent ownerAgent)
        {
            base.Initialize(ownerAgent);
            if (!useVerbatimString)
            {
                foreach (Pattern pattern in outputAction.PatternList)
                {
                    pattern.ProcessDefinition();
                    //    pattern.ProcessDefinitionList();
                }
            }
        }

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            base.Run(parameterList, out targetContext, out targetID);
            List<StringMemoryItem> queryMemoryItemList = new List<StringMemoryItem>();
            if (queryTagList != null)
            {
                foreach (string queryTag in queryTagList)
                {
                    StringMemoryItem queryMemoryItem = (StringMemoryItem)ownerAgent.WorkingMemory.GetLastItemByTag(queryTag);
                    if (queryMemoryItem != null)
                    {
                        queryMemoryItemList.Add(queryMemoryItem);
                    }
                }
            }
            //   StringMemoryItem queryMemoryItem = null;
            //   if (queryTagList != null) { queryMemoryItem = (StringMemoryItem)ownerAgent.WorkingMemory.GetLastItemByTagList(queryTagList); }
            //   string outputString = patternAction.GetString(ownerAgent.RandomNumberGenerator, queryMemoryItem);
            string outputString;
            if (repetitionCount <= maximumRepetitionCount)
            {
                if (useVerbatimString)
                {
                    outputString = outputAction.GetVerbatimString();
                }
                else
                {
                    outputString = outputAction.GetString(ownerAgent.RandomNumberGenerator, queryMemoryItemList);
                }

                
                if (outputString != null)
                {
                    if (target == AgentConstants.SPEECH_OUTPUT_TAG) { ownerAgent.SendSpeechOutput(outputString); }
                    else if (target == AgentConstants.FACE_EXPRESSION_OUTPUT_TAG) { ownerAgent.SendExpression(outputString); }
                    else if (target == AgentConstants.INTERNET_OUTPUT_TAG) { ownerAgent.SendInternetRequest(outputString); }
                }
             //   if (outputString != null) { ownerAgent.SendSpeechOutput(outputString); }
              //  if (expression != null) { ownerAgent.SendExpression(expression); }
                targetContext = outputAction.TargetContext;
                targetID = outputAction.TargetID;
            }
            else
            {
                outputString = "";
                targetContext = "";
                targetID = "";
            }
            return true;
        }

        [DataMember]
        public OutputAction OutputAction
        {
            get { return outputAction; }
            set { outputAction = value; }
        }

      /*  [DataMember]
        public string Expression
        {
            get { return expression; }
            set { expression = value; }
        }  */

        [DataMember]
        public string Target
        {
            get { return target; }
            set { target = value; }
        }

        [DataMember]
        public List<string> QueryTagList
        {
            get { return queryTagList; }
            set { queryTagList = value; }
        }

        [DataMember]
        public Boolean UseVerbatimString
        {
            get { return useVerbatimString; }
            set { useVerbatimString = value; }
        }

        [DataMember]
        public int MaximumRepetitionCount
        {
            get { return maximumRepetitionCount; }
            set { maximumRepetitionCount = value; }
        }

    }
}
