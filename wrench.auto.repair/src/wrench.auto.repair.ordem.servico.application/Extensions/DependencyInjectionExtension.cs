using Microsoft.Extensions.DependencyInjection;

namespace wrench.auto.repair.ordem.servico.application.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddOrdemServicoApplication(this IServiceCollection services)
        {
            ConfigureMediator(services);

            return services;
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
