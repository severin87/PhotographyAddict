using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Data.Models
{
    public class ThemeComment
    {
        public int Id { get; set; }

        public string UserOpinion { get; set; }

        public int? ThemeId { get; set; }
        public virtual Theme Theme { get; set; }

        public string PhotographyAddictedUserId { get; set; }
        public virtual PhotographyAddictedUser PhotographyAddictedUser { get; set; }

    }
}
