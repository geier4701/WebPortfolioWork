using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALHUMANTEXTINGSERVICE.MODELS
{
	public class Reservation
	{
		public int roomNumber { get; set; }
		public int startTimeStamp { get; set; }
		public int endTimeStamp { get; set; }
	}
}
