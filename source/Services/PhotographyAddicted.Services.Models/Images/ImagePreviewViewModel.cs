using PhotographyAddicted.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.Models.Images
{
    public class ImagePreviewViewModel
    {

        public byte[] Picture { get; set; }

        public string Title { get; set; }

        public ImageCategory ImageCategory { get; set; }

        public int Scores { get; set; }

        public string Description { get; set; }

        public string PhotographyAddictedUserId { get; set; }

    }
}
