using wrench.auto.repair.ordem.servico.domain.Data;
using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.auto.repair.ordem.servico.infra.Context;

namespace wrench.auto.repair.ordem.servico.infra.Repositories
{
    public class OrdemServicoRepository : Repository<OrdemServico>, IOrdemServicoRepository
    {
        private readonly OrdemServicoDbContext _context;

        public OrdemServicoRepository(OrdemServicoDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}

