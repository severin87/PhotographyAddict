using PhotographyAddicted.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.Models.Users
{
    public class PreviewUsersViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public byte[] ProfilePicture { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastLogin { get; set; }

        public int ImageCount { get; set; }

        public int AverageScore { get; set; }

        // public string Role { get; set; }

        public string Rang { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
