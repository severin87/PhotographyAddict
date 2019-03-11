using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotographyAddicted.Services.Models.Themes
{
    public class CreateThemeInputViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        public string AuthorOpinion { get; set; }
     
        public string PhotographyAddictedUserId { get; set; }
    }
}
