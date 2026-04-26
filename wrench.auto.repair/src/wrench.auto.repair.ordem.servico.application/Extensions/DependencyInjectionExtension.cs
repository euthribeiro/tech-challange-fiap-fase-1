using Microsoft.Extensions.DependencyInjection;

namespace wrench.auto.repair.ordem.servico.application.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddOrdemServicoApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
