using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Collections.Specialized;

// for benchmarking
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ProcessSynchronization_Space {

  // define shark properties
	class Shark{
		public string fish_Name{ get; set; }
		public string fish_Id{ get; set; }
	}

	// define fish properties
	class Fish{
		public string shark_Name{ get; set; }
		public string shark_Id{ get; set; }
	}

	class SharkFish_Class{

		/*============ fishb4eating: called each time a fish eats, before it eats. ======= */
		public static void fishb4eating(){

		}

		/*============ sharkafteating: called each time a shark eats, after it eats. ======= */
		public static void sharkafteating(){

		}

		/*============ fishafteating: called each time a fish eats, after it eats. ======= */
		public static int fishafteating(){
			return 0;
		}

		/*============ sharkfishsyncinit: called only once, before the shark and fish are created. ======= */
		public static void sharkfishsyncinit(){

        }

		/*============ sharfishsynccleanup: called only once, after all sharks and fish have finished. ======= */
        public static void sharfishsynccleanup(){

        	
        }

        /*============ syncproblem . ======= */
        public static void syncproblem (){

        }


       static Semaphore obj = new Semaphore(2, 4);



        static void Shark_SempStart(object id)
        {
            Console.WriteLine("Shark " + id + " Wants to Get Enter");
            try
            {
                obj.WaitOne();
                Console.WriteLine(" Success: Shark " + id + " is in and Weeding");

                Thread.Sleep(2000);
                Console.WriteLine("Shark "+ id +" is Leaving");
            }
            finally
            {
                obj.Release();

            }
        }

        static void Fish_SempStart(object id)
        {
            Console.WriteLine("Fish " + id + " Wants to Get Enter");
            try
            {
                obj.WaitOne();
                Console.WriteLine(" Success: Fish " + id + " is in and Weeding");

                Thread.Sleep(2000);
                Console.WriteLine("Fish "+ id +" is Leaving");
            }
            finally
            {
                obj.Release();

            }
        }

        /*============ Main Thread Start Here. ======= */
        static void Main(string[] args)
        {
        	Console.Write("3 sharks and 4 fish eating using 2 seats in the food-point\n");


        	int milliSec = 3500;
			String Result = "\0"; // null
			Console.Write("Number of sharks in Sea ?\n");

			int sharks_in_Sea;
			Result = Console.ReadLine();
			while(!Int32.TryParse(Result, out sharks_in_Sea))
			{
				Console.Write("Not a valid number, try again.\n");
				Result = Console.ReadLine();
			}

			Console.Write("Number of Fishes in Sea.\n");
			int fishes_in_Sea;
			Result = Console.ReadLine();
			while(!Int32.TryParse(Result, out fishes_in_Sea))
			{
				Console.Write("Not a valid number, try again.\n");
				Result = Console.ReadLine();
			}

            for (int i = 1; i <= sharks_in_Sea; i++)
            {
                new Thread(Shark_SempStart).Start(i);
            }

            for (int i = 1; i <= fishes_in_Sea; i++)
            {
                new Thread(Fish_SempStart).Start(i);
            }

            Console.ReadKey();
        }
	}
}
