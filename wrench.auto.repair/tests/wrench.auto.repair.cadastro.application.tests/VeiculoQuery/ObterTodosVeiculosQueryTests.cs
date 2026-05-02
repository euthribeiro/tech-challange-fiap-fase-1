using wrench.auto.repair.cadastro.application.Paginacao;
using wrench.auto.repair.cadastro.application.Queries;

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
                new ObterTodosVeiculosQuery(new VeiculoRequisicaoPaginada());

            // Act
            var valido = obterTodosVeiculosQuery.EhValido();

            // Assert
            Assert.True(valido);
        }
    }
}
