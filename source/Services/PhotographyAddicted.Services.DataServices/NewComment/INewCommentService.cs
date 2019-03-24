using PhotographyAddicted.Services.Models.NewsComments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices
{
    public interface INewCommentService
    {
        DeleteUserNewCommentsViewModel FindNewCommentById(int Id);

        Task<int> DeleteUserNewComment(DeleteUserNewCommentsViewModel input);

        Task DeleteUserNewComments(string id);
    }
}
