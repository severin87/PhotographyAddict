
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.Models.Themes
{
    public class ThemeDetailsViewModel 
    {      

        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorOpinion { get; set; }

        public string UserName { get; set; }

        public ThemeCategory ThemeCategory { get; set; }

        public string PhotographyAddictedUserId { get; set; }
        public virtual PhotographyAddictedUser PhotographyAddictedUser { get; set; }

        public virtual ICollection<ThemeComment> ThemeComments { get; set; }

    }
}
