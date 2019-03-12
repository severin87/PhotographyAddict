using PhotographyAddicted.Services.Mapping;
using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.Models.Users
{
    public class IndexUserViewModel 
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string ThemeTitle { get; set; }
    }
}
