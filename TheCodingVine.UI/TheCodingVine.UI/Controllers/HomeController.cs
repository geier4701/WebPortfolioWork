using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheCodingVine.Data;
using TheCodingVine.Model.Tables;
using TheCodingVine.UI.ViewModels;

namespace TheCodingVine.UI.Controllers
{
    public class HomeController : Controller
    {
		[HttpGet]
		[AllowAnonymous]
        public ActionResult Index()
        {
            BlogManager manager = BlogManagerFactory.Create();
			var blogRollVM = new BlogRollVM();
			var blogRoll = manager.GetAllBlogs();
			var approvedBlogs = blogRoll.Where(a => a.IsApproved == true)
										.Where(d => d.PostDate <= DateTime.Now);

			var blogWriters = manager.GetAllUsers();

			blogRollVM.SetBlogRoll(approvedBlogs);
			blogRollVM.SetUsers(blogWriters);

			return View(blogRollVM);
        }
    }
}