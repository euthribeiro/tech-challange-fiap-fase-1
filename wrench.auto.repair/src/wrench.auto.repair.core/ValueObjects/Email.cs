using System.Net.Mail;
using System.Text.RegularExpressions;
using wrench.auto.repair.core.DomainObjects;

namespace wrench.auto.repair.core.ValueObjects
{
    public class Email
    {
        protected Email() { } // EF Core

        public Guid UsuarioId { get; private set; } // EF Core

        public string Endereco { get; private set; }

        public string Dominio { get; private set; }

        private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public Email(string value)
        {
            Validar(value);

            Endereco = value.Trim().ToLowerInvariant();
            Dominio = Endereco.Split('@')[1];
        }

        private static void Validar(string value)
        {
            Validacoes.ValidarSeVazio(value, "E-mail não pode ser vazio");
            Validacoes.ValidarSeNaoCorrespondeAExpressaoRegular(value, EmailRegex, "E-mail inválido");
            if (!EhValido(value)) throw new DomainException("E-mail inválido");
        }

        public override string ToString() => Endereco;

        public override bool Equals(object? obj)
        {
            return obj is Email other && Equals(other);
        }

        public bool Equals(Email other) =>
            other is not null && Endereco == other.Endereco;

        public override int GetHashCode()
            => Endereco.GetHashCode();

        public static bool operator ==(Email left, Email right)
            => Equals(left, right);

        public static bool operator !=(Email left, Email right)
            => !Equals(left, right);

        public static bool EhValido(string email)
        {
            try
            {
                var mail = new MailAddress(email);
                return mail.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
