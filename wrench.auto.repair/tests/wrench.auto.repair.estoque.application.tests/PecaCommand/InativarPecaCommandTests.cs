using wrench.auto.repair.estoque.application.Commands;

namespace wrench.auto.repair.estoque.application.tests.PecaCommand
{
    public class InativarPecaCommandTests
    {
        [Fact(DisplayName = "Inativar Peça Comando Válido")]
        [Trait("Estoque", "Application")]
        public void Peca_InativarPecaCommand_ComandoValido()
        {
            // Arrange
            var inativarPecaCommand = new InativarPecaCommand(Guid.NewGuid());

            // Act
            var valido = inativarPecaCommand.EhValido();

            // Assert
            Assert.True(valido);
        }

        [Fact(DisplayName = "Inativar Peça Comando Inválido")]
        [Trait("Estoque", "Application")]
        public void Peca_InativarPecaCommand_ComandoInvalido()
        {
            // Arrange
            var inativarPecaCommand = new InativarPecaCommand(Guid.Empty);

            // Act
            var valido = inativarPecaCommand.EhValido();

            // Assert
            Assert.False(valido);
            Assert.Contains(
                InativarPecaCommandValidator.PecaIdVazioError,
                inativarPecaCommand.ValidationResult.Errors.Select(e => e.ErrorMessage)
            );
        }
    }
}
