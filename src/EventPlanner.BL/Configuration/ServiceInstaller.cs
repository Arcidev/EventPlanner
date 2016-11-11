using System;
using Microsoft.Extensions.DependencyInjection;
using EventPlanner.DAL.DataAccess;
using EventPlanner.DAL.DataAccess.Interfaces;

namespace EventPlanner.BL.Configuration
{
    public static class ServiceInstaller
    {
        public static IServiceCollection ConfigureBLServices(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
