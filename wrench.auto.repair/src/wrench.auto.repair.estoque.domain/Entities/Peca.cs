using wrench.auto.repair.core.DomainObjects;
using wrench.auto.repair.core.Extensions;

namespace wrench.auto.repair.estoque.domain.Entities;

public class Peca : Entity, IAggregateRoot
{
    public Peca(string nome, string descricao, double valor, double quantidade, bool ativo, DateTime dataCadastro)
    {
        Nome = nome.Trim().RemoverAcentos().RemoverEspacosDuplicados().ToUpperInvariant();
        Descricao = descricao.Trim().RemoverAcentos().RemoverEspacosDuplicados().ToUpperInvariant();
        Valor = Math.Round(valor, 2);
        Quantidade = quantidade;
        Ativo = ativo;
        DataCadastro = dataCadastro;

        Validar();
    }

    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public double Valor { get; private set; }
    public double Quantidade { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime DataCadastro { get; private set; }

    public void AlterarNome(string nome)
    {
        Validacoes.ValidarSeNulo(nome, "O nome da peça não pode ser vazio");
        Nome = nome.Trim().RemoverAcentos().ToUpperInvariant();
    }

    public void AlterarDescricao(string descricao)
    {
        Validacoes.ValidarSeNulo(descricao, "A descrição da peça não pode ser vazio");
        Descricao = descricao.Trim().RemoverAcentos().ToUpperInvariant();
    }

    public void AlterarValor(double valor)
    {
        Validacoes.ValidarSeMenorQue(valor, 0, "O valor da peça não pode ser negativo");
        Valor = Math.Round(valor, 2);
    }

    public void ReporEstoque(int quantidade)
    {
        Validacoes.ValidarSeMenorQue(quantidade, 1, "A quantidade de peças necessárias para repor o estoque é 1");
        Quantidade += quantidade;
    }

    public void BaixarEstoque(int quantidade)
    {
        Validacoes.ValidarSeMenorQue(quantidade, 1, "A quantidade mínima para retirada do estoque é 1");

        if (quantidade > Quantidade)
            throw new DomainException("Quantidade solicitada não disponível no estoque");

        Quantidade -= quantidade;
    }

    public void Inativar()
    {
        Ativo = false;
    }

    public void Ativar()
    {
        Ativo = true;
    }

    private void Validar()
    {
        Validacoes.ValidarSeNulo(Nome, "O nome da peça não pode ser vazio");
        Validacoes.ValidarSeNulo(Descricao, "A descrição da peça não pode ser vazio");
        Validacoes.ValidarSeMenorQue(Valor, 0, "O valor da peça não pode ser negativo");
        Validacoes.ValidarSeMenorQue(Quantidade, 0, "A quantidade não pode ser menor que 0");
    }
}