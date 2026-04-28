namespace wrench.auto.repair.ordem.servico.domain.Entities
{
    public class Diagnostico
    {
        public Guid Id { get; private set; }
        public Guid OrdemServicoId { get; private set; }
        public Guid MecanicoId { get; private set; }
        public string SolucaoProposta { get; private set; }
        public DateTime DataDiagnostico { get; private set; }

        // Pode haver uma lista de peças sugeridas no diagnóstico
        // public IReadOnlyCollection<PecaSugerida> PecasSugeridas => _pecasSugeridas;

        public Diagnostico(Guid ordemServicoId, Guid mecanicoId, string solucaoProposta, decimal valorEstimado)
        {
            Id = Guid.NewGuid();
            OrdemServicoId = ordemServicoId;
            MecanicoId = mecanicoId;
            SolucaoProposta = solucaoProposta;
            DataDiagnostico = DateTime.UtcNow;
        }

        protected Diagnostico() { }
    }
}
