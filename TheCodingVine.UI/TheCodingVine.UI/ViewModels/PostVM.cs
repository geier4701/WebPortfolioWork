using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCodingVine.Model.Tables;

namespace TheCodingVine.UI.ViewModels
{
	public class PostVM : SiteLinksVM
	{
		public StaticPost Post { get; set; }

		public PostVM()
		{
			Post = new StaticPost();
		}

		public void SetPost(StaticPost post)
		{
			Post = post;
		}
	}
}