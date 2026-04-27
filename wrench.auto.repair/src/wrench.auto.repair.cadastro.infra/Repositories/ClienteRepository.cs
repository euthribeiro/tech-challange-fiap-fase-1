using Microsoft.EntityFrameworkCore;
using wrench.auto.repair.cadastro.domain.Data;
using wrench.auto.repair.cadastro.domain.Entities;

namespace wrench.auto.repair.cadastro.infra.Repositories
{
    public class ClienteRepository(CadastroContext _context) :
        Repository<Cliente>(_context), IClienteRepository
    {
        public async Task<Cliente?> ObterPorDocumentAsync(string documento, CancellationToken cancellationToken)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.Documento.Numeracao == documento);
        }
    }
}
