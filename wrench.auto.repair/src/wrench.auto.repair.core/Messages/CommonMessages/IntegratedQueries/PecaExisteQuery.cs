namespace wrench.auto.repair.core.Messages.CommonMessages.IntegratedQueries
{
    public class PecaExisteQuery(Guid pecaId) : IntegratedQuery
    {
        public Guid PecaId { get; private set; } = pecaId;
    }
}
