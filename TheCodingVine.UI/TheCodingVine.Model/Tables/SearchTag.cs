using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCodingVine.Model.Tables
{
	public class SearchTag
	{
		public int SearchTagId { get; set; }
		public string SearchTagBody { get; set; }

		public ICollection<BlogPost> BlogPosts { get; set; }

		public SearchTag()
		{
			BlogPosts = new HashSet<BlogPost>();
		}
	}
}
