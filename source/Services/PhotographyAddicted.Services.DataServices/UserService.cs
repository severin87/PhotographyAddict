using PhotographyAddicted.Data.Common;
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

        public IEnumerable<IndexUserViewModel> GetSpecificUser(int specific)
        { 
            var user = userDbset.All()
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
