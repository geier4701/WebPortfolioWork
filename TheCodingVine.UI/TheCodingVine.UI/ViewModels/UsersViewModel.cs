using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCodingVine.Data;
using TheCodingVine.Model.Identities;

namespace TheCodingVine.UI.ViewModels
{
    public class UsersViewModel: SiteLinksVM
    {
        public List<AppUser> UserList { get; set; }
		public List<IdentityRole> RolesList { get; set; }


        public UsersViewModel()
        {
            UserList = new List<AppUser>();
			RolesList = new List<IdentityRole>();
        }

        public void SetUserList(IEnumerable<AppUser> userList)
        {
            foreach (var user in userList)
            {
                UserList.Add(user);
            }
        }

		public void SetRolesList(IQueryable<IdentityRole> rolesList)
		{
			foreach(var role in rolesList)
			{
				RolesList.Add(role);
			}
		}

		private void GetAllUsers()
		{
			BlogManager manager = BlogManagerFactory.Create();
			var users = manager.GetAllUsers();
			SetUserList(users);
		}
    }
}