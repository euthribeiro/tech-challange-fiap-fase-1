using Moq;
using Moq.AutoMock;
using wrench.auto.repair.autenticacao.application.Commands;
using wrench.auto.repair.autenticacao.application.tests.Fixtures;
using wrench.auto.repair.autenticacao.domain.Data;
using wrench.auto.repair.autenticacao.domain.Entities;
using wrench.auto.repair.autenticacao.domain.Models;
using wrench.auto.repair.autenticacao.domain.Security;
using wrench.auto.repair.autenticacao.infra.Security;
using wrench.auto.repair.core.Errors;
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
            var hashSenha = _fixture.GerarHashSenha(senha);
            var criarUsuarioCommand = new CriarUsuarioCommand(email, senha, Guid.NewGuid(), true);
            var mocker = new AutoMocker();
            var usuarioHandler = mocker.CreateInstance<UsuarioCommandHandler>();

            mocker.GetMock<IUsuarioRepository>()
                .Setup(u => u.UnitOfWork.CommitAsync())
                .Returns(Task.FromResult(true));

            mocker.GetMock<IUsuarioRepository>()
                .Setup(u => u.Adicionar(It.IsAny<Usuario>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true));

            mocker.GetMock<IPasswordHasher>()
               .Setup(u => u.GerarHash(It.IsAny<string>()))
               .Returns(hashSenha);

            mocker.GetMock<IUsuarioRepository>()
                .Setup(u => u.ObterPerfilPorIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult<Perfil?>(_fixture.GerarPerfil()));

            // Act
            var result = await usuarioHandler.Handle(criarUsuarioCommand, CancellationToken.None);

            // Assert
            Assert.True(result.Sucesso);
            Assert.Equal(ResultadoStatusEnum.CRIADO, result.ResultadoStatus);
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
            Assert.Equal(TipoErroEnum.VALIDACAO, result.TipoErro);
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

        [Fact(DisplayName = "Autenticar Usuário Com Sucesso")]
        [Trait("Autenticacao", "Application")]
        public async Task AutenticarUsuario_CredenciaisValidas_DeveRetornarTokenDeAcesso()
        {
            // Arrange
            var senha = _fixture.GerarSenha(24);
            var usuario = _fixture.GerarUsuario();
            var perfil = _fixture.GerarPerfil();
            var hasher = new PasswordHasher();

            usuario.DefinirSenha(hasher.GerarHash(senha));
            usuario.AlterarPerfil(perfil);
            var autenticarUsuarioCommand = new AutenticarUsuarioCommand(usuario.Email.Endereco, senha);
            var autoMocker = new AutoMocker();
            var usuarioCommandHandler = autoMocker.CreateInstance<UsuarioCommandHandler>();

            autoMocker.GetMock<IUsuarioRepository>()
                .Setup(u => u.ObterPorEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Usuario?>(usuario));

            autoMocker.GetMock<IPasswordHasher>()
                .Setup(u => u.ValidarSenha(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            autoMocker.GetMock<IJwtTokenGenerator>()
                .Setup(j => j.GerarToken(It.IsAny<Usuario>()))
                .Returns(new TokenAcesso(It.IsAny<string>(), usuario.Email.Endereco, usuario.Perfil.Nome));

            // Act
            var result = await usuarioCommandHandler.Handle(autenticarUsuarioCommand, CancellationToken.None);

            // Assert
            Assert.True(result.Sucesso);
            Assert.NotNull(result.Valor);
            Assert.Equal(usuario.Email.Endereco, result.Valor.Username);

            autoMocker.GetMock<IUsuarioRepository>()
                .Verify(u => u.ObterPorEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()), Times.Once);

            autoMocker.GetMock<IPasswordHasher>()
                .Verify(u => u.ValidarSenha(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            autoMocker.GetMock<IJwtTokenGenerator>()
                .Verify(u => u.GerarToken(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact(DisplayName = "Autenticar Usuário Inexistente")]
        [Trait("Autenticacao", "Application")]
        public async Task AutenticarUsuario_UsuarioInexistente_DeveRetornarNaoAutorizado()
        {
            // Arrange
            var senha = _fixture.GerarSenha(24);
            var usuario = _fixture.GerarUsuario();
            var perfil = _fixture.GerarPerfil();
            var hasher = new PasswordHasher();

            usuario.DefinirSenha(hasher.GerarHash(senha));
            usuario.AlterarPerfil(perfil);
            var autenticarUsuarioCommand = new AutenticarUsuarioCommand(usuario.Email.Endereco, senha);
            var autoMocker = new AutoMocker();
            var usuarioCommandHandler = autoMocker.CreateInstance<UsuarioCommandHandler>();

            autoMocker.GetMock<IUsuarioRepository>()
                .Setup(u => u.ObterPorEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Usuario?>(null));

            // Act
            var result = await usuarioCommandHandler.Handle(autenticarUsuarioCommand, CancellationToken.None);

            // Assert
            Assert.False(result.Sucesso);
            Assert.Null(result.Valor);
            Assert.Equal(result.TipoErro, TipoErroEnum.NAO_AUTORIZADO);

            autoMocker.GetMock<IUsuarioRepository>()
                .Verify(u => u.ObterPorEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()), Times.Once);

            autoMocker.GetMock<IPasswordHasher>()
                .Verify(u => u.ValidarSenha(It.IsAny<string>(), It.IsAny<string>()), Times.Never);

            autoMocker.GetMock<IJwtTokenGenerator>()
                .Verify(u => u.GerarToken(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact(DisplayName = "Autenticar Usuário Senha Inválida")]
        [Trait("Autenticacao", "Application")]
        public async Task AutenticarUsuario_SenhaInvalida_DeveRetornarNaoAutorizado()
        {
            // Arrange
            var senha = _fixture.GerarSenha(24);
            var usuario = _fixture.GerarUsuario();
            var perfil = _fixture.GerarPerfil();
            var hasher = new PasswordHasher();

            usuario.DefinirSenha(hasher.GerarHash(senha));
            usuario.AlterarPerfil(perfil);
            var autenticarUsuarioCommand = new AutenticarUsuarioCommand(usuario.Email.Endereco, senha);
            var autoMocker = new AutoMocker();
            var usuarioCommandHandler = autoMocker.CreateInstance<UsuarioCommandHandler>();

            autoMocker.GetMock<IUsuarioRepository>()
                .Setup(u => u.ObterPorEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Usuario?>(usuario));

            autoMocker.GetMock<IPasswordHasher>()
            .Setup(u => u.ValidarSenha(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(false);

            // Act
            var result = await usuarioCommandHandler.Handle(autenticarUsuarioCommand, CancellationToken.None);

            // Assert
            Assert.False(result.Sucesso);
            Assert.Null(result.Valor);
            Assert.Equal(result.TipoErro, TipoErroEnum.NAO_AUTORIZADO);

            autoMocker.GetMock<IUsuarioRepository>()
                .Verify(u => u.ObterPorEmailAsync(It.IsAny<Email>(), It.IsAny<CancellationToken>()), Times.Once);

            autoMocker.GetMock<IPasswordHasher>()
                .Verify(u => u.ValidarSenha(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            autoMocker.GetMock<IJwtTokenGenerator>()
                .Verify(u => u.GerarToken(It.IsAny<Usuario>()), Times.Never);
        }
    }
}
