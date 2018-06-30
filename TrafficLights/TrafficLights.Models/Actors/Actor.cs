using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLights.Models.Directions;

namespace TrafficLights.Models.Actors
{
	public class Actor
	{
		public Direction DirectionOfTravel { get; set; }
		public ActorType ActorType { get; set; }
		public RelativeDirection StreetSide { get; set; }
	}
}
