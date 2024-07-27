using TaskManagerSample.API.Extensions;
using TaskManagerSample.Core.Intefaces;
using TaskManagerSample.Core.Notifications;
using TaskManagerSample.Infrastructure.Context;
using TaskManagerSample.Infrastructure.Repository;
using TaskManagerSample.Infrastructure.Service;

namespace TaskManagerSample.API.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<TaskManagerDbContext>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<INotifier, Notifier>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IUserService, UserService>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, AspNetUser>();

        //services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        return services;
    }
}