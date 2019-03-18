using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.Models.Images
{
    public class AddImageViewModel
    {
        public byte[] Picture { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public string PhotographyAddictedUserId { get; set; }

        public virtual PhotographyAddictedUser PhotographyAddictedUser { get; set; }
    }
}
