using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApp.Application.Services;
using WebApp.Contracts;
using WebApp.Core.Models;
using WebApp.Hubs;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : Controller
    {
        private readonly IMessagesService _messagesService;
        private readonly IHubContext<MessageHub> _hubContext;
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(IMessagesService messagesService,
            IHubContext<MessageHub> hubContext, ILogger<MessagesController> logger) 
        {
            _messagesService = messagesService;
            _hubContext = hubContext;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageInputRequest request)
        {
            try
            {
                _logger.LogInformation("Received SendMessage request with SerialNumber: {SerialNumber}", request.SerialNumber);

                await _hubContext.Clients.All.SendAsync("ReceiveMessage", request.SerialNumber, request.Message, DateTime.Now);

                await _messagesService.CreateMessage(request.SerialNumber, request.Message);

                _logger.LogInformation("Successfully processed SendMessage request");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing SendMessage request");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLastMessagesForTenMinits()
        {
            try
            {
                _logger.LogInformation("Starting GetLastMessagesForTenMinutes operation");

                var messages = await _messagesService.GetMessages();

                _logger.LogInformation("Finished GetLastMessagesForTenMinutes operation");

                return Ok(messages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during GetLastMessagesForTenMinutes operation");
                throw;
            }
        }
    }
}
