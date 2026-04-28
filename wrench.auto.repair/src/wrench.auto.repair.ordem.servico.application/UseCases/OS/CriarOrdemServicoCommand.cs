using FluentValidation;
using wrench.auto.repair.core.Messages;
using wrench.auto.repair.ordem.servico.domain.Entities;

namespace wrench.auto.repair.ordem.servico.application.UseCases.Os
{
    public class CriarOrdemServicoCommand : Command<Guid>
    {
        public Guid ClienteId { get; set; }
        public Guid VeiculoId { get; set; }
        public Guid AtendenteId { get; set; }
        public string Descricao { get; set; }

        public override bool EhValido()
        {
            var validator = new CriarOrdemServicoCommandValidator();
            var result = validator.Validate(this);
            return result.IsValid;
        }

        public static implicit operator OrdemServico(CriarOrdemServicoCommand command)
        {
            return new OrdemServico(
                command.ClienteId,
                command.VeiculoId,
                command.AtendenteId,
                command.Descricao,
                domain.Enums.OrdemServicoStatus.Recebida,
                DateTime.UtcNow
            );
        }

        public class CriarOrdemServicoCommandValidator : AbstractValidator<CriarOrdemServicoCommand>
        {
            public CriarOrdemServicoCommandValidator()
            {
                RuleFor(c => c.ClienteId)
                    .NotEmpty()
                    .WithMessage("Cliente não informado.");                    

                RuleFor(c => c.VeiculoId)
                 .NotEmpty()
                 .WithMessage("Veículo não informado.");

                RuleFor(c => c.AtendenteId)
                    .NotEmpty()
                    .WithMessage("Atendente não informado.");

                RuleFor(c => c.Descricao)
                    .NotEmpty()
                    .WithMessage("Descrição não informada.");
            }
        }
    }
}
