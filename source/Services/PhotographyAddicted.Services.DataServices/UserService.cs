using Microsoft.EntityFrameworkCore;
using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Services.Models.Images;
using PhotographyAddicted.Services.Models.Users;
using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotographyAddicted.Services.DataServices
{
    public class UserService : IUserService
    {

        private IRepository<PhotographyAddictedUser> userDbset;

        public UserService(IRepository<PhotographyAddictedUser> userDbset)
        {
            this.userDbset = userDbset;
        }

        public int GetCount()
        {
            int count =this.userDbset.All().Count();

            return count;
        }

        public UserProfileViewModel GetCurrentUserProfile(string id)
        {
            var user = userDbset.All().Include(i => i.Images).Where(i => i.Id == id).Select(u =>
            new UserProfileViewModel
            {
                UserName = u.UserName,
                ProfilePicture = u.ProfilePicture,
                Technique = u.Technique,
                SelfDescription = u.SelfDescription,
                AverageScore = u.AverageScore,
                ImageCount = u.ImageCount,
                Rang = u.Rang,
                CreationDate = u.CreationDate,
                LastLogin = u.LastLogin,
                Blocked = u.Blocked,
                IsBanned = u.IsBanned,
                Images = u.Images
            }).FirstOrDefault();
                                                          
                return user;
        }

        public IEnumerable<IndexUserViewModel> GetSpecificUser(int specific)
        { 
            var users = userDbset.All()
                .OrderBy(g => Guid.NewGuid())
                .Select(
                x => new IndexUserViewModel
                {
                    Name = x.UserName,
                    Password = x.PasswordHash,
                    ThemeTitle = x.Themes.FirstOrDefault().Title
                }).Take(specific).ToList();

            return users; 
        }

        public IEnumerable<ImagePreviewViewModel> GetUsersPictures(string id)
        {
            var photos = userDbset.All().Include(i => i.Images)
                 .Where(i => i.Id == id).SelectMany(l => l.Images)
                 .Select(
                 x => new ImagePreviewViewModel
                 {
                     Title = x.Title,
                     Scores = x.Scores,
                     Picture = x.Picture,
                 }).Take(20).ToList();

            return photos;
        }
    }
}
