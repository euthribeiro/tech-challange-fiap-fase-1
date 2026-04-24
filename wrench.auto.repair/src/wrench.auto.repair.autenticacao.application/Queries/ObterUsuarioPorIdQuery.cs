using wrench.auto.repair.autenticacao.application.Queries.ViewModels;
using wrench.auto.repair.core.Messages;

namespace wrench.auto.repair.autenticacao.application.Queries
{
    public class ObterUsuarioPorIdQuery(Guid id) : Command<UsuarioViewModel>
    {
        public Guid Id { get; private set; } = id;
    }
}
