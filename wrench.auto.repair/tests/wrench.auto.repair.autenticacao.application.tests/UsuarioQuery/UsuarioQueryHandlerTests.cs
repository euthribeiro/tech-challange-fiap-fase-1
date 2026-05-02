using Moq;
using Moq.AutoMock;
using System.Linq.Expressions;
using wrench.auto.repair.autenticacao.application.Paginacao;
using wrench.auto.repair.autenticacao.application.Queries;
using wrench.auto.repair.autenticacao.application.tests.Fixtures;
using wrench.auto.repair.autenticacao.domain.Data;
using wrench.auto.repair.autenticacao.domain.Entities;
using wrench.auto.repair.core.Pagination;

namespace wrench.auto.repair.autenticacao.application.tests.UsuarioQuery
{
    [Collection(nameof(UsuarioCollection))]
    public class UsuarioQueryHandlerTests(UsuarioFixture _fixture)
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
            var result = await usuarioQueryHandler
                .Handle(obterPerfisQuery, CancellationToken.None);

            // Assert
            Assert.True(result.Sucesso);
            autoMocker.GetMock<IUsuarioRepository>()
                .Verify(u => u.ObterTodosPerfisAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Obter Todos Os Usuários com sucesso")]
        [Trait("Autenticacao", "Application")]
        public async Task Usuarios_ObterTodos_DeveRetornarComSucesso()
        {
            // Arrange
            var resultadoPaginadoUsuario = new ResultadoPaginado<Usuario>([], 0, 1, 10);
            var obterTodosUsuariosQuery = new ObterTodosUsuariosQuery(new UsuarioRequisicaoPaginada());
            var autoMocker = new AutoMocker();
            var usuarioQueryHandler = autoMocker.CreateInstance<UsuarioQueryHandler>();

            autoMocker.GetMock<IUsuarioRepository>().
                Setup(u => u.BuscaPaginadaAsync(It.IsAny<UsuarioRequisicaoPaginada>(), It.IsAny<Dictionary<string, Expression<Func<Usuario, object?>>>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<ResultadoPaginado<Usuario>>(resultadoPaginadoUsuario));

            // Act
            var result = await usuarioQueryHandler.Handle(obterTodosUsuariosQuery, CancellationToken.None);

            // Assert
            Assert.True(result.Sucesso);
            autoMocker.GetMock<IUsuarioRepository>()
                .Verify(u => u.BuscaPaginadaAsync(It.IsAny<UsuarioRequisicaoPaginada>(), It.IsAny<Dictionary<string, Expression<Func<Usuario, object?>>>>(), It.IsAny<CancellationToken>()), Times.Once);
        }


        [Fact(DisplayName = "Obter Usuário Por Identificador Com Sucesso")]
        [Trait("Autenticacao", "Application")]
        public async Task Usuario_ObterPorIdentificador_DeveRetornarUsuario()
        {
            // Arrange
            var usuario = _fixture.GerarUsuario();
            var obterUsuarioPorIdQuery = new ObterUsuarioPorIdQuery(usuario.Id);
            var autoMocker = new AutoMocker();
            var usuarioQueryHandler = autoMocker.CreateInstance<UsuarioQueryHandler>();
            autoMocker.GetMock<IUsuarioRepository>().
                Setup(u => u.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Usuario?>(usuario));

            // Act
            var result = await usuarioQueryHandler
                .Handle(obterUsuarioPorIdQuery, CancellationToken.None);

            // Assert
            Assert.True(result.Sucesso);
            autoMocker.GetMock<IUsuarioRepository>()
                .Verify(u => u.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Obter Usuário Por Id Usuario Nao Encontrado")]
        [Trait("Autenticacao", "Application")]
        public async Task Usuario_ObterPorId_UsuarioNaoEncontrado()
        {
            // Arrange
            var obterUsuarioPorIdQuery = new ObterUsuarioPorIdQuery(Guid.NewGuid());
            var autoMocker = new AutoMocker();
            var usuarioQueryHandler = autoMocker.CreateInstance<UsuarioQueryHandler>();
            autoMocker.GetMock<IUsuarioRepository>().
                Setup(u => u.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Usuario?>(null));

            // Act
            var result = await usuarioQueryHandler
                .Handle(obterUsuarioPorIdQuery, CancellationToken.None);

            // Assert
            Assert.False(result.Sucesso);
            autoMocker.GetMock<IUsuarioRepository>()
                .Verify(u => u.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
