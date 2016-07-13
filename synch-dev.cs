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
/* =============== Development Version =============================  */
namespace ProcessSynchronization_Space {

  // define shark properties
	class Shark{

	    private string shark_name; // This is the so-called "backing field"
	    public string shark_Name // This is your property
	    {
	        get {return shark_name;}
	        set {shark_name = value;}
	    }

	    private int shark_id; // This is the so-called "backing field"
	    public int shark_Id // This is your property
	    {
	        get {return shark_id;}
	        set {shark_id = value;}
	    }

	    private bool is_eating; // This is the so-called "backing field"
	    public bool is_Eating // This is your property
	    {
	        get {return is_eating;}
	        set {is_eating = value;}
	    }

	}

	// define fish properties
	class Fish{
	    private string fish_name; // This is the so-called "backing field"
	    public string fish_Name // This is your property
	    {
	        get {return fish_name;}
	        set {fish_name = value;}
	    }

	    private int fish_id; // This is the so-called "backing field"
	    public int fish_Id // This is your property
	    {
	        get {return fish_id;}
	        set {fish_id = value;}
	    }

	    private bool is_eating; // This is the so-called "backing field"
	    public bool is_Eating // This is your property
	    {
	        get {return is_eating;}
	        set {is_eating = value;}
	    }
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


		static Semaphore obj_Shark = new Semaphore(2, 4);
		static Semaphore obj_Fish = new Semaphore(2, 4);



		static void Shark_SempStart(object id, Shark[] new_Shark)
		{
			new_Shark[Convert.ToInt32(id)].shark_Name = "Shark_" + id;
			new_Shark[Convert.ToInt32(id)].shark_Id = (int)id;
			new_Shark[Convert.ToInt32(id)].is_Eating = true;

			Console.WriteLine("Shark " + id + " Wants to Get Enter");
			try
			{	    

				obj_Shark.WaitOne();
				Console.WriteLine(" Success: Shark " + id + " is in and Weeding");

				Thread.Sleep(2000);
				Console.WriteLine("Shark "+ id +" is Leaving");
			}
			finally
			{
				obj_Shark.Release();
				new_Shark[Convert.ToInt32(id)].is_Eating = false;

			}
		}

		static void Fish_SempStart(object id, Fish[] new_Fish)
		{
			new_Fish[Convert.ToInt32(id)].fish_Name = "Fish_" + id;
			new_Fish[Convert.ToInt32(id)].fish_Id = (int)id;
			new_Fish[Convert.ToInt32(id)].is_Eating = true;
			Console.WriteLine("Fish " + id + " Wants to Get Enter");
			try
			{
				obj_Fish.WaitOne();
				Console.WriteLine(" Success: Fish " + id + " is in and Weeding");

				Thread.Sleep(2000);
				Console.WriteLine("Fish "+ id +" is Leaving");
			}
			finally
			{
				obj_Fish.Release();
				new_Fish[Convert.ToInt32(id)].is_Eating = false;
			}
		}

        /*============ Main Thread Start Here. ======= */
		static void Main(string[] args)
		{
			Console.Write("3 sharks and 4 fish eating using 2 seats in the food-point\n");


			int milliSec = 3500;
			String Result = "\0"; // null
			Console.Write("Number of sharks in Sea ?\n");

			int sharks_in_Sea = 3;
			Result = Console.ReadLine();
			while(!Int32.TryParse(Result, out sharks_in_Sea))
			{
				Console.Write("Not a valid number, try again.\n");
				Result = Console.ReadLine();
			}

			Console.Write("Number of Fishes in Sea.\n");
			int fishes_in_Sea = 4;
			Result = Console.ReadLine();
			while(!Int32.TryParse(Result, out fishes_in_Sea))
			{
				Console.Write("Not a valid number, try again.\n");
				Result = Console.ReadLine();
			}

			Shark[] new_Shark = new Shark[sharks_in_Sea];
			Fish[] new_Fish = new Fish[fishes_in_Sea];

			for (int i = 1; i <= 10; i++)
			{	
				Thread thread = new Thread(delegate(){
			        Shark_SempStart(i, new_Shark);
			    });
			    
			    thread.Start();
			}

			for (int i = 1; i <= 10; i++)
			{	
				Thread thread = new Thread(delegate(){
			        Fish_SempStart(i, new_Fish);
			    });

			    thread.Start();
			}

			Console.ReadKey();
		}
	}
}
