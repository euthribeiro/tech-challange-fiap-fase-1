using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.auto.repair.ordem.servico.domain.Interfaces.Repositories;
using wrench.auto.repair.ordem.servico.infra.Context;

namespace wrench.auto.repair.ordem.servico.infra.Repositories
{
    public class OrdemServicoRepository : IOrdemServicoRepository
    {
        private readonly OrdemServicoDbContext _context;

        public OrdemServicoRepository(OrdemServicoDbContext context)
        {
            _context = context;
        }

        public async Task IncluirAsync(OrdemServico ordemServico)
        {
            await _context.OrdemServico.AddAsync(ordemServico);
            await _context.SaveChangesAsync();
        }
    }
}

