using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Services.Models.PhotoStoryComments;
using PhotographyAddicted.Services.Models.PhotoStorys;

namespace PhotographyAddicted.Services.DataServices.PhotoStoryCommentService
{
    public class PhotoStoryCommentService : IPhotoStoryCommentService
    {
        private IRepository<PhotoStoryComment> photoStoryCommentDbSet;

        public PhotoStoryCommentService(IRepository<PhotoStoryComment> photoStoryCommentDbSet)
        {
            this.photoStoryCommentDbSet = photoStoryCommentDbSet;
        }

        public async Task AddPhotoStoryComment(AddPhotoStoryCommentViewModel input)
        {
            var photoStoryComment = new PhotoStoryComment
            {
                PhotographyAddictedUserId = input.PhotographyAddictedUserId,
                PhotographyAddictedUser = input.PhotographyAddictedUser,
                PhotoStory = input.PhotoStory,
                PhotoStoryId = input.PhotoStoryId,
                UserOpinion = input.UserOpinion,
                CreationDate = DateTime.UtcNow,                
            };

            await photoStoryCommentDbSet.AddAsync(photoStoryComment);
            await photoStoryCommentDbSet.SaveChangesAsync();
        }

        public async Task<int> DeletePhotoStoryComment(PreviewPhotoStoryCommentViewModel input)
        {
            var photoStoryComment = photoStoryCommentDbSet.All().Where(x => x.Id == input.Id).FirstOrDefault();
            photoStoryCommentDbSet.Delete(photoStoryComment);
            await photoStoryCommentDbSet.SaveChangesAsync();

            return (int)photoStoryComment.PhotoStoryId;
        }

        public async Task DeleteUserPhotoStoryComments(string id)
        {
            var photoStoryComments = photoStoryCommentDbSet.All().Where(x => x.PhotographyAddictedUserId == id)
                .ToList();

            foreach (var photoStoryComment in photoStoryComments)
            {
                photoStoryCommentDbSet.Delete(photoStoryComment);
            }

            await photoStoryCommentDbSet.SaveChangesAsync();
        }

        public PreviewPhotoStoryCommentViewModel PreviewPhotoStoryCommentById(int Id)
        {
            var photoStoryComment = photoStoryCommentDbSet.All().Where(x => x.Id == Id)
                .Select(d => new PreviewPhotoStoryCommentViewModel
                {
                    Id = d.Id,
                    PhotographyAddictedUserId = d.PhotographyAddictedUserId,
                    CreationDate = d.CreationDate,
                    PhotoStory = d.PhotoStory,
                    PhotoStoryId = d.PhotoStoryId,
                    PhotographyAddictedUser = d.PhotographyAddictedUser,
                    UserOpinion = d.UserOpinion,
                }).FirstOrDefault();

            return photoStoryComment;
        }

        public async Task<int> UpdatePhotoStoryComment(PreviewPhotoStoryCommentViewModel input)
        {
            var photoStoryComment = photoStoryCommentDbSet.All().SingleOrDefault(t => t.Id == input.Id);

            photoStoryComment.UserOpinion = input.UserOpinion;

            await photoStoryCommentDbSet.SaveChangesAsync();

            return (int)photoStoryComment.PhotoStoryId;
        }
    }
}
