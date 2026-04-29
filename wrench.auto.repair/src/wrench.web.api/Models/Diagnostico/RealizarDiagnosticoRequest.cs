using wrench.auto.repair.ordem.servico.application.UseCases.DiagnosticoUseCase;

namespace wrench.web.api.Models.Diagnostico
{
    public class RealizarDiagnosticoRequest
    {
        public Guid OrdemServicoId { get; init; }
        public Guid MecanicoId { get; init; }
        public decimal ValorEstimado { get; init; }
        public string SolucaoProposta { get; init; }

        public static explicit operator RealizarDiagnosticoCommand(RealizarDiagnosticoRequest request)
        {
            return new RealizarDiagnosticoCommand(request.OrdemServicoId, request.MecanicoId, request.ValorEstimado, request.SolucaoProposta);
        }
    }
}
