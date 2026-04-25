namespace wrench.auto.repair.estoque.domain.Entities;

public class Peca
{
    

    public Guid Id { get; private set; }
    public string Nome { get; set; }
    public string Descricao   {get; set; }
    public double Valor { get; set; }
    public DateTime DataCadastro { get; private set; }
    
    public Peca(string nome, string descricao, double valor)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Descricao = descricao;
        Valor = valor;
        DataCadastro = DateTime.UtcNow;
    }
}