using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotographyAddicted.Services.Models.Images
{
    public class AddImageViewModel
    {
        [Required]
        public byte[] Picture { get; set; }

        public string Title { get; set; }

        public ImageCategory ImageCategory { get; set; }

        public string Description { get; set; }

        public DateTime UploadedDate { get; set; }

        public string PhotographyAddictedUserId { get; set; }

        public virtual PhotographyAddictedUser PhotographyAddictedUser { get; set; }
        
    }
}
