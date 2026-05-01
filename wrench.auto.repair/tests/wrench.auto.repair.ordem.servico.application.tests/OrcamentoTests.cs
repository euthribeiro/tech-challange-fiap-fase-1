using Moq;
using wrench.auto.repair.core.Data;
using wrench.auto.repair.ordem.servico.application.UseCases.OrcamentoUseCase;
using wrench.auto.repair.ordem.servico.domain.Data;
using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.auto.repair.ordem.servico.domain.Enums;

namespace wrench.auto.repair.ordem.servico.application.tests
{
    public class OrcamentoTests
    {

        [Fact(DisplayName = "Criar Orçamento Ordem De Serviço Válido")]
        [Trait("Ordem Serviço", "Application")]
        public async Task CriarOrcamento_QuandoCommandForValido()
        {
            // Arrange
            var mockOrdemServicoRepository = new Mock<IOrdemServicoRepository>();
            
            var ordemServicoId = Guid.NewGuid();
            var command = new CriarOrcamentoCommand(ordemServicoId);
            var ordemServicoFake = new OrdemServico(Guid.NewGuid(), ordemServicoId, "Barulho na roda", OrdemServicoStatus.EmDiagnostico, DateTime.Now);

            mockOrdemServicoRepository
                .Setup(repo => repo.ObterPorIdAsync(ordemServicoId, CancellationToken.None))
                .ReturnsAsync(ordemServicoFake);

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.CommitAsync()).ReturnsAsync(true);
            mockOrdemServicoRepository.Setup(repo => repo.UnitOfWork).Returns(mockUnitOfWork.Object);

            var handler = new OrcamentoCommandHandler(mockOrdemServicoRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Sucesso);
            
        }

        [Fact(DisplayName = "Criar Orçamento Ordem Serviço Não Encontrada")]
        [Trait("Ordem Serviço", "Application")]
        public async Task CriarOrcamento_OrdemServico_NaoEncontrada()
        {
            // Arrange
            var mockOrdemServicoRepository = new Mock<IOrdemServicoRepository>();
            
            var command = new CriarOrcamentoCommand(Guid.NewGuid());
            var ordemServicoFake = new OrdemServico(Guid.NewGuid(), Guid.NewGuid(), "Barulho na roda", OrdemServicoStatus.Recebida, DateTime.Now);

            mockOrdemServicoRepository
                .Setup(repo => repo.ObterPorIdAsync(Guid.NewGuid(), CancellationToken.None))
                .ReturnsAsync(ordemServicoFake);

            var handler = new OrcamentoCommandHandler(mockOrdemServicoRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Sucesso);          
        }
    }
}
