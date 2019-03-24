using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Services.Models.NewsComments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices
{
    public class NewCommentService : INewCommentService
    {
        private IRepository<NewComment> newCommentDbSet;

        public NewCommentService(IRepository<NewComment> newCommentDbSet)
        {
            this.newCommentDbSet = newCommentDbSet;
        }

        public async Task<int> DeleteUserNewComment(DeleteUserNewCommentsViewModel input)
        {
            var newComment = newCommentDbSet.All().Where(x => x.Id == input.Id).FirstOrDefault();
            var tempId = (int)newComment.NewId;
            newCommentDbSet.Delete(newComment);
            await newCommentDbSet.SaveChangesAsync();

            return tempId;
        }

        public async Task DeleteUserNewComments(string id)
        {
            var newComments = newCommentDbSet.All().Where(x => x.PhotographyAddictedUserId == id)
                 .ToList();

            foreach (var newComment in newComments)
            {
               newCommentDbSet.Delete(newComment);
            }

            await newCommentDbSet.SaveChangesAsync();
        }

        public  DeleteUserNewCommentsViewModel FindNewCommentById(int Id)
        {
            var newComment = newCommentDbSet.All().Where(x => x.Id == Id)
                .Select(d => new DeleteUserNewCommentsViewModel
                {
                    Id = d.Id,
                    PhotographyAddictedUserId = d.PhotographyAddictedUserId,
                    NewId = d.NewId,
                    UserOpinion = d.UserOpinion,
                }).FirstOrDefault();

            return newComment;
        }
    }
}
