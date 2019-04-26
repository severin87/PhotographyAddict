using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotographyAddicted.Services.Models.News
{
    public class AddNewViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        
        public byte[] NewImage { get; set; }

        public string TextContent { get; set; }

        public DateTime CreationDate { get; set; }

        public string PhotographyAddictedUserId { get; set; }
    }
}
