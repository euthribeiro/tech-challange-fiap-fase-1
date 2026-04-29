using MediatR;
using wrench.auto.repair.core.Errors;
using wrench.auto.repair.ordem.servico.application.UseCases.DiagnosticoUseCase;
using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.auto.repair.ordem.servico.domain.Enums;
using wrench.auto.repair.ordem.servico.domain.Interfaces.Repositories;

namespace wrench.auto.repair.ordem.servico.application.UseCases.OrdemServicoUseCase
{
    public class OrdemServicoCommandHandler : IRequestHandler<CriarOrdemServicoCommand, Result<Guid>>

    {
        private readonly IOrdemServicoRepository _ordemServicoRepository;
        private readonly IDiagnosticoRepository _diagnosticoRepository;

        public OrdemServicoCommandHandler(IOrdemServicoRepository repository, IDiagnosticoRepository diagnosticoRepository)
        {
            _ordemServicoRepository = repository;
            _diagnosticoRepository = diagnosticoRepository;
        }

        public async Task<Result<Guid>> Handle(CriarOrdemServicoCommand request, CancellationToken cancellationToken)
        {
            OrdemServico ordemServico = request;

            if (!request.EhValido())
            {
                return Result<Guid>.ValidationError(request.ObterErros());
            }

            await _ordemServicoRepository.IncluirAsync(ordemServico);

            return Result<Guid>.Created(ordemServico.Id);
        }

        


    }
}
