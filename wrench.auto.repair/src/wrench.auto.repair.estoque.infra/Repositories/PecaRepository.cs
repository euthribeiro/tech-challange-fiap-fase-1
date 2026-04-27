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

    public double MovimentaEstoque(Guid idPeca, TipoMovimentacao tipoMovimentacao, double quantidade)
    {
        var peca =  _context.Pecas.FirstOrDefault(p => p.Id == idPeca);

        if (tipoMovimentacao == TipoMovimentacao.Entrada)
        {
            peca.Quantidade += quantidade;
        }

        if (tipoMovimentacao == TipoMovimentacao.Saida)
        {
            peca.Quantidade -= quantidade;
        }
        
        _context.Pecas.Update(peca);
        _context.SaveChanges();
        return peca.Quantidade;
    }
    

    public Peca ConsultaPecaPorId(Guid idPeca)
    {
        var peca =  _context.Pecas.FirstOrDefault(p => p.Id == idPeca);
        return peca;
    }
}