namespace wrench.auto.repair.estoque.domain.Entities;

public class Peca(string nome, string descricao, double? valor, double? quantidade)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Nome { get; set; } = nome;
    public string Descricao   {get; set; } = descricao;
    public double Valor { get; set; } = valor ?? 0.0;
    public double Quantidade {get; set;} = quantidade ?? 0;
    public DateTime DataCadastro { get; private set; } = DateTime.UtcNow;
}