using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLights.Models.Actors;
using TrafficLights.Models.Directions;
using TrafficLights.Models.Mechanicals;

namespace TrafficLights.Models
{
	public class TrafficTracker
	{
		PedButton nbPedButton;
		PedButton ebPedButton;
		PedButton sbPedButton;
		PedButton wbPedButton;
		PressurePlate nbPlate;
		PressurePlate ebPlate;
		PressurePlate sbPlate;
		PressurePlate wbPlate;

		public void InitializeButtons()
		{
			nbPedButton = new PedButton
			{
				PedDirection = Direction.North
			};

			ebPedButton = new PedButton
			{
				PedDirection = Direction.East
			};

			sbPedButton = new PedButton
			{
				PedDirection = Direction.South
			};

			wbPedButton = new PedButton
			{
				PedDirection = Direction.West
			};
		}

		public void InitializePlates()
		{
			nbPlate = new PressurePlate
			{
				CarDirection = Direction.North
			};

			ebPlate = new PressurePlate
			{
				CarDirection = Direction.East
			};

			sbPlate = new PressurePlate
			{
				CarDirection = Direction.South
			};

			wbPlate = new PressurePlate
			{
				CarDirection = Direction.West
			};
		}

		public void AddActor(Actor actor)
		{
			if (actor.ActorType == ActorType.Car)
			{
				switch (actor.DirectionOfTravel)
				{
					case Direction.North:
						nbPlate.Activate();
						break;
					case Direction.East:
						ebPlate.Activate();
						break;
					case Direction.South:
						sbPlate.Activate();
						break;
					case Direction.West:
						wbPlate.Activate();
						break;
				}
			}
			else
			{
				switch (actor.DirectionOfTravel)
				{
					case Direction.North:
						nbPedButton.Activate();
						break;
					case Direction.East:
						ebPedButton.Activate();
						break;
					case Direction.South:
						sbPedButton.Activate();
						break;
					case Direction.West:
						wbPedButton.Activate();
						break;
				}
			};
		}

		public void RemoveActor(Dictionary<Direction, Trafficlight> allLights)
		{
			if (allLights[Direction.North].CarGo == true)
			{
				nbPlate.Remove();
				nbPedButton.Remove();
			}

			if (allLights[Direction.South].CarGo == true)
			{
				sbPlate.Remove();
				sbPedButton.Remove();
			}

			if (allLights[Direction.East].CarGo == true && allLights[Direction.West].CarGo == true)
			{
				ebPlate.Remove();
				ebPedButton.Remove();
			}

			if (allLights[Direction.West].CarGo == true)
			{
				wbPlate.Remove();
				wbPedButton.Remove();
			}
		}

		public Dictionary<Direction,int> CarCount()
		{
			Dictionary<Direction, int> carCounts = new Dictionary<Direction, int>
			{
				{ Direction.North, nbPlate.Count },
				{ Direction.South, sbPlate.Count },
				{ Direction.East, ebPlate.Count },
				{ Direction.West, wbPlate.Count }
			};

			return carCounts;
		}

		public Dictionary<Direction, int> PedCount()
		{
			Dictionary<Direction, int> pedCounts = new Dictionary<Direction, int>
			{
				{ Direction.North, nbPedButton.Count },
				{ Direction.South, sbPedButton.Count },
				{ Direction.East, ebPedButton.Count },
				{ Direction.West, wbPedButton.Count }
			};

			return pedCounts;
		}
	}
}
