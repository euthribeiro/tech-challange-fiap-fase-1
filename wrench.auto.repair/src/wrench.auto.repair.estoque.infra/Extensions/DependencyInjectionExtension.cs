using Microsoft.Extensions.DependencyInjection;

namespace wrench.auto.repair.estoque.infra.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddEstoqueInfra(this IServiceCollection services)
        {
            return services;
        }
    }
}
