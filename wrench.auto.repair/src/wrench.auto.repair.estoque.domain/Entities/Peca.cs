namespace wrench.auto.repair.estoque.domain.Entities;

public class Peca
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Nome { get; set; }
    public string Descricao   {get; set; }
    public double Quantidade { get; set; }
    public double Valor { get; set; }
    public DateTime DataCadastro { get; } = DateTime.Now;
    public DateTime DataAlteracao { get; } = DateTime.Now;
}