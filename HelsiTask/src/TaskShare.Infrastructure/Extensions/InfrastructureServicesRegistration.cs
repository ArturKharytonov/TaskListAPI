using Microsoft.Extensions.DependencyInjection;
using TaskShare.Core.Repositories;
using TaskShare.Infrastructure.Data;
using TaskShare.Infrastructure.Repositories;

namespace TaskShare.Infrastructure.Extensions;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<TaskShareDbContext>();
        serviceCollection.AddScoped<ITaskListRepository, TaskListRepository>();

        return serviceCollection;
    }
}