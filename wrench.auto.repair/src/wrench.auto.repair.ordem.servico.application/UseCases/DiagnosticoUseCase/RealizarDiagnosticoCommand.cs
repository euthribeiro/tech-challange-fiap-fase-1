using FluentValidation;
using wrench.auto.repair.core.Messages;

namespace wrench.auto.repair.ordem.servico.application.UseCases.DiagnosticoUseCase
{
    public class RealizarDiagnosticoCommand : Command<Guid>
    {
        public Guid OrdemServicoId { get; set; }
        public Guid MecanicoId { get; set; }
        public decimal ValorEstimado { get; set; }
        public string SolucaoProposta { get; set; }

        public override bool EhValido()
        {
            var validator = new RealizarDiagnosticoCommandValidator();
            var result = validator.Validate(this);
            return result.IsValid;
        }

        public class RealizarDiagnosticoCommandValidator : AbstractValidator<RealizarDiagnosticoCommand>
        {
            public RealizarDiagnosticoCommandValidator()
            {
                RuleFor(c => c.OrdemServicoId)
                    .NotEmpty()
                    .WithMessage("Ordem de serviço não informada.");

                RuleFor(c => c.MecanicoId)
                    .NotEmpty()
                    .WithMessage("Mecânico não informado.");

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
