using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCodingVine.Data;
using TheCodingVine.Model.Queries;

namespace TheCodingVine.UI.ViewModels
{
	public class SiteLinksVM
	{
		public List<SiteStaticLink> SiteLinks { get; set;}

		public SiteLinksVM()
		{
			SiteLinks = new List<SiteStaticLink>();
			GetStaticLinks();
		}

		public void SetStaticLinks(IEnumerable<SiteStaticLink> siteLinks)
		{
			foreach(var link in siteLinks)
			{
				SiteLinks.Add(link);
			}
		}

		private void GetStaticLinks()
		{
			BlogManager manager = BlogManagerFactory.Create();
			var siteLinkList = manager.GetSiteLinks();
			SetStaticLinks(siteLinkList);
		}
	}
}