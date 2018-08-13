using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace EventHandlerExample
{
    public class EventTestClass
    {
        private const int UPPER_LIMIT = 100000;
        private const int PROGRESS_REPORT_INTERVAL = 2500;

        private Thread runThread;
        
        public event EventHandler Started = null;
        public event EventHandler<ProgressEventArgs> Progress = null;

        private void RunLoop()
        {
            OnStarted();
            for (int ii = 1; ii <= UPPER_LIMIT; ii++)
            {
                double sum = 0;
                for (int jj = 1; jj <= ii; jj++) { sum += jj; }
                if (ii % PROGRESS_REPORT_INTERVAL == 0) { OnProgress(ii); }
            }
        }

        public void Run()
        {
            runThread = new Thread(new ThreadStart(() => RunLoop()));
            runThread.Start();
        }

        private void OnStarted()
        {
            if (Started != null)
            {
                EventHandler handler = Started;
                handler(this, EventArgs.Empty);
            }
        }

        private void OnProgress(int sumsCompleted)
        {
            if (Progress != null)
            {
                EventHandler<ProgressEventArgs> handler = Progress;
                ProgressEventArgs e = new ProgressEventArgs(sumsCompleted);
                handler(this, e);
            }
        }
    }
}
