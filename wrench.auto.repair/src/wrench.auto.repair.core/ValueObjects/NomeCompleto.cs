using wrench.auto.repair.core.DomainObjects;

namespace wrench.auto.repair.core.ValueObjects
{
    public sealed class NomeCompleto
    {
        public NomeCompleto(string primeiroNome, string sobrenomes)
        {
            Validar(primeiroNome, sobrenomes);

            PrimeiroNome = primeiroNome.Trim();
            Sobrenomes = [.. sobrenomes.Trim().Split(' ')];
        }

        private static void Validar(string primeiroNome, string sobrenomes)
        {
            Validacoes.ValidarSeVazio(primeiroNome, "O nome não pode estar vazio");
            Validacoes.ValidarSeVazio(sobrenomes, "O sobrenomes não pode estar vazio");
            Validacoes.ValidarSeNaoCorrespondeAExpressaoRegular(primeiroNome, "^[A-Za-z ]+$", "Somente letras são permitidas");
            Validacoes.ValidarSeNaoCorrespondeAExpressaoRegular(sobrenomes, "^[A-Za-z ]+$", "Somente letras são permitidas");
        }

        public string PrimeiroNome { get; private set; }

        public List<string> Sobrenomes { get; private set; }

        public string ObterNomeCompleto()
        {
            return $"{PrimeiroNome} {string.Join(' ', Sobrenomes)}";
        }

        public override string ToString()
        {
            return ObterNomeCompleto();
        }

        public override int GetHashCode() =>
            ObterNomeCompleto().GetHashCode();

        public override bool Equals(object? obj) =>
            base.Equals(obj);

        public bool Equals(NomeCompleto other) =>
            this.ObterNomeCompleto() == other.ObterNomeCompleto();

        public static bool operator ==(NomeCompleto left, NomeCompleto right) =>
            Equals(left, right);

        public static bool operator !=(NomeCompleto left, NomeCompleto right) =>
            !Equals(left, right);
    }
}
