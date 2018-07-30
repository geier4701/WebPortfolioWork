using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCodingVine.Data;
using TheCodingVine.Model.Tables;

namespace TheCodingVine.Tests
{
	[TestFixture]
	public class InMemoryTests
	{
		[Test]
		public void CanLoadBlogRoll()
		{
			InMemoryRepo repo = new InMemoryRepo();

			var blogList = repo.GetAllBlogs();

			Assert.AreEqual(4, blogList.Count());

			BlogPost blog = blogList.FirstOrDefault(b => b.BlogPostId == 1);

			Assert.AreEqual(1, blog.BlogPostId);
			Assert.AreEqual("The First Blog Test Title", blog.Title);
			Assert.AreEqual("This is the blog body. I am going to keep typing until it is now a really big body", blog.Content);
			Assert.AreEqual(new DateTime(2018, 07, 01), blog.PostDate);
			Assert.AreEqual(new DateTime(2018, 09, 01), blog.RemoveDate);
			Assert.AreEqual(true, blog.IsApproved);
			Assert.AreEqual("This blog rocks!! A note from admin.", blog.BlogNotes);
			Assert.AreEqual("TestAdmin@test.com", blog.BlogWriter.UserName);
		}

		[Test]
		public void CanLoadBlogById()
		{
			InMemoryRepo repo = new InMemoryRepo();
			var blog = new BlogPost();

			blog = repo.GetBlog(1);

			Assert.IsNotNull(blog);

			Assert.AreEqual(1, blog.BlogPostId);
			Assert.AreEqual("The First Blog Test Title", blog.Title);
			Assert.AreEqual("This is the blog body. I am going to keep typing until it is now a really big body", blog.Content);
			Assert.AreEqual(new DateTime(2018, 07, 01), blog.PostDate);
			Assert.AreEqual(new DateTime(2018, 09, 01), blog.RemoveDate);
			Assert.AreEqual(true, blog.IsApproved);
			Assert.AreEqual("This blog rocks!! A note from admin.", blog.BlogNotes);
			Assert.AreEqual("TestAdmin@test.com", blog.BlogWriter.UserName);
		}

		[Test]
		public void CanAddBlog()
		{
			InMemoryRepo repo = new InMemoryRepo();
			var blog = new BlogPost
			{
				Title = "The Fourth Blog Test Title",
				Content = "Here's another fantastic blog about business stuff!",
				PostDate = new DateTime(2018, 07, 03),
				RemoveDate = new DateTime(2018, 09, 03),
				IsApproved = false,
				BlogNotes = "This is the best blog so far!",
				BlogWriter = new Model.Identities.AppUser { UserName = "testUser@test.com" },
			};

			repo.AddBlog(blog);

			var blogList = repo.GetAllBlogs();

			Assert.AreEqual(4, blogList.Count());

			repo.GetBlog(4);

			Assert.AreEqual(4, blog.BlogPostId);
			Assert.AreEqual("The Fourth Blog Test Title", blog.Title);
			Assert.AreEqual("Here's another fantastic blog about business stuff!", blog.Content);
			Assert.AreEqual(new DateTime(2018, 07, 03), blog.PostDate);
			Assert.AreEqual(new DateTime(2018, 09, 03), blog.RemoveDate);
			Assert.AreEqual(false, blog.IsApproved);
			Assert.AreEqual("This is the best blog so far!", blog.BlogNotes);
			Assert.AreEqual("testUser@test.com", blog.BlogWriter.UserName);
		}

		[Test]
		public void CanRemoveBlog()
		{
			InMemoryRepo repo = new InMemoryRepo();
			var blog = new BlogPost();
			repo.DeleteBlog(3);

			blog = repo.GetBlog(3);

			Assert.IsNull(blog);
		}

		[Test]
		public void CanUpdateBlog()
		{
			InMemoryRepo repo = new InMemoryRepo();
			var blog = new BlogPost();
			blog = repo.GetBlog(2);

			blog.Title = "2nd Time becomes the 4th time";
			blog.Content = "Now even more businessy";
			blog.PostDate = new DateTime(2018, 09, 04);
			blog.RemoveDate = new DateTime(2018, 11, 04);
			blog.IsApproved = false;
			blog.BlogNotes = "Lets change this up.";
			blog.BlogWriter.UserName = "testUser@test.com";

			repo.UpdateBlog(blog);

			var updateBlog = new BlogPost();
			updateBlog = repo.GetBlog(2);

			Assert.AreEqual("2nd Time becomes the 4th time", updateBlog.Title);
			Assert.AreEqual("Now even more businessy", updateBlog.Content);
			Assert.AreEqual(new DateTime(2018, 09, 04), updateBlog.PostDate);
			Assert.AreEqual(new DateTime(2018, 11, 04), updateBlog.RemoveDate);
			Assert.AreEqual(false, updateBlog.IsApproved);
			Assert.AreEqual("Lets change this up.", updateBlog.BlogNotes);
			Assert.AreEqual("testUser@test.com", updateBlog.BlogWriter.UserName);
		}

		[Test]
		public void CanLoadAllStaticPages()
		{
			InMemoryRepo repo = new InMemoryRepo();

			var pageList = repo.GetAllStaticPosts();

			Assert.AreEqual(3, pageList.Count());

			StaticPost page = pageList.FirstOrDefault(b => b.StaticPostId == 1);

			Assert.AreEqual(1, page.StaticPostId);
			Assert.AreEqual("Test Static Page", page.Title);
			Assert.AreEqual(" <!DOCTYPE html><html><head><meta charset = 'UTF-8'><title>Sample Static Title</title></head><body>This is the body of a sample static page.</body></html>", page.Content);
		}

		[Test]
		public void CanLoadStaticPageById()
		{
			InMemoryRepo repo = new InMemoryRepo();
			var page = new StaticPost();

			page = repo.GetStaticPost(1);

			Assert.IsNotNull(page);

			Assert.AreEqual(1, page.StaticPostId);
			Assert.AreEqual("Test Static Page", page.Title);
			Assert.AreEqual(" <!DOCTYPE html><html><head><meta charset = 'UTF-8'><title>Sample Static Title</title></head><body>This is the body of a sample static page.</body></html>", page.Content);
		}

		[Test]
		public void CanAddStaticPage()
		{
			InMemoryRepo repo = new InMemoryRepo();
			var page = new StaticPost
			{
				Title = "Testimonials",
				Content = " <!DOCTYPE html><html><head><meta charset = 'UTF-8'><title>Testimonials</title></head><body>Listen to people who love our services.</body></html>",
			};

			repo.AddStaticPost(page);

			var pageList = repo.GetAllStaticPosts();

			Assert.AreEqual(4, pageList.Count());

			page = repo.GetStaticPost(4);

			Assert.AreEqual(4, page.StaticPostId);
			Assert.AreEqual("Testimonials", page.Title);
			Assert.AreEqual(" <!DOCTYPE html><html><head><meta charset = 'UTF-8'><title>Testimonials</title></head><body>Listen to people who love our services.</body></html>", page.Content);
		}

		[Test]
		public void CanUpdateStaticPage()
		{
			InMemoryRepo repo = new InMemoryRepo();
			var page = new StaticPost();
			page = repo.GetStaticPost(2);

			page.Title = "Our Story";
			page.Content = " <!DOCTYPE html><html><head><meta charset = 'UTF-8'><title>Our Story</title></head><body>Here's how we got started in this business!</body></html>";
			
			repo.UpdateStaticPost(page);

			var updatedPage = new StaticPost();
			updatedPage = repo.GetStaticPost(2);

			Assert.AreEqual("Our Story", updatedPage.Title);
			Assert.AreEqual(" <!DOCTYPE html><html><head><meta charset = 'UTF-8'><title>Our Story</title></head><body>Here's how we got started in this business!</body></html>", updatedPage.Content);
			
		}

		[Test]
		public void CanDeleteStaticPage()
		{
			InMemoryRepo repo = new InMemoryRepo();
			var page = new StaticPost();
			repo.DeleteStaticPost(3);

			page = repo.GetStaticPost(3);

			Assert.IsNull(page);
		}
	}
}
