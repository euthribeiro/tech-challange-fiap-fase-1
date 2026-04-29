using FluentValidation;
using wrench.auto.repair.core.Messages;
using wrench.auto.repair.estoque.application.Queries.ViewModels;

namespace wrench.auto.repair.estoque.application.Queries
{
    public class ConsultaPecaPorNomeQuery(string nome) : Command<IEnumerable<PecaViewModel>>
    {
        public string Nome { get; private set; } = nome;

        public override bool EhValido()
        {
            ValidationResult = new ConsultaPecaPorNomeQueryValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ConsultaPecaPorNomeQueryValidator : AbstractValidator<ConsultaPecaPorNomeQuery>
    {
        public static string NomeVazioError =>
            "O nome da peça precisa ser informado";

        public ConsultaPecaPorNomeQueryValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage(NomeVazioError);
        }
    }
}
