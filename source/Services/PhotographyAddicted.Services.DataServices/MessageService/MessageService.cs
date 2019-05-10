using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Data.Models;
using PhotographyAddicted.Services.Models.Messages;
using PhotographyAddicted.Web.Areas.Identity.Data;

namespace PhotographyAddicted.Services.DataServices.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message> messageDbSet;

        private readonly IRepository<Conversation> conversationDbSet;

        private IRepository<PhotographyAddictedUser> userDbset;

        public MessageService(IRepository<Message> messageDbSet, IRepository<Conversation> conversationDbSet, IRepository<PhotographyAddictedUser> userDbset)
        {
            this.messageDbSet = messageDbSet;

            this.conversationDbSet = conversationDbSet;

            this.userDbset = userDbset;
        }

        public async Task<int> AddMessageAsync(AddMessageViewModel input)
        {
            var message = new Message()
            {
                CreationDate = DateTime.UtcNow,
                Text = input.Text,
                RecepientId = input.RecepientId,
                PhotographyAddictedUserId = input.PhotographyAddictedUserId,
            };

            var isConversation = conversationDbSet.All()
                .Where(i => (i.SenderPhotographyAddictedUser.Id == input.PhotographyAddictedUserId && i.RecepientPhotographyAddictedUser.Id == input.RecepientId)
                || (i.SenderPhotographyAddictedUser.Id == input.RecepientId && i.RecepientPhotographyAddictedUser.Id == input.PhotographyAddictedUserId)).FirstOrDefault();

            if (isConversation == null)
            {
                var conversation = new Conversation()
                {
                    CreationDate = DateTime.UtcNow,
                    RecepientPhotographyAddictedUser = userDbset.All().Where(u => u.Id == input.RecepientId).First(),
                    SenderPhotographyAddictedUser = userDbset.All().Where(u => u.Id == input.PhotographyAddictedUserId).First(),
                };

                conversation.Messages.Add(message);
                await conversationDbSet.AddAsync(conversation);
                await conversationDbSet.SaveChangesAsync();
                return conversation.Id;
            }
            else
            {
                isConversation.Messages.Add(message);
               // await conversationDbSet.AddAsync(isConversation);
                await conversationDbSet.SaveChangesAsync();

                return (int)message.ConversationId;
            }
        }

        public async Task DeleteUserMessages(string id)
        {
            var userMessages = messageDbSet.All().Where(x => x.RecepientId == id || x.PhotographyAddictedUserId == id)
                .ToList();

            foreach (var userMessage in userMessages)
            {
                messageDbSet.Delete(userMessage);
            }

            await messageDbSet.SaveChangesAsync();
        }

        public int Unreaded(string userId)
        {
            var countUnreaded = messageDbSet.All().Where(m => m.RecepientId == userId && m.IsReaded == false).Count();

            return countUnreaded;
        }
    }
}
