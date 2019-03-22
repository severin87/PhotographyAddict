using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Services.Models.ThemesComments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices
{
    public class ThemeCommentService : IThemeCommentService
    {
        private IRepository<ThemeComment> themeCommentDbSet;

        public ThemeCommentService(IRepository<ThemeComment> themeCommentDbSet)
        {
            this.themeCommentDbSet = themeCommentDbSet;
        }

        public async Task AddThemeComment(AddThemeCommentViewModel input)
        {
            var themeComment = new ThemeComment
            {
                PhotographyAddictedUserId = input.PhotographyAddictedUserId,
                PhotographyAddictedUser = input.PhotographyAddictedUser,
                ThemeId = input.ThemeId,
                Theme = input.Theme,
                UserOpinion = input.UserOpinion,               

            };

            await themeCommentDbSet.AddAsync(themeComment);
            await themeCommentDbSet.SaveChangesAsync();
            
        }

        public async Task DeleteUserThemesComments(string id)
        {
            var themeComments = themeCommentDbSet.All().Where(x => x.PhotographyAddictedUserId == id)
                .ToList();

            foreach (var themeComment in themeComments)
            {
                themeCommentDbSet.Delete(themeComment);
            }

            await themeCommentDbSet.SaveChangesAsync();
        }

        public async Task<int> DeleteThemeComment(DeleteThemeComment input)
        {
            var themeComment = themeCommentDbSet.All().Where(x => x.Id == input.Id).FirstOrDefault();
            var tempId = (int)themeComment.ThemeId;
            themeCommentDbSet.Delete(themeComment);
            await themeCommentDbSet.SaveChangesAsync();

            return tempId;
        }

        public DeleteThemeComment FindThemeCommentById(int Id)
        {
            var themeComment = themeCommentDbSet.All().Where(x => x.Id == Id)
                .Select(d => new DeleteThemeComment
                {
                    Id = d.Id,
                    PhotographyAddictedUserId = d.PhotographyAddictedUserId,
                    ThemeId = d.ThemeId,
                    UserOpinion = d.UserOpinion,
                }).FirstOrDefault();

            return themeComment;
        }
    }
}
