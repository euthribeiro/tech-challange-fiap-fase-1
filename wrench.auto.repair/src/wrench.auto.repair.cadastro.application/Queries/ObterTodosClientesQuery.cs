using FluentValidation;
using wrench.auto.repair.cadastro.application.Queries.ViewModels;
using wrench.auto.repair.core.Messages;
using wrench.auto.repair.core.Pagination;

namespace wrench.auto.repair.cadastro.application.Queries
{
    public class ObterTodosClientesQuery(RequisicaoPaginada paginacao) : Command<ResultadoPaginado<ClienteViewModel>>
    {
        public RequisicaoPaginada Paginacao { get; private set; } = paginacao;

        public override bool EhValido()
        {
            ValidationResult = new ObterTodosClientesQueryValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ObterTodosClientesQueryValidator : AbstractValidator<ObterTodosClientesQuery>
    {
        public ObterTodosClientesQueryValidator()
        {

        }
    }
}
