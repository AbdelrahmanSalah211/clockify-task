using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ClockifyTask.Application.Interfaces;
using ClockifyTask.Application.Services;
using ClockifyTask.Domain.Entities;

namespace ClockifyTask.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITimeEntryService, TimeEntryService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IReportService, ReportService>();
            
            // Configure Clockify Settings
            var clockifySettings = configuration.GetSection("Clockify").Get<ClockifySettings>();
            services.AddSingleton(clockifySettings ?? new ClockifySettings());
            
            // Add HttpClient
            services.AddHttpClient();
            
            return services;
        }
    }
}
