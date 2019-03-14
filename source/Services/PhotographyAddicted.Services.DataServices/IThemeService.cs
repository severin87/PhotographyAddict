using PhotographyAddicted.Services.Models.Themes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices
{
    public interface IThemeService
    {
        Task<int> CreateTheme(CreateThemeInputViewModel input);

        Task<int> UpdateTheme(UpdateTheme input);

        UpdateTheme ViewUpdateThemeById(int id);

        ThemeDetailsViewModel ViewSpecificDetailsTheme(int id);
        
        IEnumerable<ThemeDetailsViewModel> GetAllThemes();        

    }
}
