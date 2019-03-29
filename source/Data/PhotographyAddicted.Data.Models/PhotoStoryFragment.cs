using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhotographyAddicted.Data.Models
{

    public class PhotoStoryFragment
    {
        public int Id { get; set; }

        public byte[] Picture { get; set; }

        public string Place { get; set; }

        public string Description { get; set; }

        public int? PhotoStoryId { get; set; }
        public virtual PhotoStory PhotoStory { get; set; }
    }
}
