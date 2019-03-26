using PhotographyAddicted.Services.Models.ImagesComments;
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

        DeleteImageCommentViewModel FindImageCommentById(int Id);

        Task<int> DeleteUserImageComment(DeleteImageCommentViewModel input);

        Task<int> UpdateImageComment(UpdateImageCommentViewModel input);

        UpdateImageCommentViewModel ViewUpdateImageById(int id);
    }
}
