using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using ClockifyTask.Application.Interfaces;
using ClockifyTask.Application.Services;
using ClockifyTask.Domain.Interfaces;
using ClockifyTask.Infrastructure.Persistence;
using ClockifyTask.Infrastructure.Providers;
using ClockifyTask.Domain.Entities;

namespace ClockifyTask.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))));

            services.AddScoped<ITrackingApiProvider, ClockifyApiProvider>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITimeEntryRepository, TimeEntryRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddHttpClient();

            // This should be in Infrastructure/DependencyInjection.cs
            var clockifySettings = configuration.GetSection("Clockify").Get<ClockifySettings>();
            services.AddSingleton(clockifySettings ?? new ClockifySettings());

            return services;
        }
    }
}
