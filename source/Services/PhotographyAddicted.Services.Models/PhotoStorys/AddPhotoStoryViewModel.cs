using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotographyAddicted.Services.Models.PhotoStorys
{
    public class AddPhotoStoryViewModel
    {
        public DateTime UploadedDate { get; set; }

        [Required]
        public string Title { get; set; }

        public string Author { get; set; }

        public string Introduction { get; set; }

        public string Conclusion { get; set; }

        public bool Published { get; set; } = false;

        public string PhotographyAddictedUserId { get; set; }
        public virtual PhotographyAddictedUser PhotographyAddictedUser { get; set; }
    }
}
