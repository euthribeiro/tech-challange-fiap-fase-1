using wrench.auto.repair.core.DomainObjects;
using wrench.auto.repair.ordem.servico.domain.Enums;
using wrench.auto.repair.ordem.servico.domain.ValueObjects;

namespace wrench.auto.repair.ordem.servico.domain.Entities
{
    public class OrdemServico : Entity, IAggregateRoot
    {
        public Guid ClienteId { get; private set; }
        public Guid VeiculoId { get; set; }
        public Guid AtendenteId { get; set; }
        public string Descricao { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public OrdemServicoStatus Status { get; private set; }
        public Diagnostico Diagnostico { get; private set; }

        public OrdemServico(Guid clienteId, Guid veiculoId, Guid atendenteId, string descricao, OrdemServicoStatus status, DateTime dataCriacao)
        {
            ClienteId = clienteId;
            VeiculoId = veiculoId;
            AtendenteId = atendenteId;
            Descricao = descricao;
            DataCriacao = dataCriacao;
            Status = status;

            Validar();
        }

        public void AdicionarDiagnostico(Guid mecanicoId, string solucao, decimal valorEstimado)
        {
            if (Status != OrdemServicoStatus.EmDiagnostico)
                throw new DomainException("A ordem de serviço não está em um status que permite diagnóstico.");

            Diagnostico = new Diagnostico(mecanicoId, solucao, valorEstimado);

            Status = OrdemServicoStatus.AguardandoAprovacao;
        }

        public void SolicitaDiagnostico()
        {
            if (Status != OrdemServicoStatus.Recebida)
                throw new DomainException("A ordem de serviço não está em um status que permite solicitar diagnóstico.");

            Status = OrdemServicoStatus.EmDiagnostico;
        }

        private void Validar()
        {
            Validacoes.ValidarSeVazio(ClienteId, "O identificador do cliente não pode ser vazio");
            Validacoes.ValidarSeVazio(VeiculoId, "O identificador do veículo não pode ser vazio");
            Validacoes.ValidarSeVazio(AtendenteId, "O identificador da atendente não pode ser vazio");
            Validacoes.ValidarSeVazio(Descricao, "A descrição não pode ser vazia");
            Validacoes.ValidarSeVazio(Descricao, "A descrição não pode ser vazia");
            Validacoes.ValidarSeMenorQue((int)Status, 1, "O status precisa ser informado");
        }
    }
}
