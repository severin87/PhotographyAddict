using PhotographyAddicted.Services.Models.Home;
using PhotographyAddicted.Web.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotographyAddicted.Services.DataServices
{
    public interface IUserService
    {
        IEnumerable<IndexUserViewModel> GetSpecificUser(int random);

        int GetCount();
    }
}
