using PhotographyAddicted.Services.Models.ImagesComment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices
{
    public interface IImageCommentService
    {
        Task DeleteUserImagesComments(string id);

        Task AddImageComment(AddImageCommentViewModel input);

        DeleteUserImageCommentViewModel FindImageCommentById(int Id);

        Task<int> DeleteUserImageComment(DeleteUserImageCommentViewModel input);

        Task<int> UpdateImageComment(UpdateImageCommentViewModel input);

        UpdateImageCommentViewModel ViewUpdateImageById(int id);
    }
}
