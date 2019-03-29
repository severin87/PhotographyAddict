using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Data.Models
{
    public class PhotoStory
    {
        public PhotoStory()
        {
            PhotoStoryComments = new HashSet<PhotoStoryComment>();
            PhotoStoryFragments = new HashSet<PhotoStoryFragment>();
        }

        public int Id { get; set; }

        public DateTime UploadedDate { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Introduction { get; set; }

        public string Conclusion { get; set; }

        public bool Published { get; set; }

        public string PhotographyAddictedUserId { get; set; }
        public virtual PhotographyAddictedUser PhotographyAddictedUser { get; set; }

        public virtual ICollection<PhotoStoryFragment> PhotoStoryFragments { get; set; }

        public virtual ICollection<PhotoStoryComment> PhotoStoryComments { get; set; }
    }
}
