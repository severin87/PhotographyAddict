using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.Models.ThemesComments
{
    public class UpdateThemeCommentViewModel
    {
        public int Id { get; set; }

        public string UserOpinion { get; set; }

        public int? NewId { get; set; }
        //public virtual Theme Theme { get; set; }

        public string PhotographyAddictedUserId { get; set; }
        //public virtual PhotographyAddictedUser PhotographyAddictedUser { get; set; }
    }
}
