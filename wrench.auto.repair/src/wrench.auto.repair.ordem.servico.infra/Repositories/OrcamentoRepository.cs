using wrench.auto.repair.ordem.servico.domain.Data;
using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.auto.repair.ordem.servico.infra.Context;

namespace wrench.auto.repair.ordem.servico.infra.Repositories
{
    public class OrcamentoRepository : Repository<Orcamento>, IOrcamentoRepository
    {
        private readonly OrdemServicoDbContext _context;

        public OrcamentoRepository(OrdemServicoDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
