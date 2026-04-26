using wrench.auto.repair.cadastro.application.Queries.ViewModels;
using wrench.auto.repair.core.Messages;

namespace wrench.auto.repair.cadastro.application.Queries
{
    public class ObterVeiculoPorIdQuery(Guid veiculoId) : Command<VeiculoViewModel>
    {
        public Guid VeiculoId { get; private set; } = veiculoId;
    }
}
