using wrench.auto.repair.autenticacao.application.Commands;

namespace wrench.auto.repair.autenticacao.application.tests.UsuarioCommand
{
    public class InativarUsuarioCommandTests
    {
        [Fact(DisplayName = "Inativar Usuario Command Válido")]
        [Trait("Autenticacao", "Application")]
        public void InativarUsuarioCommand_ComandoValido_DevePassarNaValidacao()
        {
            // Arrange
            var inativarUsuarioCommand = new InativarUsuarioCommand(Guid.NewGuid());

            // Act
            var result = inativarUsuarioCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Inativar Usuario Command Inválido")]
        [Trait("Autenticacao", "Application")]
        public void InativarUsuarioCommand_ComandoInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var inativarUsuarioCommand = new InativarUsuarioCommand(Guid.Empty);

            // Act
            var result = inativarUsuarioCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(InativarUsuarioCommandValidator.UsuarioIdVazio, inativarUsuarioCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }
    }
}
