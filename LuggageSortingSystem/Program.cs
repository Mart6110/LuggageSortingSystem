using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace LuggageSortingSystem
{
    class Program
    {
        public static ConcurrentQueue<Luggage> luggageSorting = new ConcurrentQueue<Luggage>();
        public static ConcurrentQueue<Luggage> londonLuggage = new ConcurrentQueue<Luggage>();
        public static ConcurrentQueue<Luggage> newYourkLuggage = new ConcurrentQueue<Luggage>();
        public static ConcurrentQueue<Luggage> berlinLuggage = new ConcurrentQueue<Luggage>();

        static void Main(string[] args)
        {
            for (int i = 0; i < 3; i++)
            {
                CheckIn checkIn = new CheckIn();
                Terminal terminal = new Terminal();
            }

            LuggageSorting luggageSorting = new LuggageSorting();

            do
            {
                Console.Clear();
                Console.WriteLine("-------------------- Check in numbers: ---------------------");
                Console.WriteLine();
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Total Luggage Checked in: " + CheckIn.TotalCount);
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Luggage checked in to London: " + CheckIn.LondonCount);
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Luggage checked in to New York: " + CheckIn.NewYorkCount);
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Luggage checked in: Berlin: " + CheckIn.BerlinCount);
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("--------------------- Sorting numbers: ---------------------");
                Console.WriteLine();
                Console.WriteLine("Luggage sorted: " + LuggageSorting.SortingCount);
                Console.WriteLine("------------------------------------------------------------");
                if (!LuggageSorting.SortingBool)
                {
                    Console.WriteLine("Waiting for luggage.");
                }
                else
                {
                    Console.WriteLine("Sorting luggage.");
                }
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("London Luggage: " + londonLuggage.Count);
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("New York Luggage: " + newYourkLuggage.Count);
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Berlin Luggage: " + berlinLuggage.Count);
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Press ECS to close threads");
                Console.WriteLine("------------------------------------------------------------");

                Thread.Sleep(TimeSpan.FromSeconds(1));
            } while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape));

            CheckIn
            londonTerminal.Join();
            newYorkTerminal.Join();
            berlinTerminal.Join();
            Console.WriteLine("Threads has been closed");
            Console.WriteLine("------------------------------------------------------------");
        }
    }
}
