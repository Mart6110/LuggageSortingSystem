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
        public Terminal()
        {
            Thread londonTerminal = new Thread(new ThreadStart(LuggageTerminal));

            londonTerminal.Start();
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
