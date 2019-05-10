using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhotographyAddicted.Data.Models
{
    public class Conversation
    {
        public Conversation()
        {           
            Messages = new HashSet<Message>();
        }

        [Key]
        public int Id { get; set; }

        public string Subject { get; set; }

        public DateTime CreationDate { get; set; }        

        //[ForeignKey("SenderPhotographyAddictedUser"), Column(Order = 0)]
        //public int SenderPhotographyAddictedUserId { get; set; }
        public virtual PhotographyAddictedUser SenderPhotographyAddictedUser { get; set; }

        //[ForeignKey("RecepientPhotographyAddictedUser"), Column(Order = 1)]
        //public int RecepientPhotographyAddictedUserId { get; set; }
        public virtual PhotographyAddictedUser RecepientPhotographyAddictedUser { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
