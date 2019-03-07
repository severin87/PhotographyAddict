using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Services.Models.Home;
using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotographyAddicted.Services.DataServices
{
    public class UserService : IUserService
    {
        private IRepository<PhotographyAddictedUser> userInfo;

        public UserService(IRepository<PhotographyAddictedUser> userInfo)
        {
            this.userInfo = userInfo;
        }

        public int GetCount()
        {
            int count =this.userInfo.All().Count();

            return count;
        }

        public IEnumerable<IndexUserViewModel> GetSpecificUser(int specific)
        {
            var user = userInfo.All()
                .OrderBy(g => Guid.NewGuid())
                .Select(
                x => new IndexUserViewModel
                {
                    Name = x.UserName,
                    Password = x.PasswordHash,
                    ThemeTitle = x.Themes.FirstOrDefault().Title
                }).Take(specific).ToList();

            return user;
        }
    }
}
