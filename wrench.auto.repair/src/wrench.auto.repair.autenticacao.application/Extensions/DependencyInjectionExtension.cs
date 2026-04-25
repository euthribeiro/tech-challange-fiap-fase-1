using Microsoft.Extensions.DependencyInjection;

namespace wrench.auto.repair.autenticacao.application.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddAutenticacaoApplication(this IServiceCollection services)
        {
            ConfigureMediator(services);
            ConfigureAutoMapper(services);

            return services;
        }

        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(configAction =>
            {
                configAction.AddMaps(typeof(DependencyInjectionExtension).Assembly);
            });
        }

        private static void ConfigureMediator(IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(typeof(DependencyInjectionExtension).Assembly);
            });
        }
    }
}
