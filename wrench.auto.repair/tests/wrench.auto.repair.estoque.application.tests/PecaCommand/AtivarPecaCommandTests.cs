using wrench.auto.repair.estoque.application.Commands;

namespace wrench.auto.repair.estoque.application.tests.PecaCommand
{
    public class AtivarPecaCommandTests
    {
        [Fact(DisplayName = "Ativar Peça Comando Válido")]
        [Trait("Estoque", "Application")]
        public void Peca_AtivarPecaCommand_ComandoValido()
        {
            // Arrange
            var ativarPecaCommand = new AtivarPecaCommand(Guid.NewGuid());

            // Act
            var valido = ativarPecaCommand.EhValido();

            // Assert
            Assert.True(valido);
        }

        [Fact(DisplayName = "Ativar Peça Comando Inválido")]
        [Trait("Estoque", "Application")]
        public void Peca_AtivarPecaCommand_ComandoInvalido()
        {
            // Arrange
            var ativarPecaCommand = new AtivarPecaCommand(Guid.Empty);

            // Act
            var valido = ativarPecaCommand.EhValido();

            // Assert
            Assert.False(valido);
            Assert.Contains(
                AtivarPecaCommandValidator.PecaIdVazioError,
                ativarPecaCommand.ValidationResult.Errors.Select(e => e.ErrorMessage)
            );
        }
    }
}
