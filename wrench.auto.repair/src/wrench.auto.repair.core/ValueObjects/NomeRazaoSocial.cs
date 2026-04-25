using wrench.auto.repair.core.DomainObjects;

namespace wrench.auto.repair.core.ValueObjects
{
    public sealed class NomeRazaoSocial
    {
        public string Nome { get; set; }

        public NomeRazaoSocial(string nome)
        {
            Validar(nome);

            Nome = nome.Trim().ToUpper();
        }


        private static void Validar(string nome)
        {
            Validacoes.ValidarSeVazio(nome, "O nome ou razão social não pode estar vazio");

            var nomes = nome.Trim().Split(' ');

            Validacoes.ValidarSeIgual(nomes.Length, 1, "O nome ou razão social deve ser composto");
        }


        public override string ToString()
        {
            return Nome;
        }

        public override int GetHashCode() =>
            Nome.GetHashCode();

        public override bool Equals(object? obj) =>
            base.Equals(obj);

        public bool Equals(NomeRazaoSocial other) =>
            this.Nome == other.Nome;

        public static bool operator ==(NomeRazaoSocial left, NomeRazaoSocial right) =>
            Equals(left, right);

        public static bool operator !=(NomeRazaoSocial left, NomeRazaoSocial right) =>
            !Equals(left, right);
    }
}
