using MediatR;
using wrench.auto.repair.estoque.domain.Entities;
using wrench.auto.repair.estoque.domain.Interfaces.Repositories;

namespace wrench.auto.repair.estoque.application.UseCases.ConsultaPecaPorId;

public class ConsultarPecaPorIdHandler : IRequestHandler<ConsultarPecaPorIdCommand, Peca>
{
    IPecaRepository _pecaRepository;

    public ConsultarPecaPorIdHandler(IPecaRepository pecaRepository)
    {
        _pecaRepository = pecaRepository;
    }


    public  Task<Peca> Handle(ConsultarPecaPorIdCommand request, CancellationToken cancellationToken)
    {
        var peca = _pecaRepository.ConsultaPecaPorId(request.IdPeca);
        return Task.FromResult(peca);
    }
}