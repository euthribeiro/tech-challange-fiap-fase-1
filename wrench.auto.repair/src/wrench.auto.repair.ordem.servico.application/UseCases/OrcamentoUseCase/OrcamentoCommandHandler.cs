using MediatR;
using wrench.auto.repair.core.Errors;
using wrench.auto.repair.ordem.servico.domain.Data;
using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.auto.repair.ordem.servico.infra.Repositories;

namespace wrench.auto.repair.ordem.servico.application.UseCases.OrcamentoUseCase
{
    public class OrcamentoCommandHandler : IRequestHandler<CriarOrcamentoCommand, Result<Guid>>,
                                           IRequestHandler<AprovaOrcamentoCommand, Result<bool>>
    {
        private readonly IOrdemServicoRepository _ordemServicoRepository;

        public OrcamentoCommandHandler(IOrdemServicoRepository ordemServicoRepository)
        {
            _ordemServicoRepository = ordemServicoRepository;
        }

        public async Task<Result<Guid>> Handle(CriarOrcamentoCommand request, CancellationToken cancellationToken)
        {
            var ordemServico = await _ordemServicoRepository.ObterPorIdAsync(request.OrdemServicoId, cancellationToken);

            if (ordemServico == null)
                return Result<Guid>.NotFound($"Ordem de serviço com ID {request.OrdemServicoId} não encontrada.");

            ordemServico.AdicionarOrcamento(DateTime.UtcNow);

            // TODO: Enviar orcamento para o cliente

            await _ordemServicoRepository.Atualizar(ordemServico);
            var salvo = await _ordemServicoRepository.UnitOfWork.CommitAsync();

            if (!salvo)
                return Result<Guid>.Unexpected("Não foi possível criar o orçamento. Por favor tente novamente.");

            return Result<Guid>.Created(ordemServico.Id);
        }

        public async Task<Result<bool>> Handle(AprovaOrcamentoCommand request, CancellationToken cancellationToken)
        {
            var ordemServico = await _ordemServicoRepository.ObterPorIdAsync(request.OrdemServicoId, cancellationToken);

            if (ordemServico == null)
                return Result<bool>.NotFound($"Ordem de serviço com ID {request.OrdemServicoId} não encontrada.");

            ordemServico.AprovarOrcamento();

            await _ordemServicoRepository.Atualizar(ordemServico);
            var salvo = await _ordemServicoRepository.UnitOfWork.CommitAsync();

            if (!salvo)
                return Result<bool>.Unexpected("Não foi possível aprovar o orçamento. Por favor tente novamente.");

            return Result<bool>.Ok(true);
        }
    }
}
