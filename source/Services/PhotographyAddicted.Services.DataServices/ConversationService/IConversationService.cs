using PhotographyAddicted.Services.Models.Conversations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices.ConversationService
{
    public interface IConversationService
    {
        Task <PreviewConversationViewModel> PreviewConversation(int id, string recepientId);

        Task DeleteUserConversations(string id);

        PreviewConversationsViewModel PreviewUsersConversations(string userId);
    }
}
