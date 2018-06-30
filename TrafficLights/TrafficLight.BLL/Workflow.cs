using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TrafficLights.BLL;
using TrafficLights.Models;
using TrafficLights.Models.Actors;
using TrafficLights.Models.Directions;
using TrafficLights.Models.Mechanicals;

namespace TrafficLights
{
	public class Workflow
	{
		
		TimerGenerator timerGenerator = new TimerGenerator();
		TrafficTracker trafficTracker = new TrafficTracker();
		TrafficGenerator trafficGen = new TrafficGenerator();
		LightController lightController = new LightController();
		Dictionary<Direction, Trafficlight> allLights = new Dictionary<Direction, Trafficlight>();
		List<Direction> waitList = new List<Direction>();
		int timer = 0;

		public void RunCars(int actorSpeed, int lightSpeed)
		{
			trafficTracker.InitializeButtons();
			trafficTracker.InitializePlates();
			
			Timer trafficTimer = timerGenerator.CreateTimer(actorSpeed);
			trafficTimer.Elapsed += TrafficTimer_Elapsed; ;
			
			allLights = lightController.SetLights();
			Timer lightTimer = timerGenerator.CreateTimer(lightSpeed);
			lightTimer.Elapsed += Lighttimer_Elapsed;

			Timer refreshRate = timerGenerator.CreateTimer(1000);
			refreshRate.Elapsed += RefreshRate_Elapsed;
		}

		private void TrafficTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			Actor actor = trafficGen.NewTraffic();

			bool addDirection = true;
			foreach (Direction direction in waitList)
			{
				if (direction == actor.DirectionOfTravel)
				{
					addDirection = false;
				}
			}

			if (addDirection == true)
			{
				waitList.Add(actor.DirectionOfTravel);
			}

			trafficTracker.AddActor(actor);
		}

		private void Lighttimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			lightController.SwitchLights(waitList, allLights);
		}

		private void RefreshRate_Elapsed(object sender, ElapsedEventArgs e)
		{
			trafficTracker.RemoveActor(allLights);
			Dictionary<Direction, int> carCount = trafficTracker.CarCount();
			Dictionary<Direction, int> pedCount = trafficTracker.PedCount();
			timer++;

			DrawBoard(allLights, carCount, pedCount, timer, waitList);
		}

		private void DrawBoard(Dictionary<Direction, Trafficlight> allLights, Dictionary<Direction, int> carCount, Dictionary<Direction, int> pedCount, int timer, List<Direction> waitList)
		{
			Console.Clear();
			Console.WriteLine($"Time elapsed: {timer}");

			Console.Write("Waitlist: ");
			foreach(Direction direction in waitList)
			{
				Console.Write($"{direction.ToString()} ");
			}
			Console.WriteLine();

			Console.WriteLine("        |      |        ");
			Console.WriteLine($"        |      |{pedCount[Direction.West]}       ");
			if (allLights[Direction.North].CarGo == true)
			{
				Console.WriteLine($"_____{pedCount[Direction.South]}__| {carCount[Direction.South]}  G |________");
			}
			else
			{
				Console.WriteLine($"_____{pedCount[Direction.South]}__| {carCount[Direction.South]}  R |________");
			}
			if (allLights[Direction.West].CarGo == true)
			{
				Console.WriteLine($"       G        {carCount[Direction.West]}       ");
			}
			else
			{
				Console.WriteLine($"       R        {carCount[Direction.West]}       ");
			}
			Console.WriteLine("                        ");
			if (allLights[Direction.East].CarGo == true)
			{
				Console.WriteLine($"       {carCount[Direction.East]}        G       ");
			}
			else
			{
				Console.WriteLine($"       {carCount[Direction.East]}        R       ");
			}
			Console.WriteLine("________        ________");
			if (allLights[Direction.South].CarGo == true)
			{
				Console.WriteLine($"        | G  {carCount[Direction.North]} |  {pedCount[Direction.North]}     ");
			}
			else
			{
				Console.WriteLine($"        | R  {carCount[Direction.North]} |  {pedCount[Direction.North]}     ");
			}
			Console.WriteLine($"       {pedCount[Direction.East]}|      |        ");
			Console.WriteLine("        |      |        ");
			Console.WriteLine("        |      |        ");

			Console.WriteLine("\nPress any key to quit");

			//        |      |        
			//        |      |        
			//        |      |#       
			//_____#__| #  R |________
			//       G        #       
			//                        
			//       #        G       
			//________        ________
			//        | R  # |  #     
			//       #|      |        
			//        |      |        
			//        |      |        
		}
	}
}