using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wrench.auto.repair.ordem.servico.application.UseCases.DiagnosticoUseCase;
using wrench.auto.repair.ordem.servico.application.UseCases.OrcamentoUseCase;
using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.auto.repair.ordem.servico.domain.Enums;
using wrench.auto.repair.ordem.servico.domain.Interfaces.Repositories;
using Xunit;

namespace wrench.auto.repair.ordem.servico.application.tests
{
    public class OrcamentoTests
    {

        [Fact]
        public async Task CriarOrcamento_QuandoCommandForValido()
        {
            // Arrange
            var mockOrdemServicoRepository = new Mock<IOrdemServicoRepository>();
            var mockOrcamentoRepository = new Mock<IOrcamentoRepository>();

            var ordemServicoId = Guid.NewGuid();
            var command = new CriarOrcamentoCommand(ordemServicoId);
            var ordemServicoFake = new OrdemServico(Guid.NewGuid(), ordemServicoId, Guid.NewGuid(), "Barulho na roda", OrdemServicoStatus.Recebida, DateTime.Now);

            mockOrdemServicoRepository
                .Setup(repo => repo.ObterPorIdAsync(ordemServicoId))
                .ReturnsAsync(ordemServicoFake);

            var handler = new OrcamentoCommandHandler(mockOrdemServicoRepository.Object, mockOrcamentoRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Sucesso);
            mockOrcamentoRepository.Verify(x => x.IncluirOrcamento(It.IsAny<Orcamento>()), Times.Once);
        }

        [Fact]
        public async Task CriarOrcamento_OrdemServico_NaoEncontrada()
        {
            // Arrange
            var mockOrdemServicoRepository = new Mock<IOrdemServicoRepository>();
            var mockOrcamentoRepository = new Mock<IOrcamentoRepository>();

            var command = new CriarOrcamentoCommand(Guid.NewGuid());
            var ordemServicoFake = new OrdemServico(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Barulho na roda", OrdemServicoStatus.Recebida, DateTime.Now);

            mockOrdemServicoRepository
                .Setup(repo => repo.ObterPorIdAsync(Guid.NewGuid()))
                .ReturnsAsync(ordemServicoFake);

            var handler = new OrcamentoCommandHandler(mockOrdemServicoRepository.Object, mockOrcamentoRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Sucesso);
            mockOrcamentoRepository.Verify(x => x.IncluirOrcamento(It.IsAny<Orcamento>()), Times.Never);
        }
    }
}
