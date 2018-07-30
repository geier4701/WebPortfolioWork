using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using TheCodingVine.Model;
using TheCodingVine.Model.Identities;
using TheCodingVine.UI.App_Start;
using TheCodingVine.UI.ViewModels;

namespace TheCodingVine.UI
{
	public partial class Startup
	{
		// For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
		public void ConfigureAuth(IAppBuilder app)
		{
			// Configure the db context, user manager and signin manager to use a single instance per request
			var DbContext = new TheCodingVineDbContext();
			app.CreatePerOwinContext(DbContext.Create);
			app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
			app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
		}
	}
}