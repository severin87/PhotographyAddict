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

        //DeleteUserNewCommentsViewModel FindNewCommentById(int Id);

        //Task<int> DeleteUserNewComment(DeleteUserNewCommentsViewModel input);              

        //Task<int> UpdateNewComment(UpdateNewCommentViewModel input);

        //UpdateNewCommentViewModel ViewUpdateNewById(int id);
    }
}
