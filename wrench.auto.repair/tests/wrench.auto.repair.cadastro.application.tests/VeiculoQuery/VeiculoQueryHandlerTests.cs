using AutoMapper;
using Moq;
using Moq.AutoMock;
using wrench.auto.repair.cadastro.application.Queries;
using wrench.auto.repair.cadastro.application.Queries.ViewModels;
using wrench.auto.repair.cadastro.application.tests.Fixture;
using wrench.auto.repair.cadastro.domain.Data;
using wrench.auto.repair.cadastro.domain.Entities;
using wrench.auto.repair.core.Pagination;

namespace wrench.auto.repair.cadastro.application.tests.VeiculoQuery
{
    [Collection(nameof(VeiculoCollection))]
    public class VeiculoQueryHandlerTests(VeiculoFixture _fixture)
    {
        [Fact(DisplayName = "Obter Todos Veiculos Deve Retornar Veiculos")]
        [Trait("Cadastro", "Application")]
        public async Task Veiculo_ObterTodosVeiculos_DeveRetornarVeiculos()
        {
            // Assert
            var mapper = _fixture.ConfigurarMapeamentoEGerarMapper();
            var resultadoPaginadoVeiculo =
                new ResultadoPaginado<Veiculo>([], 0, 1, 10);
            var resultadoPaginadoVeiculoViewModel = mapper
                .Map<ResultadoPaginado<VeiculoViewModel>>(resultadoPaginadoVeiculo);

            var obterTodosVeiculosQuery =
                new ObterTodosVeiculosQuery(new RequisicaoPaginada());
            var automocker = new AutoMocker();

            automocker.GetMock<IVeiculoRepository>()
                .Setup(v => v.BuscaPaginadaAsync(It.IsAny<RequisicaoPaginada>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<ResultadoPaginado<Veiculo>>(resultadoPaginadoVeiculo));

            automocker.GetMock<IMapper>()
                .Setup(m => m.Map<ResultadoPaginado<VeiculoViewModel>>(resultadoPaginadoVeiculo))
                .Returns(resultadoPaginadoVeiculoViewModel);

            var veiculoQueryHandler =
                automocker.CreateInstance<VeiculoQueryHandler>();

            // Act
            var resultado = await veiculoQueryHandler
                .Handle(obterTodosVeiculosQuery, CancellationToken.None);

            // Assert
            Assert.True(resultado.Sucesso);

            automocker.GetMock<IVeiculoRepository>()
            .Verify(v => v.BuscaPaginadaAsync(It.IsAny<RequisicaoPaginada>(), It.IsAny<CancellationToken>()), Times.Once);

            automocker.GetMock<IMapper>()
                .Verify(m => m.Map<ResultadoPaginado<VeiculoViewModel>>(It.IsAny<ResultadoPaginado<Veiculo>>()), Times.Once);
        }

        [Fact(DisplayName = "Obter Veiculo Por Id Deve Retornar Veiculo")]
        [Trait("Cadastro", "Application")]
        public async Task Veiculo_ObterVeiculoPorId_DeveRetornarVeiculo()
        {
            // Assert
            var mapper = _fixture.ConfigurarMapeamentoEGerarMapper();

            var veiculoId = Guid.NewGuid();
            var veiculo = _fixture.CriarVeiculoValido();
            var veiculoViewModel = mapper.Map<VeiculoViewModel>(veiculo);
            var obterVeiculoPorIdQuery =
                new ObterVeiculoPorIdQuery(veiculo.Id);
            var automocker = new AutoMocker();

            automocker.GetMock<IVeiculoRepository>()
                .Setup(v => v.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Veiculo?>(veiculo));

            automocker.GetMock<IMapper>()
                .Setup(m => m.Map<VeiculoViewModel>(veiculo))
                .Returns(veiculoViewModel);

            var veiculoQueryHandler =
                automocker.CreateInstance<VeiculoQueryHandler>();

            // Act
            var resultado = await veiculoQueryHandler
                .Handle(obterVeiculoPorIdQuery, CancellationToken.None);

            // Assert
            Assert.True(resultado.Sucesso);

            automocker.GetMock<IVeiculoRepository>()
            .Verify(v => v.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            automocker.GetMock<IMapper>()
                .Verify(m => m.Map<VeiculoViewModel>(It.IsAny<Veiculo>()), Times.Once);
        }

        [Fact(DisplayName = "Obter Veiculo Por Placa Deve Retornar Veiculo")]
        [Trait("Cadastro", "Application")]
        public async Task Veiculo_ObterVeiculoPorPlaca_DeveRetornarVeiculo()
        {
            // Assert
            var mapper = _fixture.ConfigurarMapeamentoEGerarMapper();

            var veiculoId = Guid.NewGuid();
            var veiculo = _fixture.CriarVeiculoValido();
            var veiculoViewModel = mapper.Map<VeiculoViewModel>(veiculo);
            var obterVeiculoPorPlacaQuery =
                new ObterVeiculoPorPlacaQuery(veiculo.PlacaDoVeiculo);
            var automocker = new AutoMocker();

            automocker.GetMock<IVeiculoRepository>()
                .Setup(v => v.ObterVeiculoPelaPlacaAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Veiculo?>(veiculo));

            automocker.GetMock<IMapper>()
                .Setup(m => m.Map<VeiculoViewModel>(veiculo))
                .Returns(veiculoViewModel);

            var veiculoQueryHandler =
                automocker.CreateInstance<VeiculoQueryHandler>();

            // Act
            var resultado = await veiculoQueryHandler
                .Handle(obterVeiculoPorPlacaQuery, CancellationToken.None);

            // Assert
            Assert.True(resultado.Sucesso);

            automocker.GetMock<IVeiculoRepository>()
            .Verify(v => v.ObterVeiculoPelaPlacaAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);

            automocker.GetMock<IMapper>()
                .Verify(m => m.Map<VeiculoViewModel>(It.IsAny<Veiculo>()), Times.Once);
        }
    }
}
