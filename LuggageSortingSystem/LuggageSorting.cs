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
        // Feilds
        private int sortingCount;
        private Thread sortingLuggage;
        private CancellationTokenSource source;

        // Properies
        public int SortingCount
        {
            get { return sortingCount; }
        }
        public Thread GetThread
        {
            get { return sortingLuggage; }
        }
        public CancellationTokenSource Source
        {
            get { return source; }
        }

        // Constructor, The constructor has a parameter CancellationTokenSource
        public LuggageSorting(CancellationTokenSource source)
        {
            this.source = source;

            // creating a new Thread that when we start the thread it calls the Sorting method.
            sortingLuggage = new Thread(new ThreadStart(Sorting));
            sortingLuggage.Name = "Sorting thread";

            sortingLuggage.Start();  // Starting the thread.
        }
        public void CloseSortingThreads()
        {
            Source.Cancel(); // Cancelling the thread.
        }
        public void Sorting()
        {
            while (true)
            {
                Monitor.Enter(Airport.luggageSorting);
                Monitor.Enter(Airport.londonLuggage);
                Monitor.Enter(Airport.newYourkLuggage);
                Monitor.Enter(Airport.berlinLuggage);

                // A try that has a while and a foreach inside it.
                try
                {
                    // While luggageSorting count is eqaul 0, we wait for luggageSorting to be free.
                    while (Airport.luggageSorting.Count == 0)
                    {
                        Monitor.Wait(Airport.luggageSorting);
                    }

                    // A foreach that loops through the luggageSorting queue.
                    foreach(Luggage luggage in Airport.luggageSorting)
                    {
                        string destination = luggage.Destination;

                        switch (destination) // A switch that sort the luggage to the different destination queue
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
                    Airport.luggageSorting.Clear(); // Clearing the queue.

                    // Pulse to all threads, giving the threads the info that the object is not locked.
                    Monitor.PulseAll(Airport.luggageSorting);
                    Monitor.PulseAll(Airport.londonLuggage);
                    Monitor.PulseAll(Airport.newYourkLuggage);
                    Monitor.PulseAll(Airport.berlinLuggage);
                }
                finally
                {
                    // Exit the objects.
                    Monitor.Exit(Airport.luggageSorting);
                    Monitor.Exit(Airport.londonLuggage);
                    Monitor.Exit(Airport.newYourkLuggage);
                    Monitor.Exit(Airport.berlinLuggage);
                }
            }
        }
    }
}
