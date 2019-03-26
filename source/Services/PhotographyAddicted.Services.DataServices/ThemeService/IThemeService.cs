using PhotographyAddicted.Services.Models.Themes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices
{
    public interface IThemeService
    {
        PreviewThemeViewModel PreviewTheme(int id);

        IEnumerable<PreviewThemeViewModel> PreviewThemes();

        PreviewThemeViewModel FindThemeBy(int Id);

        Task DeleteTheme(PreviewThemeViewModel input);

        Task AddTheme(AddThemeViewModel input);

        Task UpdateTheme(PreviewThemeViewModel input);

        int GetThemesCount();
    }
}
