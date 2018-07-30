using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCodingVine.Model.Identities;
using TheCodingVine.Model.Tables;

namespace TheCodingVine.UI.ViewModels
{
	public class HomeVM : SiteLinksVM
	{
		public List<BlogPost> BlogRoll { get; set; }
		public List<AppUser> BlogWriters { get; set; }

		public HomeVM()
		{
			BlogRoll = new List<BlogPost>();
			BlogWriters = new List<AppUser>();
		}

		public void SetBlogRoll(IEnumerable<BlogPost> blogPosts)
		{
			foreach(var blog in blogPosts )
			{
				if(blog.IsApproved)
				{
					BlogRoll.Add(blog);
				}
			}
		}

		public void SetBlogWriters(IEnumerable<AppUser> writers)
		{
			foreach (var writer in writers)
			{
				BlogWriters.Add(writer);
			}
		}
	}
}