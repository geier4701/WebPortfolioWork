using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCodingVine.Model.Identities;
using TheCodingVine.Model.Queries;
using TheCodingVine.Model.Tables;

namespace TheCodingVine.Data
{
    public class InMemoryRepo : IRepo
    {
        static List<BlogPost> _blogRoll = new List<BlogPost>
        {
            new BlogPost
            {
                BlogPostId = 1,
                Title = "The First Blog Test Title",
                Content = "This is the blog body. I am going to keep typing until it is now a really big body",
                PostDate = new DateTime(2018, 07, 01),
                RemoveDate = new DateTime (2018, 09, 01),
                IsApproved = true,
                BlogNotes = "This blog rocks!! A note from admin.",
                BlogWriter = new AppUser()
                {
                    UserName = "TestAdmin@test.com"
                },
            },
            new BlogPost
            {
                BlogPostId = 2,
                Title = "The 2nd Blog Test Title",
                Content = "This is the blog body. I am going to keep typing until it is now a really big body",
                PostDate = new DateTime(2017, 01, 01),
                RemoveDate = new DateTime (2017, 05, 01),
                IsApproved = false,
                BlogNotes = "This blog sucks!! A note from admin.",
                BlogWriter = new AppUser()
                {
                    UserName = "TestBlogWriterOne@test.com"
                },
            },
            new BlogPost
            {
                BlogPostId = 3,
                Title = "The Third Blog Test Title",
                Content = "This is the blog body. I am going to keep typing until it is now a really big body",
                PostDate = new DateTime(2015, 12, 01),
                RemoveDate = new DateTime (2016, 01, 01),
                IsApproved = true,
                BlogNotes = "This blog TOTALLY rocks!! A note from admin.",
                BlogWriter = new AppUser()
                {
                    UserName = "TestBlogWriterTwo@test.com"
                },
            }
        };

        public List<StaticPost> _staticPosts = new List<StaticPost>
        {
            new StaticPost
            {
                StaticPostId = 1,
                Title = "Test Static Page",
                Content = " <!DOCTYPE html><html><head><meta charset = 'UTF-8'><title>Sample Static Title</title></head><body>This is the body of a sample static page.</body></html>"
            },
            new StaticPost
            {
                StaticPostId = 2,
                Title = "About Us",
                Content = " <!DOCTYPE html><html><head><meta charset = 'UTF-8'><title>About Us</title></head><body>Here are some facts about us!</body></html>"
            },
            new StaticPost
            {
                StaticPostId = 3,
                Title = "Contacts",
                Content = " <!DOCTYPE html><html><head><meta charset = 'UTF-8'><title>Contacts</title></head><body>Here's how to get ahold of us.</body></html>"
            },

        };

        public void AddBlog(BlogPost toAdd)
        {
            var checkForExistingPost = GetBlog(toAdd.BlogPostId);

            if (checkForExistingPost != null)
            {
                throw new Exception("Blog Id already exists");
            }
            else
            {
                toAdd.BlogPostId = _blogRoll.Max(m => m.BlogPostId) + 1;
                _blogRoll.Add(toAdd);
            }
        }

		public void AddStaticPost(StaticPost pageToAdd)
		{
			var checkForExistingPage = GetStaticPost(pageToAdd.StaticPostId);

			if (checkForExistingPage != null)
			{
				throw new Exception("Static Page Id already exists");
			}
			else
			{
				pageToAdd.StaticPostId = _staticPosts.Max(m => m.StaticPostId) + 1;
				_staticPosts.Add(pageToAdd);
			}
		}

		public void DeleteBlog(int id)
        {
            var toRemove = GetBlog(id);

            if (toRemove == null)
            {
                throw new Exception();
            }
            _blogRoll.Remove(toRemove);
        }

        public void DeleteUser(string toDelete)
        {
            throw new NotImplementedException();
        }

        public void DeleteStaticPost(int id)
        {
            var toRemove = GetStaticPost(id);

            if (toRemove == null)
            {
                throw new Exception();
            }
            _staticPosts.Remove(toRemove);
        }

        public IEnumerable<AppUser> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BlogPost> GetAllBlogs()
        {
            return _blogRoll.OrderBy(b => b.PostDate);
        }

		public IEnumerable<BlogPost> GetAllPending()
		{
			return _blogRoll.Where(b => b.IsApproved == false).ToList();
		}

		public IEnumerable<StaticPost> GetAllStaticPosts()
        {
            return _staticPosts.OrderBy(s => s.Title);
        }

        public BlogPost GetBlog(int id)
        {
            return _blogRoll.SingleOrDefault(b => b.BlogPostId == id);
        }

        public AppUser GetUser(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BlogPost> GetOwnBlogs(AppUser user)
		{
            return _blogRoll.ToList();
		}

		public AppRole GetRole(string id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<AppRole> GetRoles()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<BlogPost> GetSearchResults(string searchTag)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<SiteStaticLink> GetStaticLinks()
		{
			throw new NotImplementedException();
		}

		public StaticPost GetStaticPost(int id)
        {
            return _staticPosts.SingleOrDefault(s => s.StaticPostId == id);
        }

        public void UpdateBlog(BlogPost toUpdate)
        {
            var toReplace = GetBlog(toUpdate.BlogPostId);
            if (toReplace == null)
            {
                throw new Exception("Blog ID does not exist");
            }
            _blogRoll.Remove(toReplace);
            _blogRoll.Add(toUpdate);

        }

        public void UpdateUser(AppUser toUpdate)
        {
            throw new NotImplementedException();
        }

        public void UpdateStaticPost(StaticPost pageToUpdate)
        {
            var toReplace = GetStaticPost(pageToUpdate.StaticPostId);
            if (toReplace == null)
            {
                throw new Exception("Static Page ID does not exist");
            }
            _staticPosts.Remove(toReplace);
            _staticPosts.Add(pageToUpdate);
        }
    }
}
