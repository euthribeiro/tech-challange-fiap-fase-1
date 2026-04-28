using MediatR;
using wrench.auto.repair.core.Errors;
using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.auto.repair.ordem.servico.domain.Interfaces.Repositories;

namespace wrench.auto.repair.ordem.servico.application.UseCases.OrcamentoUseCase
{
    public class OrcamentoCommandHandler : IRequestHandler<CriarOrcamentoCommand, Result<Guid>>
    {
        private readonly IOrdemServicoRepository _ordemServicoRepository;
        private readonly IOrcamentoRepository _orcamentoRepository;

        public OrcamentoCommandHandler(IOrdemServicoRepository ordemServicoRepository, IOrcamentoRepository orcamentoRepository)
        {
            _ordemServicoRepository = ordemServicoRepository;
            _orcamentoRepository = orcamentoRepository;
        }

        public async Task<Result<Guid>> Handle(CriarOrcamentoCommand request, CancellationToken cancellationToken)
        {
            var ordemServico = _ordemServicoRepository.ObterPorIdAsync(request.OrdemServicoId);

            if (ordemServico == null)
                return Result<Guid>.NotFound($"Ordem de serviço com ID {request.OrdemServicoId} não encontrada.");

            var orcamento = new Orcamento
            {
                OrdemServicoId = request.OrdemServicoId,
                DataEnvio = DateTime.UtcNow
            };

            await _orcamentoRepository.IncluirOrcamento(orcamento);

            // TODO: Enviar orcamento para o cliente

            return Result<Guid>.Created(orcamento.Id);
        }
    }
}
