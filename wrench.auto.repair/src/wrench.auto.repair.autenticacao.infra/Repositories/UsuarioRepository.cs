using wrench.auto.repair.autenticacao.domain.Data;
using wrench.auto.repair.autenticacao.domain.Entities;

namespace wrench.auto.repair.autenticacao.infra.Repositories
{
    public sealed class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AutenticacaoContext context) :
            base(context)
        {

        }
    }
}
