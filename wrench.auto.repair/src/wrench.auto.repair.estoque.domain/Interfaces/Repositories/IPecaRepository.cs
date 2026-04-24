using wrench.auto.repair.estoque.domain.Entities;

namespace wrench.auto.repair.estoque.domain.Interfaces.Repositories;

public interface IPecaRepository
{
    Task CriarPeca(Peca peca);
    void DeletaPeca(Guid idPeca);
    //void EntradaPeca(Guid idPeca, double quantidadeEntrada);
    //void ReservarPeca(Guid idPeca, double quantidadeReserva);
    //void SaidaPeca(Guid idPeca, double quantidadeSaida);
    //List<Peca> ConsultaPecaPorNome(string nomePeca);
    //Peca ConsultaPecaPorId(Guid idPeca);
    //List<Peca> ConsultaPecaPorDescricao(string descricaoPeca);
}