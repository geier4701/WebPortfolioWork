using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using TheCodingVine.Data;
using TheCodingVine.Model;
using TheCodingVine.Model.Identities;
using TheCodingVine.Model.Queries;
using TheCodingVine.Model.Tables;
using TheCodingVine.UI.ViewModels;

namespace TheCodingVine.UI.Controllers
{
    public class BlogController : Controller
    {
 
        [HttpGet]
        [Authorize]
        public ActionResult MyBlogs()
        {
            UserManager<AppUser> userMgr = new UserManager<AppUser>(new UserStore<AppUser>(new TheCodingVineDbContext()));
            var user = userMgr.FindByName(User.Identity.Name);

            var blogRollVM = new BlogRollVM();

            blogRollVM.SetBlogRoll(user.UserPosts);

            return View(blogRollVM);
        }

		[HttpGet]
		[Authorize]
		public ActionResult ViewBlog (int id)
		{
			BlogManager manager = BlogManagerFactory.Create();
			var blog = manager.GetBlog(id);

			UserManager<AppUser> userMgr = new UserManager<AppUser>(new UserStore<AppUser>(new TheCodingVineDbContext()));
			var users = userMgr.Users;

			var blogVM = new BlogVM();
			blogVM.SetBlog(blog);
			blogVM.SetBlogWriters(users);

			return View(blogVM);
		}

		[HttpGet]
		[Authorize]
		public ActionResult ViewSinglePending(int id)
		{
			BlogManager manager = BlogManagerFactory.Create();
			var blog = manager.GetBlog(id);

			UserManager<AppUser> userMgr = new UserManager<AppUser>(new UserStore<AppUser>(new TheCodingVineDbContext()));
			var users = userMgr.Users;

			var blogVM = new BlogVM();
			blogVM.SetBlog(blog);
			blogVM.SetBlogWriters(users);

			return View(blogVM);
		}

		[HttpGet]
		[Authorize]
		public ActionResult ViewSingleAll(int id)
		{
			BlogManager manager = BlogManagerFactory.Create();
			var blog = manager.GetBlog(id);

			UserManager<AppUser> userMgr = new UserManager<AppUser>(new UserStore<AppUser>(new TheCodingVineDbContext()));
			var users = userMgr.Users;

			var blogVM = new BlogVM();
			blogVM.SetBlog(blog);
			blogVM.SetBlogWriters(users);

			return View(blogVM);
		}

		[HttpGet]
        [Authorize(Roles = "Admin,BlogWriter")]
        public ActionResult GetAllBlogs()
        {
            BlogManager manager = BlogManagerFactory.Create();
            var blogRoll = manager.GetAllBlogs();

			UserManager<AppUser> userMgr = new UserManager<AppUser>(new UserStore<AppUser>(new TheCodingVineDbContext()));
			var users = userMgr.Users;

			var blogRollVM = new BlogRollVM();
            blogRollVM.SetBlogRoll(blogRoll);
			blogRollVM.SetUsers(users);

            return View(blogRollVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,BlogWriter")]
        public ActionResult AddBlogPost()
        {
            BlogPost blog = new BlogPost();

            var blogVM = new BlogVM();
            blogVM.SetBlog(blog);

            return View(blogVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,BlogWriter")]
        public ActionResult AddBlogPost(BlogVM model)
        {
            if (ModelState.IsValid)
            {

                if (string.IsNullOrEmpty(model.Blog.Title))
                {
                    ModelState.AddModelError("Title", "You must enter a title.");
                }
                else if (string.IsNullOrEmpty(model.Blog.Content))
                {
                    ModelState.AddModelError("Content", "You must enter something in the blog body.");
                }
                else
                {

                    BlogManager manager = BlogManagerFactory.Create();

					if(model.Blog.TagInputs != null)
					{
						string[] tags = model.Blog.TagInputs.Split(',');

						foreach (var tag in tags)
						{
							var searchTag = new SearchTag()
							{
								SearchTagBody = tag
							};
							model.Blog.SearchTags.Add(searchTag);
						}

						model.Blog.TagInputs = null;
					}

					TheCodingVineDbContext context = new TheCodingVineDbContext();

                    UserManager<AppUser> userManager = new UserManager<AppUser>(new UserStore<AppUser>(context));

                    model.Blog.BlogWriter = userManager.FindByName(User.Identity.Name);

                    manager.AddBlog(model.Blog);

                    return RedirectToAction("MyBlogs");
                }
            }
            return View(model); 
        }

        [HttpGet]
        [Authorize(Roles = "Admin,BlogWriter")]
        public ActionResult EditABlog(int id)
        {
            BlogManager manager = BlogManagerFactory.Create();
            var blogToEdit = manager.GetBlog(id);

			UserManager<AppUser> userMgr = new UserManager<AppUser>(new UserStore<AppUser>(new TheCodingVineDbContext()));
			var users = userMgr.Users;

			foreach (var tag in blogToEdit.SearchTags)
            {
                if (blogToEdit.TagInputs == null)
                {
                    blogToEdit.TagInputs = tag.SearchTagBody;
                }
                else
                {
                    blogToEdit.TagInputs = (blogToEdit.TagInputs + "," + tag.SearchTagBody);
                }
            }

            var blogVM = new BlogVM();
            blogVM.SetBlog(blogToEdit);
			blogVM.SetBlogWriters(users);

            return View(blogVM);
        }

		[HttpPost]
		[Authorize(Roles = "Admin,BlogWriter")]
		public ActionResult EditABlog(BlogVM blogVM)
		{
			BlogManager manager = BlogManagerFactory.Create();
			
			if(blogVM.Blog.TagInputs != null)
			{
				string[] tags = blogVM.Blog.TagInputs.Split(',');

				foreach (var tag in tags)
				{
					var searchTag = new SearchTag()
					{
						SearchTagBody = tag
					};

					blogVM.Blog.SearchTags.Add(searchTag);
				}

				blogVM.Blog.TagInputs = null;
			}

			if (ModelState.IsValid)
			{
				manager.UpdateBlog(blogVM.Blog);
				return RedirectToAction("MyBlogs");
			}
			else
			{
				return RedirectToAction("EditABlog");
			}
		}

		[HttpGet]
		[Authorize(Roles = "Admin,BlogWriter")]
		public ActionResult DeleteABlog(int id)
		{
			BlogManager manager = BlogManagerFactory.Create();

			var blog = manager.GetBlog(id);

			BlogVM blogVM = new BlogVM();
			blogVM.SetBlog(blog);

			return View(blogVM);
		}

		[HttpPost]
		[Authorize(Roles = "Admin,BlogWriter")]
		public ActionResult DeleteABlog(BlogVM blogVM)
		{
			BlogManager manager = BlogManagerFactory.Create();
			manager.DeleteBlog(blogVM.Blog.BlogPostId);
			return RedirectToAction("MyBlogs");
		}


		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult ViewPending()
		{
			BlogManager manager = BlogManagerFactory.Create();
			var blogRoll = manager.GetAllPending();

			UserManager<AppUser> userMgr = new UserManager<AppUser>(new UserStore<AppUser>(new TheCodingVineDbContext()));
			var users = userMgr.Users;

			var blogRollVM = new BlogRollVM();
			blogRollVM.SetBlogRoll(blogRoll);
			blogRollVM.SetUsers(users);

			return View(blogRollVM);
		}

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Review(int id)
        {
            BlogManager manager = BlogManagerFactory.Create();
            BlogPost toReview = manager.GetBlog(id);

            foreach (var tag in toReview.SearchTags)
            {
                if (toReview.TagInputs == null)
                {
                    toReview.TagInputs = tag.SearchTagBody;
                }
                else
                {
                    toReview.TagInputs = (toReview.TagInputs + "," + tag.SearchTagBody);
                }
            }

			UserManager<AppUser> userMgr = new UserManager<AppUser>(new UserStore<AppUser>(new TheCodingVineDbContext()));
			var users = userMgr.Users;

			var blogVM = new BlogVM();
            blogVM.SetBlog(toReview);
			blogVM.SetBlogWriters(users);

            return View(blogVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Review(BlogVM model)
        {
            BlogManager manager = BlogManagerFactory.Create();

			if(model.Blog.TagInputs != null)
			{
				string[] tags = model.Blog.TagInputs.Split(',');

				foreach (var tag in tags)
				{
					var searchTag = new SearchTag()
					{
						SearchTagBody = tag
					};

					model.Blog.SearchTags.Add(searchTag);
				}

				model.Blog.TagInputs = null;
			}

			if (ModelState.IsValid)
			{
				manager.UpdateBlog(model.Blog);
				return RedirectToAction("ViewPending");
			}
			else
			{
				return RedirectToAction("Review");
			}
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ViewSearchResults(string searchTag)
        {
            BlogManager manager = BlogManagerFactory.Create();
            var blogRoll = manager.GetSearchResults(searchTag);
			var approvedBlogs = blogRoll.Where(a => a.IsApproved == true)
										.Where(d => d.PostDate <= DateTime.Now);

			UserManager<AppUser> userMgr = new UserManager<AppUser>(new UserStore<AppUser>(new TheCodingVineDbContext()));
			var users = userMgr.Users;

			var blogRollVM = new BlogRollVM();
            blogRollVM.SetBlogRoll(approvedBlogs);
			blogRollVM.SetUsers(users);

			return View(blogRollVM);
        }

    }
}