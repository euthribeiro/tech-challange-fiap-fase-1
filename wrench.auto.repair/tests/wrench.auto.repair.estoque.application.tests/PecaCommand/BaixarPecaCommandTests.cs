using wrench.auto.repair.estoque.application.Commands;

namespace wrench.auto.repair.estoque.application.tests.PecaCommand
{
    public class BaixarPecaCommandTests
    {
        [Fact(DisplayName = "Baixar Peça Comando Válido")]
        [Trait("Estoque", "Application")]
        public void Peca_BaixarPecaCommand_ComandoValido()
        {
            // Arrange
            var baixarPecaCommand = new BaixarPecaCommand(Guid.NewGuid(), 1);

            // Act
            var valido = baixarPecaCommand.EhValido();

            // Assert
            Assert.True(valido);
        }

        [Fact(DisplayName = "Baixar Peça Comando Inválido")]
        [Trait("Estoque", "Application")]
        public void Peca_BaixarPecaCommand_ComandoInvalido()
        {
            // Arrange
            var baixarPecaCommand = new BaixarPecaCommand(Guid.Empty, -1);

            // Act
            var valido = baixarPecaCommand.EhValido();

            // Assert
            Assert.False(valido);
            Assert.Contains(
                BaixarPecaCommandValidator.PecaIdVazioError,
                baixarPecaCommand.ValidationResult.Errors.Select(e => e.ErrorMessage)
            );
            Assert.Contains(
                BaixarPecaCommandValidator.QuantidadeMinimaError,
                baixarPecaCommand.ValidationResult.Errors.Select(e => e.ErrorMessage)
            );
        }
    }
}
