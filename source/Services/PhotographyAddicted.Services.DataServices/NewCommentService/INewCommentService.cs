using PhotographyAddicted.Services.Models.NewsComments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices
{
    public interface INewCommentService
    {
        DeleteNewCommentsViewModel FindNewCommentById(int Id);

        Task<int> DeleteUserNewComment(DeleteNewCommentsViewModel input);

        Task DeleteUserNewComments(string id);

        Task AddNewComment(AddNewCommentViewModel input);

        Task<int> UpdateNewComment(UpdateNewCommentViewModel input);

        UpdateNewCommentViewModel ViewUpdateNewById(int id);

    }
}
