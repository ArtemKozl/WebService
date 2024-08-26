
namespace WebApp.Application.Services
{
    public interface IMessagesService
    {
        Task CreateMessage(int serialNumber, string message);
        Task<object> GetMessages();
    }
}