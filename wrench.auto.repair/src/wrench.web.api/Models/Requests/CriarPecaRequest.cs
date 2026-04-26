using wrench.auto.repair.estoque.application.UseCases.CriarPeca;
using wrench.auto.repair.ordem.servico.application.UseCases.CriarOrdemServico;

namespace wrench.web.api.Models.Requests
{
    /// <summary>
    /// Representa a solicitação para criar uma ordem de serviço.
    /// </summary>
    public class CriarPecaRequest
    {
        /// <summary>
        /// Descrição da ordem de serviço
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição da ordem de serviço
        /// </summary>
        public string Descricao { get; set; }
        /// <summary>
        /// Descrição da ordem de serviço
        /// </summary>
        public double Valor { get; set; }
        /// <summary>
        /// Descrição da ordem de serviço
        /// </summary>
        public double Quantidade { get; set; }

        public static implicit operator CriarPecaCommand(CriarPecaRequest request)
        {
            if (request is null) return null;

            return new CriarPecaCommand
            {
                Nome = request.Nome,
                Descricao = request.Descricao,
                Valor = request.Valor,
                Quantidade = request.Quantidade,
            };
        }
    }
}
