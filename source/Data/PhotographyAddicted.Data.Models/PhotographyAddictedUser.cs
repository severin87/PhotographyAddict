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
            News = new HashSet<New>();
            NewComments = new HashSet<NewComment>();
            Images = new HashSet<Image>();
            ImageComments = new HashSet<ImageComment>();
            Themes = new HashSet<Theme>();
            ThemeComments = new HashSet<ThemeComment>();
            PhotoStoryComments = new HashSet<PhotoStoryComment>();
            PhotoStorys = new HashSet<PhotoStory>();
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

        public virtual ICollection<New> News { get; set; }

        public virtual ICollection<NewComment> NewComments { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<ImageComment> ImageComments { get; set; }

        public virtual ICollection<ThemeComment> ThemeComments { get; set; }

        public virtual ICollection<Theme> Themes { get; set; }

        public virtual ICollection<PhotoStoryComment> PhotoStoryComments { get; set; }

        public virtual ICollection<PhotoStory> PhotoStorys { get; set; }

        //TODO: Dobavqne i statut s priwilegiii zadulveniq. Roli
        //TODO: Dobavqne na lubimi snimki i avtori.
        //TODO: tochki na glasa
        //TODO: Suobshteniq
        //TODO: Technika na vsqka snimka
        //TODO: ogranichenie na kacheni snimki za den!!!
        //TODO: Dobavqna na zaqwka kum admin za smqna na Username.
        //TODO: DObavqne na linkove kum komentarite.
        //TODO: Da probvam da napravq Generic za proverka na Rolq.
        //TODO: Proveri novite entitita(PhototStory) i tehnite komentari kak se triqt pri iztrivane na potrebitel.
        //TODO: Napishi si na vsichki async metodi Async na kraq.
    }
}
