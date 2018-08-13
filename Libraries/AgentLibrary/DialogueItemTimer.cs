using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgentLibrary
{
    public class DialogueItemTimer
    {
        private const int DEFAULT_MILLISECOND_SLEEP_TIME = 50;

        private double timeoutInterval; // In s.
        private Boolean running = false;
        private Boolean stopped = true;
        private Thread runThread;
        private int millisecondSleepTime = DEFAULT_MILLISECOND_SLEEP_TIME;
        private Stopwatch stopWatch;

        public event EventHandler TimeoutReached = null;

        private void OnTimeoutReached()
        {
            if (TimeoutReached != null)
            {
                EventHandler handler = TimeoutReached;
                handler(this, EventArgs.Empty);
            }
        }

        private void RunLoop()
        {
            stopWatch = new Stopwatch();
            stopWatch.Start();
            stopped = false;
            while (running)
            {
                double elapsedTime = stopWatch.ElapsedTicks / (double)Stopwatch.Frequency;
                if (elapsedTime >= timeoutInterval)
                {
                    stopWatch.Stop();
                    stopWatch.Reset();
                    if (running)  // running might have been changed (externally) since the last while-check ...
                    {
                        running = false;
                        stopped = true;
                        OnTimeoutReached();
                    }
                }
                else
                {
                    Thread.Sleep(millisecondSleepTime);
                }
            }
            stopWatch.Stop();  // needed here in case the timer was stopped externally.
            stopWatch.Reset();
            stopped = true;
        }

        public void Stop()
        {
            running = false;
        }

        public void Run(double timeoutInterval)
        {
            if (running)
            {
                running = false;
            }
            // First make sure that the previous thread has stopped, in case the timer was already running:
            while (!stopped) { Thread.Sleep(millisecondSleepTime); }
            // Then start the new run:
            this.timeoutInterval = timeoutInterval;
            running = true;
            runThread = new Thread(new ThreadStart(RunLoop));
            runThread.Start();
        }
    }
}
