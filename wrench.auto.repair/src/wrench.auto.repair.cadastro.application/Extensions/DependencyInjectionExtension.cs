using Microsoft.Extensions.DependencyInjection;

namespace wrench.auto.repair.cadastro.application.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddCadastroApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
