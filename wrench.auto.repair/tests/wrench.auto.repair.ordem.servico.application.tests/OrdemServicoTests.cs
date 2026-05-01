using Moq;
using wrench.auto.repair.core.Mediator;
using wrench.auto.repair.core.Messages.CommonMessages.IntegratedQueries;
using wrench.auto.repair.ordem.servico.application.UseCases.OrdemServicoUseCase;
using wrench.auto.repair.ordem.servico.domain.Data;
using wrench.auto.repair.ordem.servico.domain.Entities;

namespace wrench.auto.repair.ordem.servico.application.tests
{
    public class OrdemServicoTests
    {
        private readonly Mock<IOrdemServicoRepository> _repositoryMock;
        private readonly Mock<IMediatorHandler> _mediatorMock;
        private readonly OrdemServicoCommandHandler _handler;

        public OrdemServicoTests()
        {
            _repositoryMock = new Mock<IOrdemServicoRepository>();
            _mediatorMock = new Mock<IMediatorHandler>();

            _handler = new OrdemServicoCommandHandler(_mediatorMock.Object, _repositoryMock.Object);
        }

        [Fact(DisplayName = "Criar Ordem Serviço Com Sucesso")]
        [Trait("Ordem Serviço", "Application")]
        public async Task CriarOrdemServico_DeveCriarEAdicionarOrdemServico_QuandoCommandValido()
        {
            // Arrange
            var command = new CriarOrdemServicoCommand(Guid.NewGuid(), Guid.NewGuid(), "Cliente falou que o carro tá com barulho na roda.");

            // Setup de comportamentos do mock (se necessário)
            _mediatorMock.Setup(m => m.ConsultaIntegrada(It.IsAny<VeiculoExisteEPertenteAoClienteQuery>()))
                         .ReturnsAsync(true);

            _repositoryMock.Setup(r => r.Adicionar(It.IsAny<OrdemServico>(), It.IsAny<CancellationToken>()))
                           .Returns(Task.CompletedTask);

            _repositoryMock.Setup(r => r.UnitOfWork.CommitAsync())
                         .Returns(Task.FromResult(true));

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado);
            Assert.True(resultado.Sucesso);
            _repositoryMock.Verify(r => r.Adicionar(It.IsAny<OrdemServico>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Criar Ordem Serviço Comando Inválido")]
        [Trait("Ordem Serviço", "Application")]
        public async Task CriarOrdemServico_DeveRetornarErro_QuandoDadosForemInvalidos()
        {
            // Arrange
            var command = new CriarOrdemServicoCommand(default, default, String.Empty);

            // Act
            var resultado = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(resultado.Sucesso);
        }
    }
}
