using wrench.auto.repair.core.Data;
using wrench.auto.repair.estoque.domain.Entities;

namespace wrench.auto.repair.estoque.domain.Data;

public interface IPecaRepository : IRepository<Peca>
{
    Task<IEnumerable<Peca>> ConsultaPecaPorNome(string nomePeca);
}