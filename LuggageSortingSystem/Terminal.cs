using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LuggageSortingSystem
{
    class Terminal
    {
        // Feilds
        private Thread terminal;
        private CancellationTokenSource source;

        // Properies
        public Thread GetThread
        {
            get { return terminal; }
        }
        public CancellationTokenSource Source
        {
            get { return source; }
        }

        // Constructor, The constructor has a parameter CancellationTokenSource
        public Terminal(CancellationTokenSource source)
        {
            this.source = source;

            // creating a new Thread that when we start the thread it calls the LuggageTerminal method.
            terminal = new Thread(new ThreadStart(LuggageTerminal));
            terminal.Name = "Terminal thread";

            terminal.Start(); // Starting the thread.
        }

        public void CloseTerminalThreads()
        {
            Source.Cancel(); // Cancelling the thread.
        }

        public void LuggageTerminal()
        {
            while (true)
            {
                Monitor.Enter(Program.londonLuggage);
                Monitor.Enter(Program.newYourkLuggage);
                Monitor.Enter(Program.berlinLuggage);
                try
                {
                    // TryDequeue tries to remove the object from the beginning of the concurrent queue, it returns true if the object was removed else it return false.
                    Program.londonLuggage.TryDequeue(out Luggage londonLuggage);
                    Program.newYourkLuggage.TryDequeue(out Luggage newYourkLuggage);
                    Program.berlinLuggage.TryDequeue(out Luggage berlinLuggage);
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                finally
                {
                    // Exit the objects.
                    Monitor.Exit(Program.londonLuggage);
                    Monitor.Exit(Program.newYourkLuggage);
                    Monitor.Exit(Program.berlinLuggage);
                }
            }
        }
    }
}
