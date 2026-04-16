using wrench.auto.repair.core.DomainObjects;
using wrench.auto.repair.core.ValueObjects;

namespace wrench.auto.repair.cadastro.domain.Entities
{
    public class Cliente : Entity, IAggregateRoot
    {
        public Cliente(CpfCnpj documento, NomeCompleto nomeCompleto, DataNascimento dataNascimento, Telefone telefone, Email email, Guid enderecoId, DateTime dataCadastro)
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

        public DataNascimento DataNascimento { get; set; }

        public Telefone Telefone { get; private set; }

        public Email Email { get; private set; }

        public Guid EnderecoId { get; private set; }

        public DateTime DataCadastro { get; private set; }

        public Endereco Endereco { get; private set; }

        public void AtualizarNomeCompleto(NomeCompleto nomeCompleto)
        {
            NomeCompleto = nomeCompleto;
        }

        public void AtualizarNascimento(DataNascimento dataNascimento)
        {
            DataNascimento = dataNascimento;
        }

        public void AtualizarTelefone(Telefone telefone)
        {
            Telefone = telefone;
        }

        public void AtualizarEmail(Email email)
        {
            Email = email;
        }

        public void AtualizarEndereco(Endereco endereco)
        {
            Endereco = endereco;
            EnderecoId = endereco.Id;
        }
    }
}
