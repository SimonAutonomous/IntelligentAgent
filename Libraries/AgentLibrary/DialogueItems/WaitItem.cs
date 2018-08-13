using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgentLibrary.DialogueItems
{
    public class WaitItem: AsynchronousDialogueItem
    {
        private OutputAction outputAction;
        private double waitingTime;
        private int millisecondWaitingTime;

        public WaitItem()
        {

        }

        public WaitItem(string id, double waitingTime)
        {
            this.id = id;
            this.waitingTime = waitingTime;
        }

        public override void Initialize(Agent ownerAgent)
        {
            base.Initialize(ownerAgent);
            millisecondWaitingTime = (int)Math.Round(1000 * waitingTime);
        }

        protected override void RunLoop(List<object> parameterList)
        {
            string originalContext = ownerAgent.WorkingMemory.CurrentContext;
            Thread.Sleep(millisecondWaitingTime);
            AsynchronousDialogueItemEventArgs e = new AsynchronousDialogueItemEventArgs(originalContext, outputAction.TargetContext, outputAction.TargetID);
            OnRunCompleted(e);
        }

        [DataMember]
        public OutputAction OutputAction
        {
            get { return outputAction; }
            set { outputAction = value; }
        }

        [DataMember]
        public double WaitingTime
        {
            get { return waitingTime; }
            set { waitingTime = value; }
        }
    }
}
