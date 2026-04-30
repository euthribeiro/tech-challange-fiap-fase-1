namespace wrench.auto.repair.core.Messages.CommonMessages.IntegratedQueries
{
    public class VeiculoExisteEPertenteAoClienteQuery(Guid clienteId, Guid veiculoId) : IntegratedQuery
    {
        public Guid ClienteId { get; private set; } = clienteId;
        public Guid VeiculoId { get; private set; } = veiculoId;
    }
}
