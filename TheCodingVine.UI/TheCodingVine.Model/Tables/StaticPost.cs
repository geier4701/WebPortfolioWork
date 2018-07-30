using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TheCodingVine.Model.Tables
{
	public class StaticPost
	{
		public int StaticPostId { get; set; }
		public string Title { get; set; }

		[AllowHtml]
		public string Content { get; set; }
	}
}
