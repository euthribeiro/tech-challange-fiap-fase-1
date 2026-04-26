using Microsoft.Extensions.DependencyInjection;

namespace wrench.auto.repair.cadastro.infra.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddCadastroInfra(this IServiceCollection services)
        {
            return services;
        }
    }
}
