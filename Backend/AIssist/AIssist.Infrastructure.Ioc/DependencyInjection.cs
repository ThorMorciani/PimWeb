using AIssist.Application.Services;
using AIssist.Application.Services.Interfaces;
using AIssist.Domain.Services;
using AIssist.Domain.Services.Interfaces;
using AIssist.Infrastructure.Ioc.Configs.AutoMap;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AIssist.Infrastructure.Ioc
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            AddApplicationServices(services);
            AddDomainServices(services);
            AddAutoMapperConfig(services);
        }

        private static void AddAutoMapperConfig(IServiceCollection services)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging() // Add logging services
                .BuildServiceProvider();

            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
                mc.AddProfile(new MappingUser());
                mc.AddProfile(new MappingRootCause());
                mc.AddProfile(new MappingFunctionality());
            }, loggerFactory);

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private static void AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IProfileAppService, ProfileAppService>();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IRootCauseAppService, RootCauseAppService>();
            services.AddScoped<IFunctionalityAppService, FunctionalityAppService>();
            services.AddScoped<IStatusAppService, StatusAppService>();
        }

        private static void AddDomainServices(IServiceCollection services)
        {
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRootCauseService, RootCauseService>();
            services.AddScoped<IFunctionalityService, FunctionalityService>();
            services.AddScoped<IStatusService, StatusService>();
        }
    }
}

