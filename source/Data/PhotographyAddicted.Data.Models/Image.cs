﻿using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Data.Models
{
    public class Image
    {
        public Image()
        {
            ImageComments = new HashSet<ImageComment>();
            FavouriteImages = new HashSet<FavouriteImage>();
        }

        public int Id { get; set; }

        public DateTime UploadedDate { get; set; }

        public byte[] Picture { get; set; }

        public string Title { get; set; }

        public string Equipment { get; set; }

        public string Settings { get; set; }

        public int Scores { get; set; }

        public ImageCategory ImageCategory { get; set; }

        public string Description { get; set; }

        public string PhotographyAddictedUserId { get; set; }
        public virtual PhotographyAddictedUser PhotographyAddictedUser { get; set; }

        public virtual ICollection<FavouriteImage> FavouriteImages { get; set; }

        public virtual ICollection<ImageComment> ImageComments { get; set; }
    }
}
