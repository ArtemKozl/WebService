using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApp.Application.Services;
using WebApp.DataAccess;
using WebApp.DataAccess.Repositories;
using WebApp.Hubs;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Services.AddDbContext<WebApplicationDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(WebApplicationDbContext)));
    });

builder.Services.AddLogging(builder => builder.AddConsole());

builder.Services.AddScoped<IMessagesRepository, MessagesRepository>();
builder.Services.AddScoped<IMessagesService, MessagesService>();

builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Service V1");
    });
}

app.MapHub<MessageHub>("/messagehub");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
