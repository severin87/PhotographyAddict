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
            Images = new HashSet<Image>();
            ImageComments = new HashSet<ImageComment>();
            Themes = new HashSet<Theme>();
            ThemeComments = new HashSet<ThemeComment>();
        }
        
        public string SelfDescription { get; set; }

        public string Technique { get; set; }

        public bool IsAgree { get; set; }

        public byte[] ProfilePicture { get; set; }

        public DateTime CreationDate { get; set; }


        public DateTime Blocked { get; set; }

        public DateTime LastLogin { get; set; }

        public int ImageCount { get; set; }

        public int AverageScore { get; set; }

        public bool IsBanned { get; set; }

        // public string Role { get; set; }

        public string Rang { get; set; }


        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<ImageComment> ImageComments { get; set; }

        public virtual ICollection<ThemeComment> ThemeComments { get; set; }

        public virtual ICollection<Theme> Themes { get; set; }

        //TODO: Dobavqne i statut s priwilegiii zadulveniq. Roli
        //TODO: Dobavqne na lubimi snimki i avtori.
        //TODO: tochki na glasa
        //TODO: Suobshteniq
        //TODO: Technika na vsqka snimka
        //TODO: Vkluchvane na CreationDate i na LasLogin za Facebook
        //TODO: ogranichenie na kacheni snimki za den!!!
        //TODO:  Da napravq Enumeraciite
        //TODO:  Cascadno triene!
        //TODO:  Repair FirstLAstLogin!
        //TODO: Link ot registraciq kum facebook registraciq

    }
}
