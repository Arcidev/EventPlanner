using EventPlanner.BL.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Tests.BL
{
    public abstract class BaseFacadeTests
    {
        private IServiceCollection services;
        protected IServiceProvider serviceProvider;

        public BaseFacadeTests()
        {
            EventPlanner.BL.Configuration.AutoMapper.Init();
            services = new ServiceCollection();
            services.ConfigureBLServices();
            ConfigureFacade(services);
            serviceProvider = services.BuildServiceProvider();
        }

        protected abstract void ConfigureFacade(IServiceCollection services);
    }
}
