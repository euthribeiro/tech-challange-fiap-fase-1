using wrench.auto.repair.autenticacao.application.Queries.ViewModels;
using wrench.auto.repair.core.Messages;
using wrench.auto.repair.core.Pagination;

namespace wrench.auto.repair.autenticacao.application.Queries
{
    public class ObterTodosUsuariosQuery(RequisicaoPaginada paginacao) : Command<ResultadoPaginado<UsuarioViewModel>>
    {
        public RequisicaoPaginada Paginacao { get; private set; } = paginacao;

        public override bool EhValido()
        {
            // Objeto Paginação não precisa ser validado
            // Alterar conforme novas propriedades
            return true;
        }
    }
}
