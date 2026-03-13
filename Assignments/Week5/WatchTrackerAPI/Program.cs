using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WatchTrackerAPI.Data;
using WatchTrackerAPI.Interfaces;
using WatchTrackerAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDBContext>(options => options.UseMySql(
    connectionString,
    ServerVersion.AutoDetect(connectionString)
    ));

builder.Services.AddScoped<IMediaService, MediaService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMediaProgressService, MediaProgressService>();
builder.Services.AddScoped<IUserStatsService, UserStatsService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
