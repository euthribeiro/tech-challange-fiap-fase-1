using wrench.auto.repair.cadastro.domain.ValueObjects;
using wrench.auto.repair.core.DomainObjects;
using wrench.auto.repair.core.ValueObjects;

namespace wrench.auto.repair.cadastro.domain.Entities
{
    public class Cliente : Entity, IAggregateRoot
    {
        public Cliente(CpfCnpj documento, NomeRazaoSocial nomeCompleto, Telefone telefone, Email email, Guid enderecoId, DateTime dataCadastro)
        {
            Documento = documento;
            Nome = nomeCompleto;
            Telefone = telefone;
            Email = email;
            EnderecoId = enderecoId;
            DataCadastro = dataCadastro;

            Validar();
        }

        public CpfCnpj Documento { get; private set; }

        public NomeRazaoSocial Nome { get; private set; }

        public Telefone Telefone { get; private set; }

        public Email Email { get; private set; }

        public Guid EnderecoId { get; private set; }

        public DateTime DataCadastro { get; private set; }

        // EF Relation
        public Endereco Endereco { get; private set; }

        // EF Relation
        public IEnumerable<Veiculo> Veiculos { get; private set; }

        public void AtualizarNome(NomeRazaoSocial nomeCompleto)
        {
            Validacoes.ValidarSeNulo(nomeCompleto, "Nome não pode ser nulo");
            Nome = nomeCompleto;
        }

        public void AtualizarTelefone(Telefone telefone)
        {
            Validacoes.ValidarSeNulo(telefone, "Telefone não pode ser nulo");
            Telefone = telefone;
        }

        public void AtualizarEmail(Email email)
        {
            Validacoes.ValidarSeNulo(email, "E-mail não pode ser nulo");
            Email = email;
        }

        public void AtualizarEndereco(Endereco endereco)
        {
            Validacoes.ValidarSeNulo(endereco, "Endereço não pode ser nulo");
            Endereco = endereco;
            EnderecoId = endereco.Id;
        }

        private void Validar()
        {
            Validacoes.ValidarSeNulo(Documento, "Documento não pode ser nulo");
            Validacoes.ValidarSeNulo(Nome, "Nome não pode ser nulo");
            Validacoes.ValidarSeNulo(Telefone, "Telefone não pode ser nulo");
            Validacoes.ValidarSeNulo(Email, "E-mail não pode ser nulo");
            Validacoes.ValidarSeVazio(EnderecoId, "ID do Endereço não pode ser vazio");
        }
    }
}
