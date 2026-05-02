using wrench.auto.repair.estoque.application.Commands;

namespace wrench.auto.repair.estoque.application.tests.PecaCommand
{
    public class ReporPecaCommandTests
    {
        [Fact(DisplayName = "Repor Peça Comando Válido")]
        [Trait("Estoque", "Application")]
        public void Peca_ReporPecaCommand_ComandoValido()
        {
            // Arrange
            var reporPecaCommand = new ReporPecaCommand(Guid.NewGuid(), 1);

            // Act
            var valido = reporPecaCommand.EhValido();

            // Assert
            Assert.True(valido);
        }

        [Fact(DisplayName = "Repor Peça Comando Inválido")]
        [Trait("Estoque", "Application")]
        public void Peca_ReporPecaCommand_ComandoInvalido()
        {
            // Arrange
            var reporPecaCommand = new ReporPecaCommand(Guid.Empty, -1);

            // Act
            var valido = reporPecaCommand.EhValido();

            // Assert
            Assert.False(valido);
            Assert.Contains(
                ReporPecaCommandValidator.PecaIdVazioError,
                reporPecaCommand.ValidationResult.Errors.Select(e => e.ErrorMessage)
            );
            Assert.Contains(
                ReporPecaCommandValidator.QuantidadeMinimaError,
                reporPecaCommand.ValidationResult.Errors.Select(e => e.ErrorMessage)
            );
        }
    }
}
