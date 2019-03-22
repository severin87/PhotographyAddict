using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.Models.Images
{
    public class DeleteImageViewModel
    {
        public int Id { get; set; }

        public byte[] Picture { get; set; }

        public string Title { get; set; }

        public int Scores { get; set; }

        public string PhotographyAddictedUserId { get; set; }
    }
}
