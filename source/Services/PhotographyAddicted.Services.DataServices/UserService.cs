using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Services.Models.Images;
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

        
        public async Task<string> AddProfilePicture( EditUserViewModel input, IFormFile ProfilePicture)
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


        public void AddUserLastLogin()
        {
            
           
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
                ImageCount = u.Images.Count(),
                Rang = u.Rang,
                AverageScore = UsersScores(id),
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

        public int UsersImagesCount(string id)
        {
            var usersImages = 0;
            //try
            //{
            //    usersImages = userDbset.All().Include(i => i.Images).Where(i => i.Id == id)
            //    .Select(c => c.Images).Count();
            //}
            //catch (Exception)
            //{

            //}
            usersImages = userDbset.All().Include(i => i.Images).Where(i => i.Id == id)
                .Select(c => c.Images).Count();

            return usersImages;
        }

        public int UsersScores(string id)
        {
            int userImC = userDbset.All().Include(i => i.Images)
                   .Where(i => i.Id == id).FirstOrDefault().Images.Count();
            var userScores = 0;
            
            if (userImC != 0)
            {
                userScores = (userDbset.All().Include(i => i.Images)
                .Where(i => i.Id == id).Select(i => i.Images.Sum(s => s.Scores)).Sum())/userImC;
            }
            else
            {
                userScores = 0;
            }

            return userScores;
        }
    }
}
