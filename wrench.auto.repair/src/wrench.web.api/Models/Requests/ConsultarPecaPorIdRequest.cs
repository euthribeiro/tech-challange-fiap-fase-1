using wrench.auto.repair.estoque.application.UseCases.ConsultaPecaPorId;
using wrench.auto.repair.estoque.application.UseCases.CriarPeca;
using wrench.auto.repair.ordem.servico.application.UseCases.CriarOrdemServico;

namespace wrench.web.api.Models.Requests
{
    /// <summary>
    /// Representa a solicitação para criar uma ordem de serviço.
    /// </summary>
    public class ConsultarPecaPorIdRequest
    {
        /// <summary>
        /// Id da peça
        /// </summary>
        public Guid IdPeca { get; set; }
        

        public static implicit operator ConsultarPecaPorIdCommand(ConsultarPecaPorIdRequest request)
        {
            return new ConsultarPecaPorIdCommand
            {
              IdPeca = request.IdPeca
            };
        }
    }
}
