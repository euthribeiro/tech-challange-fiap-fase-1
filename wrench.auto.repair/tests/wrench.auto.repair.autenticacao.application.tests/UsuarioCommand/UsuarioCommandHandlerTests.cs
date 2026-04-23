using Moq;
using Moq.AutoMock;
using wrench.auto.repair.autenticacao.application.Commands;
using wrench.auto.repair.autenticacao.application.tests.Fixtures;
using wrench.auto.repair.autenticacao.domain.Data;
using wrench.auto.repair.autenticacao.domain.Entities;
using wrench.auto.repair.core.ValueObjects;

namespace wrench.auto.repair.autenticacao.application.tests.UsuarioCommand
{
    [Collection(nameof(UsuarioCollection))]
    public class UsuarioCommandHandlerTests(UsuarioFixture _fixture)
    {
        [Fact(DisplayName = "Cadastrar Novo Usuário Com Sucesso")]
        [Trait("Autenticacao", "Application")]
        public async Task CriarUsuario_NovoUsuario_DeveCadastrarComSucesso()
        {
            // Arrange 
            var email = _fixture.GerarEmail();
            var senha = _fixture.GerarSenha(24);
            var criarUsuarioCommand = new CriarUsuarioCommand(email, senha, Guid.NewGuid(), true);
            var mocker = new AutoMocker();
            var usuarioHandler = mocker.CreateInstance<UsuarioCommandHandler>();

            mocker.GetMock<IUsuarioRepository>()
                .Setup(u => u.UnitOfWork.CommitAsync())
                .Returns(Task.FromResult(true));

            mocker.GetMock<IUsuarioRepository>()
                .Setup(u => u.Adicionar(It.IsAny<Usuario>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            mocker.GetMock<IUsuarioRepository>()
                .Setup(u => u.ObterPerfilPorIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<Perfil?>(_fixture.GerarPerfil()));

            // Act
            var result = await usuarioHandler.Handle(criarUsuarioCommand, CancellationToken.None);

            // Assert
            Assert.True(result.Sucesso);
            mocker.GetMock<IUsuarioRepository>().Verify(
                u => u.ObterPorEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()),
                Times.Once
            );

            mocker.GetMock<IUsuarioRepository>().Verify(
                u => u.ObterPerfilPorIdAsync(It.IsAny<Guid>()),
                Times.Once
            );

            mocker.GetMock<IUsuarioRepository>().Verify(
                u => u.Adicionar(It.IsAny<Usuario>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }

        [Fact(DisplayName = "Cadastrar Usuário E-mail Existente Deve Retornar Erro")]
        [Trait("Autenticacao", "Application")]
        public async Task CriarUsuario_EmailExistente_DeveRetornarErro()
        {
            // Arrange 
            var usuarioExistente = _fixture.GerarUsuario();
            var criarUsuarioCommand = new CriarUsuarioCommand(usuarioExistente.Email.Endereco, null, Guid.NewGuid(), true);
            var mocker = new AutoMocker();
            var usuarioHandler = mocker.CreateInstance<UsuarioCommandHandler>();

            mocker.GetMock<IUsuarioRepository>()
                .Setup(u => u.ObterPorEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Usuario?>(usuarioExistente));

            // Act
            var result = await usuarioHandler.Handle(criarUsuarioCommand, CancellationToken.None);

            // Assert
            Assert.False(result.Sucesso);
            Assert.Contains("Usuário já cadastrado", result.Erros);

            mocker.GetMock<IUsuarioRepository>().Verify(
                u => u.ObterPorEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()),
                Times.Once
            );

            mocker.GetMock<IUsuarioRepository>().Verify(
                u => u.ObterPerfilPorIdAsync(It.IsAny<Guid>()),
                Times.Never
            );

            mocker.GetMock<IUsuarioRepository>().Verify(
                u => u.Adicionar(It.IsAny<Usuario>(), It.IsAny<CancellationToken>()),
                Times.Never
            );
        }
    }
}
