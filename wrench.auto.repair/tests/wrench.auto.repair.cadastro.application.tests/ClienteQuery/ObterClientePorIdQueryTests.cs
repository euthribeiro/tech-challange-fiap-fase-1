using wrench.auto.repair.cadastro.application.Queries;
using wrench.auto.repair.cadastro.application.tests.Fixture;

namespace wrench.auto.repair.cadastro.application.tests.ClienteQuery
{
    [Collection(nameof(ClienteCollection))]
    public class ObterClientePorIdQueryTests(ClienteFixture _fixture)
    {
        [Fact(DisplayName = "Obter Cliente Por Id Command Válido")]
        [Trait("Cadastro", "Application")]
        public void ObterClientePorIdCommand_ComandoValido_DevePassarNaValidacao()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var obterClientePorIdQuery = new ObterClientePorIdQuery(clienteId);

            // Act
            var valido = obterClientePorIdQuery.EhValido();

            // Assert
            Assert.True(valido);
        }

        [Fact(DisplayName = "Obter Cliente Por Id Command Inválido")]
        [Trait("Cadastro", "Application")]
        public void ObterClientePorIdCommand_ComandoInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var clienteIdInvalido = Guid.Empty;
            var obterClientePorIdQuery = new ObterClientePorIdQuery(clienteIdInvalido);

            // Act
            var valido = obterClientePorIdQuery.EhValido();

            // Assert
            Assert.False(valido);
            Assert.Contains(
                ObterClientePorIdQueryValidator.ClienteIdVazio,
                obterClientePorIdQuery
                    .ValidationResult
                    .Errors.Select(e => e.ErrorMessage)
            );
        }
    }
}
