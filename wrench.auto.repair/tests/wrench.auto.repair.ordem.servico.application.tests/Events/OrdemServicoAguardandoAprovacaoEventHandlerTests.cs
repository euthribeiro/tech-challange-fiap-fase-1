using Moq;
using wrench.auto.repair.ordem.servico.application.Events;
using wrench.auto.repair.ordem.servico.domain.Data;
using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.auto.repair.ordem.servico.domain.Enums;

namespace wrench.auto.repair.ordem.servico.application.tests.Events
{
    public class OrdemServicoAguardandoAprovacaoEventHandlerTests
    {
        [Fact(DisplayName = "Event handler não deve falhar quando ordem não existir")]
        [Trait("Ordem Serviço", "Application")]
        public async Task Handle_DeveConcluir_QuandoOrdemNaoExistir()
        {
            var id = Guid.NewGuid();
            var repo = new Mock<IOrdemServicoRepository>();
            repo.Setup(r => r.ObterPorIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((OrdemServico?)null);

            var handler = new OrdemServicoAguardandoAprovacaoEventHandler(repo.Object);
            await handler.Handle(new OrdemServicoAguardandoAprovacaoEvent(id), CancellationToken.None);

            repo.Verify(r => r.ObterPorIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Event handler deve consultar ordem quando existir")]
        [Trait("Ordem Serviço", "Application")]
        public async Task Handle_DeveConsultarOrdem_QuandoExistir()
        {
            var ordem = new OrdemServico(Guid.NewGuid(), Guid.NewGuid(), "Freios", OrdemServicoStatus.AguardandoAprovacao, DateTime.UtcNow);
            var repo = new Mock<IOrdemServicoRepository>();
            repo.Setup(r => r.ObterPorIdAsync(ordem.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(ordem);

            var handler = new OrdemServicoAguardandoAprovacaoEventHandler(repo.Object);
            await handler.Handle(new OrdemServicoAguardandoAprovacaoEvent(ordem.Id), CancellationToken.None);

            repo.Verify(r => r.ObterPorIdAsync(ordem.Id, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
