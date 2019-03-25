using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Services.Models.ImagesComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices
{
    public class ImageCommentService : IImageCommentService
    {

        private IRepository<ImageComment> imageCommentDbSet;

        public ImageCommentService(IRepository<ImageComment> imageCommentDbSet)
        {
            this.imageCommentDbSet = imageCommentDbSet;
        }

        public async Task AddImageComment(AddImageCommentViewModel input)
        {
            var imageComment = new ImageComment
            {
                PhotographyAddictedUserId = input.PhotographyAddictedUserId,
                ImageId = input.ImageId,
                UserOpinion = input.UserOpinion,
                CreationDate = DateTime.UtcNow,
            };

            await imageCommentDbSet.AddAsync(imageComment);
            await imageCommentDbSet.SaveChangesAsync();

        }

        public async Task DeleteUserImagesComments(string id)
        {
            var imageComments = imageCommentDbSet.All().Where(x => x.PhotographyAddictedUserId == id)
                .ToList();

            foreach (var imageComment in imageComments)
            {
                imageCommentDbSet.Delete(imageComment);
            }

            await imageCommentDbSet.SaveChangesAsync();
        }

    }
}
