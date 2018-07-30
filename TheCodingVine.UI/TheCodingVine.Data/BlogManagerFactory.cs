using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCodingVine.Model;

namespace TheCodingVine.Data
{
    public class BlogManagerFactory
    {
        public static BlogManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch(mode)
            {
                case "InMemoryRepo":
                    return new BlogManager(new InMemoryRepo());
                case "DbContext":
                    TheCodingVineDbContext entityRepo = new TheCodingVineDbContext();
                    return new BlogManager(entityRepo);
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
