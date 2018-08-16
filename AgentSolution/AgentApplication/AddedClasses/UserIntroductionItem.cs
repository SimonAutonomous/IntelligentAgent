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

namespace AgentApplication.AddedClasses
{
    public class UserIntroductionItem : DialogueItem
    {

        private string memoryIdentifier;
        private List<string> inputQueryTagList; // The tag (in working memory) for the item sought.

        private string successTargetContext;
        private string successTargetID;
        private string failureTargetContext;
        private string failureTargetID;

        private readonly UltraManager _ultraManager = UltraManager.Instance;


        public UserIntroductionItem(string id, string memoryIdentifier, List<string> inputQueryTagList, string successTargetContext, string successTargetID,
                                string failureTargetContext, string failureTargetID) // TODO maybe no list since only one query tag
        {
            this.id = id;
            this.memoryIdentifier = memoryIdentifier;
            this.inputQueryTagList = inputQueryTagList;
            this.successTargetContext = successTargetContext;
            this.successTargetID = successTargetID;
            this.failureTargetContext = failureTargetContext;
            this.failureTargetID = failureTargetID;
        }

        //TODO default --> necessary?
        public UserIntroductionItem()
        {

        }

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            repetitionCount++;

            Boolean existingUser = false;
            foreach (var user in _ultraManager.UserList)
            {
                foreach (string queryTag in inputQueryTagList)
                {
                    if (user.Name == queryTag)
                    {
                        existingUser = true;
                    }
                }
            }

            if (existingUser)
            {
                //TODO: trigger rating dialogue 
            }
            else
            {
                var user = new User(inputQueryTagList[0], false);
                _ultraManager.UserList.Add(user);
 
            }

            //TODO: set current User in working memory


            if (existingUser) // success if existing user
            {
                targetContext = successTargetContext;
                targetID = successTargetID;
            }
            else // no success if new user
            {
                targetContext = failureTargetContext;
                targetID = failureTargetID;
            }
            return existingUser;
        }

        [DataMember]
        public string MemoryIdentifier
        {
            get { return memoryIdentifier; }
            set { memoryIdentifier = value; }
        }

        [DataMember]
        public List<string> InputQueryTagList
        {
            get { return inputQueryTagList; }
            set { inputQueryTagList = value; }
        }

        [DataMember]
        public string SuccessTargetContext
        {
            get { return successTargetContext; }
            set { successTargetContext = value; }
        }

        [DataMember]
        public string SuccessTargetID
        {
            get { return successTargetID; }
            set { successTargetID = value; }
        }

        [DataMember]
        public string FailureTargetContext
        {
            get { return failureTargetContext; }
            set { failureTargetContext = value; }
        }

        [DataMember]
        public string FailureTargetID
        {
            get { return failureTargetID; }
            set { failureTargetID = value; }
        }

    }
}