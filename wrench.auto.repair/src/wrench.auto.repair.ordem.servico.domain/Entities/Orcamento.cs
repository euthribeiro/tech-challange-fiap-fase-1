using wrench.auto.repair.core.DomainObjects;

namespace wrench.auto.repair.ordem.servico.domain.Entities
{
    public class Orcamento : Entity, IAggregateRoot
    {
        public Orcamento(Guid ordemServicoId, DateTime dataEnvio, DateTime? dataAprovacao)
        {
            OrdemServicoId = ordemServicoId;
            DataEnvio = dataEnvio;
            DataAprovacao = dataAprovacao;

            Validar();
        }

        public Guid OrdemServicoId { get; private set; }
        public DateTime DataEnvio { get; private set; }
        public DateTime? DataAprovacao { get; private set; }

        private void Validar()
        {
            Validacoes.ValidarSeVazio(OrdemServicoId, "O identificador da ordem de serviço precisa ser informado");
        }
    }
}
