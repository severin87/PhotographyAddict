using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Services.Models.Users;
using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices
{
    public class UserService : IUserService
    {
        private IRepository<PhotographyAddictedUser> userDbset;
               
        public UserService(IRepository<PhotographyAddictedUser> userDbset)
        {
            this.userDbset = userDbset;
        }
        
        public async Task<string> UpdateProfilePicture(PreviewUserViewModel input, IFormFile ProfilePicture)
        {
            if (ProfilePicture.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await ProfilePicture.CopyToAsync(stream);
                    input.ProfilePicture = stream.ToArray();
                }
            }

            PhotographyAddictedUser user = userDbset.All().FirstOrDefault(i => i.Id == input.Id);
            user.ProfilePicture = input.ProfilePicture;
            await userDbset.SaveChangesAsync();
            return user.Id;
        }
                
        public PreviewUserViewModel PreviewUser(string id)
        {
            var user = userDbset.All().Include(i => i.Images).Where(i => i.Id == id).Select(u =>
            new PreviewUserViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                ProfilePicture = u.ProfilePicture,
                Technique = u.Technique,
                SelfDescription = u.SelfDescription,
                ImageCount = u.Images.Count(),
                Rang = u.Rang,
                AverageScore = UserScores(id),
                CreationDate = u.CreationDate,
                LastLogin = u.LastLogin,
                BanLengthDays = u.BanLengthDays,
                BannedDate = u.BannedDate,
                IsBanned = u.IsBanned,
                Images = u.Images,
                Favourite = u.Favourite,
            }).FirstOrDefault();
            
                return user;
        }

        public int GetUsersCount()
        {
            int count = this.userDbset.All().Count();

            return count;
        }

        //public IEnumerable<PreviewUserViewModel> PreviewUsers()
        //{
        //    var userInfos = userDbset.All().Select(u =>
        //    new PreviewUserViewModel
        //    {
        //        CreationDate = u.CreationDate,
        //        Id = u.Id,
        //        ImageCount = u.Images.Count(),
        //        LastLogin = u.LastLogin,
        //        ProfilePicture = u.ProfilePicture,
        //        Rang = u.Rang,
        //        UserName = u.UserName,                
        //    });

        //    return userInfos;
        //}
        
        public int UserScores(string id)
        {
            int userImagesCount = userDbset.All()
                   .Where(i => i.Id == id).FirstOrDefault().Images.Count();
            var userScores = 0;
            
            if (userImagesCount != 0)
            {
                userScores = (userDbset.All().Include(i => i.Images)
                .Where(i => i.Id == id).Select(i => i.Images.Sum(s => s.Scores)).Sum())/ userImagesCount;
            }
            else
            {
                userScores = 0;
            }

            return userScores;
        }

       public PreviewUsersViewModel PreviewUsers(string input)
       {
            PreviewUsersViewModel themes = new PreviewUsersViewModel();
            
            if (input == null)
            {
                themes.PreviewUsers = userDbset.All().Select(u =>
                new PreviewUserViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    ProfilePicture = u.ProfilePicture,
                    Technique = u.Technique,
                    SelfDescription = u.SelfDescription,
                    ImageCount = u.Images.Count(),
                    Rang = u.Rang,
                    AverageScore = u.AverageScore,
                    CreationDate = u.CreationDate,
                    LastLogin = u.LastLogin,
                    BanLengthDays = u.BanLengthDays,
                    BannedDate = u.BannedDate,
                    IsBanned = u.IsBanned,
                    Images = u.Images,
                    Favourite = u.Favourite,
                });
            }
            else
            {
                themes.PreviewUsers = userDbset.All().Where(n => n.UserName.Contains(input)).Select(u =>
                new PreviewUserViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    ProfilePicture = u.ProfilePicture,
                    Technique = u.Technique,
                    SelfDescription = u.SelfDescription,
                    ImageCount = u.Images.Count(),
                    Rang = u.Rang,
                    AverageScore = u.AverageScore,
                    CreationDate = u.CreationDate,
                    LastLogin = u.LastLogin,
                    BanLengthDays = u.BanLengthDays,
                    BannedDate = u.BannedDate,
                    IsBanned = u.IsBanned,
                    Images = u.Images,
                    Favourite = u.Favourite,
                });
            }

            return themes;
       }
    }
}
