using PhotographyAddicted.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.Models.News
{
    public class PreviewNewViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public byte[] NewImage { get; set; }

        public string TextContent { get; set; }

        public string PhotographyAddictedUserId { get; set; }

        public int ComentsCount { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual ICollection<NewComment> NewComments { get; set; }
    }
}
