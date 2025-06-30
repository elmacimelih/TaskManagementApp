using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services;

namespace TaskManagement.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,IConfiguration configuration)
        {

            // Business katmanındaki servis kaydı
            services.AddScoped<ITaskService, TaskService>();

            return services;
        }
    }
}
