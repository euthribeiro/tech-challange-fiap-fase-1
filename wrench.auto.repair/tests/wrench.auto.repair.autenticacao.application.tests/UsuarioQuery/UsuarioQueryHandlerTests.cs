using Moq;
using Moq.AutoMock;
using wrench.auto.repair.autenticacao.application.Queries;
using wrench.auto.repair.autenticacao.domain.Data;
using wrench.auto.repair.autenticacao.domain.Entities;

namespace wrench.auto.repair.autenticacao.application.tests.UsuarioQuery
{
    public class UsuarioQueryHandlerTests
    {
        [Fact(DisplayName = "Listar Todos Os Perfis Com Sucesso")]
        [Trait("Autenticacao", "Application")]
        public async Task Perfil_ListarTodosPerfis_DeveRetornarPerfis()
        {
            // Arrange
            var obterPerfisQuery = new ObterTodosPerfisQuery();
            var autoMocker = new AutoMocker();
            var usuarioQueryHandler = autoMocker.CreateInstance<UsuarioQueryHandler>();
            autoMocker.GetMock<IUsuarioRepository>().
                Setup(u => u.ObterTodosPerfisAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<IEnumerable<Perfil>>([]));

            // Act
            var result = await usuarioQueryHandler.Handle(obterPerfisQuery, CancellationToken.None);

            // Assert
            Assert.True(result.Sucesso);
            autoMocker.GetMock<IUsuarioRepository>().Verify(u => u.ObterTodosPerfisAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
