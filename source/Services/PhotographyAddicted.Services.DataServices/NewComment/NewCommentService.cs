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
        
        public async Task AddNewComment(AddNewCommentViewModel input)
        {
            var newComment = new NewComment
            {
                PhotographyAddictedUserId = input.PhotographyAddictedUserId,
               // PhotographyAddictedUser = input.PhotographyAddictedUser,
                NewId = input.NewId,
                //Theme = input.Theme,
                UserOpinion = input.UserOpinion,
                CreationDate = DateTime.UtcNow,
            };

            await newCommentDbSet.AddAsync(newComment);
            await newCommentDbSet.SaveChangesAsync();

        }

        public async Task<int> DeleteUserNewComment(DeleteUserNewCommentsViewModel input)
        {
            var newComment = newCommentDbSet.All().Where(x => x.Id == input.Id).FirstOrDefault();
            newCommentDbSet.Delete(newComment);
            await newCommentDbSet.SaveChangesAsync();

            return (int)newComment.NewId;
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


        public async Task<int> UpdateNewComment(UpdateNewComment input)
        {
            var updateNew = newCommentDbSet.All().SingleOrDefault(t => t.Id == input.Id);

            updateNew.UserOpinion = input.UserOpinion;
            
            await newCommentDbSet.SaveChangesAsync();

            return (int)updateNew.NewId;
        }


        public UpdateNewComment ViewUpdateNewById(int id)
        {
            var specificTheme = newCommentDbSet.All()
                .Where(x => x.Id == id).Select(m => new UpdateNewComment
                {
                    Id = m.Id,
                    UserOpinion = m.UserOpinion,
                    NewId = m.NewId,
                    PhotographyAddictedUserId = m.PhotographyAddictedUserId,
                    
                }).FirstOrDefault();

            return specificTheme;
        }
    }
}
