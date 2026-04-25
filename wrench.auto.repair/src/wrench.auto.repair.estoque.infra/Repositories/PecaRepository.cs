using wrench.auto.repair.estoque.domain.Entities;
using wrench.auto.repair.estoque.domain.Interfaces.Repositories;
using wrench.auto.repair.estoque.infra.Context;

namespace wrench.auto.repair.estoque.infra.Repositories;

public class PecaRepository : IPecaRepository
{
    private readonly PecaDbContext _context;

    public PecaRepository(PecaDbContext context)
    {
        _context = context;
    }
    public async Task CriarPeca(Peca peca)
    {
        await _context.Pecas.AddAsync(peca);
        await _context.SaveChangesAsync();
    }

    public void DeletaPeca(Guid idPeca)
    {
        var peca = _context.Pecas.FirstOrDefault(p => p.Id == idPeca);
        _context.Pecas.Remove(peca);
        _context.SaveChanges();
    }
}