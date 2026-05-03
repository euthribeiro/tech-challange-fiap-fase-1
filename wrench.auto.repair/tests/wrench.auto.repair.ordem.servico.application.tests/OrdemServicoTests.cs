using Moq;
using wrench.auto.repair.core.Mediator;
using wrench.auto.repair.core.Messages.CommonMessages.IntegratedQueries;
using wrench.auto.repair.ordem.servico.application.Queries;
using wrench.auto.repair.ordem.servico.application.UseCases.OrdemServicoUseCase;
using wrench.auto.repair.ordem.servico.domain.Data;
using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.auto.repair.ordem.servico.domain.Enums;

namespace wrench.auto.repair.ordem.servico.application.tests
{
    public class OrdemServicoTests
    {
        private readonly Mock<IOrdemServicoRepository> _repositoryMock;
        private readonly Mock<IMediatorHandler> _mediatorMock;
        private readonly OrdemServicoCommandHandler _commandHandler;
        private readonly OrdemServicoQueryHandler _queryHandler;

        public OrdemServicoTests()
        {
            _repositoryMock = new Mock<IOrdemServicoRepository>();
            _mediatorMock = new Mock<IMediatorHandler>();

            _commandHandler = new OrdemServicoCommandHandler(_mediatorMock.Object, _repositoryMock.Object);

            _queryHandler = new OrdemServicoQueryHandler(_repositoryMock.Object);
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
            var resultado = await _commandHandler.Handle(command, CancellationToken.None);

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
            var resultado = await _commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(resultado.Sucesso);
        }

        [Fact(DisplayName = "Consultar Ordem Serviço Com Sucesso")]
        [Trait("Ordem Serviço", "Application")]
        public async Task OrdemServico_DeveConsultarPorId_QuandoQueryValido()
        {
            // Arrange
            var id = Guid.NewGuid();
            var query = new ObterOrdemServicoIdQuery(id);
            var ordemServico = new OrdemServico(id, Guid.NewGuid(), "Veículo Teste", OrdemServicoStatus.Recebida , DateTime.UtcNow);
            ordemServico.Id = id;

            _repositoryMock.Setup(r => r.ObterPorIdAsync(id, CancellationToken.None))
                .ReturnsAsync(ordemServico);

            // Act
            var result = await _queryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Valor.OrdemServicoId);
            _repositoryMock.Verify(r => r.ObterPorIdAsync(id, CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Consultar Ordem Serviço Com Falha - Não Encontrado")]
        [Trait("Ordem Serviço", "Application")]
        public async Task OrdemServico_DeveRetornarNulo_QuandoOrdemServicoNaoExistir()
        {
            // Arrange
            var id = Guid.NewGuid();
            var query = new ObterOrdemServicoIdQuery(id);

            _repositoryMock.Setup(r => r.ObterPorIdAsync(id, CancellationToken.None))
                .ReturnsAsync((OrdemServico)null);

            // Act
            var result = await _queryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.Sucesso);
            _repositoryMock.Verify(r => r.ObterPorIdAsync(id, CancellationToken.None), Times.Once);
        }
    }
}
