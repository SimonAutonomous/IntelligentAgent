using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AgentLibrary;
using AgentLibrary.DialogueItems;
using AgentLibrary.Memories;

namespace AgentApplication.AddedClasses.DialogueItems
{
    class UserCheckItem : DialogueItem
    {
        private List<string> inputQueryTagList;
        private string successTargetContext;
        private string successTargetID;
        private string failureTargetContext;
        private string failureTargetID;

        public UserCheckItem() { }

        public UserCheckItem(string id, List<string> inputQueryTagList, string successTargetContext, string successTargetID, string failureTargetContext, string failureTargetID)
        {
            this.id = id;
            this.inputQueryTagList = inputQueryTagList;
            this.successTargetContext = successTargetContext;
            this.successTargetID = successTargetID;
            this.failureTargetContext = failureTargetContext;
            this.failureTargetID = failureTargetID;
        }

        public override void Initialize(Agent ownerAgent)
        {
            base.Initialize(ownerAgent);
        }

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            base.Run(parameterList, out targetContext, out targetID);

            string userName = "";
            string noCurrentUserFound = "Please make sure to first introduce yourself";

            MemoryItem itemSought = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[0]);
            if (itemSought != null)
            {
                userName = (string)itemSought.GetContent();
            }

            if (userName == "")
            {
                ownerAgent.SendSpeechOutput(noCurrentUserFound);
                targetContext = failureTargetContext;
                targetID = failureTargetID;
            }
            else
            {
                targetContext = successTargetContext;
                targetID = successTargetID;
            }
            return true;
        }

        [DataMember]
        public List<string> InputQueryTagList
        {
            get { return inputQueryTagList; }
            set { inputQueryTagList = value; }
        }
    }
}
