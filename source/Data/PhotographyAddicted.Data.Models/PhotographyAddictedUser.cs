using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
            PhotoStories = new HashSet<PhotoStory>();
            SenderConversation = new HashSet<Conversation>();
            RecepientConversation = new HashSet<Conversation>();
            Messages = new HashSet<Message>();
        }
        
        public string SelfDescription { get; set; }

        public string Technique { get; set; }

        public bool IsAgree { get; set; }

        public byte[] ProfilePicture { get; set; }

        public DateTime CreationDate { get; set; }        

        public DateTime LastLogin { get; set; }

        public int ImageCount { get; set; }

        public int AverageScore { get; set; }

        public bool IsBanned { get; set; }

        public DateTime BannedDate { get; set; }

        public int BanLengthDays { get; set; }

        public string Rang { get; set; }

        public virtual Favourite Favourite { get; set; }

        [InverseProperty("SenderPhotographyAddictedUser")]
        public virtual ICollection<Conversation> SenderConversation { get; set; }

        [InverseProperty("RecepientPhotographyAddictedUser")]
        public virtual ICollection<Conversation> RecepientConversation { get; set; }

        public virtual ICollection<New> News { get; set; }

        public virtual ICollection<NewComment> NewComments { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<ImageComment> ImageComments { get; set; }

        public virtual ICollection<ThemeComment> ThemeComments { get; set; }

        public virtual ICollection<Theme> Themes { get; set; }

        public virtual ICollection<PhotoStoryComment> PhotoStoryComments { get; set; }

        public virtual ICollection<PhotoStory> PhotoStories { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        //Frontend
        //TODO: Da se opravi cveta na bukvite na vsqkude.
        //TODO: da se naglasi razmera na butonite.
        //TODO: Da se namesti navbara sus mesindjura.
        //TODO: Navsqkude da opravq preview pictures.


        //TODO: Ostava problema s mnogo visokite snimki pri pokazvaneto im v maluk variant ot strani.

        //TODO: Nastroika dali sa syglasni s pravilata

        //TODO: Pokazvane na maluk variant na snimka kogato se uploadva.
        //TODO: UpdateComments na vsichki mesta sa izlishni.
        //TODO: Da sloja snimka na usarite bez profilni snimki.
        //TODO: Proveri vsichki Contrroleri s action Preview dali imat zashtita on Null stoinosti.
        //TODO Chasa da se opravi.
        //TODO RAzchisti HomeControlera.
        //TODO: public IHttpContextAccessor httpContextAccessor; Vse oshte ne mi trqbva.
        //TODO: Dobavqne na opciq za dobavqne na reklami za Admina
        //TODO: Da razchistq HomeControlera
        //TODO: DNevna proverka za broi snimki kacheni.
        //TODO: DObavqne na linkove kum komentarite.
        //TODO: ogranichenie na kacheni snimki za den!!!
        //TODO: Vsichki Services and Dbsets da stanat readonly
        //TODO: Amina da skriva a ne da trie syobshteniq.
        //TODO: Da si kriptiram suobshteniqta.
        //TODO: Da vidq s ajax kakvo stava.
        //TODO: Da si podredq i preimenuvam vsichki commenti  !!!!!!!!!
        //TODO: Napishi si na vsichki async metodi Async na kraq.
        //TODO: Dobavqna na zaqwka kum admin za smqna na Username.
        //13
    }
}
