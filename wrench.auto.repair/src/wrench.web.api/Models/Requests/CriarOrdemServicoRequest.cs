using wrench.auto.repair.ordem.servico.application.UseCases.CriarOrdemServico;

namespace wrench.web.api.Models.Requests
{
    /// <summary>
    /// Representa a solicitação para criar uma ordem de serviço.
    /// </summary>
    public class CriarOrdemServicoRequest
    {
        /// <summary>
        /// Identificado do cliente
        /// </summary>
        public Guid ClienteId { get; set; }

        /// <summary>
        /// Identificador do veículo
        /// </summary>
        public Guid Veiculo { get; set; }

        /// <summary>
        /// Identificado do atendente que está iniciando a ordem de serviço
        /// </summary>
        public Guid Atendente { get; set; }

        /// <summary>
        /// Descrição da ordem de serviço
        /// </summary>
        public string Descricao { get; set; }

        public static implicit operator CriarOrdemServicoCommand(CriarOrdemServicoRequest request)
        {
            if (request is null) return null;

            return new CriarOrdemServicoCommand
            {
                ClienteId = request.ClienteId,
                VeiculoId = request.Veiculo,
                AtendenteId = request.Atendente,
                Descricao = request.Descricao,
                DataCriacao = DateTime.UtcNow
            };
        }
    }
}
