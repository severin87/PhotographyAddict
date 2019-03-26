using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.Models.ImagesComments
{
    public class AddImageCommentViewModel
    {
        public int Id { get; set; }

        public string UserOpinion { get; set; }

        public DateTime CreationDate { get; set; }

        public int? ImageId { get; set; }
        //public virtual Image Image { get; set; }

        public string PhotographyAddictedUserId { get; set; }
        //public virtual PhotographyAddictedUser PhotographyAddictedUser { get; set; }
    }
}
