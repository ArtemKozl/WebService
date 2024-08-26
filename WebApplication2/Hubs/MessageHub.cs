using Microsoft.AspNetCore.SignalR;

namespace WebApp.Hubs
{
    public class MessageHub : Hub
    {
        private readonly ILogger<MessageHub> _logger;

        public MessageHub(ILogger<MessageHub> logger)
        {
            _logger = logger;
        }
        public async Task SendMessage(int serialNumber, string message, DateTime time)
        {
            try
            {
                _logger.LogInformation("Sending message through SignalR for SerialNumber: {SerialNumber}", serialNumber);

                await Clients.All.SendAsync("ReceiveMessage", serialNumber, message, time);

                _logger.LogInformation("Message successfully sent through SignalR for SerialNumber: {SerialNumber}, Message: {Message}, Time: {Time}",
                    serialNumber, message, time);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending message through SignalR for SerialNumber: {SerialNumber}, Message: {Message}, Time: {Time}",
                    serialNumber, message, time);
                throw;
            }
        }
    }
}
