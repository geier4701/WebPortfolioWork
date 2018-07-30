using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using TheCodingVine.Model.Identities;
using TheCodingVine.UI.ViewModels;

[assembly: OwinStartupAttribute(typeof(TheCodingVine.UI.Startup))]
namespace TheCodingVine.UI
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}
