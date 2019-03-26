using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.Models.ImagesComments
{
    public class DeleteImageCommentViewModel
    {
        public int Id { get; set; }

        public string UserOpinion { get; set; }

        public int? ImageId { get; set; }

        public string PhotographyAddictedUserId { get; set; }

    }
}
