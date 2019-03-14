using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Data.Models
{
    public class Theme
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorOpinion { get; set; }

        public string PhotographyAddictedUserId { get; set; }

        public virtual PhotographyAddictedUser PhotographyAddictedUser { get; set; }

    }
}
