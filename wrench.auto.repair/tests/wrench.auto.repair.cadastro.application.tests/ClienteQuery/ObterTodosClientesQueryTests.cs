using wrench.auto.repair.cadastro.application.Paginacao;
using wrench.auto.repair.cadastro.application.Queries;

namespace wrench.auto.repair.cadastro.application.tests.ClienteQuery
{
    public class ObterTodosClientesQueryTests
    {
        [Fact(DisplayName = "Obter Todos Clientes Command Válido")]
        [Trait("Cadastro", "Application")]
        public void ObterTodosClientesCommand_ComandoValido_DevePassarNaValidacao()
        {
            // Arrange
            var obterTodosClientesQuery = new ObterTodosClientesQuery(new ClienteRequisicaoPaginada());

            // Act
            var valido = obterTodosClientesQuery.EhValido();

            // Assert
            Assert.True(valido);
        }
    }
}
