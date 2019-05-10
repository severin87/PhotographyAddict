using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Data.Models
{
    public  class FavouriteImage
    {
        public int FavouriteId { get; set; }
        public virtual Favourite Favorite { get; set; }
        public int ImageId { get; set; }
        public virtual Image Image { get; set; }
    }
}
