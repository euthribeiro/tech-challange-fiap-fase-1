using wrench.auto.repair.cadastro.domain.Entities;
using wrench.auto.repair.cadastro.domain.tests.Fixtures;
using wrench.auto.repair.core.DomainObjects;

namespace wrench.auto.repair.cadastro.domain.tests
{
    public class VeiculoTests(VeiculoFixture _veiculoFixture, ClienteFixture _clienteFixture) :
        IClassFixture<VeiculoFixture>, IClassFixture<ClienteFixture>
    {
        [Fact(DisplayName = "Criar Novo Veículo Dados Vazio e Invalido Deve Retornar Exception")]
        [Trait("Cadastro", "Domains")]
        public void CriarNovoVeiculo_DadosVazioEInvalidos_DeveRetornarException()
        {
            // Arrange
            var placa = _veiculoFixture.GerarPlacaVeiculo();

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            {
                new Veiculo(Guid.Empty, "Fiat", "Toro", "Preto", 2018, 2019, placa, null, null, 0);
            });

            Assert.Throws<DomainException>(() =>
            {
                new Veiculo(Guid.NewGuid(), "", "Toro", "Preto", 2018, 2019, placa, null, null, 0);
            });

            Assert.Throws<DomainException>(() =>
            {
                new Veiculo(Guid.NewGuid(), "Fiat", "", "Preto", 2018, 2019, placa, null, null, 0);
            });

            Assert.Throws<DomainException>(() =>
            {
                new Veiculo(Guid.NewGuid(), "Fiat", "Toro", "", 0, 2019, placa, null, null, 0);
            });

            Assert.Throws<DomainException>(() =>
            {
                new Veiculo(Guid.NewGuid(), "Fiat", "Toro", "Preto", 0, 2019, placa, null, null, 0);
            });

            Assert.Throws<DomainException>(() =>
            {
                new Veiculo(Guid.NewGuid(), "Fiat", "Toro", "Preto", 2018, 0, "", null, null, 0);
            });

            Assert.Throws<DomainException>(() =>
            {
                new Veiculo(Guid.NewGuid(), "Fiat", "Toro", "Preto", 2018, 0, "GGG", null, null, 0);
            });
        }


        [Fact(DisplayName = "Criar Novo Veículo Valido Com Sucesso")]
        [Trait("Cadastro", "Domains")]
        public void CriarNovoVeiculo_VeiculoValido_DeveCriarComSucesso()
        {
            // Arrange
            var placa = _veiculoFixture.GerarPlacaVeiculo();

            // Act
            var veiculo = new Veiculo(Guid.NewGuid(), "Fiat", "Toro", "Preto", 2018, 2019, placa, null, DateTime.UtcNow, 100000);

            // Assert
            Assert.NotNull(veiculo);
        }

        [Fact(DisplayName = "Veiculo Alterar Cliente Nulo Deve Retornar Exception")]
        [Trait("Cadastro", "Domains")]
        public void Veiculo_AlterarClienteNulo_DeveRetornarException()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido();

            // Act & Assert
            Assert.Throws<DomainException>(() => veiculo.AlterarCliente(null));
        }

        [Fact(DisplayName = "Veiculo Atualizar Cliente Valido Deve Atualizar")]
        [Trait("Cadastro", "Domains")]
        public void Veiculo_AlterClienteValido_DeveAtualizar()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido();
            var cliente = _clienteFixture.GerarClienteValido();

            // Act
            veiculo.AlterarCliente(cliente);

            // Assert
            Assert.Equal(cliente, veiculo.Cliente);
            Assert.Equal(cliente.Id, veiculo.ClienteId);
        }

        [Fact(DisplayName = "Veiculo Atualizar Cor Vazio Deve Retornar Exception")]
        [Trait("Cadastro", "Domains")]
        public void Veiculo_AtualizarCorVazio_DeveRetornarException()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido();

            // Act & Assert
            Assert.Throws<DomainException>(() => veiculo.AlterarCor(""));
        }

        [Fact(DisplayName = "Veiculo Atualizar Quilometragem Negativa Deve Retornar Exception")]
        [Trait("Cadastro", "Domains")]
        public void Veiculo_AtualizarQuilometragemNegativa_DeveRetornarException()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido();

            // Act & Assert
            Assert.Throws<DomainException>(() => veiculo.AtualizarQuilometragem(-1));
        }

        [Fact(DisplayName = "Veiculo Atualizar Quilometragem Menor Que Anterior Deve Retornar Exception")]
        [Trait("Cadastro", "Domains")]
        public void Veiculo_AtualizarQuilometragemMenorQueAnterior_DeveRetornarException()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido(km: 10000);

            // Act & Assert
            Assert.Throws<DomainException>(() => veiculo.AtualizarQuilometragem(9999));
        }

        [Fact(DisplayName = "Veiculo Atualizar Quilometragem Válida")]
        [Trait("Cadastro", "Domains")]
        public void Veiculo_AtualizarQuilometragemValidar_DeveAtualizar()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido(km: 10000);

            // Act
            veiculo.AtualizarQuilometragem(10001);

            // Assert
            Assert.Equal(10001, veiculo.QuilometragemAtual);
        }

        [Fact(DisplayName = "Veiculo Atualizar Descrição")]
        [Trait("Cadastro", "Domains")]
        public void Veiculo_AtualizarDescricao_DeveAtualizar()
        {
            // Arrange
            var descricao = "Descrição do Veículo";
            var veiculo = _veiculoFixture.CriarVeiculoValido(km: 10000);

            // Act
            veiculo.AtualizarDescricao(descricao);

            // Assert
            Assert.Equal(descricao, veiculo.Descricao);
        }

        [Fact(DisplayName = "Veiculo Atualizar Última Revisão Data Anterior Deve Retornar Exception")]
        [Trait("Cadastro", "Domains")]
        public void AtualizarUltimaRevisao_DataAnterior_DeveRetornarException()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido(ultimaRevisao: DateTime.UtcNow);

            // Act & Assert
            Assert.Throws<DomainException>(() => veiculo.AtualizarUltimaRevisao(DateTime.UtcNow.AddDays(-1)));
        }

        [Fact(DisplayName = "Veiculo Atualizar Última Revisão Com Sucesso")]
        [Trait("Cadastro", "Domains")]
        public void AtualizarUltimaRevisao_DataValida_DeveAtualizarComSucesso()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido(ultimaRevisao: DateTime.UtcNow.AddDays(-1));
            var dataNovaRevisao = DateTime.UtcNow;

            // Act
            veiculo.AtualizarUltimaRevisao(dataNovaRevisao);

            // Assert
            Assert.Equal(veiculo.UltimaRevisao, dataNovaRevisao);
        }

        [Fact(DisplayName = "Corrigir Marca Veiculo Marca Vazia Deve Retornar Exception")]
        [Trait("Cadastro", "Domains")]
        public void CorrigirMarcaVeiculo_MarcaVazia_DeveRetornarException()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido();

            // Act & Assert
            Assert.Throws<DomainException>(() => veiculo.CorrigirMarca(""));
        }

        [Fact(DisplayName = "Corrigir Marca Veiculo Marca Valida")]
        [Trait("Cadastro", "Domains")]
        public void CorrigirMarcaVeiculo_MarcaValida_DeveCorrigir()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido();
            var novaMarca = "Chevrolet";

            // Act
            veiculo.CorrigirMarca(novaMarca);

            // Assert
            Assert.Equal(novaMarca, veiculo.Marca);
        }

        [Fact(DisplayName = "Corrigir Modelo Veiculo Modelo Vazia Deve Retornar Exception")]
        [Trait("Cadastro", "Domains")]
        public void CorrigirModeloVeiculo_ModeloVazia_DeveRetornarException()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido();

            // Act & Assert
            Assert.Throws<DomainException>(() => veiculo.CorrigirModelo(""));
        }

        [Fact(DisplayName = "Corrigir Modelo Veiculo Modelo Valida")]
        [Trait("Cadastro", "Domains")]
        public void CorrigirModeloVeiculo_ModeloValido_DeveCorrigir()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido();
            var novoModelo = "Prisma";

            // Act
            veiculo.CorrigirModelo(novoModelo);

            // Assert
            Assert.Equal(novoModelo, veiculo.Modelo);
        }

        [Fact(DisplayName = "Corrigir Veiculo Ano Fabricação Inválido")]
        [Trait("Cadastro", "Domains")]
        public void CorrigirVeiculo_AnoFabricacaoInvalido_DeveRetornarException()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido();

            // Act & Assert
            Assert.Throws<DomainException>(() => veiculo.CorrigirAnoFabricacao(0));
        }

        [Fact(DisplayName = "Corrigir Ano Fabricação Válido")]
        [Trait("Cadastro", "Domains")]
        public void CorrigirVeiculo_AnoFabricacao_DeveCorrigir()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido();
            var novoAno = 2015;

            // Act
            veiculo.CorrigirAnoFabricacao(novoAno);

            // Assert
            Assert.Equal(novoAno, veiculo.AnoFabricacao);
        }

        [Fact(DisplayName = "Corrigir Veiculo Ano Modelo Inválido")]
        [Trait("Cadastro", "Domains")]
        public void CorrigirVeiculo_AnoModeloInvalido_DeveRetornarException()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido();

            // Act & Assert
            Assert.Throws<DomainException>(() => veiculo.CorrigirAnoModelo(0));
        }

        [Fact(DisplayName = "Corrigir Ano Modelo Válido")]
        [Trait("Cadastro", "Domains")]
        public void CorrigirVeiculo_AnoModelo_DeveCorrigir()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido();
            var novoAno = 2015;

            // Act
            veiculo.CorrigirAnoModelo(novoAno);

            // Assert
            Assert.Equal(novoAno, veiculo.AnoModelo);
        }

        [Fact(DisplayName = "Corrigir Veiculo Quilometragem Inválido")]
        [Trait("Cadastro", "Domains")]
        public void CorrigirVeiculo_QuilometragemInvalida_DeveRetornarException()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido();

            // Act & Assert
            Assert.Throws<DomainException>(() => veiculo.CorrigirQuilometragem(-1));
        }

        [Fact(DisplayName = "Corrigir Veiculo Quilometragem Maior Que Anterior Deve Retornar Exception")]
        [Trait("Cadastro", "Domains")]
        public void CorrigirVeiculo_QuilometragemMaiorQueAnterior_DeveRetornarException()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido(km: 50);
            var novaKm = 60;


            // Act & Assert
            Assert.Throws<DomainException>(() => veiculo.CorrigirQuilometragem(novaKm));
        }

        [Fact(DisplayName = "Corrigir Quilometragem Menor Válido")]
        [Trait("Cadastro", "Domains")]
        public void CorrigirVeiculo_CorrigirQuilometragemValorMenor_DeveCorrigir()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido(km: 50);
            var novaQuilometragem = 40;

            // Act
            veiculo.CorrigirQuilometragem(novaQuilometragem);

            // Assert
            Assert.Equal(novaQuilometragem, veiculo.QuilometragemAtual);
        }

        [Fact(DisplayName = "Corrigir Placa Veiculo")]
        [Trait("Cadastro", "Domains")]
        public void CorrigirVeiculo_PlacaVazia_DeveRetornarException()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido();

            // Act & Assert
            Assert.Throws<DomainException>(() => veiculo.CorrigirPlacaVeiculo(""));
        }

        [Fact(DisplayName = "Corrigir Placa Inválida")]
        [Trait("Cadastro", "Domains")]
        public void CorrigirVeiculo_PlacaInvalida_DeveRetornarException()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido();

            // Act & Assert
            Assert.Throws<DomainException>(() => veiculo.CorrigirPlacaVeiculo("GGG"));
        }

        [Fact(DisplayName = "Corrigir Placa Válida")]
        [Trait("Cadastro", "Domains")]
        public void CorrigirVeiculo_PlacaValida_DeveCorrigir()
        {
            // Arrange
            var veiculo = _veiculoFixture.CriarVeiculoValido();
            var novaPlaca = _veiculoFixture.GerarPlacaVeiculo();

            // Act
            veiculo.CorrigirPlacaVeiculo(novaPlaca);

            // Assert
            Assert.Equal(novaPlaca.Replace("-", ""), veiculo.PlacaDoVeiculo);
        }
    }
}
