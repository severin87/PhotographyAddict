﻿using PhotographyAddicted.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.Models.Users
{
    public class PreviewUserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string SelfDescription { get; set; }

        public string Technique { get; set; }            

        public byte[] ProfilePicture { get; set; }

        public DateTime CreationDate { get; set; }
                    

        public DateTime LastLogin { get; set; }

        public int ImageCount { get; set; }

        public int AverageScore { get; set; }

        public bool IsBanned { get; set; }

        public DateTime BannedDate { get; set; }

        public int BanLengthDays { get; set; }

        public string Rang { get; set; }
               
        public virtual ICollection<Image> Images { get; set; }
    }
}
