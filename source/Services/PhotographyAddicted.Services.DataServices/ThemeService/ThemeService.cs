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
        }

        public async Task DeleteTheme(PreviewThemeViewModel input)
        {
            var themeComment = themeDbSet.All().Where(x => x.Id == input.Id).FirstOrDefault();

            themeDbSet.Delete(themeComment);
            await themeDbSet.SaveChangesAsync();

        }
        
        public IEnumerable<PreviewThemeViewModel> PreviewThemes()
        {
            var allThemes = themeDbSet.All().Include(g => g.PhotographyAddictedUser).Select(m => new PreviewThemeViewModel
            {
                Id = m.Id,
                AuthorOpinion = m.AuthorOpinion,
                Title = m.Title,
                UserName = m.PhotographyAddictedUser.UserName,
                ThemeCategory = m.ThemeCategory,
                ThemeComments = m.ThemeComments,
                PhotographyAddictedUserId = m.PhotographyAddictedUserId,
                CreationDate = m.CreationDate,

            }).ToList();

            return allThemes;
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
            var specificTheme = themeDbSet.All().Include(g=>g.PhotographyAddictedUser)
                .Where(x => x.Id == id).Select(m=> new PreviewThemeViewModel
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
            var theme = themeDbSet.All().Where(x => x.Id == Id)
                .Select(d => new PreviewThemeViewModel
                {
                    Id = d.Id,
                    PhotographyAddictedUserId = d.PhotographyAddictedUserId,
                    Title = d.Title,
                    AuthorOpinion = d.AuthorOpinion,
                }).FirstOrDefault();

            return theme;
        }
    }
}
