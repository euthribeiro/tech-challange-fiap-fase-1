using wrench.auto.repair.estoque.application.Commands;

namespace wrench.auto.repair.estoque.application.tests.PecaCommand
{
    public class CadastrarPecaCommandTests
    {
        [Fact(DisplayName = "Cadastrar Peça Comando Válido")]
        [Trait("Estoque", "Application")]
        public void Peca_CadastrarPecaCommand_ComandoValido()
        {
            // Arrange
            var cadastrarPecaCommand =
                new CadastrarPecaCommand("Pneu", "Aro 15", 100.00, 10, true); ;

            // Act
            var valido = cadastrarPecaCommand.EhValido();

            // Assert
            Assert.True(valido);
        }

        [Fact(DisplayName = "Cadastrar Peça Comando Inválido")]
        [Trait("Estoque", "Application")]
        public void Peca_CadastrarPecaCommand_ComandoInvalido()
        {
            // Arrange
            var cadastrarPecaCommand = new CadastrarPecaCommand("", "", -1, -1, true); ;

            // Act
            var valido = cadastrarPecaCommand.EhValido();

            // Assert
            Assert.False(valido);
            Assert.Contains(
                CadastrarPecaCommandValidator.NomeVazioError,
                cadastrarPecaCommand.ValidationResult.Errors.Select(e => e.ErrorMessage)
            );
            Assert.Contains(
                CadastrarPecaCommandValidator.DescricaoVazioError,
                cadastrarPecaCommand.ValidationResult.Errors.Select(e => e.ErrorMessage)
            );
            Assert.Contains(
                CadastrarPecaCommandValidator.ValorMinimoError,
                cadastrarPecaCommand.ValidationResult.Errors.Select(e => e.ErrorMessage)
            );
            Assert.Contains(
                CadastrarPecaCommandValidator.QuantidadeMinimaError,
                cadastrarPecaCommand.ValidationResult.Errors.Select(e => e.ErrorMessage)
            );
        }
    }
}
