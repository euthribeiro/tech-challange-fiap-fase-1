using Microsoft.Extensions.DependencyInjection;
using wrench.auto.repair.ordem.servico.domain.Interfaces.Repositories;
using wrench.auto.repair.ordem.servico.infra.Repositories;

namespace wrench.auto.repair.ordem.servico.infra.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddOrdemServicoInfra(this IServiceCollection services)
        {
            services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();
            services.AddScoped<IDiagnosticoRepository, DiagnosticoRepository>();
            services.AddScoped<IOrcamentoRepository, OrcamentoRepository>();
        }
    }
}
