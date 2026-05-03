using Microsoft.Extensions.DependencyInjection;

namespace wrench.auto.repair.ordem.servico.application.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddOrdemServicoApplication(this IServiceCollection services)
        {
            services.ConfigureMediator();
            services.ConfigureAutoMapper();

            return services;
        }


        private static void ConfigureMediator(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(typeof(DependencyInjectionExtension).Assembly);
            });
        }

        private static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(configAction =>
            {
                configAction.AddMaps(typeof(DependencyInjectionExtension).Assembly);
            });
        }
    }
}
