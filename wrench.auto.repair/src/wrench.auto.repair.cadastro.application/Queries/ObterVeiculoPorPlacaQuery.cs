using wrench.auto.repair.cadastro.application.Queries.ViewModels;
using wrench.auto.repair.core.Messages;

namespace wrench.auto.repair.cadastro.application.Queries
{
    public class ObterVeiculoPorPlacaQuery : Command<VeiculoViewModel>
    {
        public ObterVeiculoPorPlacaQuery(string placa)
        {
            Placa = placa;
        }

        public string Placa { get; private set; }
    }
}
