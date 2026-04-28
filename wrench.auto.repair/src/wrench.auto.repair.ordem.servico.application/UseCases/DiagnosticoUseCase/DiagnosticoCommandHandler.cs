using MediatR;
using wrench.auto.repair.core.Errors;
using wrench.auto.repair.ordem.servico.domain.Interfaces.Repositories;

namespace wrench.auto.repair.ordem.servico.application.UseCases.DiagnosticoUseCase
{
    public class DiagnosticoCommandHandler : IRequestHandler<SolicitarDiagnosticoCommand, Result<Guid>>,
                                             IRequestHandler<RealizarDiagnosticoCommand, Result<Guid>>
    {
        private readonly IDiagnosticoRepository _diagnosticoRepository;
        private readonly IOrdemServicoRepository _ordemServicoRepository;

        public DiagnosticoCommandHandler(IDiagnosticoRepository repository, IOrdemServicoRepository ordemServicoRepository)
        {
            _diagnosticoRepository = repository;
            _ordemServicoRepository = ordemServicoRepository;
        }

        public async Task<Result<Guid>> Handle(SolicitarDiagnosticoCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido())
            {
                var erros = request.ValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Guid>.ValidationError(erros);
            }

            var ordemServico = await _ordemServicoRepository.ObterPorIdAsync(request.OrdemServicoId);

            if (ordemServico == null)
                return Result<Guid>.NotFound($"Ordem de serviço com ID {request.OrdemServicoId} não encontrada.");

            ordemServico.SolicitaDiagnostico();

            await _ordemServicoRepository.AtualizarAsync(ordemServico);

            return Result<Guid>.Ok(ordemServico.Id);
        }

        public async Task<Result<Guid>> Handle(RealizarDiagnosticoCommand request, CancellationToken cancellationToken)
        {
            var ordemServico = await _ordemServicoRepository.ObterPorIdAsync(request.OrdemServicoId);

            if (ordemServico == null)
                return Result<Guid>.NotFound($"Ordem de serviço com ID {request.OrdemServicoId} não encontrada.");

            ordemServico.AdicionarDiagnostico(request.MecanicoId, request.SolucaoProposta, request.ValorEstimado);

            await _diagnosticoRepository.IncluirAsync(ordemServico.Diagnostico);

            await _ordemServicoRepository.AtualizarAsync(ordemServico);

            // TODO: Chamar comando de Orçamento (Criar e enviar)

            return Result<Guid>.Created(ordemServico.Diagnostico.Id);
        }
    }
}
