using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.Models.Messages
{
    public class PreviewMessageViewModel
    {
        public string Text { get; set; }

        public string RecepientId { get; set; }

        public int ConversationId { get; set; }

        public string PhotographyAddictedUserId { get; set; }
    }
}
