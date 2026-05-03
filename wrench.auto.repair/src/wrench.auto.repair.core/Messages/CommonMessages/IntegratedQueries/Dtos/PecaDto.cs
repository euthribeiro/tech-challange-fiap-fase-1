namespace wrench.auto.repair.core.Messages.CommonMessages.IntegratedQueries.Dtos
{
    public class PecaDto
    {
        public Guid PecaId { get; init; }
        public string Nome { get; init; }
        public decimal ValorUnitario { get; init; }
        public int Quantidade { get; init; }
    }
}
