using wrench.auto.repair.ordem.servico.application.UseCases.CriarOrdemServico;

namespace wrench.web.api.Models.Requests
{
    /// <summary>
    /// Representa a solicitação para criar uma ordem de serviço.
    /// </summary>
    public class CriarOrdemServicoRequest
    {
        /// <summary>
        /// Propriedade de exemplo para demonstração. Substitua por propriedades reais conforme necessário.
        /// </summary>
        public int MyProperty { get; set; }

        /// <summary>
        /// Propriedade de exemplo para demonstração. Substitua por propriedades reais conforme necessário. 
        /// </summary>
        public int MyProperty1 { get; set; }

        /// <summary>
        /// Propriedade de exemplo para demonstração. Substitua por propriedades reais conforme necessário.
        /// </summary>
        public int MyProperty2 { get; set; }

        public static implicit operator CriarOrdemServicoCommand(CriarOrdemServicoRequest request)
        {
            if (request is null) return null;

            return new CriarOrdemServicoCommand
            {
                ClienteId = default,
                VeiculoId = default,
                AtendenteId = default,
                Descricao = default,
                DataCriacao = default
            };
        }
    }
}
