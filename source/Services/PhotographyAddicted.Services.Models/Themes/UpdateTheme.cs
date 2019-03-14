using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotographyAddicted.Services.Models.Themes
{
    public class UpdateTheme
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string AuthorOpinion { get; set; }

        //public string PhotographyAddictedUserId { get; set; }
    }
}
