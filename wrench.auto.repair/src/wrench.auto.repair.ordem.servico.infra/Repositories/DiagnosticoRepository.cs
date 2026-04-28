using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.auto.repair.ordem.servico.domain.Interfaces.Repositories;
using wrench.auto.repair.ordem.servico.infra.Context;

namespace wrench.auto.repair.ordem.servico.infra.Repositories
{
    public class DiagnosticoRepository : IDiagnosticoRepository
    {
        private readonly OrdemServicoDbContext _context;

        public DiagnosticoRepository(OrdemServicoDbContext context)
        {
            _context = context;
        }

        public async Task IncluirAsync(Diagnostico diagnostico)
        {
            await _context.Diagnostico.AddAsync(diagnostico);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Diagnostico diagnostico)
        {
            _context.Diagnostico.Update(diagnostico);
            await _context.SaveChangesAsync();
        }
    }
}
