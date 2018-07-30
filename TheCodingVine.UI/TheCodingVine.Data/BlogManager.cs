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
    public class BlogManager
    {
        private IRepo _repo;

        public BlogManager(IRepo blogRepo)
        {
            _repo = blogRepo;
        }

        public IEnumerable<BlogPost> GetAllBlogs()
        {
            return _repo.GetAllBlogs();
        }

		public IEnumerable<BlogPost> GetAllPending()
		{
			return _repo.GetAllPending();
		}

        public IEnumerable<SiteStaticLink> GetSiteLinks()
		{
			return  _repo.GetStaticLinks();
		}

        public BlogPost GetBlog(int b)
        {
            return _repo.GetBlog(b);
        }

        public void AddBlog(BlogPost postToAdd)
        {
            _repo.AddBlog(postToAdd);
        }

        public void DeleteBlog(int id)
        {
			_repo.DeleteBlog(id);
        }

        public void UpdateBlog(BlogPost postToUpdate)
        {
			_repo.UpdateBlog(postToUpdate);
        }

		//public IEnumerable<AppRole> GetAllRoles()
		//{
		//	return _repo.GetRoles();
		//}

		//public AppRole GetRoleById(string id)
		//{
		//	return _repo.GetRole(id);
		//}

		public IEnumerable<StaticPost> GetAllStaticPosts()
		{
			return _repo.GetAllStaticPosts();
		}

		public void AddPost(StaticPost sPost)
		{
			_repo.AddStaticPost(sPost);
		}

		public StaticPost GetStaticPost(int id)
		{
			return _repo.GetStaticPost(id);
		}

		public void DeleteStaticPost(int staticPostId)
		{
			_repo.DeleteStaticPost(staticPostId);
		}

		public void UpdateStaticPost(StaticPost postToEdit)
		{
			_repo.UpdateStaticPost(postToEdit);
		}

        public IEnumerable<AppUser> GetAllUsers()
        {

            return _repo.GetAllUsers();
        }

        public AppUser GetUserById(string id)
        {
            return _repo.GetUser(id);
        }

        public void DeleteBlogger(string toDelete)
        {
            _repo.DeleteUser(toDelete);
        }

        public void UpdateBlogger(AppUser toUpdate)
        {
            _repo.UpdateUser(toUpdate);
        }

        public IEnumerable<BlogPost> GetSearchResults(string searchTag)
		{
			return _repo.GetSearchResults(searchTag);
		}
    }
}
