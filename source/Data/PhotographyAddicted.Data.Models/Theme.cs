using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotographyAddicted.Data.Models
{
    public class Theme
    {

        public Theme()
        {
            ThemeComments = new HashSet<ThemeComment>();
        }
    
        public int Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string AuthorOpinion { get; set; }

        public string PhotographyAddictedUserId { get; set; }
        public virtual PhotographyAddictedUser PhotographyAddictedUser { get; set; }

        public int ComentsCount { get; set; }

        public virtual ICollection<ThemeComment> ThemeComments { get; set; }

    }
}
