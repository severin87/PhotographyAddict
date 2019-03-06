using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Services.Models.Home;
using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.DataServices
{
    public class UserService : IUserService
    {
        private IRepository<IndexViewModel> userInfo;

        public UserService(IRepository<IndexViewModel> userInfo)
        {
            this.userInfo = userInfo;
        }

        public IEnumerable<IndexViewModel> GetRandomUser(int random)
        {
            throw new NotImplementedException();
        }
    }
}
