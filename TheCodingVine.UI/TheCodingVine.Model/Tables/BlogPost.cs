using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TheCodingVine.Model.Attributes;
using TheCodingVine.Model.Identities;

namespace TheCodingVine.Model.Tables
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }

        [Required(ErrorMessage = "Please enter a title")]
        public string Title { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Please type in something in the body paragraph.")]
        public string Content { get; set; }

        [ValidDate(ErrorMessage = "Please type in a valid date")]
        public DateTime? PostDate { get; set; }

        [ValidDateTime(ErrorMessage = "Please type in a valid date")]
        public DateTime? RemoveDate { get; set; }

		public bool IsApproved { get; set; }
		public string BlogNotes { get; set; }

		public string TagInputs { get; set; }
		public virtual ICollection<SearchTag> SearchTags { get; set; }

		public virtual AppUser BlogWriter { get; set; }

        public BlogPost()
        {
            SearchTags = new HashSet<SearchTag>();
        }
    }
}
