using FluentValidation;
using wrench.auto.repair.core.Messages;

namespace wrench.auto.repair.ordem.servico.application.UseCases.DiagnosticoUseCase
{
    public class SolicitarDiagnosticoCommand : Command<Guid>
    {
        public Guid OrdemServicoId { get; set; }
        
        public override bool EhValido()
        {
            var validator = new CriarDiagnosticoCommandValidator();
            var result = validator.Validate(this);
            return result.IsValid;
        }

        public class CriarDiagnosticoCommandValidator : AbstractValidator<SolicitarDiagnosticoCommand>
        {
            public CriarDiagnosticoCommandValidator()
            {
                RuleFor(c => c.OrdemServicoId)
                    .NotEmpty()
                    .WithMessage("Ordem de serviço não informada.");
             
            }
        }
    }
}               