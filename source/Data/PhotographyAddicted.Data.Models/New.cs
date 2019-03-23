using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Data.Models
{
    public class New
    {

        public New()
        {
            NewComments = new HashSet<NewComment>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public byte[] NewImage { get; set; }

        public string TextContent { get; set; }

        public string PhotographyAddictedUserId { get; set; }
        public virtual PhotographyAddictedUser PhotographyAddictedUser { get; set; }

        public int ComentsCount { get; set; }

        public virtual ICollection<NewComment> NewComments { get; set; }
    }
}
