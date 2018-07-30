using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCodingVine.Model;
using TheCodingVine.Model.Identities;
using TheCodingVine.Model.Tables;

namespace TheCodingVine.Tests
{
	internal sealed class DropCreateDatabaseAlwaysAndSeed : DropCreateDatabaseAlways<TheCodingVineDbContext>
	{

		public override void InitializeDatabase(TheCodingVineDbContext context)
		{
			if(context.Database.Exists())
			{
				context.Database.ExecuteSqlCommand(
					TransactionalBehavior.DoNotEnsureTransaction,
					$"ALTER DATABASE [{context.Database.Connection.Database}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
				//SqlConnection.ClearAllPools();
				//context.Database.Delete();

			}
			//context.Database.Create();
			base.InitializeDatabase(context);
		}

		protected override void Seed(TheCodingVineDbContext context)
		{
			context.BlogPosts.RemoveRange(context.BlogPosts);
			context.SaveChanges();


			UserManager<AppUser> userMgr = new UserManager<AppUser>(new UserStore<AppUser>(context));
			RoleManager<Model.Identities.AppRole> roleMgr = new RoleManager<AppRole>(new RoleStore<AppRole>(context));

			if (!roleMgr.RoleExists("Admin"))
			{
				roleMgr.Create(new AppRole() { Name = "Admin" });
				AppUser TestAdmin = new AppUser() { UserName = "TestAdmin@test.com" };
				var result = userMgr.Create(TestAdmin, "test123");
				if (!result.Succeeded)
				{
					//something went wrong on adding a user
				}
				userMgr.AddToRole(TestAdmin.Id, "Admin");
			}


			if (!roleMgr.RoleExists("BlogWriter"))
			{
				roleMgr.Create(new AppRole() { Name = "BlogWriter" });
				AppUser TestBlogWriter = new AppUser() { UserName = "TestBlogWriter@test.com" };
				var result = userMgr.Create(TestBlogWriter, "test123");
				if (!result.Succeeded)
				{
					//something went wrong on adding a user
				}
				userMgr.AddToRole(TestBlogWriter.Id, "BlogWriter");
			}

			// blog 1
			context.BlogPosts.AddOrUpdate(
				b => b.Title,
				new BlogPost
				{
					Title = "Test Blog",
					Content = "<h3>Section One</h3> <p><span style=\"font-family: 'Open Sans', Arial, sans-serif;" +
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
					  " mi et, sollicitudin eleifend purus.</span></p>",
					PostDate = new DateTime(2018, 9, 1),

					RemoveDate = new DateTime(2018, 11, 1),
					IsApproved = true,
					BlogNotes = "These are sample notes that would be input from the admin",
					BlogWriter = userMgr.FindByName("TestBlogWriter@test.com")
				}
				);
			// blog 2
			context.BlogPosts.AddOrUpdate(
				b => b.Title,
				new BlogPost
				{
					Title = "A Fishy Writer's Blog",
					Content = "<h3>Section One</h3> <p><span style=\"font-family: 'Open Sans', Arial, sans-serif;" +
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
					  " mi et, sollicitudin eleifend purus.</span></p>",
					PostDate = DateTime.Now,

					RemoveDate = DateTime.Now.AddDays(60),
					IsApproved = false,
					BlogWriter = userMgr.FindByName("TestBlogWriter@test.com")
				}
				);
			// blog 3
			context.BlogPosts.AddOrUpdate(
				b => b.Title,
				new BlogPost
				{
					Title = "The Fishiest Writer's Blog",
					Content = "<h3>Section One</h3> <p><span style=\"font-family: 'Open Sans', Arial, sans-serif;" +
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
					  " mi et, sollicitudin eleifend purus.</span></p>",
					PostDate = DateTime.Now,

					RemoveDate = DateTime.Now.AddDays(15),
					IsApproved = true,
					BlogNotes = "Some fishy admin notes",
					BlogWriter = userMgr.FindByName("TestBlogWriter@test.com")
				}
				);
			// blog 4
			context.BlogPosts.AddOrUpdate(
				b => b.Title,
				new BlogPost
				{
					Title = "Cod Based Admin Blog",
					Content = "<h3>Section One</h3> <p><span style=\"font-family: 'Open Sans', Arial, sans-serif;" +
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
					  " mi et, sollicitudin eleifend purus.</span></p>",
					PostDate = DateTime.Now,

					RemoveDate = DateTime.Now.AddDays(30),
					IsApproved = true,
					BlogNotes = "I can note my own blog!",
					BlogWriter = userMgr.FindByName("TestAdmin@test.com")
				}
				);
			// blog 5
			context.BlogPosts.AddOrUpdate(
				b => b.Title,
				new BlogPost
				{
					Title = "Expired Cod Blog",
					Content = "<h3>Section One</h3> <p><span style=\"font-family: 'Open Sans', Arial, sans-serif;" +
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
					  " mi et, sollicitudin eleifend purus.</span></p>",
					PostDate = DateTime.Now,

					RemoveDate = DateTime.Now.AddDays(30),
					IsApproved = true,
					BlogWriter = userMgr.FindByName("TestAdmin@test.com")
				}
				);

			context.SearchTags.AddOrUpdate(
				t => t.SearchTagBody,
				new SearchTag { SearchTagBody = "TestTag" }
				);

			context.SearchTags.AddOrUpdate(
				t => t.SearchTagBody,
				new SearchTag { SearchTagBody = "Fishing" }
				);

			context.SearchTags.AddOrUpdate(
				t => t.SearchTagBody,
				new SearchTag { SearchTagBody = "CodIsBest" }
				);

			context.SearchTags.AddOrUpdate(
				t => t.SearchTagBody,
				new SearchTag { SearchTagBody = "LoremIpsum" }
				);

			context.SearchTags.AddOrUpdate(
				t => t.SearchTagBody,
				new SearchTag { SearchTagBody = "OldCod" }
				);

			context.StaticPosts.AddOrUpdate(
				s => s.Title,
				new StaticPost
				{
					Title = "Test Static Post",
					Content = " <h3>Bacon!</h3> <p><span style=\"color: #333333; font-family: Georgia, 'Bitstream Charter', serif;" +
					" font-size: 16px;\">Spicy jalapeno bacon ipsum dolor amet filet mignon shankle ground round pig corned beef tail jowl." +
					" Pastrami chuck kielbasa landjaeger beef venison sirloin biltong ham andouille. Leberkas tenderloin meatloaf landjaeger" +
					" pork belly. Filet mignon salami ground round, ball tip shoulder kielbasa pancetta bacon biltong prosciutto turducken " +
					"cupim leberkas jowl. Sirloin porchetta pastrami, pork loin cow ribeye tail burgdoggen flank frankfurter capicola tri-tip. " +
					"Biltong jerky swine tongue andouille ham hock.</span></p>"
				}
				);


			context.SaveChanges();

			SearchTag testTag = context.SearchTags.Single(t => t.SearchTagBody == "TestTag");
			BlogPost testBlog = context.BlogPosts.Single(b => b.Title == "Test Blog");
			testBlog.SearchTags.Add(testTag);
			testTag.BlogPosts.Add(testBlog);

			SearchTag testTag2 = context.SearchTags.Single(t => t.SearchTagBody == "Fishing");
			BlogPost testBlog2 = context.BlogPosts.Single(b => b.Title == "A Fishy Writer's Blog");
			testBlog2.SearchTags.Add(testTag2);
			testTag2.BlogPosts.Add(testBlog2);

			SearchTag testTag3 = context.SearchTags.Single(t => t.SearchTagBody == "CodIsBest");
			BlogPost testBlog3 = context.BlogPosts.Single(b => b.Title == "The Fishiest Writer's Blog");
			testBlog3.SearchTags.Add(testTag3);
			testBlog3.SearchTags.Add(testTag2);
			testTag3.BlogPosts.Add(testBlog3);

			SearchTag testTag4 = context.SearchTags.Single(t => t.SearchTagBody == "LoremIpsum");
			BlogPost testBlog4 = context.BlogPosts.Single(b => b.Title == "Cod Based Admin Blog");
			testBlog4.SearchTags.Add(testTag4);
			testTag4.BlogPosts.Add(testBlog4);

			SearchTag testTag5 = context.SearchTags.Single(t => t.SearchTagBody == "OldCod");
			BlogPost testBlog5 = context.BlogPosts.Single(b => b.Title == "Expired Cod Blog");
			testBlog5.SearchTags.Add(testTag5);
			testTag5.BlogPosts.Add(testBlog5);

			context.Entry(testBlog).State = EntityState.Modified;
			context.Entry(testTag).State = EntityState.Modified;

			context.SaveChanges();
		}
	}
}
