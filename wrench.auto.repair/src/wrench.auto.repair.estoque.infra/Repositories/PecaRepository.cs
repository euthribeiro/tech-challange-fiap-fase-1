using Microsoft.EntityFrameworkCore;
using wrench.auto.repair.estoque.domain.Data;
using wrench.auto.repair.estoque.domain.Entities;
using wrench.auto.repair.estoque.infra.Context;

namespace wrench.auto.repair.estoque.infra.Repositories;

public class PecaRepository(PecaDbContext _context) : Repository<Peca>(_context), IPecaRepository
{
    public async Task<IEnumerable<Peca>> ConsultaPecaPorNome(string nomePeca)
    {
        return await _context.Pecas.Where(p => p.Nome.Contains(nomePeca)).ToListAsync();
    }
}