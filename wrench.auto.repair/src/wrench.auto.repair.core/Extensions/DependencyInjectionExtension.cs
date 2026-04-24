using Microsoft.Extensions.DependencyInjection;
using wrench.auto.repair.core.Mediator;

namespace wrench.auto.repair.core.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            return services;
        }
    }
}
