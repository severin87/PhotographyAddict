using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Data.Models
{
    public class Image
    {
        public Image()
        {
            ImageCommnets = new HashSet<ImageComment>();
        }

        public int Id { get; set; }

        public DateTime UploadedDate { get; set; }

        public byte[] Picture { get; set; }

        public string Title { get; set; }

        public int Scores { get; set; }

        public ImageCategory ImageCategory { get; set; }

        public string Description { get; set; }

        public string PhotographyAddictedUserId { get; set; }
        public virtual PhotographyAddictedUser PhotographyAddictedUser { get; set; }

        public virtual ICollection<ImageComment> ImageCommnets { get; set; }
    }
}
