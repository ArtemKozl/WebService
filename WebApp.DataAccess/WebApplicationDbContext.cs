using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using WebApp.DataAccess.Entities;

namespace WebApp.DataAccess
{
    public class WebApplicationDbContext : DbContext
    {

        public WebApplicationDbContext(DbContextOptions<WebApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MessagesEntity> Messages { get; set; }
    }
}
