using FluentValidation;
using System.Text.RegularExpressions;
using wrench.auto.repair.core.Messages;

namespace wrench.auto.repair.cadastro.application.Commands
{
    public class CadastrarVeiculoCommand(
        Guid clienteId,
        string marca,
        string modelo,
        string cor,
        int anoFabricacao,
        int anoModelo,
        string placaDoVeiculo,
        string? descricao,
        DateTime? ultimaRevisao,
        int quilometragemAtual
    ) : Command<Guid>
    {
        public Guid ClienteId { get; private set; } = clienteId;
        public string Marca { get; private set; } = marca;
        public string Modelo { get; private set; } = modelo;
        public string Cor { get; set; } = cor;
        public int AnoFabricacao { get; private set; } = anoFabricacao;
        public int AnoModelo { get; private set; } = anoModelo;
        public string PlacaDoVeiculo { get; private set; } = placaDoVeiculo;
        public string? Descricao { get; private set; } = descricao;
        public DateTime? UltimaRevisao { get; set; } = ultimaRevisao;
        public int QuilometragemAtual { get; set; } = quilometragemAtual;

        public string ObterPlacaVeiculoSemHifens()
        {
            return PlacaDoVeiculo.Replace("-", "");
        }

        public override bool EhValido()
        {
            ValidationResult = new CadastrarVeiculoCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public partial class CadastrarVeiculoCommandValidator : AbstractValidator<CadastrarVeiculoCommand>
    {
        [GeneratedRegex("^[A-Z]{3}-?[0-9][0-9A-Z][0-9]{2}$")]
        private static partial Regex PlacaVeiculoValidoRegex();

        private static int TAMANHO_MINIMO_COR => 3;
        private static int TAMANHO_MAXIMO_COR => 30;
        private static int MENOR_ANO_FABRICACAO_MODELO => 1886;
        private static int MAIOR_ANO_FABRICACAO => DateTime.Now.Year;
        private static int MAIOR_ANO_MODELO => DateTime.Now.Year + 1;
        private static int QUILOMETRAGEM_MINIMA => 0;

        public static string ClienteIdVazioError =>
            "O ID do cliente dono do veículo deve ser informado";
        public static string MarcaVaziaError =>
            "A marca do carro deve ser informado";

        public static string ModeloVazioError =>
            "O modelo do carro deve ser informado";

        public static string CorVazioError =>
            "A cor do veículo deve ser informado";

        public static string TamanhoMinimoCorError =>
            $"A cor do veículo deve ter no mínimo {TAMANHO_MINIMO_COR}";

        public static string TamanhoMaximoCorError =>
            $"A cor do veículo deve ter no mínimo {TAMANHO_MAXIMO_COR}";

        public static string MenorAnoDeFabricacaoError =>
            $"A ano de fabricação de veículo não pode ser menor que {MENOR_ANO_FABRICACAO_MODELO}";

        public static string MaiorAnoDeFabricacaoError =>
            $"A ano de fabricação de veículo não pode ser maior que {MAIOR_ANO_FABRICACAO}";

        public static string MenorAnoModeloError =>
            $"A ano do modelo de veículo não pode ser menor que {MENOR_ANO_FABRICACAO_MODELO}";

        public static string MaiorAnoModeloError =>
            $"A ano do modelo de veículo não pode ser maior que {MAIOR_ANO_MODELO}";

        public static string PlacaVeiculoVazioError =>
            "A placa do veículo deve ser informada";

        public static string PlacaVeiculoInvalidoError =>
            "A placa do veículo informado não é válido";

        public static string QuilometragemInvalidaError =>
            $"A quilometragem não pode ser menor que {QUILOMETRAGEM_MINIMA}";

        public CadastrarVeiculoCommandValidator()
        {
            RuleFor(c => c.ClienteId)
                .NotEmpty()
                .WithMessage(ClienteIdVazioError);

            RuleFor(c => c.Marca)
                .NotEmpty()
                .WithMessage(MarcaVaziaError);

            RuleFor(c => c.Modelo)
                .NotEmpty()
                .WithMessage(ModeloVazioError);

            RuleFor(c => c.Cor)
                .NotEmpty()
                .WithMessage(CorVazioError)
                .MinimumLength(3)
                .WithMessage(TamanhoMinimoCorError)
                .MaximumLength(30)
                .WithMessage(TamanhoMaximoCorError);

            RuleFor(c => c.AnoFabricacao)
                .GreaterThan(MENOR_ANO_FABRICACAO_MODELO)
                .WithMessage(MenorAnoDeFabricacaoError)
                .LessThan(MAIOR_ANO_FABRICACAO)
                .WithMessage(MaiorAnoDeFabricacaoError);

            RuleFor(c => c.AnoModelo)
                .GreaterThan(MENOR_ANO_FABRICACAO_MODELO)
                .WithMessage(MenorAnoModeloError)
                .LessThan(MAIOR_ANO_FABRICACAO)
                .WithMessage(MaiorAnoModeloError);

            RuleFor(c => c.PlacaDoVeiculo)
                .NotEmpty()
                .WithMessage(PlacaVeiculoVazioError)
                .Must(placa => PlacaVeiculoValidoRegex().IsMatch(placa))
                .WithMessage(PlacaVeiculoInvalidoError);

            RuleFor(c => c.QuilometragemAtual)
                .GreaterThan(0)
                .WithMessage(QuilometragemInvalidaError);
        }
    }
}
