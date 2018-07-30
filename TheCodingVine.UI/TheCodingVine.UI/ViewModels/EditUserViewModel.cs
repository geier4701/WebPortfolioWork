using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheCodingVine.Model.Identities;

namespace TheCodingVine.UI.ViewModels
{
    public class EditUserViewModel : SiteLinksVM
    {
        public AppUser User { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
        public string SelectedRoleId { get; set; }
        public string UserName { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

        public EditUserViewModel()
        {
            User = new AppUser();
        }

        public void SetUser(AppUser user)
        {
            User = user;
        }

    }
}