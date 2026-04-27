using wrench.auto.repair.cadastro.application.Queries;
using wrench.auto.repair.core.Pagination;

namespace wrench.auto.repair.cadastro.application.tests.ClienteQuery
{
    public class ObterTodosClientesQueryTests
    {
        [Fact(DisplayName = "Obter Todos Clientes Command Válido")]
        [Trait("Cadastro", "Application")]
        public void ObterTodosClientesCommand_ComandoValido_DevePassarNaValidacao()
        {
            // Arrange
            var obterTodosClientesQuery = new ObterTodosClientesQuery(new RequisicaoPaginada());

            // Act
            var valido = obterTodosClientesQuery.EhValido();

            // Assert
            Assert.True(valido);
        }
    }
}
