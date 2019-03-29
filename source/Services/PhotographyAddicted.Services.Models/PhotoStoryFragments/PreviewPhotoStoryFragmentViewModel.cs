using PhotographyAddicted.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.Models.PhotoStoryFragments
{
    public class PreviewPhotoStoryFragmentViewModel
    {
        public int Id { get; set; }

        public byte[] Picture { get; set; }

        public string Place { get; set; }

        public string Description { get; set; }

        public int? PhotoStoryId { get; set; }
        public virtual PhotoStory PhotoStory { get; set; }
    }
}
