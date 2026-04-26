using wrench.auto.repair.cadastro.application.Queries.ViewModels;
using wrench.auto.repair.core.Messages;
using wrench.auto.repair.core.Pagination;

namespace wrench.auto.repair.cadastro.application.Queries
{
    public class ObterTodosVeiculosQuery(RequisicaoPaginada paginacao) : Command<ResultadoPaginado<VeiculoViewModel>>
    {
        public RequisicaoPaginada Paginacao { get; private set; } = paginacao;
    }
}
