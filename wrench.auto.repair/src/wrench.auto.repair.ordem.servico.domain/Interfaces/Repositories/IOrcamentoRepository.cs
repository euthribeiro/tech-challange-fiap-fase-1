using wrench.auto.repair.ordem.servico.domain.Entities;

namespace wrench.auto.repair.ordem.servico.domain.Interfaces.Repositories
{
    public interface IOrcamentoRepository
    {
        Task IncluirOrcamento(Orcamento orcamento);
    }
}
