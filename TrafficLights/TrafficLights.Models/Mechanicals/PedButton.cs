using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficLights.Models.Directions;

namespace TrafficLights.Models.Mechanicals
{
	public class PedButton
	{
		public Direction PedDirection { get; set; }
		public int Count { get; set; }

		public void Activate()
		{
			Count++;
		}

		public void Remove()
		{
			if (Count > 0)
			{
				Count--;
			}
		}
	}
}
