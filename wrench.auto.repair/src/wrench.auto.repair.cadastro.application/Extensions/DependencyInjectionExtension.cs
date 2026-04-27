using Microsoft.Extensions.DependencyInjection;

namespace wrench.auto.repair.cadastro.application.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddCadastroApplication(this IServiceCollection services)
        {
            services.ConfigureAutoMapper();

            services.ConfigureMediator();

            return services;
        }

        private static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(configAction =>
            {
                configAction.AddMaps(typeof(DependencyInjectionExtension).Assembly);
            });
        }

        private static void ConfigureMediator(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(typeof(DependencyInjectionExtension).Assembly);
            });
        }
    }
}
