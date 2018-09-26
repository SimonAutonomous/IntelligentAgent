using AgentLibrary;
using AgentLibrary.DialogueItems;
using AgentLibrary.Memories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AgentApplication.AddedClasses
{
    [DataContract]
    class MovieInputItem : DialogueItem
    {
        private List<string> inputQueryTagList;
        private string outputQueryTag;
        private string targetContext;
        private string targetID;

        public MovieInputItem() { }

        public MovieInputItem(string id, List<string> inputQueryTagList, string outputQueryTag, string targetContext, string targetID)
        {
            this.id = id;
            this.inputQueryTagList = inputQueryTagList;
            this.outputQueryTag = outputQueryTag;
            this.targetContext = targetContext;
            this.targetID = targetID;
        }

        public override void Initialize(Agent ownerAgent)
        {
            base.Initialize(ownerAgent);
        }

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            base.Run(parameterList, out targetContext, out targetID);

            string movieTitle = "";

            MemoryItem itemSought1 = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[0]);
            DateTime timeToCheckFor = itemSought1.InsertionTime; // Movie title consists of at least one word --> if the insertion time of the other query items is the same concat name
            if (itemSought1 != null)
            {
                movieTitle = (string)itemSought1.GetContent();
            }
            MemoryItem itemSought2 = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[1]);
            if (itemSought2 != null)
            {
                DateTime insertionTime2 = itemSought2.InsertionTime;
                if (insertionTime2 == timeToCheckFor)
                {
                    movieTitle = movieTitle + " " + (string)itemSought2.GetContent();
                }
            }
            MemoryItem itemSought3 = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[2]);
            if (itemSought3 != null)
            {
                DateTime insertionTime3 = itemSought3.InsertionTime;
                if (insertionTime3 == timeToCheckFor)
                {
                    movieTitle = movieTitle + " " + (string)itemSought3.GetContent();
                }
            }
            StringMemoryItem imdbSearchMemoryItem = new StringMemoryItem();
            imdbSearchMemoryItem.TagList = new List<string>() { outputQueryTag };
            imdbSearchMemoryItem.SetContent(movieTitle);
            ownerAgent.WorkingMemory.AddItem(imdbSearchMemoryItem);

            targetContext = this.targetContext;
            targetID = this.targetID;

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
