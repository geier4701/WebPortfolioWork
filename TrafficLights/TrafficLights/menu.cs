using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLights.BLL;
using TrafficLights.Models;

namespace TrafficLights
{
	public class menu
	{
		public static void Start()
		{
			bool runApp = true;
			while (runApp)
			{
				Console.WriteLine("Welcome to traffic light simulator");
				Console.WriteLine("----------------------------------");
				Console.WriteLine("1. Watch lights go!");
				Console.WriteLine("Q. Quit");
				string menuChoice = Console.ReadLine().ToUpper();

				switch (menuChoice)
				{
					case "1":
						int actorSpeed;
						int lightSpeed;

						Console.WriteLine("What second interval do you want to generate actors?");
						try
						{
							actorSpeed = int.Parse(Console.ReadLine()) * 1000;
							if (actorSpeed < 1)
							{
								Console.Clear();
								Console.WriteLine("Invalid time entered");
								break;
							}
						}
						catch
						{
							Console.Clear();
							Console.WriteLine("Invalid time entered");
							break;
						}

						Console.WriteLine("What second interval do you want to cycle lights?");
						try
						{
							lightSpeed = int.Parse(Console.ReadLine()) * 1000;
							if (lightSpeed < 1)
							{
								Console.Clear();
								Console.WriteLine("Invalid time entered");
								break;
							}
						}
						catch
						{
							Console.Clear();
							Console.WriteLine("Invalid time entered");
							break;
						}

						Workflow RunProgram = new Workflow();
						RunProgram.RunCars(actorSpeed, lightSpeed);
						Console.ReadKey();
						runApp = false;
						break;

					case "Q":
						runApp = false;
						break;

					default:
						Console.Clear();
						Console.WriteLine("Please enter a number or the letter 'Q'");
						break;
				}
			}
		}
	}
}
