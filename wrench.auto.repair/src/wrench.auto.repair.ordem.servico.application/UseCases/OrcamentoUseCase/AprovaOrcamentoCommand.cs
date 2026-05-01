using FluentValidation;
using wrench.auto.repair.core.Messages;

namespace wrench.auto.repair.ordem.servico.application.UseCases.OrcamentoUseCase
{
    public class AprovaOrcamentoCommand : Command<bool>
    {
        public Guid OrdemServicoId { get; set; }

        public AprovaOrcamentoCommand(Guid ordemServicoId)
        {
            OrdemServicoId = ordemServicoId;
        }

        public override bool EhValido()
        {
            ValidationResult = new AprovaOrcamentoCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AprovaOrcamentoCommandValidator : AbstractValidator<AprovaOrcamentoCommand>
        {
            public AprovaOrcamentoCommandValidator()
            {
                RuleFor(c => c.OrdemServicoId)
                    .NotEmpty()
                    .WithMessage("Ordem de serviço não informada.");
           
            }
        }
    }
}
