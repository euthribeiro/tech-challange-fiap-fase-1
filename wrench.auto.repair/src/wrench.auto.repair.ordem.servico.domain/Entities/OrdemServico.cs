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
        public Diagnostico Diagnostico { get; private set; }
        public OrdemServico(Guid clienteId, Guid veiculoId, Guid atendenteId, string descricao, OrdemServicoStatus status, DateTime dataCriacao)
        {
            Id = Guid.NewGuid();
            ClienteId = clienteId;
            VeiculoId = veiculoId;
            AtendenteId = atendenteId;
            Descricao = descricao;
            DataCriacao = dataCriacao;
            Status = status;
        }
        public OrdemServico() { }

        public void AdicionarDiagnostico(Guid mecanicoId, string solucao, decimal valorEstimado)
        {
            if (Status != OrdemServicoStatus.EmDiagnostico)
                throw new Exception("A ordem de serviço não está em um status que permite diagnóstico.");

            Diagnostico = new Diagnostico(this.Id, mecanicoId, solucao, valorEstimado);

            Status = OrdemServicoStatus.AguardandoAprovacao;
        }

        public void SolicitaDiagnostico()
        {
            if (Status != OrdemServicoStatus.Recebida)
                throw new Exception("A ordem de serviço não está em um status que permite solicitar diagnóstico.");
            Status = OrdemServicoStatus.EmDiagnostico;

        }
    }
}
