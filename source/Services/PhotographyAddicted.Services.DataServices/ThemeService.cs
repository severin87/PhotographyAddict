using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Services.Mapping;
using PhotographyAddicted.Services.Models.Themes;
using PhotographyAddicted.Web.Areas.Identity.Data;

namespace PhotographyAddicted.Services.DataServices
{
    public class ThemeService : IThemeService
    {

        private IRepository<Theme> userInfo;

        public ThemeService(IRepository<Theme> userInfo)
        {
            this.userInfo = userInfo;
        }

        public async Task<int> CreateTheme(CreateThemeInputViewModel input)
        {
            var theme = new Theme
            {
                PhotographyAddictedUserId=input.PhotographyAddictedUserId,
                AuthorOpinion = input.AuthorOpinion,
                Title = input.Title
            };

            await userInfo.AddAsync(theme);
            await userInfo.SaveChangesAsync();

            return theme.Id;
        }

        public IEnumerable<ThemeDetailsViewModel> GetAllThemes()
        {
            var allThemes = userInfo.All().Include(g => g.PhotographyAddictedUser)
                .To<ThemeDetailsViewModel>().ToList();
            return allThemes; //.To<IndexUserViewModel>()
        }

        public ThemeDetailsViewModel ViewSpecificTheme(int id)
        {
            var specificTheme = userInfo.All().Include(g=>g.PhotographyAddictedUser).Where(x => x.Id == id).Select(m=> new ThemeDetailsViewModel
            {
                AuthorOpinion = m.AuthorOpinion,
                Title = m.Title,
                UserName = m.PhotographyAddictedUser.UserName,             
                
            }).FirstOrDefault();
                    
            return specificTheme;
        }
    }
}
