
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhotographyAddicted.Services.Models.Themes
{
    public class PreviewThemeViewModel 
    {      
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        public string Title { get; set; }

        [Required]
        [MinLength(15)]
        public string AuthorOpinion { get; set; }

        public DateTime CreationDate { get; set; }

        public string UserName { get; set; }
        
        public ThemeCategory ThemeCategory { get; set; }

        public string PhotographyAddictedUserId { get; set; }
        public virtual PhotographyAddictedUser PhotographyAddictedUser { get; set; }

        public virtual ICollection<ThemeComment> ThemeComments { get; set; }
    }
}
