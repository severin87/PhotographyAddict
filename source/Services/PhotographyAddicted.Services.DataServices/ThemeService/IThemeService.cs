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

        PreviewThemeViewModel FindThemeBy(int Id);

        PreviewThemesViewModel PreviewThemes(string input);

        Task DeleteTheme(PreviewThemeViewModel input);

        Task AddTheme(AddThemeViewModel input);

        Task UpdateTheme(PreviewThemeViewModel input);

        int GetThemesCount();

        string CreationThemeDate(DateTime creationDate);
    }
}
