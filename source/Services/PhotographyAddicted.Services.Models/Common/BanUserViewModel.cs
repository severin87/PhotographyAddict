using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.Models.Common
{
    public class BanUserViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public bool IsBanned { get; set; }

        public DateTime BannedDate { get; set; }

        public int BanLengthDays { get; set; }
    }
}
