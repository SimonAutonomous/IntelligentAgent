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
    public class UltraManagerInsertionItem : DialogueItem
    {
        private List<string> inputQueryTagList;
        private readonly UltraManager _ultraManager = UltraManager.Instance;

        private string targetContext;
        private string targetID;
        private string outputQueryTag;

        public UltraManagerInsertionItem() { }

        public UltraManagerInsertionItem(string id, List<string> inputQueryTagList, string outputQueryTag, string targetContext, string targetID)
        {
            this.id = id;
            this.inputQueryTagList = inputQueryTagList;
            this.targetContext = targetContext;
            this.targetID = targetID;
            this.outputQueryTag = outputQueryTag;
        }

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            base.Run(parameterList, out targetContext, out targetID);

            string movieInformation = "";
            MemoryItem itemSought = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[0]);
            if (itemSought != null)  // 20171201
            {
                movieInformation = (string)itemSought.GetContent();
            }

            List<string> movieInformationList = movieInformation.Split('/').ToList<string>();
            Movie movieToAdd = new Movie(movieInformationList[0], Convert.ToInt32(movieInformationList[1]), Convert.ToDouble(movieInformationList[2]), movieInformationList[3]);
            _ultraManager.MovieList.Add(movieToAdd);

            StringMemoryItem movieTitleMemoryItem = new StringMemoryItem();
            movieTitleMemoryItem.TagList = new List<string>() { outputQueryTag };
            movieTitleMemoryItem.SetContent(movieToAdd.Title);
            ownerAgent.WorkingMemory.AddItem(movieTitleMemoryItem);

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

