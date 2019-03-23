using Microsoft.AspNetCore.Http;
using PhotographyAddicted.Services.Models.Images;
using PhotographyAddicted.Services.Models.Users;
using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices
{
    public interface IUserService
    {
        
        UserProfileViewModel GetCurrentUserProfile(string id);
        
        Task<string> AddProfilePicture(EditUserViewModel input, IFormFile ProfilePicture);

        IEnumerable<ImagePreviewViewModel> GetUsersPictures(string id);

        int UsersScores(string id);

        int GetUsersCount();

        IEnumerable<PreviewUsersViewModel> GetUsersInfos();

    }
}
