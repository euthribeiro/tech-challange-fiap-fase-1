namespace wrench.auto.repair.ordem.servico.domain.ValueObjects
{
    public class Diagnostico
    {
        protected Diagnostico() { } // EF Core

        public string SolucaoProposta { get; private set; }
        public DateTime DataDiagnostico { get; private set; }

        // Pode haver uma lista de peças sugeridas no diagnóstico
        // public IReadOnlyCollection<PecaSugerida> PecasSugeridas => _pecasSugeridas;

        public Diagnostico(string solucaoProposta, decimal valorEstimado)
        {
            SolucaoProposta = solucaoProposta;
            DataDiagnostico = DateTime.UtcNow;
        }
    }
}
