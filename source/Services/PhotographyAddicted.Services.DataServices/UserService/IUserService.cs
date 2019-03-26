using Microsoft.AspNetCore.Http;
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
        PreviewUserViewModel PreviewUser(string id);

        IEnumerable<PreviewUserViewModel> PreviewUsers();

        Task<string> UpdateProfilePicture(PreviewUserViewModel input, IFormFile ProfilePicture);

        int UserScores(string id);

        int GetUsersCount();

    }
}
