using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALHUMANTEXTINGSERVICE.MODELS
{
	public class Guest
	{
		public int id { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public Reservation reservation { get; set; }
	}
}
