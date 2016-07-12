using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Collections.Specialized;

namespace ProcessSynchronization_Space {

  // define shark properties
	class Shark{
    // set the getter and setter for each Block method
		public string fish_Name{
			get;
			set;
		}

	}

	// define fish properties
	class Fish{
    // set the getter and setter for each Block method
		public string shark_Name{
			get;
			set;
		}

	}

	class SharkFish_Class{

		/*============ fishb4eating: called each time a fish eats, before it eats. ======= */
		public static void fishb4eating(Block[] remaining_Blocks, int chunk_Size, string process_Name){

		}

		/*============ sharkafteating: called each time a shark eats, after it eats. ======= */
		public static void sharkafteating(List <Block> blocks_List){

		}

		/*============ fishafteating: called each time a fish eats, after it eats. ======= */
		public static int fishafteating(int size, int current_Index, Block[] remaining_Blocks){

		}

		/*============ sharkfishsyncinit: called only once, before the shark and fish are created. ======= */
		public static void sharkfishsyncinit(string process_Name, int size, Block[] remaining_Blocks){

        }

		/*============ sharfishsynccleanup: called only once, after all sharks and fish have finished. ======= */
        public static void sharfishsynccleanup(Block[] remaining_Blocks, List<Block> blocks_List, string process_Name){

        	
        }

        /*============ syncproblem . ======= */
        public static void syncproblem (string  process_Name, Block[] remaining_Blocks, int size, int chunk_Size){

        }

        /*============ Main Thread Start Here. ======= */
        static void Main(string []args){

			Console.Write("3 sharks and 4 fish eating using 2 seats in the food-point\n");
		}
	}
