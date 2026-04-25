using NSubstitute;
using wrench.auto.repair.estoque.application.UseCases.CriarPeca;
using wrench.auto.repair.estoque.domain.Entities;
using wrench.auto.repair.estoque.domain.Interfaces.Repositories;

namespace TestProject1;

public class CriarPecaUseCaseTest
{
    [Fact(DisplayName = "Chama criação de peça no repository")]
    public void Chama_Criar_Peca_No_Repository()
    {
       var pecaRepositoryMock = Substitute.For<IPecaRepository>();
        
        var criaPecaUseCase = new CriarPecaUseCase(pecaRepositoryMock);
        var peca = new Peca
        {
            Nome = "Peca",
            Descricao = "Descricao",
            Quantidade = 10,
            Valor = 15,
        };
        
      
        criaPecaUseCase.criarPeca(peca);
        pecaRepositoryMock.CriarPeca(peca);
       
    }
}