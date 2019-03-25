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

        public async Task<int> UpdateImageComment(UpdateImageCommentViewModel input)
        {
            var updateImage = imageCommentDbSet.All().SingleOrDefault(t => t.Id == input.Id);

            updateImage.UserOpinion = input.UserOpinion;

            await imageCommentDbSet.SaveChangesAsync();

            return (int)updateImage.ImageId;
        }


        public UpdateImageCommentViewModel ViewUpdateImageById(int id)
        {
            var specificImageComment = imageCommentDbSet.All()
                .Where(x => x.Id == id).Select(m => new UpdateImageCommentViewModel
                {
                    Id = m.Id,
                    UserOpinion = m.UserOpinion,
                    ImageId = m.ImageId,
                    PhotographyAddictedUserId = m.PhotographyAddictedUserId,

                }).FirstOrDefault();

            return specificImageComment;
        }

        public DeleteUserImageCommentViewModel FindImageCommentById(int Id)
        {
            var imageComment = imageCommentDbSet.All().Where(x => x.Id == Id)
                .Select(d => new DeleteUserImageCommentViewModel
                {
                    Id = d.Id,
                    PhotographyAddictedUserId = d.PhotographyAddictedUserId,
                    ImageId = d.ImageId,
                    UserOpinion = d.UserOpinion,
                }).FirstOrDefault();

            return imageComment;
        }

        public async Task<int> DeleteUserImageComment(DeleteUserImageCommentViewModel input)
        {
            var imageComment = imageCommentDbSet.All().Where(x => x.Id == input.Id).FirstOrDefault();
            imageCommentDbSet.Delete(imageComment);
            await imageCommentDbSet.SaveChangesAsync();

            return (int)imageComment.ImageId;
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
