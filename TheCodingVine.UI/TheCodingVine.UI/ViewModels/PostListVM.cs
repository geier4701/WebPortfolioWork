using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCodingVine.Model.Tables;

namespace TheCodingVine.UI.ViewModels
{
	public class PostListVM : SiteLinksVM
	{
		public List<StaticPost> PostList { get; set; }

		public PostListVM()
		{
			PostList = new List<StaticPost>();
		}

		public void SetPostList(IEnumerable<StaticPost> postList)
		{
			foreach(var post in postList)
			{
				PostList.Add(post);
			}
		}
	}
}