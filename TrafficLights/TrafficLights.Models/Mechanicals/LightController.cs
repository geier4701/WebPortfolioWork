using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TrafficLights.Models.Actors;
using TrafficLights.Models.Directions;

namespace TrafficLights.Models.Mechanicals
{
	public class Trafficlight
	{
		public bool CarGo { get; set; }
		public bool PedLeftGo { get; set; }
		public bool PedRightGo { get; set; }
	}

	public class LightController
	{
		public void SwitchLights(List<Direction> waitList, Dictionary<Direction, Trafficlight> allLights)
		{
			if (waitList.Count > 0)
			{
				Direction pulled = waitList[0];

				if (pulled == Direction.North || pulled == Direction.South)
				{
					ChangeLightsNorthSouth(allLights);

					for (int i = waitList.Count - 1; i >= 0; i--)
					{
						if (waitList[i] == Direction.North || waitList[i] == Direction.South)
						{
							waitList.RemoveAt(i);
						}
					}

				}
				else if (pulled == Direction.East || pulled == Direction.West)
				{
					ChangeLightsEastWest(allLights);

					for (int i = waitList.Count - 1; i >= 0; i--)
					{
						if (waitList[i] == Direction.East || waitList[i] == Direction.West)
						{
							waitList.RemoveAt(i);
						}
					}
				}
			}
		}

		public Dictionary<Direction, Trafficlight> SetLights()
		{
			Dictionary<Direction, Trafficlight> allLights = new Dictionary<Direction, Trafficlight>();

			Trafficlight nbLight = new Trafficlight
			{
				CarGo = true,
				PedRightGo = false,
				PedLeftGo = true
			};
			allLights.Add(Direction.North, nbLight);

			Trafficlight sbLight = new Trafficlight
			{
				CarGo = true,
				PedRightGo = false,
				PedLeftGo = true,
			};
			allLights.Add(Direction.South, sbLight);

			Trafficlight ebLight = new Trafficlight
			{
				CarGo = false,
				PedRightGo = true,
				PedLeftGo = false
			};
			allLights.Add(Direction.East, ebLight);

			Trafficlight wbLight = new Trafficlight
			{
				CarGo = false,
				PedRightGo = true,
				PedLeftGo = false
			};
			allLights.Add(Direction.West, wbLight);
			
			return allLights;
		}

		private void ChangeLightsNorthSouth(Dictionary<Direction, Trafficlight> allLights)
		{
			allLights[Direction.North].CarGo = true;
			allLights[Direction.East].PedRightGo = false;
			allLights[Direction.North].PedLeftGo = true;

			allLights[Direction.South].CarGo = true;
			allLights[Direction.West].PedRightGo = false;
			allLights[Direction.South].PedLeftGo = true;

			allLights[Direction.East].CarGo = false;
			allLights[Direction.South].PedRightGo = true;
			allLights[Direction.East].PedLeftGo = false;

			allLights[Direction.West].CarGo = false;
			allLights[Direction.North].PedRightGo = true;
			allLights[Direction.West].PedLeftGo = false;
		}

		private void ChangeLightsEastWest(Dictionary<Direction, Trafficlight> allLights)
		{
			allLights[Direction.North].CarGo = false;
			allLights[Direction.East].PedRightGo = true;
			allLights[Direction.North].PedLeftGo = false;

			allLights[Direction.South].CarGo = false;
			allLights[Direction.West].PedRightGo = true;
			allLights[Direction.South].PedLeftGo = false;

			allLights[Direction.East].CarGo = true;
			allLights[Direction.South].PedRightGo = false;
			allLights[Direction.East].PedLeftGo = true;

			allLights[Direction.West].CarGo = true;
			allLights[Direction.North].PedRightGo = false;
			allLights[Direction.West].PedLeftGo = true;
		}
	}
}
