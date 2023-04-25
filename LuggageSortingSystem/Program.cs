using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace LuggageSortingSystem
{
    class Program
    {
        // Creating Concurrent queues.
        public static ConcurrentQueue<Luggage> luggageSorting = new ConcurrentQueue<Luggage>();
        public static ConcurrentQueue<Luggage> londonLuggage = new ConcurrentQueue<Luggage>();
        public static ConcurrentQueue<Luggage> newYourkLuggage = new ConcurrentQueue<Luggage>();
        public static ConcurrentQueue<Luggage> berlinLuggage = new ConcurrentQueue<Luggage>();

        private static CheckIn checkIn;
        private static Terminal terminal;
        private static LuggageSorting sortingLuggage;


        static void Main(string[] args)
        {
            CancellationTokenSource source = new CancellationTokenSource();

            // Running a for loop that loops 3 times and and create objects of the classes CheckIn and Terminal.
            for (int i = 0; i < 3; i++)
            {
                checkIn = new CheckIn(source);
                terminal = new Terminal(source);
            }

            // Creating a object of the class Luggage.
            sortingLuggage = new LuggageSorting(source);

            do
            {
                Console.Clear();
                Console.WriteLine("-------------------- Check in numbers: ---------------------");
                Console.WriteLine();
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Total Luggage Checked in: " + checkIn.TotalCount);
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Luggage checked in to London: " + checkIn.LondonCount);
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Luggage checked in to New York: " + checkIn.NewYorkCount);
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Luggage checked in: Berlin: " + checkIn.BerlinCount);
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("--------------------- Sorting numbers: ---------------------");
                Console.WriteLine();
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("Luggage sorted: " + sortingLuggage.SortingCount);
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
            } 
            // The Do/While will keep looping intil the Escape button is pressed.
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape));

            // Calling methods to close threads
            Console.Clear();
            checkIn.CloseCheckInThreads();
            Console.WriteLine(checkIn.GetThread.Name + " Status: " + checkIn.GetThread.ThreadState);
            Console.WriteLine("------------------------------------------------------------");
            sortingLuggage.CloseSortingThreads();
            Console.WriteLine(sortingLuggage.GetThread.Name + " Status: " + sortingLuggage.GetThread.ThreadState);
            Console.WriteLine("------------------------------------------------------------");
            terminal.CloseTerminalThreads();
            Console.WriteLine(sortingLuggage.GetThread.Name + " Status: " + sortingLuggage.GetThread.ThreadState);
            Console.WriteLine("------------------------------------------------------------");
        }
    }
}
