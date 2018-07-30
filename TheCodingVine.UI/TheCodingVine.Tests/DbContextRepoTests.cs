using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCodingVine.Model;
using TheCodingVine.Model.Identities;
using TheCodingVine.Model.Tables;

namespace TheCodingVine.Tests
{


	[TestFixture]
	internal sealed class DbContextRepoTests : BaseIntegrationTestFixture
	{
		[Test]
		public void CanLoadContextBlogRoll()
		{
			//TheCodingVineDbContext repo = new TheCodingVineDbContext();

			var blogList = TestContext.GetAllBlogs();

			Assert.AreEqual(6, blogList.Count());

			BlogPost blog = blogList.FirstOrDefault(b => b.BlogPostId == 1);

			Assert.AreEqual(1, blog.BlogPostId);
			Assert.AreEqual("Test Blog", blog.Title);
			Assert.AreEqual("<h3>Section One</h3> <p><span style=\"font-family: 'Open Sans', Arial, sans-serif;" +
					  " text-align: justify;\">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi in ipsum sagittis, bibendum nisl in," +
					  " convallis magna. Pellentesque varius gravida pharetra. Nam hendrerit sem orci, eu pharetra purus scelerisque euismod. Sed nec " +
					  "ipsum posuere, maximus risus eget, placerat sem. Pellentesque eget aliquet mauris. Pellentesque pretium porttitor nunc vitae semper. " +
					  "Mauris in tincidunt nulla, quis feugiat lorem. Pellentesque finibus nec nibh eu mattis. Nam id dui arcu. Morbi imperdiet lectus" +
					  " tincidunt erat ultrices facilisis. Quisque mi orci, volutpat vestibulum mi et, sollicitudin eleifend purus.</span></p>" +
					  " <h3><span style=\"font-family: 'Open Sans', Arial, sans-serif; text-align: justify;\">Section Two</span></h3> <p><span style=" +
					  "\"font-family: 'Open Sans', Arial, sans-serif; text-align: justify;\">Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
					  "Morbi in ipsum sagittis, bibendum nisl in, convallis magna. Pellentesque varius gravida pharetra. Nam hendrerit sem orci, " +
					  "eu pharetra purus scelerisque euismod. Sed nec ipsum posuere, maximus risus eget, placerat sem. Pellentesque eget aliquet " +
					  "mauris. Pellentesque pretium porttitor nunc vitae semper. Mauris in tincidunt nulla, quis feugiat lorem. Pellentesque finibus" +
					  " nec nibh eu mattis. Nam id dui arcu. Morbi imperdiet lectus tincidunt erat ultrices facilisis. Quisque mi orci, volutpat vestibulum" +
					  " mi et, sollicitudin eleifend purus.</span></p>", blog.Content);
			Assert.AreEqual(new DateTime(2018, 9, 1), blog.PostDate);
			Assert.AreEqual(new DateTime(2018, 11, 1), blog.RemoveDate);
			Assert.AreEqual(true, blog.IsApproved);
			Assert.AreEqual("These are sample notes that would be input from the admin", blog.BlogNotes);
			Assert.AreEqual("TestBlogWriter@test.com", blog.BlogWriter.UserName);

		}

		[Test]
		public void CanGetContextBlogById()
		{
			//TheCodingVineDbContext repo = new TheCodingVineDbContext();

			BlogPost blog = TestContext.GetBlog(1);

			Assert.AreEqual(1, blog.BlogPostId);
			Assert.AreEqual("Test Blog", blog.Title);
			Assert.AreEqual("<h3>Section One</h3> <p><span style=\"font-family: 'Open Sans', Arial, sans-serif;" +
					  " text-align: justify;\">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi in ipsum sagittis, bibendum nisl in," +
					  " convallis magna. Pellentesque varius gravida pharetra. Nam hendrerit sem orci, eu pharetra purus scelerisque euismod. Sed nec " +
					  "ipsum posuere, maximus risus eget, placerat sem. Pellentesque eget aliquet mauris. Pellentesque pretium porttitor nunc vitae semper. " +
					  "Mauris in tincidunt nulla, quis feugiat lorem. Pellentesque finibus nec nibh eu mattis. Nam id dui arcu. Morbi imperdiet lectus" +
					  " tincidunt erat ultrices facilisis. Quisque mi orci, volutpat vestibulum mi et, sollicitudin eleifend purus.</span></p>" +
					  " <h3><span style=\"font-family: 'Open Sans', Arial, sans-serif; text-align: justify;\">Section Two</span></h3> <p><span style=" +
					  "\"font-family: 'Open Sans', Arial, sans-serif; text-align: justify;\">Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
					  "Morbi in ipsum sagittis, bibendum nisl in, convallis magna. Pellentesque varius gravida pharetra. Nam hendrerit sem orci, " +
					  "eu pharetra purus scelerisque euismod. Sed nec ipsum posuere, maximus risus eget, placerat sem. Pellentesque eget aliquet " +
					  "mauris. Pellentesque pretium porttitor nunc vitae semper. Mauris in tincidunt nulla, quis feugiat lorem. Pellentesque finibus" +
					  " nec nibh eu mattis. Nam id dui arcu. Morbi imperdiet lectus tincidunt erat ultrices facilisis. Quisque mi orci, volutpat vestibulum" +
					  " mi et, sollicitudin eleifend purus.</span></p>", blog.Content);
			Assert.AreEqual(new DateTime(2018, 9, 1), blog.PostDate);
			Assert.AreEqual(new DateTime(2018, 11, 1), blog.RemoveDate);
			Assert.AreEqual(true, blog.IsApproved);
			Assert.AreEqual("These are sample notes that would be input from the admin", blog.BlogNotes);
			Assert.AreEqual("TestBlogWriter@test.com", blog.BlogWriter.UserName);
		}

		[Test]
		public void CanAddContextBlog()
		{
			//TheCodingVineDbContext repo = new TheCodingVineDbContext();
			UserManager<AppUser> userMgr = new UserManager<AppUser>(new UserStore<AppUser>(TestContext));
			RoleManager<AppRole> roleMgr = new RoleManager<AppRole>(new RoleStore<AppRole>(TestContext));

			BlogPost blogToAdd = new BlogPost
			{
				Title = "Test Blog 6",
				Content = "This is also a test post.This text represents the post's main body.",
				PostDate = new DateTime(2018, 8, 30),
				RemoveDate = new DateTime(2018, 10, 30),
				IsApproved = false,
				BlogNotes = "Notes about why blog was not approved",
				BlogWriter = userMgr.FindByName("TestAdmin@test.com")
			};

			TestContext.AddBlog(blogToAdd);

			var blogRoll = TestContext.GetAllBlogs();

			Assert.AreEqual(6, blogRoll.Count());

			var savedBlog = TestContext.GetBlog(6);

			Assert.AreEqual("Test Blog 6", savedBlog.Title);
			Assert.AreEqual("This is also a test post.This text represents the post's main body.", savedBlog.Content);
			Assert.AreEqual(new DateTime(2018, 8, 30), savedBlog.PostDate);
			Assert.AreEqual(new DateTime(2018, 10, 30), savedBlog.RemoveDate);
			Assert.AreEqual(false, savedBlog.IsApproved);
			Assert.AreEqual("Notes about why blog was not approved", savedBlog.BlogNotes);

		}

		[Test]
		public void CanRemoveContextBlog()
		{
			TestContext.DeleteBlog(6);
			var blog = TestContext.GetBlog(6);
			Assert.IsNull(blog);
		}

		[Test]
		public void CanUpdateContextBlog()
		{
			var blog = TestContext.GetBlog(1);

			blog.Title = "Revise Test Blog 1";
			blog.Content = "Revise Test Blog 1";
			blog.PostDate = new DateTime(2018, 8, 30);
			blog.RemoveDate = new DateTime(2018, 10, 30);
			blog.IsApproved = true;
			blog.BlogNotes = "This post is now approved";

			TestContext.UpdateBlog(blog);

			var updatedBlog = new BlogPost();
			updatedBlog = TestContext.GetBlog(1);

			Assert.AreEqual("Revise Test Blog 1", updatedBlog.Title);
			Assert.AreEqual("Revise Test Blog 1", updatedBlog.Content);
			Assert.AreEqual(new DateTime(2018, 8, 30), updatedBlog.PostDate);
			Assert.AreEqual(new DateTime(2018, 10, 30), updatedBlog.RemoveDate);
			Assert.AreEqual(true, updatedBlog.IsApproved);
			Assert.AreEqual("This post is now approved", updatedBlog.BlogNotes);
		}

		[Test]
		public void CanLoadAContextStaticPost()
		{
			var post = new StaticPost();

			post = TestContext.GetStaticPost(1);

			Assert.AreEqual("Test Static Post", post.Title);
			Assert.AreEqual(" <h3>Bacon!</h3> <p><span style=\"color: #333333; font-family: Georgia, 'Bitstream Charter', serif;" +
					" font-size: 16px;\">Spicy jalapeno bacon ipsum dolor amet filet mignon shankle ground round pig corned beef tail jowl." +
					" Pastrami chuck kielbasa landjaeger beef venison sirloin biltong ham andouille. Leberkas tenderloin meatloaf landjaeger" +
					" pork belly. Filet mignon salami ground round, ball tip shoulder kielbasa pancetta bacon biltong prosciutto turducken " +
					"cupim leberkas jowl. Sirloin porchetta pastrami, pork loin cow ribeye tail burgdoggen flank frankfurter capicola tri-tip. " +
					"Biltong jerky swine tongue andouille ham hock.</span></p>", post.Content);
		}

		[Test]
		public void CanGetBlogsBySearchingTags()
		{

		}

		[Test]
		public void CanAddStaticPostAndGetAllPosts()
		{
			var post = new StaticPost()
			{
				Title = "Second Static Post",
				Content = "<h3>Static Post Content.</h3><p>Not much to see here, this is just a test post that will be deleted anyway.</p>"
			};

			TestContext.AddStaticPost(post);

			var postRoll = TestContext.GetAllStaticPosts();

			Assert.AreEqual(2, postRoll.Count());

			var savedPost = TestContext.GetStaticPost(2);

			Assert.AreEqual("Second Static Post", savedPost.Title);
			Assert.AreEqual("<h3>Static Post Content.</h3><p>Not much to see here, this is just a test post that will be deleted anyway.</p>", savedPost.Content);
		}

		[Test]
		public void CanGetStaticLinkList()
		{
			var staticLinks = TestContext.GetStaticLinks().ToList(); ;

			Assert.AreEqual(2, staticLinks.Count());

			Assert.AreEqual("Test Static Post", staticLinks[0].StaticPageTitle);
			Assert.AreEqual("Second Static Post", staticLinks[1].StaticPageTitle);
		}

		[Test]
		public void CanUpdateStaticPost()
		{
			var post = TestContext.GetStaticPost(2);

			post.Title = "Second Test Static Post";
			post.Content = "<h3>Static Post Content.</h3><p>Not much to see here, this is just a test post that will be deleted anyway.</p><p>Forgot to add the word test to the title</p>";

			TestContext.UpdateStaticPost(post);

			var savedPost = TestContext.GetStaticPost(2);

			Assert.AreEqual("Second Test Static Post", savedPost.Title);
			Assert.AreEqual("<h3>Static Post Content.</h3><p>Not much to see here, this is just a test post that will be deleted anyway.</p><p>Forgot to add the word test to the title</p>", savedPost.Content);
		}

		[Test]
		public void CanDeleteStaticPost()
		{
			TestContext.DeleteStaticPost(2);

			var post = TestContext.GetStaticPost(2);
			Assert.IsNull(post);
		}

		[Test]
		public void CanGetAllUsers()
		{
			var userList = TestContext.GetAllUsers();

			Assert.AreEqual(2, userList.Count());
		}

		[Test]
		public void CanAddUser()
		{

		}






	}
}

