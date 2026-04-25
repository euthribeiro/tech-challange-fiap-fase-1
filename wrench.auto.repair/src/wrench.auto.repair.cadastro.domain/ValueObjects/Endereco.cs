using wrench.auto.repair.cadastro.domain.Entities;
using wrench.auto.repair.core.DomainObjects;

namespace wrench.auto.repair.cadastro.domain.ValueObjects
{
    public class Endereco
    {
        public Endereco(string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string unidadeFederativa, string pais)
        {
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cep = cep;
            Cidade = cidade;
            UnidadeFederativa = unidadeFederativa;
            Pais = pais;

            Validar();
        }

        private void Validar()
        {
            Validacoes.ValidarSeVazio(Logradouro, "Logradouro não pode ser vazio");
            Validacoes.ValidarSeVazio(Numero, "Número não pode ser vazio");
            Validacoes.ValidarSeVazio(Bairro, "Bairro não pode ser vazio");
            Validacoes.ValidarSeVazio(Cep, "CEP não pode ser vazio");
            Validacoes.ValidarMinimoMaximo(Cep.Length, 8, 8, "CEP deve ter exatamente 8 caracteres");
            Validacoes.ValidarSeVazio(Cidade, "Cidade não pode ser vazio");
            Validacoes.ValidarSeVazio(UnidadeFederativa, "Unidade Federativa não pode ser vazio");
            Validacoes.ValidarSeVazio(Pais, "País não pode ser vazio");
        }

        public Guid Id { get; private set; } = Guid.NewGuid();

        public string Logradouro { get; private set; }

        public string Numero { get; private set; }

        public string Complemento { get; private set; }

        public string Bairro { get; private set; }

        public string Cep { get; private set; }

        public string Cidade { get; private set; }

        public string UnidadeFederativa { get; private set; }

        public string Pais { get; private set; }

        // EF Relation
        public Cliente Cliente { get; private set; }

        public string ObterEnderecoFormatado()
        {
            var cepFormatado = Cep?.Length == 8
                ? $"{Cep[..5]}-{Cep.Substring(5, 3)}"
                : Cep;

            var complemento = string.IsNullOrWhiteSpace(Complemento)
                ? string.Empty
                : $" - {Complemento}";

            return $"{Logradouro}, {Numero}{complemento}, {Bairro}, {Cidade} - {UnidadeFederativa}, CEP: {cepFormatado}, {Pais}";
        }
    }
}
