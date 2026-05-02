using Moq;
using Moq.AutoMock;
using wrench.auto.repair.estoque.application.Commands;
using wrench.auto.repair.estoque.domain.Data;
using wrench.auto.repair.estoque.domain.Entities;

namespace wrench.auto.repair.estoque.application.tests.PecaCommand
{
    public class PecaCommandHandlerTests
    {
        [Fact(DisplayName = "Ativar Peça Existente Com Sucesso")]
        [Trait("Estoque", "Application")]
        public async Task AtivarPeca_PecaExistente_DeveAtivarPeca()
        {
            // Arrange
            var pecaInativa = new Peca("Pneu", "Aro 15", 100, 10, false, DateTime.UtcNow);
            var ativarPecaCommand =
                new AtivarPecaCommand(Guid.NewGuid());
            var automocker = new AutoMocker();

            var pecaCommandHandler = automocker.CreateInstance<PecaCommandHandler>();

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Peca?>(pecaInativa));

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.UnitOfWork.CommitAsync())
                .Returns(Task.FromResult(true));

            // Act
            var resultado = await pecaCommandHandler
                .Handle(ativarPecaCommand, CancellationToken.None);

            // Assert
            Assert.True(resultado.Sucesso);


            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.Atualizar(It.IsAny<Peca>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
               .Verify(p => p.UnitOfWork.CommitAsync(), Times.Once);
        }

        [Fact(DisplayName = "Ativar Peça Inexistente Deve Retornar Erro")]
        [Trait("Estoque", "Application")]
        public async Task AtivarPeca_PecaInexistente_DeveRetornarErro()
        {
            // Arrange
            var pecaInativa = new Peca("Pneu", "Aro 15", 100, 10, false, DateTime.UtcNow);
            var ativarPecaCommand =
                new AtivarPecaCommand(Guid.NewGuid());
            var automocker = new AutoMocker();

            var pecaCommandHandler = automocker.CreateInstance<PecaCommandHandler>();

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Peca?>(null));

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.UnitOfWork.CommitAsync())
                .Returns(Task.FromResult(true));

            // Act
            var resultado = await pecaCommandHandler
                .Handle(ativarPecaCommand, CancellationToken.None);

            // Assert
            Assert.False(resultado.Sucesso);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.Atualizar(It.IsAny<Peca>()), Times.Never);

            automocker.GetMock<IPecaRepository>()
               .Verify(p => p.UnitOfWork.CommitAsync(), Times.Never);
        }

        [Fact(DisplayName = "Inativar Peça Existente Com Sucesso")]
        [Trait("Estoque", "Application")]
        public async Task InativarPeca_PecaExistente_DeveAtivarPeca()
        {
            // Arrange
            var pecaAtiva = new Peca("Pneu", "Aro 15", 100, 10, true, DateTime.UtcNow);
            var inativarPecaCommand =
                new InativarPecaCommand(Guid.NewGuid());
            var automocker = new AutoMocker();

            var pecaCommandHandler = automocker.CreateInstance<PecaCommandHandler>();

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Peca?>(pecaAtiva));

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.UnitOfWork.CommitAsync())
                .Returns(Task.FromResult(true));

            // Act
            var resultado = await pecaCommandHandler
                .Handle(inativarPecaCommand, CancellationToken.None);

            // Assert
            Assert.True(resultado.Sucesso);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.Atualizar(It.IsAny<Peca>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
               .Verify(p => p.UnitOfWork.CommitAsync(), Times.Once);
        }

        [Fact(DisplayName = "Inativar Peça Inexistente Deve Retornar Erro")]
        [Trait("Estoque", "Application")]
        public async Task InativarPeca_PecaInexistente_DeveRetornarErro()
        {
            // Arrange
            var pecaInativa = new Peca("Pneu", "Aro 15", 100, 10, true, DateTime.UtcNow);
            var inativarPecaCommand =
                new InativarPecaCommand(Guid.NewGuid());
            var automocker = new AutoMocker();

            var pecaCommandHandler = automocker.CreateInstance<PecaCommandHandler>();

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Peca?>(null));

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.UnitOfWork.CommitAsync())
                .Returns(Task.FromResult(true));

            // Act
            var resultado = await pecaCommandHandler
                .Handle(inativarPecaCommand, CancellationToken.None);

            // Assert
            Assert.False(resultado.Sucesso);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.Atualizar(It.IsAny<Peca>()), Times.Never);

            automocker.GetMock<IPecaRepository>()
               .Verify(p => p.UnitOfWork.CommitAsync(), Times.Never);
        }

        [Fact(DisplayName = "Repor Peça Existente Com Sucesso")]
        [Trait("Estoque", "Application")]
        public async Task ReporPeca_PecaExistente_DeveReporPeca()
        {
            // Arrange
            var pecaAtiva = new Peca("Pneu", "Aro 15", 100, 10, true, DateTime.UtcNow);
            var reporPecaCommand =
                new ReporPecaCommand(Guid.NewGuid(), 10);
            var automocker = new AutoMocker();

            var pecaCommandHandler = automocker.CreateInstance<PecaCommandHandler>();

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Peca?>(pecaAtiva));

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.UnitOfWork.CommitAsync())
                .Returns(Task.FromResult(true));

            // Act
            var resultado = await pecaCommandHandler
                .Handle(reporPecaCommand, CancellationToken.None);

            // Assert
            Assert.True(resultado.Sucesso);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.Atualizar(It.IsAny<Peca>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
               .Verify(p => p.UnitOfWork.CommitAsync(), Times.Once);
        }

        [Fact(DisplayName = "Repor Peça Inexistente Com Sucesso")]
        [Trait("Estoque", "Application")]
        public async Task ReporPeca_PecaInexistente_DeveRetornarErro()
        {
            // Arrange
            var pecaAtiva = new Peca("Pneu", "Aro 15", 100, 10, true, DateTime.UtcNow);
            var reporPecaCommand =
                new ReporPecaCommand(Guid.NewGuid(), 10);
            var automocker = new AutoMocker();

            var pecaCommandHandler = automocker.CreateInstance<PecaCommandHandler>();

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Peca?>(null));

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.UnitOfWork.CommitAsync())
                .Returns(Task.FromResult(true));

            // Act
            var resultado = await pecaCommandHandler
                .Handle(reporPecaCommand, CancellationToken.None);

            // Assert
            Assert.False(resultado.Sucesso);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.Atualizar(It.IsAny<Peca>()), Times.Never);

            automocker.GetMock<IPecaRepository>()
               .Verify(p => p.UnitOfWork.CommitAsync(), Times.Never);
        }

        [Fact(DisplayName = "Baixar Estoque Peça Existente Com Sucesso")]
        [Trait("Estoque", "Application")]
        public async Task BaixarEstoque_PecaExistente_DeveBaixarPecaNoEstoque()
        {
            // Arrange
            var pecaAtiva = new Peca("Pneu", "Aro 15", 100, 10, true, DateTime.UtcNow);
            var baixarPecaCommand =
                new BaixarPecaCommand(Guid.NewGuid(), 10);
            var automocker = new AutoMocker();

            var pecaCommandHandler = automocker.CreateInstance<PecaCommandHandler>();

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Peca?>(pecaAtiva));

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.UnitOfWork.CommitAsync())
                .Returns(Task.FromResult(true));

            // Act
            var resultado = await pecaCommandHandler
                .Handle(baixarPecaCommand, CancellationToken.None);

            // Assert
            Assert.True(resultado.Sucesso);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.Atualizar(It.IsAny<Peca>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
               .Verify(p => p.UnitOfWork.CommitAsync(), Times.Once);
        }

        [Fact(DisplayName = "Baixar Estoque Peça Inexistente")]
        [Trait("Estoque", "Application")]
        public async Task BaixarEstoque_PecaInexistente_DeveRetornarErro()
        {
            // Arrange
            var pecaAtiva = new Peca("Pneu", "Aro 15", 100, 10, true, DateTime.UtcNow);
            var baixarPecaCommand =
                new BaixarPecaCommand(Guid.NewGuid(), 10);
            var automocker = new AutoMocker();

            var pecaCommandHandler = automocker.CreateInstance<PecaCommandHandler>();

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Peca?>(null));

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.UnitOfWork.CommitAsync())
                .Returns(Task.FromResult(true));

            // Act
            var resultado = await pecaCommandHandler
                .Handle(baixarPecaCommand, CancellationToken.None);

            // Assert
            Assert.False(resultado.Sucesso);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.Atualizar(It.IsAny<Peca>()), Times.Never);

            automocker.GetMock<IPecaRepository>()
               .Verify(p => p.UnitOfWork.CommitAsync(), Times.Never);
        }

        [Fact(DisplayName = "Atualizar Peça Existente Com Sucesso")]
        [Trait("Estoque", "Application")]
        public async Task AtualizarPeca_PecaExistente_DeveAtualizarComSucesso()
        {
            // Arrange
            var peca = new Peca("Pneu", "Aro 15", 100, 10, true, DateTime.UtcNow);
            var atualizarPecaCommand =
                new AtualizarPecaCommand(Guid.NewGuid(), "Pneu XPTO", "Aro 15 sem camara", 200, true);
            var automocker = new AutoMocker();

            var pecaCommandHandler = automocker.CreateInstance<PecaCommandHandler>();

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Peca?>(peca));

            automocker.GetMock<IPecaRepository>()
             .Setup(p => p.ObterPorNomeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
             .Returns(Task.FromResult<Peca?>(null));

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.UnitOfWork.CommitAsync())
                .Returns(Task.FromResult(true));

            // Act
            var resultado = await pecaCommandHandler
                .Handle(atualizarPecaCommand, CancellationToken.None);

            // Assert
            Assert.True(resultado.Sucesso);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.ObterPorNomeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.Atualizar(It.IsAny<Peca>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
               .Verify(p => p.UnitOfWork.CommitAsync(), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Peça Inexistente Com Sucesso")]
        [Trait("Estoque", "Application")]
        public async Task AtualizarPeca_PecaInexistente_DeveAtualizarComSucesso()
        {
            // Arrange
            var peca = new Peca("Pneu", "Aro 15", 100, 10, true, DateTime.UtcNow);
            var atualizarPecaCommand =
                new AtualizarPecaCommand(Guid.NewGuid(), "Pneu XPTO", "Aro 15 sem camara", 200, true);
            var automocker = new AutoMocker();

            var pecaCommandHandler = automocker.CreateInstance<PecaCommandHandler>();

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Peca?>(null));

            automocker.GetMock<IPecaRepository>()
             .Setup(p => p.ObterPorNomeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
             .Returns(Task.FromResult<Peca?>(null));

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.UnitOfWork.CommitAsync())
                .Returns(Task.FromResult(true));

            // Act
            var resultado = await pecaCommandHandler
                .Handle(atualizarPecaCommand, CancellationToken.None);

            // Assert
            Assert.False(resultado.Sucesso);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.ObterPorNomeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.Atualizar(It.IsAny<Peca>()), Times.Never);

            automocker.GetMock<IPecaRepository>()
               .Verify(p => p.UnitOfWork.CommitAsync(), Times.Never);
        }

        [Fact(DisplayName = "Atualizar Peça Nome Já Existe Para Outra Peça")]
        [Trait("Estoque", "Application")]
        public async Task AtualizarPeca_NomePecaEmUso_NaoDeveAtualizar()
        {
            // Arrange
            var pecaComOutroId = new Peca("Pneu XPTO", "Aro 15", 100, 10, true, DateTime.UtcNow);
            var peca = new Peca("Pneu", "Aro 15", 100, 10, true, DateTime.UtcNow);
            var atualizarPecaCommand =
                new AtualizarPecaCommand(Guid.NewGuid(), "Pneu XPTO", "Aro 15 sem camara", 200, true);
            var automocker = new AutoMocker();

            var pecaCommandHandler = automocker.CreateInstance<PecaCommandHandler>();

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Peca?>(peca));

            automocker.GetMock<IPecaRepository>()
             .Setup(p => p.ObterPorNomeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
             .Returns(Task.FromResult<Peca?>(pecaComOutroId));

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.UnitOfWork.CommitAsync())
                .Returns(Task.FromResult(true));

            // Act
            var resultado = await pecaCommandHandler
                .Handle(atualizarPecaCommand, CancellationToken.None);

            // Assert
            Assert.False(resultado.Sucesso);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.ObterPorNomeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.Atualizar(It.IsAny<Peca>()), Times.Never);

            automocker.GetMock<IPecaRepository>()
               .Verify(p => p.UnitOfWork.CommitAsync(), Times.Never);
        }

        [Fact(DisplayName = "Cadastrar Peça Com Sucesso")]
        [Trait("Estoque", "Application")]
        public async Task CadastrarPeca_NovaPeca_DeveCadastrarComSucesso()
        {
            // Arrange
            var cadastrarPecaCommand =
                new CadastrarPecaCommand("Pneu XPTO", "Aro 15 sem camara", 200, 10, true);
            var automocker = new AutoMocker();

            var pecaCommandHandler = automocker.CreateInstance<PecaCommandHandler>();

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.ObterPorNomeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Peca?>(null));

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.UnitOfWork.CommitAsync())
                .Returns(Task.FromResult(true));

            // Act
            var resultado = await pecaCommandHandler
                .Handle(cadastrarPecaCommand, CancellationToken.None);

            // Assert
            Assert.True(resultado.Sucesso);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.ObterPorNomeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.Adicionar(It.IsAny<Peca>(), It.IsAny<CancellationToken>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
               .Verify(p => p.UnitOfWork.CommitAsync(), Times.Once);
        }

        [Fact(DisplayName = "Cadastrar Peça Que Já Existe")]
        [Trait("Estoque", "Application")]
        public async Task CadastrarPeca_PecaExistente_NaoDeveCadastrar()
        {
            // Arrange
            var peca = new Peca("Pneu XPTO", "Aro 15", 200, 10, true, DateTime.UtcNow);
            var cadastrarPecaCommand =
                new CadastrarPecaCommand("Pneu XPTO", "Aro 15", 200, 10, true);
            var automocker = new AutoMocker();

            var pecaCommandHandler = automocker.CreateInstance<PecaCommandHandler>();

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.ObterPorNomeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Peca?>(peca));

            automocker.GetMock<IPecaRepository>()
                .Setup(p => p.UnitOfWork.CommitAsync())
                .Returns(Task.FromResult(true));

            // Act
            var resultado = await pecaCommandHandler
                .Handle(cadastrarPecaCommand, CancellationToken.None);

            // Assert
            Assert.False(resultado.Sucesso);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.ObterPorNomeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);

            automocker.GetMock<IPecaRepository>()
                .Verify(p => p.Adicionar(It.IsAny<Peca>(), It.IsAny<CancellationToken>()), Times.Never);

            automocker.GetMock<IPecaRepository>()
               .Verify(p => p.UnitOfWork.CommitAsync(), Times.Never);
        }
    }
}
