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
        private Thread terminal;
        private CancellationTokenSource source;

        public Thread GetThread
        {
            get { return terminal; }
        }
        public CancellationTokenSource Source
        {
            get { return source; }
        }

        public Terminal(CancellationTokenSource source)
        {
            this.source = source;

            terminal = new Thread(new ThreadStart(LuggageTerminal));
            terminal.Name = "Terminal thread";

            terminal.Start();
        }
        public void CloseTerminalThreads()
        {
            Source.Cancel();
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
                    Program.londonLuggage.TryDequeue(out Luggage londonLuggage);
                    Program.newYourkLuggage.TryDequeue(out Luggage newYourkLuggage);
                    Program.berlinLuggage.TryDequeue(out Luggage berlinLuggage);
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                finally
                {
                    Monitor.Exit(Program.londonLuggage);
                    Monitor.Exit(Program.newYourkLuggage);
                    Monitor.Exit(Program.berlinLuggage);
                }
            }
        }
    }
}
