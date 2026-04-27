using wrench.auto.repair.cadastro.domain.Entities;
using wrench.auto.repair.cadastro.domain.tests.Fixtures;
using wrench.auto.repair.core.DomainObjects;

namespace wrench.auto.repair.cadastro.domain.tests
{
    [Collection(nameof(ClienteCollection))]
    public class ClienteTests(ClienteFixture _fixture)
    {
        [Fact(DisplayName = "Cliente Dados Vazio Deve Retornar Exception")]
        [Trait("Cadastro", "Domains")]
        public void CriarCliente_DadosVazio_DeveRetornarException()
        {
            // Arrange
            var document = _fixture.GerarCpfValido();
            var nome = _fixture.GerarNomeValido();
            var telefone = _fixture.GerarTelefoneValido();
            var email = _fixture.GerarEmailValido();
            var endereco = _fixture.GerarEnderecoValido();

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            {
                new Cliente(null, nome, telefone, email, endereco, DateTime.Now);
            });

            Assert.Throws<DomainException>(() =>
            {
                new Cliente(document, null, telefone, email, endereco, DateTime.Now);
            });

            Assert.Throws<DomainException>(() =>
            {
                new Cliente(document, nome, null, email, endereco, DateTime.Now);
            });

            Assert.Throws<DomainException>(() =>
            {
                new Cliente(document, nome, telefone, null, endereco, DateTime.Now);
            });

            Assert.Throws<DomainException>(() =>
            {
                new Cliente(document, nome, telefone, email, null, DateTime.Now);
            });
        }

        [Fact(DisplayName = "Alterar Nome Nulo Deve Retornar Exception")]
        [Trait("Cadastro", "Domains")]
        public void AlterarNomeCliente_NomeNulo_DeveRetornarException()
        {
            // Arrange
            var cliente = _fixture.GerarClienteValido();

            // Act & Assert
            Assert.Throws<DomainException>(() => cliente.AtualizarNome(null));
        }

        [Fact(DisplayName = "Alterar Nome Valido Deve Atualizar")]
        [Trait("Cadastro", "Domains")]
        public void AlterarNomeCliente_NomeValido_DeveAtualizar()
        {
            // Arrange
            var cliente = _fixture.GerarClienteValido();
            var nome = _fixture.GerarNomeValido();

            // Act
            cliente.AtualizarNome(nome);

            // Assert
            Assert.Equal(nome, cliente.Nome);
        }

        [Fact(DisplayName = "Alterar Telefone Nulo Deve Retornar Exception")]
        [Trait("Cadastro", "Domains")]
        public void AlterarTelefoneCliente_TelefoneNulo_DeveRetornarException()
        {
            // Arrange
            var cliente = _fixture.GerarClienteValido();

            // Act & Assert
            Assert.Throws<DomainException>(() => cliente.AtualizarTelefone(null));
        }

        [Fact(DisplayName = "Alterar Telefone Valido Deve Atualizar")]
        [Trait("Cadastro", "Domains")]
        public void AlterarTelefoneCliente_TelefoneValido_DeveAtualizar()
        {
            // Arrange
            var cliente = _fixture.GerarClienteValido();
            var telefone = _fixture.GerarTelefoneValido();

            // Act
            cliente.AtualizarTelefone(telefone);

            // Assert
            Assert.Equal(telefone, cliente.Telefone);
        }

        [Fact(DisplayName = "Alterar Email Nulo Deve Retornar Exception")]
        [Trait("Cadastro", "Domains")]
        public void AlterarEmailCliente_EmailNulo_DeveRetornarException()
        {
            // Arrange
            var cliente = _fixture.GerarClienteValido();

            // Act & Assert
            Assert.Throws<DomainException>(() => cliente.AtualizarEmail(null));
        }

        [Fact(DisplayName = "Alterar Email Valido Deve Atualizar")]
        [Trait("Cadastro", "Domains")]
        public void AlterarEmailCliente_EmailValido_DeveAtualizar()
        {
            // Arrange
            var cliente = _fixture.GerarClienteValido();
            var email = _fixture.GerarEmailValido();

            // Act
            cliente.AtualizarEmail(email);

            // Assert
            Assert.Equal(email, cliente.Email);
        }

        [Fact(DisplayName = "Alterar Endereco Nulo Deve Retornar Exception")]
        [Trait("Cadastro", "Domains")]
        public void AlterarEnderecoCliente_EnderecoNulo_DeveRetornarException()
        {
            // Arrange
            var cliente = _fixture.GerarClienteValido();

            // Act & Assert
            Assert.Throws<DomainException>(() => cliente.AtualizarEndereco(null));
        }

        [Fact(DisplayName = "Alterar Endereco Valido Deve Atualizar")]
        [Trait("Cadastro", "Domains")]
        public void AlterarEnderecoCliente_EnderecoValido_DeveAtualizar()
        {
            // Arrange
            var cliente = _fixture.GerarClienteValido();
            var endereco = _fixture.GerarEnderecoValido();

            // Act
            cliente.AtualizarEndereco(endereco);

            // Assert
            Assert.Equal(endereco, cliente.Endereco);
        }
    }
}
