using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TrafficLights.Models;
using TrafficLights.Models.Actors;
using TrafficLights.Models.Directions;

namespace TrafficLights.BLL
{
	public class TrafficGenerator
	{
		private Random random = new Random();

		public Actor NewTraffic()
		{
			Actor actor = new Actor();
			actor.DirectionOfTravel = (Direction)random.Next(1, 5);

			int trafficType = random.Next(1, 3);
			if (trafficType == 1)
			{
				actor.ActorType = ActorType.Car;
			}
			else
			{
				actor.StreetSide = (RelativeDirection)random.Next(1, 3);
				actor.ActorType = ActorType.Pedestrian;
			};

			return actor;
		}
	}
}
