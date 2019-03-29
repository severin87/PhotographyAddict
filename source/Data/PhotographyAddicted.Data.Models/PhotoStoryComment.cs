using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Data.Models
{
    public class PhotoStoryComment
    {
        public int Id { get; set; }

        public string UserOpinion { get; set; }

        public DateTime CreationDate { get; set; }

        public int? PhotoStoryId { get; set; }
        public virtual PhotoStory PhotoStory { get; set; }

        public string PhotographyAddictedUserId { get; set; }
        public virtual PhotographyAddictedUser PhotographyAddictedUser { get; set; }
    }
}
