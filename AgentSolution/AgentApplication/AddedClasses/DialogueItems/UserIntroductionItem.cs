using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AgentLibrary.Memories;
using AgentLibrary.DialogueItems;
using AgentLibrary;

namespace AgentApplication.AddedClasses //TODO: right namespace?
{
    [DataContract]
    public class UserIntroductionItem : DialogueItem
    {
        //private OutputAction outputAction = new OutputAction();
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

       /* public override void Initialize(Agent ownerAgent) // TODO use patter list
        {
            base.Initialize(ownerAgent);
            //foreach (Pattern pattern in outputAction.PatternList)
            //{
            //    pattern.ProcessDefinition();
            //    //     pattern.ProcessDefinitionList();
            //}
        }*/

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            base.Run(parameterList, out targetContext, out targetID);

            string queryTag = inputQueryTagList[0]; //TODO maybe not needed or change GetLastStringByTag
            string currentUser = "";
            MemoryItem itemSought = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[0]);
            if (itemSought != null)  // 20171201
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