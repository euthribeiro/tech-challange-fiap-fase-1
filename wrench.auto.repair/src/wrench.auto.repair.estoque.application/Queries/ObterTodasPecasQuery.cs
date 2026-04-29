using FluentValidation;
using wrench.auto.repair.core.Messages;
using wrench.auto.repair.core.Pagination;
using wrench.auto.repair.estoque.application.Queries.ViewModels;

namespace wrench.auto.repair.estoque.application.Queries
{
    public class ObterTodasPecasQuery(RequisicaoPaginada paginacao) : Command<ResultadoPaginado<PecaViewModel>>
    {
        public RequisicaoPaginada Paginacao { get; private set; } = paginacao;

        public override bool EhValido()
        {
            ValidationResult = new ObterTodasPecasQueryValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ObterTodasPecasQueryValidator : AbstractValidator<ObterTodasPecasQuery>
    {
        public ObterTodasPecasQueryValidator()
        {

        }
    }
}
