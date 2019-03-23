using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Data.Models
{
    public class NewComment
    {
        public int Id { get; set; }

        public string UserOpinion { get; set; }

        public DateTime CreationDate { get; set; }

        public int? NewId { get; set; }
        public virtual New New { get; set; }

        public string PhotographyAddictedUserId { get; set; }
        public virtual PhotographyAddictedUser PhotographyAddictedUser { get; set; }
    }
}
