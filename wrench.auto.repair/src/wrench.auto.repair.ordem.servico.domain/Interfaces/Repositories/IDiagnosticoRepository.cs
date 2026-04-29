using wrench.auto.repair.ordem.servico.domain.Entities;

namespace wrench.auto.repair.ordem.servico.domain.Interfaces.Repositories
{
    public interface IDiagnosticoRepository
    {
        Task IncluirAsync(Diagnostico diagnostico);

        Task AtualizarAsync(Diagnostico diagnostico);
    }
}
