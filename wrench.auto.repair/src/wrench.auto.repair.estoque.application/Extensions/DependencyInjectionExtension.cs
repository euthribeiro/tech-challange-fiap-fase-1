using Microsoft.Extensions.DependencyInjection;

namespace wrench.auto.repair.estoque.application.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddEstoqueApplication(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjectionExtension).Assembly));

            services.AddAutoMapper(configAction =>
            {
                configAction.AddMaps(typeof(DependencyInjectionExtension).Assembly);
            });

            return services;
        }
    }
}
