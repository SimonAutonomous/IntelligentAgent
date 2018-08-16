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

//using AgentApplication.AddedClasses;

//namespace AgentApplication.AddedClasses
//{
//    [DataContract]
//    public class UserIntroductionItem : DialogueItem
//    {

//        private string memoryIdentifier;
//        private List<string> inputQueryTagList; // The tag (in working memory) for the item sought.

//        private string successTargetContext;
//        private string successTargetID;
//        private string failureTargetContext;
//        private string failureTargetID;

//        private OutputAction outputAction; // TODO added

//        private readonly UltraManager _ultraManager = UltraManager.Instance;


//        public UserIntroductionItem(string id/*, string memoryIdentifier, List<string> inputQueryTagList, string successTargetContext, string successTargetID,
//                                string failureTargetContext, string failureTargetID*/) // TODO maybe no list since only one query tag
//        {
//            this.id = id;
//            //this.memoryIdentifier = memoryIdentifier;
//            //this.inputQueryTagList = inputQueryTagList;
//            //this.successTargetContext = successTargetContext;
//            //this.successTargetID = successTargetID;
//            //this.failureTargetContext = failureTargetContext;
//            //this.failureTargetID = failureTargetID;
//        }

//        //TODO default --> necessary?
//        //public UserIntroductionItem()
//        //{

//        //}
//        public override void Initialize(Agent ownerAgent)
//        {
//            base.Initialize(ownerAgent);
//            foreach (Pattern pattern in outputAction.PatternList)
//            {
//                pattern.ProcessDefinition();
//                //     pattern.ProcessDefinitionList();
//            }
//        }

//        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
//        {
//            base.Run(parameterList, out targetContext, out targetID); // TODO: necessary?
//            repetitionCount++;

//            targetContext = outputAction.TargetContext;
//            targetID = outputAction.TargetID;
//            return true;

//            //Boolean existingUser = false;
//            //foreach (var user in _ultraManager.UserList)
//            //{
//            //    foreach (string queryTag in inputQueryTagList)
//            //    {
//            //        if (user.Name == queryTag)
//            //        {
//            //            existingUser = true;
//            //        }
//            //    }
//            //}

//            //if (existingUser)
//            //{
//            //    //TODO: trigger rating dialogue 
//            //}
//            //else
//            //{
//            //    var user = new User(inputQueryTagList[0], false);
//            //    _ultraManager.UserList.Add(user);

//            //}

//            ////TODO: set current User in working memory


//            //if (existingUser) // success if existing user
//            //{
//            //    targetContext = successTargetContext;
//            //    targetID = successTargetID;
//            //}
//            //else // no success if new user
//            //{
//            //    targetContext = failureTargetContext;
//            //    targetID = failureTargetID;
//            //}
//            //return existingUser;
//        }

//        [DataMember]
//        public OutputAction OutputAction
//        {
//            get { return outputAction; }
//            set { outputAction = value; }
//        }

//        [DataMember]
//        public string MemoryIdentifier
//        {
//            get { return memoryIdentifier; }
//            set { memoryIdentifier = value; }
//        }

//        [DataMember]
//        public List<string> InputQueryTagList
//        {
//            get { return inputQueryTagList; }
//            set { inputQueryTagList = value; }
//        }

//        [DataMember]
//        public string SuccessTargetContext
//        {
//            get { return successTargetContext; }
//            set { successTargetContext = value; }
//        }

//        [DataMember]
//        public string SuccessTargetID
//        {
//            get { return successTargetID; }
//            set { successTargetID = value; }
//        }

//        [DataMember]
//        public string FailureTargetContext
//        {
//            get { return failureTargetContext; }
//            set { failureTargetContext = value; }
//        }

//        [DataMember]
//        public string FailureTargetID
//        {
//            get { return failureTargetID; }
//            set { failureTargetID = value; }
//        }

//    }
//}




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
            foreach (Pattern pattern in outputAction.PatternList)
            {
                pattern.ProcessDefinition();
                //     pattern.ProcessDefinitionList();
            }
        }*/

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            base.Run(parameterList, out targetContext, out targetID);

            //string timePrefixString = outputAction.GetString(ownerAgent.RandomNumberGenerator, null);

            string queryTag = inputQueryTagList[0]; //TODO maybe not needed
            //string greetingString = "";
            string currentUser = "";

            MemoryItem itemSought = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[0]);
            if (itemSought != null)  // 20171201
            {
                currentUser = (string)itemSought.GetContent();
            }

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
                //TODO: trigger rating dialogue 
                targetContext = existingUserTargetContext;
                targetID = existingUserTargetID;
                //greetingString = greetingString + "Welcome back " + currentUser;
            }
            else
            {
                var user = new User(currentUser, false);
                _ultraManager.UserList.Add(user);
                targetContext = newUserTargetContext;
                targetID = newUserTargetID;
                //greetingString = greetingString + "Hello " + currentUser + "    it seems you are new";

            }


            //foreach (string inputQueryTag in inputQueryTagList)
            //{
            //    greetingString = greetingString + " " + inputQueryTag;
            //}

            //string timePrefixString = outputAction.GetString(ownerAgent.RandomNumberGenerator, null);
            //string timeString = DateTime.Now.Hour.ToString() + " " + DateTime.Now.Minute.ToString();
            //if (timePrefixString != null)
            //{
            //    timeString = timePrefixString + " " + timeString;
            //}
            //ownerAgent.SendSpeechOutput(greetingString);
            //targetContext = outputAction.TargetContext;
            //targetID = outputAction.TargetID;
            return true;
        }
        /*
        [DataMember]
        public OutputAction OutputAction
        {
            get { return outputAction; }
            set { outputAction = value; }
        }*/

        [DataMember]
        public List<string> InputQueryTagList
        {
            get { return inputQueryTagList; }
            set { inputQueryTagList = value; }
        }
    }
}