using MediatR;
using wrench.auto.repair.estoque.domain.Entities;
using wrench.auto.repair.estoque.domain.Interfaces.Repositories;

namespace wrench.auto.repair.estoque.application.UseCases.ConsultaPecas;

public class ConsultaPecasHandler(IPecaRepository _pecaRepository): IRequestHandler<ConsultaPecasCommand, IEnumerable<Peca>>
{
    
    
    public Task<IEnumerable<Peca>> Handle(ConsultaPecasCommand request, CancellationToken cancellationToken)
    {
        var pecas = _pecaRepository.ConsultaPecas();
        return Task.FromResult<IEnumerable<Peca>>(pecas);
    }
}