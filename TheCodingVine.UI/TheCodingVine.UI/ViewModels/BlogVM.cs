using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCodingVine.Data;
using TheCodingVine.Model.Identities;
using TheCodingVine.Model.Tables;

namespace TheCodingVine.UI.ViewModels
{
	public class BlogVM : SiteLinksVM
	{
		public BlogPost Blog { get; set; }
		public List<AppUser> BlogWriters {get; set;}

		public BlogVM()
		{
			Blog = new BlogPost();
			BlogWriters = new List<AppUser>();
		}

		public void SetBlog(BlogPost blog)
		{
			Blog = blog;
		}

		public void SetBlogWriters(IEnumerable<AppUser> blogWriters)
		{
			foreach(var writer in blogWriters)
			{
				BlogWriters.Add(writer);
			}
		}

	}
}