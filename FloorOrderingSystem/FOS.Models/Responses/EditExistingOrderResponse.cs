﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Models.Responses
{
	public class EditExistingOrderResponse : Response
	{
		public Order Order { get; set; }
	}
}
