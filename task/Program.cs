using Microsoft.EntityFrameworkCore;
using task.Data;
using task.Repositories;
using task.Services;
using task.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseMySql(builder.Configuration.GetConnectionString("Default"),
  ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Default"))));

// Register Clockify settings
builder.Services.Configure<ClockifySettings>(
  builder.Configuration.GetSection("Clockify"));

builder.Services.AddSingleton(sp =>
  builder.Configuration.GetSection("Clockify").Get<ClockifySettings>());


builder.Services.AddHttpClient<ClockifyService>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();

