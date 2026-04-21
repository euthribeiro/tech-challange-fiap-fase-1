using wrench.auto.repair.core.DomainObjects;

namespace wrench.auto.repair.cadastro.domain.Entities
{
    public class Veiculo : Entity, IAggregateRoot
    {
        public Veiculo(Guid clienteId, string marca, string modelo, string cor, int anoFabricacao, int anoModelo, string placaDoVeiculo, string? descricao)
        {
            ClienteId = clienteId;
            Marca = marca;
            Modelo = modelo;
            Cor = cor;
            AnoFabricacao = anoFabricacao;
            AnoModelo = anoModelo;
            PlacaDoVeiculo = placaDoVeiculo;
            Descricao = descricao;
        }

        public Guid ClienteId { get; private set; }
        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public string Cor { get; private set; }
        public int AnoFabricacao { get; private set; }
        public int AnoModelo { get; private set; }
        public string PlacaDoVeiculo { get; private set; }
        public string? Descricao { get; private set; }
        public Cliente Cliente { get; private set; }

        public void AlterarCliente(Cliente cliente)
        {
            ClienteId = cliente.Id;
            Cliente = cliente;
        }
    }
}
