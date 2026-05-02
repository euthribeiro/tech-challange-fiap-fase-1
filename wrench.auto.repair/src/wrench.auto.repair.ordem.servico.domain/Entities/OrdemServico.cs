using wrench.auto.repair.core.DomainObjects;
using wrench.auto.repair.ordem.servico.domain.Enums;
using wrench.auto.repair.ordem.servico.domain.ValueObjects;

namespace wrench.auto.repair.ordem.servico.domain.Entities
{
    public class OrdemServico : Entity, IAggregateRoot
    {
        public Guid ClienteId { get; private set; }
        public Guid VeiculoId { get; set; }
        public string Descricao { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public OrdemServicoStatus Status { get; private set; }
        public Diagnostico Diagnostico { get; private set; }
        public Orcamento Orcamento { get; private set; }

        public OrdemServico(Guid clienteId, Guid veiculoId, string descricao, OrdemServicoStatus status, DateTime dataCriacao)
        {
            ClienteId = clienteId;
            VeiculoId = veiculoId;
            Descricao = descricao;
            DataCriacao = dataCriacao;
            Status = status;

            Validar();
        }

        public void AdicionarDiagnostico(string solucao, decimal valorEstimado)
        {
            if (Status != OrdemServicoStatus.EmDiagnostico)
                throw new DomainException("A ordem de serviço não está em um status que permite diagnóstico.");

            Diagnostico = new Diagnostico(solucao, valorEstimado);

            Orcamento = new Orcamento(Id, DateTime.UtcNow, null);
            Status = OrdemServicoStatus.AguardandoAprovacao;
        }

        public void SolicitaDiagnostico()
        {
            if (Status != OrdemServicoStatus.Recebida)
                throw new DomainException("A ordem de serviço não está em um status que permite solicitar diagnóstico.");

            Status = OrdemServicoStatus.EmDiagnostico;
        }

        public void AprovarOrcamento()
        {
            if (Status != OrdemServicoStatus.AguardandoAprovacao)
                throw new DomainException("A ordem de serviço não está em um status que permite aprovação.");

            Orcamento.Aprovar();

            Status = OrdemServicoStatus.EmExecucao;
        }

        public void FinalizarOrdemServico()
        {
            if (Status != OrdemServicoStatus.EmExecucao)
                throw new DomainException("A ordem de serviço não está em um status que permite finalização.");
            Status = OrdemServicoStatus.Finalizada;
        }

        private void Validar()
        {
            Validacoes.ValidarSeVazio(ClienteId, "O identificador do cliente não pode ser vazio");
            Validacoes.ValidarSeVazio(VeiculoId, "O identificador do veículo não pode ser vazio");
            Validacoes.ValidarSeVazio(Descricao, "A descrição não pode ser vazia");
            Validacoes.ValidarSeMenorQue((int)Status, 1, "O status precisa ser informado");
        }
    }
}
