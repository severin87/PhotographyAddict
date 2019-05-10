using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotographyAddicted.Services.Models.Messages
{
    public class AddMessageViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public string RecepientId { get; set; }

        public int ConversationId { get; set; }

        public string PhotographyAddictedUserId { get; set; }
    }
}
