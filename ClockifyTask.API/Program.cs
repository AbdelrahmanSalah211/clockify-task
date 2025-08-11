using Microsoft.EntityFrameworkCore;

// Application Layer
using ClockifyTask.Application.Interfaces;
using ClockifyTask.Application.Services;

// Domain Layer  
using ClockifyTask.Domain.Interfaces;
using ClockifyTask.Domain.Entities;

// Infrastructure Layer
using ClockifyTask.Infrastructure.Persistence;
using ClockifyTask.Infrastructure.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repositories
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITimeEntryRepository, TimeEntryRepository>();
builder.Services.AddScoped<IAssignedTaskRepository, AssignedTaskRepository>();

// Application Services
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITimeEntryService, TimeEntryService>();
builder.Services.AddScoped<IAssignedTaskService, AssignedTaskService>();
builder.Services.AddScoped<IReportService, ReportService>();

// External API Provider
builder.Services.AddScoped<ITrackingApiProvider, ClockifyApiProvider>();
builder.Services.AddHttpClient();

// Configuration
var clockifySettings = builder.Configuration.GetSection("Clockify").Get<ClockifySettings>();
builder.Services.AddSingleton(clockifySettings ?? new ClockifySettings());

// API Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
