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

    }
}
