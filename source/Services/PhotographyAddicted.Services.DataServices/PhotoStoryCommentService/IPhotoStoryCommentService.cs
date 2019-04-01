using PhotographyAddicted.Services.Models.PhotoStoryComments;
using PhotographyAddicted.Services.Models.PhotoStorys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices.PhotoStoryCommentService
{
    public interface IPhotoStoryCommentService
    {
        Task DeleteUserPhotoStoryComments(string id);

        Task AddPhotoStoryComment(AddPhotoStoryCommentViewModel input);

        PreviewPhotoStoryCommentViewModel PreviewPhotoStoryCommentById(int Id);

        Task<int> DeletePhotoStoryComment(PreviewPhotoStoryCommentViewModel input);

        Task<int> UpdatePhotoStoryComment(PreviewPhotoStoryCommentViewModel input);
    }
}
