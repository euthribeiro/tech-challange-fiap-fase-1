using wrench.auto.repair.autenticacao.application.Commands;

namespace wrench.auto.repair.autenticacao.application.tests.UsuarioCommand
{
    public class AtivarUsuarioCommandTests
    {
        [Fact(DisplayName = "Ativar Usuario Command Válido")]
        [Trait("Autenticacao", "Application")]
        public void AtivarUsuarioCommand_ComandoValido_DevePassarNaValidacao()
        {
            // Arrange
            var ativarUsuarioCommand = new AtivarUsuarioCommand(Guid.NewGuid());

            // Act
            var result = ativarUsuarioCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Ativar Usuario Command Inválido")]
        [Trait("Autenticacao", "Application")]
        public void AtivarUsuarioCommand_ComandoInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var ativarUsuarioCommand = new AtivarUsuarioCommand(Guid.Empty);

            // Act
            var result = ativarUsuarioCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(AtivarUsuarioCommandValidator.UsuarioIdVazio, ativarUsuarioCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }
    }
}
