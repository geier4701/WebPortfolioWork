using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCodingVine.Model.Identities;

namespace TheCodingVine.UI.ViewModels
{
    public class UserViewModel: SiteLinksVM
    {
        public AppUser User { get; set; }
		public List<IdentityRole> AvailRoles { get; set; }

        public UserViewModel()
        {
			User = new AppUser();
			AvailRoles = new List<IdentityRole>();
        }

        public void SetUser(AppUser user)
        {
            User = user;
        }

		public void SetUsersRoles(IQueryable<IdentityRole> availRoles)
		{
			foreach(var role in availRoles)
			{
				AvailRoles.Add(role);
			}
		}
    }
}