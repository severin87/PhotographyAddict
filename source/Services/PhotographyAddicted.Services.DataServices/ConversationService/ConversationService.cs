using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Services.Models.Conversations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Services.DataServices.ConversationService
{
    public class ConversationService : IConversationService
    {
        private IRepository<Conversation> conversationDbSet;

        public ConversationService(IRepository<Conversation> conversationDbSet)
        {
            this.conversationDbSet = conversationDbSet;
        }

        public async Task DeleteUserConversations(string id)
        {
            var userConversations = conversationDbSet.All().Where(x => x.SenderPhotographyAddictedUser.Id == id || x.RecepientPhotographyAddictedUser.Id == id)
                .ToList();

            foreach (var userConversation in userConversations)
            {
                conversationDbSet.Delete(userConversation);
            }

            await conversationDbSet.SaveChangesAsync();
        }

        public async Task<PreviewConversationViewModel> PreviewConversation(int id, string recepientId)
        {
            var conversation = conversationDbSet.All().Where(x => x.Id == id).Select(m =>
            new PreviewConversationViewModel
            {
                Id = m.Id,
                CreationDate = m.CreationDate,
                Messages = m.Messages,
                RecepientPhotographyAddictedUser = m.RecepientPhotographyAddictedUser,
                SenderPhotographyAddictedUser = m.SenderPhotographyAddictedUser,
                Subject = m.Subject,
            }).FirstOrDefault();
            var mesRec = conversation.Messages.Where(m => m.RecepientId == recepientId).ToList();

            foreach (var item in mesRec)
            {
                item.IsReaded = true;
            }

            await conversationDbSet.SaveChangesAsync();

            return conversation;
        }

        //public PreviewConversationViewModel PreviewConversation(int id)
        //{
        //    var conversation = conversationDbSet.All().Where(x => x.Id == id).Select(m =>
        //    new PreviewConversationViewModel
        //    {
        //        Id = m.Id,
        //        CreationDate = m.CreationDate,
        //        Messages = m.Messages,
        //        RecepientPhotographyAddictedUser = m.RecepientPhotographyAddictedUser,
        //        SenderPhotographyAddictedUser = m.SenderPhotographyAddictedUser,
        //        Subject = m.Subject,
        //    }).FirstOrDefault();

        //    return conversation;
        //}

        public PreviewConversationsViewModel PreviewUsersConversations(string userId)
        {
            var conversations = conversationDbSet.All().Where(i => i.SenderPhotographyAddictedUser.Id == userId || i.RecepientPhotographyAddictedUser.Id == userId)
                .Select(p => new PreviewConversationViewModel
                {
                    Id = p.Id,
                    CreationDate = p.CreationDate,
                    Messages = p.Messages,
                    Subject = p.Subject,
                    SenderPhotographyAddictedUser = p.SenderPhotographyAddictedUser,
                    RecepientPhotographyAddictedUser = p.RecepientPhotographyAddictedUser,
                })
                .ToList();

            var userConversations = new PreviewConversationsViewModel()
            {
                PreviewConversations = conversations,
            };

            return userConversations;
        }       
    }
}
