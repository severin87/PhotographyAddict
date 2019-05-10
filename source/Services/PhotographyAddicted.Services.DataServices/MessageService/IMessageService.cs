using PhotographyAddicted.Services.Models.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices.MessageService
{
    public interface IMessageService
    {
        Task<int> AddMessageAsync(AddMessageViewModel input);

        Task DeleteUserMessages(string id);

        int Unreaded(string userId);
    }
}
