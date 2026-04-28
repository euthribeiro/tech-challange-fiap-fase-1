using MediatR;
using wrench.auto.repair.estoque.domain.Entities;
using wrench.auto.repair.estoque.domain.Interfaces.Repositories;

namespace wrench.auto.repair.estoque.application.UseCases.CriarPeca;

public class CriarPecaHandler : IRequestHandler<CriarPecaCommand, Peca>
{
    private readonly IPecaRepository _pecaRepository;

    public CriarPecaHandler(IPecaRepository pecaRepository)
    {
        _pecaRepository = pecaRepository;
    }

    public async Task<Peca> Handle(CriarPecaCommand request, CancellationToken cancellationToken)
    {
        var peca = new Peca(
            request.Nome,
            request.Descricao,
            request.Valor,
            request.Quantidade
        );
        
      await _pecaRepository.CriarPeca(peca);
      return peca;
    }
}


