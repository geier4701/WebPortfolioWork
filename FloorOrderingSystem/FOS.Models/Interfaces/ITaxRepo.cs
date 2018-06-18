using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Models.Interfaces
{
	public interface ITaxRepo
	{
		Tax GetState(string inputState);
		IEnumerable<Tax> GetAllStates();
	}
}
