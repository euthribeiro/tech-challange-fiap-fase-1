using MediatR;
using wrench.auto.repair.ordem.servico.domain.Data;

namespace wrench.auto.repair.ordem.servico.application.Events
{
    public class OrdemServicoAguardandoAprovacaoEventHandler(
        IOrdemServicoRepository _ordemServicoRepository
    )
        : INotificationHandler<OrdemServicoAguardandoAprovacaoEvent>
    {
        public async Task Handle(OrdemServicoAguardandoAprovacaoEvent notification, CancellationToken cancellationToken)
        {
            var ordemServico = await _ordemServicoRepository
                .ObterPorIdAsync(notification.OrdemServicoId, cancellationToken);

            if (ordemServico == null) return;

            // Notificar cliente sobre o orçamento
        }
    }
}
