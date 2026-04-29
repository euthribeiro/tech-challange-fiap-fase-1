using wrench.auto.repair.estoque.application.UseCases.ConsultaPecaPorNome;

namespace wrench.web.api.Models.Requests;

public class ConsultaPecaPorNomeRequest
{
    /// <summary>
    /// Nome da peça
    /// </summary>
    public String NomePeca { get; set; }

    static public implicit operator ConsultaPecaPorNomeCommand(ConsultaPecaPorNomeRequest request)
    {
        return new ConsultaPecaPorNomeCommand
        {
            NomePeca = request.NomePeca
        };
    }
}