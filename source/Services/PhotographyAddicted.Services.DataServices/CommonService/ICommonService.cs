using PhotographyAddicted.Services.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices.CommonService
{
    public interface ICommonService
    {

        Task BecomeModerator(string Id);

        Task StopModeratorRights(string Id);

        Task<bool> IsUserModerator(string Id);

        Task<bool> IsUserAdmin(string Id);

        Task<bool> IsUserBanned(string userName);

        Task BanUser(BanUserViewModel input);

        string CreationDate(DateTime creationDate);
    }
}
