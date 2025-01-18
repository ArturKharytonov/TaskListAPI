using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TaskShare.Application.Services;
using TaskShare.Application.Services.Interfaces;

namespace TaskShare.Application.Extensions;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);

        services.AddScoped<ITaskListService, TaskListService>();
        services.AddScoped<ITaskListValidationService, TaskListValidationService>();

        return services;
    }
}