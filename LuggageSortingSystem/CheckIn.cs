using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LuggageSortingSystem
{
    class CheckIn
    {
        public Random rnd = new Random();

        private static int totalCount;
        private static int londonCount;
        private static int newYourkCount;
        private static int berlinCount;

        public static int TotalCount
        {
            get { return totalCount; }
        }
        public static int LondonCount
        {
            get { return londonCount; }
        }
        public static int NewYorkCount
        {
            get { return newYourkCount; }
        }
        public static int BerlinCount
        {
            get { return berlinCount; }
        }

        public CheckIn()
        {
            Thread checkIns = new Thread(new ThreadStart(LuggageCheckIn));

            checkIns.Start();
        }

        public void LuggageCheckIn()
        {
            string destination = "";
            while (true)
            {
                Monitor.Enter(Program.luggageSorting);
                try
                {
                    if(Program.luggageSorting.Count == 0)
                    {
                        for(int i = 0; i < 10; i++)
                        {
                            int destinationNumber = rnd.Next(1, 4);

                            switch (destinationNumber)
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

                            totalCount++;
                            Luggage luggage = new Luggage(destination);
                            Program.luggageSorting.Enqueue(luggage);

                            Thread.Sleep(TimeSpan.FromSeconds(1));
                        }
                    }
                    Monitor.PulseAll(Program.luggageSorting);
                }
                finally
                {
                    Monitor.Exit(Program.luggageSorting);
                }
            }
        }
    }
}
