using wrench.auto.repair.autenticacao.application.Commands;

namespace wrench.auto.repair.autenticacao.application.tests.UsuarioCommand
{
    public class ResetarSenhaUsuarioCommandTests
    {
        [Fact(DisplayName = "Resetar Senha Usuario Command Válido")]
        [Trait("Autenticacao", "Application")]
        public void ResetarSenhaUsuarioCommand_ComandoValido_DevePassarNaValidacao()
        {
            // Arrange
            var resetarSenhaUsuarioCommand = new ResetarSenhaUsuarioCommand(Guid.NewGuid());

            // Act
            var result = resetarSenhaUsuarioCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Resetar Senha Command Inválido")]
        [Trait("Autenticacao", "Application")]
        public void ResetarSenhaUsuarioCommand_ComandoInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var resetarSenhaUsuarioCommand = new ResetarSenhaUsuarioCommand(Guid.Empty);

            // Act
            var result = resetarSenhaUsuarioCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(ResetarSenhaUsuarioCommandValidator.UsuarioIdVazio, resetarSenhaUsuarioCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }
    }
}
