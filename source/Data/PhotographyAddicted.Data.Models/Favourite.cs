using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Data.Models
{
    public class Favourite
    {
        public Favourite()
        {
            FavouriteImages = new HashSet<FavouriteImage>();
        }

        public int Id { get; set; }

        public string PhotographyAddictedUserId { get; set; }
        public virtual PhotographyAddictedUser PhotographyAddictedUser { get; set; }

        public virtual ICollection<FavouriteImage> FavouriteImages { get; set; }
    }
}
