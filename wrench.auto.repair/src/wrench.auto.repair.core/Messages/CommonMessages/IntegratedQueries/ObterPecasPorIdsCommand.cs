using FluentValidation;
using wrench.auto.repair.core.Messages.CommonMessages.IntegratedQueries.Dtos;

namespace wrench.auto.repair.core.Messages.CommonMessages.IntegratedQueries
{
    public class ObterPecasPorIdsCommand(IEnumerable<Guid> pecaIds) : IntegratedQuery<IEnumerable<PecaDto>>
    {
        public IEnumerable<Guid> PecaIds { get; private set; } = pecaIds;

        public override bool EhValido()
        {
            ValidationResult = new ObterPecasPorIdsQueryValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ObterPecasPorIdsQueryValidator : AbstractValidator<ObterPecasPorIdsCommand>
    {
        public ObterPecasPorIdsQueryValidator()
        {

        }
    }
}
