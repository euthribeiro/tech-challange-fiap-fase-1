using MediatR;
using wrench.auto.repair.core.Errors;
using wrench.auto.repair.core.Mediator;
using wrench.auto.repair.core.Messages.CommonMessages.IntegratedQueries;
using wrench.auto.repair.ordem.servico.domain.Data;
using wrench.auto.repair.ordem.servico.domain.Entities;

namespace wrench.auto.repair.ordem.servico.application.UseCases.OrdemServicoUseCase
{
    public class OrdemServicoCommandHandler(
        IMediatorHandler _mediatorHandler,
        IOrdemServicoRepository repository
    ) : IRequestHandler<CriarOrdemServicoCommand, Result<Guid>>

    {
        private readonly IOrdemServicoRepository _ordemServicoRepository = repository;

        public async Task<Result<Guid>> Handle(CriarOrdemServicoCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido())
                return Result<Guid>.ValidationError(request.ObterErros());

            var veiculoExisteEPertenceAoClienteQuery =
                new VeiculoExisteEPertenteAoClienteQuery(request.ClienteId, request.VeiculoId);

            var veiculoExisteEPertenceAoCliente = await _mediatorHandler
                .ConsultaIntegrada(veiculoExisteEPertenceAoClienteQuery);

            if (!veiculoExisteEPertenceAoCliente)
                return Result<Guid>.NotFound("Cliente/Veículo não encontrado");

            OrdemServico ordemServico = request;

            await _ordemServicoRepository.Adicionar(ordemServico, cancellationToken);

            var salvo = await _ordemServicoRepository.UnitOfWork.CommitAsync();

            if (!salvo) return Result<Guid>.Unexpected("Não foi possível criar a ordem de serviço. Por favor tente novamente.");

            return Result<Guid>.Created(ordemServico.Id);
        }
    }
}
