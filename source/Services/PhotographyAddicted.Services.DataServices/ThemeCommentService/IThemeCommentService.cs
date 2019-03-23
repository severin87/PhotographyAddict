using PhotographyAddicted.Services.Models.ThemesComments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices
{
    public interface IThemeCommentService
    {
        Task DeleteUserThemesComments(string id);

        Task AddThemeComment(AddThemeCommentViewModel input);

        DeleteThemeCommentViewModel FindThemeCommentById(int Id);

        Task<int> DeleteThemeComment(DeleteThemeCommentViewModel input);
    }
}
