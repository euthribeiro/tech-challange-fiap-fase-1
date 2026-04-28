namespace wrench.auto.repair.ordem.servico.domain.Entities
{
    public class Orcamento
    {
        public Guid Id { get; set; }
        public Guid OrdemServicoId { get; set; }
        public DateTime DataEnvio { get; set; }
        public DateTime DataAprovacao { get; set; }
    }
}
