namespace wrench.auto.repair.ordem.servico.domain.ValueObjects
{
    public class Diagnostico
    {
        protected Diagnostico() { } // EF Core

        public Guid MecanicoId { get; private set; }
        public string SolucaoProposta { get; private set; }
        public DateTime DataDiagnostico { get; private set; }

        // Pode haver uma lista de peças sugeridas no diagnóstico
        // public IReadOnlyCollection<PecaSugerida> PecasSugeridas => _pecasSugeridas;

        public Diagnostico(Guid mecanicoId, string solucaoProposta, decimal valorEstimado)
        {
            MecanicoId = mecanicoId;
            SolucaoProposta = solucaoProposta;
            DataDiagnostico = DateTime.UtcNow;
        }
    }
}
