using System;
using System.Collections.Generic;
using System.Linq;
using TheCodingVine.Model.Tables;
using TheCodingVine.Data;
using TheCodingVine.Model.Identities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TheCodingVine.Model.Queries;
using Microsoft.AspNet.Identity;

namespace TheCodingVine.Model
{
    public class TheCodingVineDbContext : IdentityDbContext<AppUser>, IRepo
	{
		public TheCodingVineDbContext() : base("TheCodingVine")
		{
		}

		public TheCodingVineDbContext(string testConnection) : base(testConnection)
		{
		
		}

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<SearchTag> SearchTags { get; set; }
        public DbSet<StaticPost> StaticPosts { get; set; }

		public TheCodingVineDbContext Create()
		{
			return new TheCodingVineDbContext();
		}

		public IEnumerable<BlogPost> GetAllBlogs()
		{
			return BlogPosts.ToList();
		}

		public IEnumerable<BlogPost> GetAllPending()
		{
			return BlogPosts.Where(b => b.IsApproved == false).ToList();
		}

		public BlogPost GetBlog(int id)
		{
			return BlogPosts.SingleOrDefault(p => p.BlogPostId == id);
		}

		public void AddBlog(BlogPost toAdd)
        {
			toAdd.BlogWriter = Users.Single(u => u.Id == toAdd.BlogWriter.Id);
			BlogPosts.Add(toAdd);

			SaveChanges();
		}

		public void UpdateBlog(BlogPost toUpdate)
		{
			BlogPost existing = GetBlog(toUpdate.BlogPostId);
            existing.Title = toUpdate.Title;
            existing.Content = toUpdate.Content;
            existing.PostDate = toUpdate.PostDate;
            existing.RemoveDate = toUpdate.RemoveDate;
			int tagCount = existing.SearchTags.Count();
			for (int i = tagCount - 1; i >= 0; i--)
			{
				existing.SearchTags.Remove(existing.SearchTags.ToList()[i]);
			}
            existing.SearchTags = toUpdate.SearchTags;
			existing.BlogNotes = toUpdate.BlogNotes;
			existing.IsApproved = toUpdate.IsApproved;

            SaveChanges();
		}

		public void DeleteBlog(int id)
		{
			BlogPost toRemove = GetBlog(id);
			BlogPosts.Remove(toRemove);
            SaveChanges();   
		}

		public void DeleteStaticPost(int id)
        {
			StaticPost toDelete = GetStaticPost(id);
			StaticPosts.Remove(toDelete);
            SaveChanges();
        }

		public IEnumerable<StaticPost> GetAllStaticPosts()
		{
			return StaticPosts.ToList();
		}

		public StaticPost GetStaticPost(int id)
		{
			return StaticPosts.SingleOrDefault(p => p.StaticPostId == id);
		}

		public void AddStaticPost(StaticPost postToAdd)
        {
			StaticPosts.Add(postToAdd);
			SaveChanges();
        }

		public void UpdateStaticPost(StaticPost postToUpdate)
		{
			StaticPost existing = GetStaticPost(postToUpdate.StaticPostId);
			existing.Title = postToUpdate.Title;
			existing.Content = postToUpdate.Content;

			SaveChanges();
		}


		public IEnumerable<SiteStaticLink> GetStaticLinks()
		{
			var staticPosts = GetAllStaticPosts().ToList();
			List<SiteStaticLink> linkList = new List<SiteStaticLink>();
			foreach(var post in staticPosts)
			{
				var link = new SiteStaticLink();
				link.StaticPageTitle = post.Title;
				link.StaticPageId = post.StaticPostId;
				linkList.Add(link);
			}

			return linkList;
		}

        public IEnumerable<AppUser> GetAllUsers()
        {

            var users = Users;
            return users;
        }

        public void DeleteUser(string toDelete)
        {
            var user = Users.Where(u => u.UserName == toDelete).FirstOrDefault();
            Users.Remove(user);
            this.SaveChanges();
        }

		public IEnumerable<BlogPost> GetSearchResults(string searchTag)
		{
			var searchResults = BlogPosts.Where(b => b.SearchTags.Any(t => t.SearchTagBody == searchTag)).ToList();

			return searchResults;
		}

        public AppUser GetUser(string id)
        {
            return Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public void UpdateUser(AppUser toUpdate)
        {
            AppUser existing = GetUser(toUpdate.Email);
            //existing.Email = toUpdate.Email;
            existing.UserName = toUpdate.UserName;
            existing.PasswordHash = toUpdate.PasswordHash;

            this.SaveChanges();
        }

		//public IEnumerable<AppRole> GetRoles()
		//{
		//	var roles = Roles;
		//	return roles;
		//}

		//public AppRole GetRole(string id)
		//{
		//	AppRole role = Roles.Where(i => i.Id == id).FirstOrDefault();
		//	return role;
		//}
	}
}
