using wrench.auto.repair.ordem.servico.domain.Entities;

namespace wrench.auto.repair.ordem.servico.domain.Interfaces.Repositories
{
    public interface IOrdemServicoRepository
    {
        Task IncluirAsync(OrdemServico ordemServico);
    }
}
