using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.Models.ImagesComment
{
    public class DeleteUserImageCommentViewModel
    {
        public int Id { get; set; }

        public string UserOpinion { get; set; }

        public int? ImageId { get; set; }

        public string PhotographyAddictedUserId { get; set; }

    }
}
