using WebApp.Core.Models;
using WebApp.DataAccess.Repositories;

namespace WebApp.Application.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly IMessagesRepository _messagesRepository;
        public MessagesService(IMessagesRepository messagesRepository)
        {
            _messagesRepository = messagesRepository;
        }

        public async Task CreateMessage(int serialNumber, string message)
        {
            await _messagesRepository.Create(serialNumber, message);
        }

        public async Task<object> GetMessages()
        {
            return await _messagesRepository.GetLastMessagesForTenMin();
        }
    }
}
