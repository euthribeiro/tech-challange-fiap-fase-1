using AutoMapper;
using Moq;
using wrench.auto.repair.core.Errors;
using wrench.auto.repair.core.Mediator;
using wrench.auto.repair.core.Messages.CommonMessages.IntegratedQueries;
using wrench.auto.repair.ordem.servico.application.Queries;
using wrench.auto.repair.ordem.servico.application.UseCases.OrcamentoUseCase;
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
        private readonly OrcamentoCommandHandler _orcamentoHandler;
        private readonly OrdemServicoQueryHandler _queryHandler;

        public OrdemServicoTests()
        {
            _repositoryMock = new Mock<IOrdemServicoRepository>();
            _mediatorMock = new Mock<IMediatorHandler>();
            var mapper = new Mock<IMapper>();

            _commandHandler = new OrdemServicoCommandHandler(_mediatorMock.Object, _repositoryMock.Object);
            _orcamentoHandler = new OrcamentoCommandHandler(_repositoryMock.Object);
            _queryHandler = new OrdemServicoQueryHandler(mapper.Object, _repositoryMock.Object);
        }

        [Fact(DisplayName = "Criar Ordem Serviço Com Sucesso")]
        [Trait("Ordem Serviço", "Application")]
        public async Task CriarOrdemServico_DeveCriarEAdicionarOrdemServico_QuandoCommandValido()
        {
            // Arrange
            var command = new CriarOrdemServicoCommand(Guid.NewGuid(), Guid.NewGuid(), "Cliente falou que o carro tá com barulho na roda.");

            // Setup de comportamentos do mock (se necessário)
            _mediatorMock.Setup(m => m.ConsultaIntegrada(It.IsAny<VeiculoExisteEPertenteAoClienteQuery>()))
                         .ReturnsAsync(Result.Ok());

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
            var ordemServico = new OrdemServico(id, Guid.NewGuid(), "Veículo Teste", OrdemServicoStatus.Recebida, DateTime.UtcNow);
            ordemServico.Id = id;

            _repositoryMock.Setup(r => r.ObterPorIdAsync(id, CancellationToken.None))
                .ReturnsAsync(ordemServico);

            // Act
            var result = await _queryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Valor!.Id);
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


        [Fact(DisplayName = "Finalizar ordem deve retornar erro quando comando for inválido")]
        [Trait("Ordem Serviço", "Application")]
        public async Task FinalizarOrdemServico_DeveRetornarValidationError_QuandoCommandInvalido()
        {
            var command = new FinalizarOrdemServicoCommand(Guid.Empty);

            var result = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.False(result.Sucesso);
            _repositoryMock.Verify(r => r.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact(DisplayName = "Finalizar ordem deve retornar not found quando ordem não existir")]
        [Trait("Ordem Serviço", "Application")]
        public async Task FinalizarOrdemServico_DeveRetornarNotFound_QuandoOrdemNaoExistir()
        {
            var ordemId = Guid.NewGuid();
            var command = new FinalizarOrdemServicoCommand(ordemId);

            _repositoryMock.Setup(r => r.ObterPorIdAsync(ordemId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((OrdemServico?)null);

            var result = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.False(result.Sucesso);
        }

        [Fact(DisplayName = "Finalizar ordem deve retornar erro inesperado quando commit falhar")]
        [Trait("Ordem Serviço", "Application")]
        public async Task FinalizarOrdemServico_DeveRetornarUnexpected_QuandoCommitFalhar()
        {
            var ordem = new OrdemServico(Guid.NewGuid(), Guid.NewGuid(), "Troca de pneu", OrdemServicoStatus.EmExecucao, DateTime.UtcNow);
            var command = new FinalizarOrdemServicoCommand(ordem.Id);

            _repositoryMock.Setup(r => r.ObterPorIdAsync(ordem.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(ordem);
            _repositoryMock.Setup(r => r.Atualizar(It.IsAny<OrdemServico>()))
                .Returns(Task.CompletedTask);
            _repositoryMock.Setup(r => r.UnitOfWork.CommitAsync())
                .ReturnsAsync(false);

            var result = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.False(result.Sucesso);
        }

        [Fact(DisplayName = "Finalizar ordem deve finalizar com sucesso quando ordem estiver em execução")]
        [Trait("Ordem Serviço", "Application")]
        public async Task FinalizarOrdemServico_DeveFinalizar_QuandoStatusEmExecucao()
        {
            var ordem = new OrdemServico(Guid.NewGuid(), Guid.NewGuid(), "Alinhamento", OrdemServicoStatus.EmExecucao, DateTime.UtcNow);
            var command = new FinalizarOrdemServicoCommand(ordem.Id);

            _repositoryMock.Setup(r => r.ObterPorIdAsync(ordem.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(ordem);
            _repositoryMock.Setup(r => r.Atualizar(It.IsAny<OrdemServico>()))
                .Returns(Task.CompletedTask);
            _repositoryMock.Setup(r => r.UnitOfWork.CommitAsync())
                .ReturnsAsync(true);

            var result = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.True(result.Sucesso);
            Assert.Equal(OrdemServicoStatus.Finalizada, ordem.Status);
        }

        [Fact(DisplayName = "Aprovar orçamento deve retornar not found quando ordem não existir")]
        [Trait("Ordem Serviço", "Application")]
        public async Task AprovarOrcamento_DeveRetornarNotFound_QuandoOrdemNaoExistir()
        {
            var ordemId = Guid.NewGuid();
            var command = new AprovaOrcamentoCommand(ordemId);

            _repositoryMock.Setup(r => r.ObterPorIdAsync(ordemId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((OrdemServico?)null);

            var result = await _orcamentoHandler.Handle(command, CancellationToken.None);

            Assert.False(result.Sucesso);
        }

        [Fact(DisplayName = "Aprovar orçamento deve retornar erro inesperado quando commit falhar")]
        [Trait("Ordem Serviço", "Application")]
        public async Task AprovarOrcamento_DeveRetornarUnexpected_QuandoCommitFalhar()
        {
            var ordem = new OrdemServico(Guid.NewGuid(), Guid.NewGuid(), "Freio", OrdemServicoStatus.EmDiagnostico, DateTime.UtcNow);
            ordem.AdicionarDiagnostico("Troca de disco e pastilha", 780m);
            var command = new AprovaOrcamentoCommand(ordem.Id);

            _repositoryMock.Setup(r => r.ObterPorIdAsync(ordem.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(ordem);
            _repositoryMock.Setup(r => r.Atualizar(It.IsAny<OrdemServico>()))
                .Returns(Task.CompletedTask);
            _repositoryMock.Setup(r => r.UnitOfWork.CommitAsync())
                .ReturnsAsync(false);

            var result = await _orcamentoHandler.Handle(command, CancellationToken.None);

            Assert.False(result.Sucesso);
        }

        [Fact(DisplayName = "Aprovar orçamento deve atualizar status para em execução")]
        [Trait("Ordem Serviço", "Application")]
        public async Task AprovarOrcamento_DeveAtualizarStatus_QuandoSucesso()
        {
            var ordem = new OrdemServico(Guid.NewGuid(), Guid.NewGuid(), "Motor", OrdemServicoStatus.EmDiagnostico, DateTime.UtcNow);
            ordem.AdicionarDiagnostico("Troca de bomba d'água", 1200m);
            var command = new AprovaOrcamentoCommand(ordem.Id);

            _repositoryMock.Setup(r => r.ObterPorIdAsync(ordem.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(ordem);
            _repositoryMock.Setup(r => r.Atualizar(It.IsAny<OrdemServico>()))
                .Returns(Task.CompletedTask);
            _repositoryMock.Setup(r => r.UnitOfWork.CommitAsync())
                .ReturnsAsync(true);

            var result = await _orcamentoHandler.Handle(command, CancellationToken.None);

            Assert.True(result.Sucesso);
            Assert.Equal(OrdemServicoStatus.EmExecucao, ordem.Status);
        }

        [Fact(DisplayName = "Criar ordem deve retornar not found quando veículo não pertence ao cliente")]
        [Trait("Ordem Serviço", "Application")]
        public async Task CriarOrdemServico_DeveRetornarNotFound_QuandoVeiculoNaoPertenceAoCliente()
        {
            var command = new CriarOrdemServicoCommand(Guid.NewGuid(), Guid.NewGuid(), "Ruído no motor");

            _mediatorMock.Setup(m => m.ConsultaIntegrada(It.IsAny<VeiculoExisteEPertenteAoClienteQuery>()))
                .ReturnsAsync(Result.NotFound());

            var result = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.False(result.Sucesso);
        }

        [Fact(DisplayName = "Criar ordem deve retornar erro inesperado quando commit falhar")]
        [Trait("Ordem Serviço", "Application")]
        public async Task CriarOrdemServico_DeveRetornarUnexpected_QuandoCommitFalhar()
        {
            var command = new CriarOrdemServicoCommand(Guid.NewGuid(), Guid.NewGuid(), "Ruído no câmbio");

            _mediatorMock.Setup(m => m.ConsultaIntegrada(It.IsAny<VeiculoExisteEPertenteAoClienteQuery>()))
                .ReturnsAsync(Result.Ok());
            _repositoryMock.Setup(r => r.Adicionar(It.IsAny<OrdemServico>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _repositoryMock.Setup(r => r.UnitOfWork.CommitAsync())
                .ReturnsAsync(false);

            var result = await _commandHandler.Handle(command, CancellationToken.None);

            Assert.False(result.Sucesso);
        }

        [Fact(DisplayName = "Query deve retornar erro quando identificador for inválido")]
        [Trait("Ordem Serviço", "Application")]
        public async Task ObterOrdemServicoIdQuery_DeveRetornarErro_QuandoIdInvalido()
        {
            var query = new ObterOrdemServicoIdQuery(Guid.Empty);

            var result = await _queryHandler.Handle(query, CancellationToken.None);

            Assert.False(result.Sucesso);
            _repositoryMock.Verify(r => r.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact(DisplayName = "Aprova orçamento command deve ser inválido quando ordem não informada")]
        [Trait("Ordem Serviço", "Application")]
        public void AprovaOrcamentoCommand_DeveSerInvalido_QuandoIdVazio()
        {
            var command = new AprovaOrcamentoCommand(Guid.Empty);

            var valido = command.EhValido();

            Assert.False(valido);
        }

        [Fact(DisplayName = "Finalizar ordem command deve ser inválido quando ordem não informada")]
        [Trait("Ordem Serviço", "Application")]
        public void FinalizarOrdemServicoCommand_DeveSerInvalido_QuandoIdVazio()
        {
            var command = new FinalizarOrdemServicoCommand(Guid.Empty);

            var valido = command.EhValido();

            Assert.False(valido);
        }
    }
}
