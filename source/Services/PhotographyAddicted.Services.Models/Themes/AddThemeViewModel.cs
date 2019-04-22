﻿using PhotographyAddicted.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotographyAddicted.Services.Models.Themes
{
    public class AddThemeViewModel
    {
        [Required]
        [MinLength(4)]
        public string Title { get; set; }

        [Required]
        [MinLength(15)]
        public string AuthorOpinion { get; set; }

        public DateTime CreationDate { get; set; }

        public string PhotographyAddictedUserId { get; set; }

        [Required]
        public ThemeCategory ThemeCategory { get; set; }
    }
}
