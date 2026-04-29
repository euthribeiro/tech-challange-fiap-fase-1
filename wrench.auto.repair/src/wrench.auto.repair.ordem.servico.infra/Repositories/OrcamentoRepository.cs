using System;
using System.Collections.Generic;
using System.Text;
using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.auto.repair.ordem.servico.domain.Interfaces.Repositories;
using wrench.auto.repair.ordem.servico.infra.Context;

namespace wrench.auto.repair.ordem.servico.infra.Repositories
{
    public class OrcamentoRepository : IOrcamentoRepository
    {
        private readonly OrdemServicoDbContext _context;

        public OrcamentoRepository(OrdemServicoDbContext context)
        {
            _context = context;
        }

        public async Task IncluirOrcamento(Orcamento orcamento)
        {
            await _context.Orcamento.AddAsync(orcamento);
            await _context.SaveChangesAsync();
        }
    }
}
