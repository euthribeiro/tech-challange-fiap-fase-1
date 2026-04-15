using wrench.auto.repair.core.DomainObjects;
using wrench.auto.repair.core.ValueObjects;

namespace wrench.auto.repair.cadastro.domain.Entities
{
    public class Cliente : Entity, IAggregateRoot
    {
        public Cliente(CpfCnpj documento, NomeCompleto nomeCompleto, DateTime dataNascimento, Telefone telefone, Email email, Guid enderecoId, DateTime dataCadastro)
        {
            Documento = documento;
            NomeCompleto = nomeCompleto;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Email = email;
            EnderecoId = enderecoId;
            DataCadastro = dataCadastro;
        }

        public CpfCnpj Documento { get; private set; }

        public NomeCompleto NomeCompleto { get; private set; }

        public DateTime DataNascimento { get; set; }

        public Telefone Telefone { get; private set; }

        public Email Email { get; private set; }

        public Guid EnderecoId { get; private set; }

        public DateTime DataCadastro { get; private set; }

        public Endereco Endereco { get; private set; }

        public void AlterarEndereco(Endereco endereco)
        {
            EnderecoId = Endereco.Id;
            Endereco = endereco;
        }
    }
}
