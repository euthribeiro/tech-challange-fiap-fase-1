using wrench.auto.repair.estoque.domain.Entities;
using wrench.auto.repair.estoque.domain.Interfaces.Repositories;

namespace wrench.auto.repair.estoque.infra.Repositories;

public class PecaRepository: IPecaRepository
{
    public void CriarPeca(Peca peca)
    {
        throw new NotImplementedException();
    }

    public void DeletaPeca(Guid idPeca)
    {
        throw new NotImplementedException();
    }

    public void EntradaPeca(Guid idPeca, double quantidadeEntrada)
    {
        throw new NotImplementedException();
    }

    public void ReservarPeca(Guid idPeca, double quantidadeReserva)
    {
        throw new NotImplementedException();
    }

    public void SaidaPeca(Guid idPeca, double quantidadeSaida)
    {
        throw new NotImplementedException();
    }

    public List<Peca> ConsultaPecaPorNome(string nomePeca)
    {
        throw new NotImplementedException();
    }

    public Peca ConsultaPecaPorId(Guid idPeca)
    {
        throw new NotImplementedException();
    }

    public List<Peca> ConsultaPecaPorDescricao(string descricaoPeca)
    {
        throw new NotImplementedException();
    }
}