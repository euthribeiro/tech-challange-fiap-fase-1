using wrench.auto.repair.core.Messages;

namespace wrench.auto.repair.ordem.servico.application.UseCases.OrcamentoUseCase
{
    public class CriarOrcamentoCommand : Command<Guid>
    {
        public Guid OrdemServicoId { get; set; }
    }
}
