using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Data.Models
{
    public class VotedUser
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int? ImageId { get; set; }
        public virtual Image Image { get; set; }
    }
}
