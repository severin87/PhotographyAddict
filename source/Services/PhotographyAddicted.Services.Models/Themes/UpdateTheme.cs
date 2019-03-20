using PhotographyAddicted.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotographyAddicted.Services.Models.Themes
{
    public class UpdateTheme
    {

        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        public string Title { get; set; }

        [Required]
        [MinLength(15)]
        public string AuthorOpinion { get; set; }

        public ThemeCategory ThemeCategory { get; set; }

    }
}
