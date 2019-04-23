using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.Models.Users
{
    public class PreviewUsersViewModel
    {
        public IEnumerable<PreviewUserViewModel> PreviewUsers { get; set; }
        public string input { get; set; }
    }
}
