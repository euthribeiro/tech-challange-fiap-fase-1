using wrench.auto.repair.ordem.servico.domain.Entities;

namespace wrench.auto.repair.ordem.servico.domain.Interfaces.Repositories
{
    public interface IOrdemServicoRepository
    {
        Task IncluirAsync(OrdemServico ordemServico);
        Task AtualizarAsync(OrdemServico ordemServico);
        Task<OrdemServico> ObterPorIdAsync(Guid id);
    }
}
