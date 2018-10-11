using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AgentLibrary.DialogueItems;
using AgentLibrary.Memories;

namespace AgentApplication.AddedClasses.DialogueItems 
{
    [DataContract]
    public class UserIntroductionItem : DialogueItem
    {
        private List<string> inputQueryTagList;
        private readonly UltraManager _ultraManager = UltraManager.Instance;

        private string newUserTargetContext;
        private string newUserTargetID;
        private string existingUserTargetContext;
        private string existingUserTargetID;

        public UserIntroductionItem() { }

        public UserIntroductionItem(string id, List<string> inputQueryTagList, string newUserTargetContext, string newUserTargetID, string existingUserTargetContext, string existingUserTargetID)
        {
            this.id = id;
            this.inputQueryTagList = inputQueryTagList;
            this.newUserTargetContext = newUserTargetContext;
            this.newUserTargetID = newUserTargetID;
            this.existingUserTargetContext = existingUserTargetContext;
            this.existingUserTargetID = existingUserTargetID;
        }

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            base.Run(parameterList, out targetContext, out targetID);

            string queryTag = inputQueryTagList[0];
            string currentUser = "";
            MemoryItem itemSought = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[0]);
            if (itemSought != null)
            {
                currentUser = (string)itemSought.GetContent();
            }

            // Check if user already exists
            Boolean existingUser = false;
            foreach (var user in _ultraManager.UserList)
            {
                    if (user.Name == currentUser)
                    {
                        existingUser = true;
                    }
            }

            if (existingUser)
            {
                targetContext = existingUserTargetContext;
                targetID = existingUserTargetID;
            }
            else
            {
                var user = new User(currentUser, false, "");
                _ultraManager.UserList.Add(user);
                targetContext = newUserTargetContext;
                targetID = newUserTargetID;
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