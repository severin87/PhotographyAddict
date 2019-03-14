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

        public async Task<int> CreateTheme(CreateThemeInputViewModel input)
        {
            var theme = new Theme
            {
                PhotographyAddictedUserId=input.PhotographyAddictedUserId,
                AuthorOpinion = input.AuthorOpinion,
                Title = input.Title
            };

            await themeDbSet.AddAsync(theme);
            await themeDbSet.SaveChangesAsync();

            return theme.Id;
        }

        public IEnumerable<ThemeDetailsViewModel> GetAllThemes()
        {
            var allThemes = themeDbSet.All().Include(g => g.PhotographyAddictedUser).Select(m => new ThemeDetailsViewModel
            {
                Id = m.Id,
                AuthorOpinion = m.AuthorOpinion,
                Title = m.Title,
                UserName = m.PhotographyAddictedUser.UserName,

            }).ToList();

            return allThemes;

        }

        public async Task<int> UpdateTheme(UpdateTheme input)
        {
            var updateTheme = themeDbSet.All().SingleOrDefault(t => t.Id == input.Id);

            updateTheme.AuthorOpinion = input.AuthorOpinion;
            updateTheme.Title = input.Title;

            await themeDbSet.SaveChangesAsync();

            return updateTheme.Id;
     
        }     

        public ThemeDetailsViewModel ViewSpecificTheme(int id)
        {
            var specificTheme = themeDbSet.All().Include(g=>g.PhotographyAddictedUser).Where(x => x.Id == id).Select(m=> new ThemeDetailsViewModel
            {   Id=m.Id,
                AuthorOpinion = m.AuthorOpinion,
                Title = m.Title,
                UserName = m.PhotographyAddictedUser.UserName,             
                
            }).FirstOrDefault();
                    
            return specificTheme;
        }

        public UpdateTheme ViewUpdateThemeById(int id)
        {
            var specificTheme = themeDbSet.All().Include(g => g.PhotographyAddictedUser).Where(x => x.Id == id).Select(m => new UpdateTheme
            {
                Id = m.Id,
                AuthorOpinion = m.AuthorOpinion,
                Title = m.Title,             

            }).FirstOrDefault();

            return specificTheme;
        }
    }
}
