using wrench.auto.repair.estoque.domain.Entities;
using wrench.auto.repair.estoque.domain.Enums;

namespace wrench.auto.repair.estoque.domain.Interfaces.Repositories;

public interface IPecaRepository
{
    Task CriarPeca(Peca peca);
    void DeletaPeca(Guid idPeca);
    double MovimentaEstoque(Guid idPeca, TipoMovimentacao tipoMovimentacao, double quantidade);
    //void ReservarPeca(Guid idPeca, double quantidadeReserva);
    IEnumerable<Peca> ConsultaPecaPorNome(string nomePeca);
    Peca ConsultaPecaPorId(Guid idPeca);
    IEnumerable<Peca> ConsultaPecas();
    //List<Peca> ConsultaPecaPorDescricao(string descricaoPeca);
}