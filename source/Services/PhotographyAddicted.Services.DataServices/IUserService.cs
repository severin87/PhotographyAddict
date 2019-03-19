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
        IEnumerable<IndexUserViewModel> GetSpecificUser(int specific);

        UserProfileViewModel GetCurrentUserProfile(string id);
        
        Task<string> AddProfilePicture(EditUserViewModel input, IFormFile ProfilePicture);

        IEnumerable<ImagePreviewViewModel> GetUsersPictures(string id);

        void AddUserLastLogin();

        int UsersImagesCount(string id);

        int UsersScores(string id);

        int GetCount();
    }
}
