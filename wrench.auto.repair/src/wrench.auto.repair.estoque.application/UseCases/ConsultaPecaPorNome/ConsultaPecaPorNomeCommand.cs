using System.ComponentModel.DataAnnotations;
using MediatR;
using wrench.auto.repair.estoque.domain.Entities;

namespace wrench.auto.repair.estoque.application.UseCases.ConsultaPecaPorNome;

public record ConsultaPecaPorNomeCommand() : IRequest<IEnumerable<Peca>>
{
    public String NomePeca
    {
        get;
        set
        {
            NomePeca = value.ToLower();
        }
    }
}