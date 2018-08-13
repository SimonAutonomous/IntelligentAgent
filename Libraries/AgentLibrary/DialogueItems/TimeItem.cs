using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AgentLibrary.Memories;

namespace AgentLibrary.DialogueItems
{
    [DataContract]
    public class TimeItem : DialogueItem
    {
        private OutputAction outputAction;

        public TimeItem() { }

        public TimeItem(string id)
        {
            this.id = id;
        }

        public override void Initialize(Agent ownerAgent)
        {
            base.Initialize(ownerAgent);
            foreach (Pattern pattern in outputAction.PatternList)
            {
                pattern.ProcessDefinition();
           //     pattern.ProcessDefinitionList();
            }
        }

        public override Boolean Run(List<object> parameterList, out string targetContext, out string targetID)
        {
            base.Run(parameterList, out targetContext, out targetID);
            string timePrefixString = outputAction.GetString(ownerAgent.RandomNumberGenerator, null);
            string timeString = DateTime.Now.Hour.ToString() + " " + DateTime.Now.Minute.ToString();
            if (timePrefixString != null)
            {
                timeString = timePrefixString + " " + timeString;
            }
            ownerAgent.SendSpeechOutput(timeString);
            targetContext = outputAction.TargetContext;
            targetID = outputAction.TargetID;
            return true;
        }

        [DataMember]
        public OutputAction OutputAction
        {
            get { return outputAction; }
            set { outputAction = value; }
        }
    }
}
