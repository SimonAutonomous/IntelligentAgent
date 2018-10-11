using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AgentLibrary.DialogueItems;
using AgentLibrary.Memories;

namespace AgentApplication.AddedClasses.DialogueItems
{
    class OpenRatingItem : DialogueItem
    {
        private List<string> inputQueryTagList;
        private readonly UltraManager _ultraManager = UltraManager.Instance;

        private string successTargetContext;
        private string successTargetID;
        private string failureTargetContext;
        private string failureTargetID;

        public string outputQueryTag;

        public OpenRatingItem() { }

        public OpenRatingItem(string id, List<string> inputQueryTagList, string outputQueryTag, string successTargetContext, string successTargetID, string failureTargetContext, string failureTargetID)
        {
            this.id = id;
            this.inputQueryTagList = inputQueryTagList;
            this.successTargetContext = successTargetContext;
            this.successTargetID = successTargetID;
            this.failureTargetContext = failureTargetContext;
            this.failureTargetID = failureTargetID;
            this.outputQueryTag = outputQueryTag;
        }

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            base.Run(parameterList, out targetContext, out targetID);

            string currentUser = "";
            MemoryItem itemSought = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[0]);
            if (itemSought != null)  // 20171201
            {
                currentUser = (string)itemSought.GetContent();
            }

            Boolean openRating = false;
            int listIndexOfCurrentUser = 0;
            string movieToRate = "";
            foreach (var user in _ultraManager.UserList)
            {
                if (user.Name == currentUser)
                {
                    listIndexOfCurrentUser = _ultraManager.UserList.IndexOf(user);
                }
            }

            if (_ultraManager.UserList[listIndexOfCurrentUser].OpenRaiting)
            {
                openRating = true;
                movieToRate = _ultraManager.UserList[listIndexOfCurrentUser].MovieNameOpenRating;
                _ultraManager.UserList[listIndexOfCurrentUser] = new User(_ultraManager.UserList[listIndexOfCurrentUser].Name, false, "");
            }

            StringMemoryItem rateMemoryItem = new StringMemoryItem();
            rateMemoryItem.TagList = new List<string>() { outputQueryTag };
            rateMemoryItem.SetContent(movieToRate);
            ownerAgent.WorkingMemory.AddItem(rateMemoryItem);

            if (openRating)
            {
                targetContext = successTargetContext;
                targetID = successTargetID;
            }
            else
            {
                targetContext = failureTargetContext;
                targetID = failureTargetID;
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
