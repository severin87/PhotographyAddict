using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PhotographyAddicted.Data.Models;

namespace PhotographyAddicted.Web.Areas.Identity.Data
{    
    public class PhotographyAddictedUser : IdentityUser
    {
        public PhotographyAddictedUser()
        {
            Themes = new HashSet<Theme>();
        }

        public virtual ICollection<Theme> Themes { get; set; }
    }
}
