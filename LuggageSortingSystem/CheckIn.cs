using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LuggageSortingSystem
{
    class CheckIn
    {
        public Random rnd = new Random();

        // Feilds
        private static int totalCount;
        private static int londonCount;
        private static int newYourkCount;
        private static int berlinCount;
        private Thread checkIns;
        private CancellationTokenSource source;

        // Properies
        public int TotalCount
        {
            get { return totalCount; }
        }
        public int LondonCount
        {
            get { return londonCount; }
        }
        public int NewYorkCount
        {
            get { return newYourkCount; }
        }
        public int BerlinCount
        {
            get { return berlinCount; }
        }
        public Thread GetThread
        {
            get { return checkIns; }
        }
        public CancellationTokenSource Source
        {
            get { return source; }
        }

        // Constructor, The constructor has a parameter CancellationTokenSource
        public CheckIn(CancellationTokenSource source)
        {
            this.source = source;

            // creating a new Thread that when we start the thread it calls the LuggageCheckIn method.
            checkIns = new Thread(new ThreadStart(LuggageCheckIn));
            checkIns.Name = "Check in thread"; // Giving the thread a name.

            checkIns.Start(); // Starting the thread.
        }
        public void CloseCheckInThreads()
        {
            Source.Cancel(); // Cancelling the thread.
        }
        public void LuggageCheckIn()
        {
            string destination = "";
            while (true)
            {
                Monitor.Enter(Airport.luggageSorting);

                // A try that has an if statement inside it.
                try
                {
                    // If the queue is eqaul to 0. We run a for loop that loops 10 times.
                    if(Airport.luggageSorting.Count == 0)
                    {
                        for(int i = 0; i < 10; i++)
                        {
                            int destinationNumber = rnd.Next(1, 4); // Generating a random number between 1 and 3.

                            switch (destinationNumber) // A Switch that sets the destination.
                            {
                                case 1:
                                    londonCount++;
                                    destination = "London";
                                    break;
                                case 2:
                                    newYourkCount++;
                                    destination = "New York";
                                    break;
                                case 3:
                                    berlinCount++;
                                    destination = "Berlin";
                                    break;
                                default:
                                    break;
                            }

                            totalCount++; // Count up the total check ins
                            Luggage luggage = new Luggage(destination); // creating a new object of the Luggage class
                            Airport.luggageSorting.Enqueue(luggage);

                            Thread.Sleep(TimeSpan.FromSeconds(1));
                        }
                    }
                    Monitor.PulseAll(Airport.luggageSorting); // Pulse to all threads, giving the threads the info that the object is not locked.
                }
                finally
                {
                    Monitor.Exit(Airport.luggageSorting); // Exit the object.
                }
            }
        }
    }
}
