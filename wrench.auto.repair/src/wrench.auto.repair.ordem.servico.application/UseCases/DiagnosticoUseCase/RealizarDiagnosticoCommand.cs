using FluentValidation;
using wrench.auto.repair.core.Messages;

namespace wrench.auto.repair.ordem.servico.application.UseCases.DiagnosticoUseCase
{
    public class RealizarDiagnosticoCommand : Command<Guid>
    {
        public RealizarDiagnosticoCommand(Guid ordemServicoId, decimal valorEstimado, string solucaoProposta)
        {
            OrdemServicoId = ordemServicoId;
            ValorEstimado = valorEstimado;
            SolucaoProposta = solucaoProposta;
        }

        public Guid OrdemServicoId { get; set; }
        public decimal ValorEstimado { get; set; }
        public string SolucaoProposta { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new RealizarDiagnosticoCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }

        public class RealizarDiagnosticoCommandValidator : AbstractValidator<RealizarDiagnosticoCommand>
        {
            public RealizarDiagnosticoCommandValidator()
            {
                RuleFor(c => c.OrdemServicoId)
                    .NotEmpty()
                    .WithMessage("Ordem de serviço não informada.");

                RuleFor(c => c.SolucaoProposta)
                    .NotEmpty()
                    .WithMessage("Solução proposta não informada.");

                RuleFor(c => c.ValorEstimado)
                    .GreaterThan(0)
                    .WithMessage("Valor estimado deve ser positivo.");
            }
        }
    }
}
