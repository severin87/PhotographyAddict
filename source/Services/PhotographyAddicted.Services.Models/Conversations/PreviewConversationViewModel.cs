using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotographyAddicted.Services.Models.Conversations
{
    public class PreviewConversationViewModel
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual PhotographyAddictedUser SenderPhotographyAddictedUser { get; set; }

        public virtual PhotographyAddictedUser RecepientPhotographyAddictedUser { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
