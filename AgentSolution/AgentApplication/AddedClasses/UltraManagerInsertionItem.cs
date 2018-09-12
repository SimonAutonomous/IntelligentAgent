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
        //private OutputAction outputAction = new OutputAction();
        private List<string> inputQueryTagList;
        private readonly UltraManager _ultraManager = UltraManager.Instance;

        private string targetContext;
        private string targetID;

        //private string successTargetContext;
        //private string successTargetID;
        //private string failureTargetContext;
        //private string failureTargetID;

        public UltraManagerInsertionItem() { }

        public UltraManagerInsertionItem(string id, List<string> inputQueryTagList, string targetContext, string targetID/*string successTargetContext, string successTargetID, string failureTargetContext, string failureTargetID*/)
        {
            this.id = id;
            this.inputQueryTagList = inputQueryTagList;
            this.targetContext = targetContext;
            this.targetID = targetID;
            //this.successTargetContext = successTargetContext;
            //this.successTargetID = successTargetID;
            //this.failureTargetContext = failureTargetContext;
            //this.failureTargetID = failureTargetID;
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

           // string queryTag = inputQueryTagList[0]; //TODO maybe not needed or change GetLastStringByTag
            string movieInformation = "";
            MemoryItem itemSought = ownerAgent.WorkingMemory.GetLastItemByTag(inputQueryTagList[0]);
            if (itemSought != null)  // 20171201
            {
                movieInformation = (string)itemSought.GetContent();
                //System.IO.File.WriteAllText(@"C:\Users\Simon\Desktop\WriteText.txt", movieInformation);
            }

            List<string> movieInformationList = movieInformation.Split(',').ToList<string>();
            Movie movieToAdd = new Movie(movieInformationList[0], Convert.ToInt32(movieInformationList[1]), Convert.ToDouble(movieInformationList[2]), movieInformationList[3]);
            _ultraManager.MovieList.Add(movieToAdd);

            targetContext = this.targetContext;
            targetID = this.targetID;

            //Boolean existingUser = false;
            //foreach (var user in _ultraManager.UserList)
            //{
            //    if (user.Name == currentUser)
            //    {
            //        existingUser = true;
            //    }
            //}

            //if (existingUser)
            //{
            //    targetContext = successTargetContext;
            //    targetID = successTargetID;
            //}
            //else
            //{
            //    targetContext = failureTargetContext;
            //    targetID = failureTargetID;

            //}
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

