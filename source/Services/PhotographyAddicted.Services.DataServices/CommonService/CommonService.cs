using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Services.Models.Common;
using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices.CommonService
{
    public class CommonService : ICommonService
    {
        private readonly UserManager<PhotographyAddictedUser> userManager;
        private readonly IRepository<PhotographyAddictedUser> userDbset;
        private readonly SignInManager<PhotographyAddictedUser> _signInManager;

        public CommonService( UserManager<PhotographyAddictedUser> userManager, IRepository<PhotographyAddictedUser> userDbset
            , SignInManager<PhotographyAddictedUser> signInManager)
        {
            this.userManager = userManager;
            this.userDbset = userDbset;
            _signInManager = signInManager;
        }
        
        public async Task BecomeModerator(string Id)
        {
            var user = userDbset.All().Where(i => i.Id == Id).FirstOrDefault();
            await userManager.AddToRoleAsync(user, "Moderator");
        }
        
        public async Task StopModeratorRights(string Id)
        {
            var user = userDbset.All().Where(i => i.Id == Id).FirstOrDefault();
            await userManager.RemoveFromRoleAsync(user, "Moderator");
        }
        
        public async Task<bool> IsUserModerator(string Id)
        {
            var user = userDbset.All().Where(i => i.Id == Id).FirstOrDefault();

            bool isUserModerator = await userManager.IsInRoleAsync(user, "Moderator");

            return isUserModerator;
        }

        public async Task<bool> IsUserBanned(string userName)
        {
            if (userName != null)
            {
                var signedUser = await _signInManager.UserManager.FindByNameAsync(userName);

                if (signedUser.IsBanned)
                {
                    await _signInManager.SignOutAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public async Task<bool> IsUserAdmin(string Id)
        {
            var user = userDbset.All().Where(i => i.Id == Id).FirstOrDefault();

            bool isUserModerator = await userManager.IsInRoleAsync(user, "Admin");

            return isUserModerator;
        }

        public async Task BanUser(BanUserViewModel input)
        {
            var user = userDbset.All().SingleOrDefault(t => t.Id == input.UserId);

            user.BanLengthDays = input.BanLengthDays;
            user.BannedDate = DateTime.UtcNow;
            if (input.IsBanned)
            {
                user.IsBanned = false;
            }
            else
            {
                user.IsBanned = true;
            }
           
            await userDbset.SaveChangesAsync();
        }

        public string CreationDate(DateTime creationDate)
        {

            string formatedCreationDate = String.Format("{0:d}", creationDate);
            string formatedNowDate = String.Format("{0:d}", DateTime.UtcNow);

            if (formatedNowDate == formatedCreationDate)
            {
                string creationHour = String.Format("{0:t}", creationDate);

                return creationHour;
            }

            return formatedCreationDate;
        }
    }
}
