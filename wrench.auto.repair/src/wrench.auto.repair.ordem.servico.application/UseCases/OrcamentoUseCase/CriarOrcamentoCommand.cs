using FluentValidation;
using wrench.auto.repair.core.Messages;

namespace wrench.auto.repair.ordem.servico.application.UseCases.OrcamentoUseCase
{
    public class CriarOrcamentoCommand : Command<Guid>
    {
        public Guid OrdemServicoId { get; set; }

        public CriarOrcamentoCommand(Guid ordemServicoId)
        {
            OrdemServicoId = ordemServicoId;
        }

        public override bool EhValido()
        {
            ValidationResult = new CriarOrcamentoCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        public class CriarOrcamentoCommandValidator : AbstractValidator<CriarOrcamentoCommand>
        {
            public CriarOrcamentoCommandValidator()
            {
                RuleFor(c => c.OrdemServicoId)
                    .NotEmpty()
                    .WithMessage("Ordem de serviço não informada.");
           
            }
        }
    }
}
