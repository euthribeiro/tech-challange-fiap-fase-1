using MediatR;
using wrench.auto.repair.core.Errors;
using wrench.auto.repair.ordem.servico.domain.Data;
using wrench.auto.repair.ordem.servico.domain.Entities;

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
            var ordemServico = await _ordemServicoRepository.ObterPorIdAsync(request.OrdemServicoId, cancellationToken);

            if (ordemServico == null)
                return Result<Guid>.NotFound($"Ordem de serviço com ID {request.OrdemServicoId} não encontrada.");

            var orcamento = new Orcamento(request.OrdemServicoId, DateTime.UtcNow, null);

            await _orcamentoRepository.Adicionar(orcamento, cancellationToken);

            // TODO: Enviar orcamento para o cliente

            var salvo = await _orcamentoRepository.UnitOfWork.CommitAsync();

            if (!salvo)
                return Result<Guid>.Unexpected("Não foi possível criar o orçamento. Por favor tente novamente.");

            return Result<Guid>.Created(orcamento.Id);
        }
    }
}
