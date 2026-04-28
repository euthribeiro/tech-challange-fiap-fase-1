using wrench.auto.repair.cadastro.application.Queries;
using wrench.auto.repair.core.Pagination;

namespace wrench.auto.repair.cadastro.application.tests.VeiculoQuery
{
    public class ObterTodosVeiculosQueryTests()
    {
        [Fact(DisplayName = "Obter Todos Veiculos Command Válido")]
        [Trait("Cadastro", "Application")]
        public void ObterTodosVeiculosCommand_ComandoValido_DevePassarNaValidacao()
        {
            // Arrange
            var obterTodosVeiculosQuery =
                new ObterTodosVeiculosQuery(new RequisicaoPaginada());

            // Act
            var valido = obterTodosVeiculosQuery.EhValido();

            // Assert
            Assert.True(valido);
        }
    }
}
