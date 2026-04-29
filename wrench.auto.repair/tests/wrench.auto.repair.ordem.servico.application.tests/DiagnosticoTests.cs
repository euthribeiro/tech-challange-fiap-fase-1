using Moq;
using wrench.auto.repair.ordem.servico.application.UseCases.DiagnosticoUseCase;
using wrench.auto.repair.ordem.servico.domain.Data;
using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.auto.repair.ordem.servico.domain.Enums;

namespace wrench.auto.repair.ordem.servico.application.tests
{
    public class DiagnosticoTests
    {
        private readonly Mock<IOrdemServicoRepository> _ordemServicoRepositoryMock;
        private readonly DiagnosticoCommandHandler _handler;

        public DiagnosticoTests()
        {
            _ordemServicoRepositoryMock = new Mock<IOrdemServicoRepository>();
            _handler = new DiagnosticoCommandHandler(_ordemServicoRepositoryMock.Object);
        }

        [Fact(DisplayName = "Solicitar Diagnóstico Com Sucesso")]
        [Trait("Ordem Serviço", "Application")]
        public async Task SolicitarDiagnostico_OrdemServico_Valida_Com_Sucesso()
        {
            // Arrange
            var ordemServicoId = Guid.NewGuid();
            var command = new SolicitarDiagnosticoCommand(ordemServicoId);
            var ordemServicoFake = new OrdemServico(Guid.NewGuid(), ordemServicoId, Guid.NewGuid(), "Barulho na roda", OrdemServicoStatus.Recebida, DateTime.Now);

            _ordemServicoRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(ordemServicoId, CancellationToken.None))
                .ReturnsAsync(ordemServicoFake);

            _ordemServicoRepositoryMock.Setup(r => r.UnitOfWork.CommitAsync())
                .Returns(Task.FromResult(true));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Sucesso);
            _ordemServicoRepositoryMock.Verify(repo => repo.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Solicitar Diagnóstico Ordem Serviço Inexistente")]
        [Trait("Ordem Serviço", "Application")]
        public async Task SolicitarDiagnostico_OrdemServico_Nao_Existente_Retorna_Erro()
        {
            // Arrange
            var ordemServicoId = Guid.NewGuid();
            var command = new SolicitarDiagnosticoCommand(Guid.NewGuid());
            var ordemServicoFake = new OrdemServico(Guid.NewGuid(), ordemServicoId, Guid.NewGuid(), "Barulho na roda", OrdemServicoStatus.Recebida, DateTime.Now);

            _ordemServicoRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(ordemServicoId, CancellationToken.None))
                .ReturnsAsync(ordemServicoFake);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Sucesso);
            _ordemServicoRepositoryMock.Verify(repo => repo.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        }


        [Fact(DisplayName = "Realizar Diagnóstico Dados Válido")]
        [Trait("Ordem Serviço", "Application")]
        public async Task RealizarDiagnostico_Deve_Realizar_Diagnostico_Com_Sucesso_Quando_Dados_Forem_Validos()
        {
            // Arrange
            var ordemServicoId = Guid.NewGuid();

            var command = new RealizarDiagnosticoCommand(
                ordemServicoId,
                Guid.NewGuid(),
                200.00m,
                "Substituir pastilhas de freio"
            );

            var ordemServicoFake = new OrdemServico(Guid.NewGuid(), ordemServicoId, Guid.NewGuid(), "Barulho na roda", OrdemServicoStatus.EmDiagnostico, DateTime.Now);
            _ordemServicoRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(ordemServicoId, CancellationToken.None))
                .ReturnsAsync(ordemServicoFake);

            _ordemServicoRepositoryMock.Setup(r => r.UnitOfWork.CommitAsync())
                  .Returns(Task.FromResult(true));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Sucesso);
            _ordemServicoRepositoryMock.Verify(repo => repo.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Realizar Diagnóstico Ordem Serviço Inexistente")]
        [Trait("Ordem Serviço", "Application")]
        public async Task RealizarDiagnostico_Deve_Retornar_Erro_Quando_Ordem_De_Servico_Nao_Existir()
        {
            // Arrange
            var ordemServicoId = Guid.NewGuid();

            var command = new RealizarDiagnosticoCommand(
                ordemServicoId,
                Guid.NewGuid(),
                200.00m,
                "Substituir pastilhas de freio"
            );

            var ordemServicoFake = new OrdemServico(Guid.NewGuid(), ordemServicoId, Guid.NewGuid(), "Barulho na roda", OrdemServicoStatus.EmDiagnostico, DateTime.Now);

            _ordemServicoRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(Guid.NewGuid(), CancellationToken.None))
                .ReturnsAsync(ordemServicoFake);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Sucesso);
            _ordemServicoRepositoryMock.Verify(repo => repo.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
