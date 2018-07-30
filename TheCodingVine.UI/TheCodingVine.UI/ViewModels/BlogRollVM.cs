using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCodingVine.Data;
using TheCodingVine.Model.Identities;
using TheCodingVine.Model.Tables;

namespace TheCodingVine.UI.ViewModels
{
	public class BlogRollVM : SiteLinksVM
	{
		public List<BlogPost> BlogRoll { get; set; }
		public List<AppUser> BlogWriters { get; set; }

		public BlogRollVM()
		{
			BlogRoll = new List<BlogPost>();
			BlogWriters = new List<AppUser>();
		}

		public void SetBlogRoll(IEnumerable<BlogPost> blogRoll)
		{
			foreach (var blog in blogRoll)
			{
				BlogRoll.Add(blog);
			}
		}

		public void SetUsers(IEnumerable<AppUser> blogWriters)
		{
			foreach (var user in blogWriters)
			{
				BlogWriters.Add(user);
			}
		}
	}
}