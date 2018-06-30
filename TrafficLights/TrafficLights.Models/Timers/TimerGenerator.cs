using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TrafficLights.Models.Mechanicals;

namespace TrafficLights.Models
{
	public class TimerGenerator
	{
		public Timer CreateTimer(int interval)
		{
			Timer timer = new Timer();

			timer = new Timer(interval)
			{
				AutoReset = true,
				Enabled = true
			};

			return timer;
		}
	}
}
