using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheCodingVine.Data;
using TheCodingVine.UI.ViewModels;

namespace TheCodingVine.UI.Controllers
{
    public class StaticPostController : Controller
    {
        
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult CreateStaticPost()
		{
			BlogManager manager = BlogManagerFactory.Create();

			var postVM = new PostVM();

			return View(postVM);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public ActionResult CreateStaticPost(PostVM model)
		{
			BlogManager manager = BlogManagerFactory.Create();

			manager.AddPost(model.Post);

			return RedirectToAction("ViewStaticPosts");
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult EditStaticPost(int id)
		{
			BlogManager manager = BlogManagerFactory.Create();

			var postToEdit = manager.GetStaticPost(id);

			var postVM = new PostVM();
			postVM.SetPost(postToEdit);

			return View(postVM);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public ActionResult EditStaticPost(PostVM postVM)
		{
			BlogManager manager = BlogManagerFactory.Create();

			manager.UpdateStaticPost(postVM.Post);

			return RedirectToAction("ViewStaticPosts");
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult ViewStaticPosts()
		{
			BlogManager manager = BlogManagerFactory.Create();
			var postList = manager.GetAllStaticPosts();

			var postListVM = new PostListVM();
			postListVM.SetPostList(postList);

			return View(postListVM);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ActionResult DeleteStaticPost(int id)
		{
			BlogManager manager = BlogManagerFactory.Create();
			var postToDelete = manager.GetStaticPost(id);

			var postVM = new PostVM();
			postVM.SetPost(postToDelete);

			return View(postVM);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public ActionResult DeleteStaticPost(PostVM postVM)
		{
			BlogManager manager = BlogManagerFactory.Create();
			manager.DeleteStaticPost(postVM.Post.StaticPostId);

			return RedirectToAction("ViewStaticPosts");
		}


		[HttpGet]
		[AllowAnonymous]
		public ActionResult ViewPost(int id)
		{
			BlogManager manager = BlogManagerFactory.Create();
			var postToView = manager.GetStaticPost(id);

			var postVM = new PostVM();
			postVM.SetPost(postToView);
	
			return View(postVM);
		}

		[HttpGet]
		[AllowAnonymous]
		public ActionResult ViewPostFromList(int id)
		{
			BlogManager manager = BlogManagerFactory.Create();
			var postToView = manager.GetStaticPost(id);

			var postVM = new PostVM();
			postVM.SetPost(postToView);

			return View(postVM);
		}
	}
}