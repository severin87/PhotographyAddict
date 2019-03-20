using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices
{
    public interface IThemeCommentService
    {
        Task DeleteUserThemesComments(string id);
    }
}
