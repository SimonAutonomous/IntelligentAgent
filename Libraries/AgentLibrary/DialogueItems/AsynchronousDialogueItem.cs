using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgentLibrary.DialogueItems
{
    public abstract class AsynchronousDialogueItem: DialogueItem
    {
        protected Thread runThread = null;

        public event EventHandler<AsynchronousDialogueItemEventArgs> RunCompleted = null;

        protected abstract void RunLoop(List<object> parameterList);

        public override void Initialize(Agent ownerAgent)
        {
            base.Initialize(ownerAgent);
            this.RunCompleted += new EventHandler<AsynchronousDialogueItemEventArgs>(ownerAgent.HandleAsynchronousDialogueItemCompleted);  // A bit ugly, but OK...
        }

        public void RunAsynchronously(List<object> parameterList)
        {
            runThread = new Thread(new ThreadStart(() => RunLoop(parameterList)));
            runThread.Start();
        }

        public void OnRunCompleted(AsynchronousDialogueItemEventArgs e)
        {
            if (RunCompleted != null)
            {
                EventHandler<AsynchronousDialogueItemEventArgs> handler = RunCompleted;
                handler(this, e);
            }
        }
    }
}
