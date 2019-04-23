using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Services.Models.Themes;
using PhotographyAddicted.Web.Areas.Identity.Data;

namespace PhotographyAddicted.Services.DataServices
{
    public class ThemeService : IThemeService
    {
        private IRepository<Theme> themeDbSet;

        public ThemeService(IRepository<Theme> themeDbSet)
        {
            this.themeDbSet = themeDbSet;
        }

        public async Task AddTheme(AddThemeViewModel input)
        {
            var theme = new Theme
            {
                PhotographyAddictedUserId=input.PhotographyAddictedUserId,
                AuthorOpinion = input.AuthorOpinion,
                Title = input.Title,
                ThemeCategory = input.ThemeCategory,
                CreationDate = DateTime.UtcNow,                
            };

            await themeDbSet.AddAsync(theme);
            await themeDbSet.SaveChangesAsync();

            var themeComment = new ThemeComment
            {
                PhotographyAddictedUser = theme.PhotographyAddictedUser,
                PhotographyAddictedUserId = input.PhotographyAddictedUserId,
                ThemeId = theme.Id,
                Theme = theme,
                UserOpinion = theme.AuthorOpinion,
                CreationDate = DateTime.UtcNow,
            };

            theme.ThemeComments.Add(themeComment);
            await themeDbSet.SaveChangesAsync();
        }

        public async Task DeleteTheme(PreviewThemeViewModel input)
        {
            var theme = themeDbSet.All().Where(x => x.Id == input.Id).FirstOrDefault();

            themeDbSet.Delete(theme);
            await themeDbSet.SaveChangesAsync();
        }

        public async Task UpdateTheme(PreviewThemeViewModel input)
        {
            var updateTheme = themeDbSet.All().SingleOrDefault(t => t.Id == input.Id);

            updateTheme.AuthorOpinion = input.AuthorOpinion;
            updateTheme.Title = input.Title;
            updateTheme.ThemeCategory = input.ThemeCategory;

            await themeDbSet.SaveChangesAsync();
        }     

        public PreviewThemeViewModel PreviewTheme(int id)
        {
            var specificTheme = themeDbSet.All().Where(x => x.Id == id).Select(m=> 
            new PreviewThemeViewModel
                {
                  Id =m.Id,
                  AuthorOpinion = m.AuthorOpinion,
                  Title = m.Title,
                  UserName = m.PhotographyAddictedUser.UserName,
                  ThemeCategory = m.ThemeCategory,
                  PhotographyAddictedUserId = m.PhotographyAddictedUserId,
                  ThemeComments = m.ThemeComments,
                  PhotographyAddictedUser = m.PhotographyAddictedUser,
                  CreationDate = m.CreationDate,
                }).FirstOrDefault();
                    
            return specificTheme;
        }
             
        public int GetThemesCount()
        {
            int count = this.themeDbSet.All().Count();

            return count;
        }

        public PreviewThemeViewModel FindThemeBy(int Id)
        {
            var theme = PreviewTheme(Id);

            return theme;
        }

        public PreviewThemesViewModel PreviewThemes(string input)
        {
            PreviewThemesViewModel themes = new PreviewThemesViewModel();

            if (input == null)
            {
                themes.PreviewThemes = themeDbSet.All().Select(u =>
                new PreviewThemeViewModel
                {
                    CreationDate = u.CreationDate,
                    AuthorOpinion = u.AuthorOpinion,
                    Title = u.Title,
                    Id = u.Id,
                    PhotographyAddictedUser = u.PhotographyAddictedUser,
                    PhotographyAddictedUserId = u.PhotographyAddictedUserId,
                    ThemeCategory = u.ThemeCategory,
                    ThemeComments = u.ThemeComments,
                    UserName = u.PhotographyAddictedUser.UserName, 
                });
            }
            else
            {
                themes.PreviewThemes = themeDbSet.All().Where(n => n.Title.Contains(input)).Select(u =>
                new PreviewThemeViewModel
                {
                    CreationDate = u.CreationDate,
                    AuthorOpinion = u.AuthorOpinion,
                    Title = u.Title,
                    Id = u.Id,
                    PhotographyAddictedUser = u.PhotographyAddictedUser,
                    PhotographyAddictedUserId = u.PhotographyAddictedUserId,
                    ThemeCategory = u.ThemeCategory,
                    ThemeComments = u.ThemeComments,
                    UserName = u.PhotographyAddictedUser.UserName,
                });
                themes.input = input;
            }

            return themes;
        }
    }
}
