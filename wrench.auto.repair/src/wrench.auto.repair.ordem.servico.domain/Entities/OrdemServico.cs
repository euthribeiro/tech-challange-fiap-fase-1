using wrench.auto.repair.ordem.servico.domain.Enums;

namespace wrench.auto.repair.ordem.servico.domain.Entities
{
    public class OrdemServico
    {
        public Guid Id { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid VeiculoId { get; set; }
        public Guid AtendenteId { get; set; }
        public string Descricao { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public OrdemServicoStatus Status { get; private set; }

        public OrdemServico(Guid clienteId, Guid veiculoId, Guid atendenteId, string descricao, OrdemServicoStatus status)
        {
            Id = Guid.NewGuid();
            ClienteId = clienteId;
            VeiculoId = veiculoId;
            AtendenteId = atendenteId;
            Descricao = descricao;
            DataCriacao = DateTime.UtcNow;
            Status = status;
        }

        public OrdemServico()
        {

        }
    }
}
