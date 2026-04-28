using wrench.auto.repair.autenticacao.application.Commands;

namespace wrench.auto.repair.autenticacao.application.tests.UsuarioCommand
{
    public class AutenticarUsuarioCommandTests
    {
        [Fact(DisplayName = "Autenticar Usuario Command Válido")]
        [Trait("Autenticacao", "Application")]
        public void AutenticarUsuarioCommand_ComandoValido_DevePassarNaValidacao()
        {
            // Arrange
            var autenticarUsuarioCommand = new AutenticarUsuarioCommand("email@email.com", "My@Secret123!");

            // Act
            var result = autenticarUsuarioCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Autenticar Usuario Command Inválido")]
        [Trait("Autenticacao", "Application")]
        public void AutenticarUsuarioCommand_ComandoInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var autenticarUsuarioCommand = new AutenticarUsuarioCommand("", "");

            // Act
            var result = autenticarUsuarioCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(AutenticarUsuarioCommandValidator.SenhaVazia, autenticarUsuarioCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(CriarUsuarioCommandValidator.EmailVazioErro, autenticarUsuarioCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
            Assert.Contains(CriarUsuarioCommandValidator.EmailInvalidoErro, autenticarUsuarioCommand.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }
    }
}
