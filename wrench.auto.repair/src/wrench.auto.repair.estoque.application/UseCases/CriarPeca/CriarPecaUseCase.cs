using wrench.auto.repair.estoque.domain.Entities;
using wrench.auto.repair.estoque.domain.Interfaces.Repositories;

namespace wrench.auto.repair.estoque.application.UseCases.CriarPeca;

public class CriarPecaUseCase
{
    IPecaRepository pecaRepository;

    public CriarPecaUseCase(IPecaRepository pecaRepository)
    {
        this.pecaRepository = pecaRepository;
    }

    public void criarPeca(Peca peca)
    {
        pecaRepository.CriarPeca(peca);
    }
}


public class Teste
{
    
}