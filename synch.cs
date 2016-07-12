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

        public static void AccessCodewithSemaphore(object objSemaphore)
        {
            bool IsComplete = false;
            Semaphore l_SemaPhore = (Semaphore)objSemaphore;
            while (!IsComplete)
            {
                if (l_SemaPhore.WaitOne(200, false))
                {
                    try
                    {
                        Console.BeginInvoke(new ParameterizedThreadStart(UpdateUI), new object[] { "Thread ID : " + Thread.CurrentThread.ManagedThreadId.ToString() + " : Entered" });
                        Thread.Sleep(500);
                    }
                    finally
                    {
                        l_SemaPhore.Release();
                        Console.BeginInvoke(new ParameterizedThreadStart(UpdateUI), new object[] { "Thread ID : " + Thread.CurrentThread.ManagedThreadId.ToString() + " : Exit" });
                        IsComplete = true;
                    }
                }
                else
                {
                    Console.BeginInvoke(new ParameterizedThreadStart(UpdateUI), new object[] { "Thread ID : " + Thread.CurrentThread.ManagedThreadId.ToString() + " : Waiting To enter" });
                }
            }
        }

        /*============ Main Thread Start Here. ======= */
        static void Main(string []args){

			Console.Write("3 sharks and 4 fish eating using 2 seats in the food-point\n");


            int TotalThread = 5;
            int SemaphoreCount = 3;
            Thread[] Threads = new Thread[TotalThread];
            Semaphore Sema = new Semaphore(SemaphoreCount, SemaphoreCount);

            for (int i = 0; i < TotalThread; i++)
            {
                Threads[i] = new Thread(new ParameterizedThreadStart(AccessCodewithSemaphore));
                Threads[i].IsBackground = true;
                Threads[i].Start(Sema);
            }

		}
	}
}
