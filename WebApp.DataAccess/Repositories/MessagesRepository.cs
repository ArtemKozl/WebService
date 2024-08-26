using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using WebApp.Core.Models;


namespace WebApp.DataAccess.Repositories
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly WebApplicationDbContext _context;
        private readonly ILogger<MessagesRepository> _logger;
        public MessagesRepository(WebApplicationDbContext context, ILogger<MessagesRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Create(int serialNumberDb, string messageDb)
        {
            try
            {
                _logger.LogInformation("Creating new message for SerialNumber: {SerialNumberDb}", serialNumberDb);

                var sqlQuery = @"INSERT INTO ""Messages"" (serialnumber, message, ""sendTime"") VALUES (@serialnumber, @message, @sendTime)";
                var parameters = new[]
                {
                new NpgsqlParameter("serialnumber", serialNumberDb),
                new NpgsqlParameter("message", messageDb),
                new NpgsqlParameter("sendTime", DateTime.Now)
            };

                await _context.Database.ExecuteSqlRawAsync(sqlQuery, parameters);

                _logger.LogInformation("New message successfully created for SerialNumber: {SerialNumberDb}", serialNumberDb);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating message for SerialNumber: {SerialNumberDb}", serialNumberDb);
                throw;
            }
        }

        public async Task<object> GetLastMessagesForTenMin()
        {
            try
            {
                _logger.LogInformation("Starting operation to retrieve last messages for 10 minutes");

                var chatGroupIds = await _context.Messages
                    .FromSqlRaw("SELECT * FROM \"Messages\" WHERE \"Messages\".\"sendTime\" >= NOW() - INTERVAL '10 minutes' ORDER BY \"Messages\".\"sendTime\" DESC")
                    .ToListAsync();

                _logger.LogInformation($"Retrieved {chatGroupIds.Count} messages for the last 10 minutes");

                return chatGroupIds;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during operation to retrieve last messages for 10 minutes");
                throw;
            }
        }
    }
}
