using FluentValidation;
using wrench.auto.repair.autenticacao.application.Validators;
using wrench.auto.repair.core.Messages;

namespace wrench.auto.repair.autenticacao.application.Commands
{
    public class CriarUsuarioCommand : Command
    {
        public CriarUsuarioCommand(string email, string senha, Guid perfilId, bool ativo)
        {
            Email = email;
            Senha = senha;
            PerfilId = perfilId;
            Ativo = ativo;
        }

        public string Email { get; private set; }
        public string Senha { get; private set; }
        public Guid PerfilId { get; private set; }
        public bool Ativo { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new CriarUsuarioCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CriarUsuarioCommandValidator : AbstractValidator<CriarUsuarioCommand>
    {
        public static readonly string EmailInvalidoErro = "O e-mail informado não é válido";
        public static readonly string EmailVazioErro = "E-mail não pode ser vazio";
        public static readonly string PerfilVazioErro = "O Perfil não pode ser vazio";

        public CriarUsuarioCommandValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage(EmailInvalidoErro)
                .EmailAddress()
                .WithMessage(EmailVazioErro);

            RuleFor(c => c.PerfilId)
                .NotEmpty()
                .WithMessage(PerfilVazioErro);

            RuleFor(c => c.Senha)
                .SetValidator(new PasswordValidator(string.Empty, null, null));
        }
    }
}
