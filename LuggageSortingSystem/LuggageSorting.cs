using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LuggageSortingSystem
{
    class LuggageSorting
    {
        private static int sortingCount;
        private static bool sortingBool;

        public static int SortingCount
        {
            get { return sortingCount; }
        }
        public static bool SortingBool
        {
            get { return sortingBool; }
        }

        public LuggageSorting()
        {
            Thread sortingLuggage = new Thread(new ThreadStart(Sorting));

            sortingLuggage.Start();
        }
        public void Sorting()
        {
            while (true)
            {
                Monitor.Enter(Program.luggageSorting);
                Monitor.Enter(Program.londonLuggage);
                Monitor.Enter(Program.newYourkLuggage);
                Monitor.Enter(Program.berlinLuggage);
                try
                {
                    while(Program.luggageSorting.Count == 0)
                    {
                        Monitor.Wait(Program.luggageSorting);
                        sortingBool = false;
                    }

                    foreach(Luggage luggage in Program.luggageSorting)
                    {
                        string destination = luggage.Destination;
                        sortingBool = true;

                        switch (destination)
                        {
                            case "London":
                                Program.londonLuggage.Enqueue(luggage);
                                break;
                            case "New York":
                                Program.newYourkLuggage.Enqueue(luggage);
                                break;
                            case "Berlin":
                                Program.berlinLuggage.Enqueue(luggage);
                                break;
                            default:
                                break;
                        }
                        sortingCount++;
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                    }
                    Program.luggageSorting.Clear();
                    Monitor.PulseAll(Program.luggageSorting);
                    Monitor.PulseAll(Program.londonLuggage);
                    Monitor.PulseAll(Program.newYourkLuggage);
                    Monitor.PulseAll(Program.berlinLuggage);
                }
                finally
                {
                    Monitor.Exit(Program.luggageSorting);
                    Monitor.Exit(Program.londonLuggage);
                    Monitor.Exit(Program.newYourkLuggage);
                    Monitor.Exit(Program.berlinLuggage);
                }
            }
        }
    }
}
