using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TheCodingVine.Model.Tables;
using TheCodingVine.Data;

namespace TheCodingVine.Model
{
	public class TheCodingVineEntitiesContext : DbContext, IRepo
	{
		public TheCodingVineEntitiesContext() : base("TheCodingVine")
		{
		}

		public DbSet<BlogPost> BlogPosts { get; set; }
		public DbSet<SearchTag> SearchTags { get; set; }
		public DbSet<StaticPage> StaticPages { get; set; }

		public void AddBlog(BlogPost toAdd)
		{
			var checkForExistingPost = GetBlog(toAdd.BlogPostId);

			if (checkForExistingPost != null)
			{
				throw new Exception("Blog Id already exists");
			}
			else
			{
				toAdd.BlogPostId = BlogPosts.Max(m => m.BlogPostId) + 1;
				BlogPosts.Add(toAdd);
			}

		}

		public void AddStaticPage(StaticPage pageToAdd)
		{
			var checkForExistingPage = GetStaticPage(pageToAdd.StaticPageId);

			if (checkForExistingPage != null)
			{
				throw new Exception("Static Page Id already exists");
			}
			else
			{
				pageToAdd.StaticPageId = StaticPages.Max(m => m.StaticPageId) + 1;
				StaticPages.Add(pageToAdd);
			}
		}

		public void DeleteBlog(int id)
		{
			var toRemove = GetBlog(id);

			if (toRemove == null)
			{
				throw new Exception();
			}
			BlogPosts.Remove(toRemove);
		}

		public void DeleteStaticPage(int id)
		{
			var toRemove = GetStaticPage(id);

			if (toRemove == null)
			{
				throw new Exception();
			}
			StaticPages.Remove(toRemove);
		}

		public IEnumerable<BlogPost> GetAllBlogs()
		{
			return BlogPosts.ToList().OrderBy(b => b.PostDate);
		}

		public IEnumerable<StaticPage> GetAllStaticPages()
		{
			return StaticPages.ToList().OrderBy(p => p.Title);
		}

		public BlogPost GetBlog(int id)
		{
			return BlogPosts.SingleOrDefault(p => p.BlogPostId == id);
		}

		public StaticPage GetStaticPage(int id)
		{
			return StaticPages.SingleOrDefault(p => p.StaticPageId == id);
		}

		public void UpdateBlog(BlogPost toUpdate)
		{
			var toReplace = GetBlog(toUpdate.BlogPostId);
			if (toReplace == null)
			{
				throw new Exception("Blog ID does not exist");
			}
			BlogPosts.Remove(toReplace);
			BlogPosts.Add(toUpdate);
		}

		public void UpdateStaticPage(StaticPage pageToUpdate)
		{
			var toReplace = GetStaticPage(pageToUpdate.StaticPageId);
			if (toReplace == null)
			{
				throw new Exception("Static Page ID does not exist");
			}
			StaticPages.Remove(toReplace);
			StaticPages.Add(pageToUpdate);
		}
	}
}
