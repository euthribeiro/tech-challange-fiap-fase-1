using Microsoft.EntityFrameworkCore;
using wrench.auto.repair.autenticacao.domain.Data;
using wrench.auto.repair.autenticacao.domain.Entities;
using wrench.auto.repair.core.ValueObjects;

namespace wrench.auto.repair.autenticacao.infra.Repositories
{
    public sealed class UsuarioRepository(AutenticacaoContext _context) :
        Repository<Usuario>(_context), IUsuarioRepository
    {
        public async Task<Usuario?> ObterPorEmailAsync(Email email, CancellationToken cancellationToken)
        {
            return await DbSet.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<Perfil?> ObterPerfilPorIdAsync(Guid id)
        {
            return await _context.Perfis.FindAsync(id);
        }

        public async Task<IEnumerable<Perfil>> ObterTodosPerfis(CancellationToken cancellationToken)
        {
            return await _context.Perfis.ToListAsync();
        }
    }
}
