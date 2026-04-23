namespace wrench.auto.repair.core.Errors
{
    public class Result<T>
    {
        public Guid Id { get; private set; }
        public DateTime Timestamp { get; private set; }
        public bool Sucesso { get; private set; }
        public T? Valor { get; private set; }
        public IEnumerable<string> Erros { get; private set; } = [];
        public TipoErroEnum? TipoErro { get; private set; }
        public ResultadoStatusEnum? ResultadoStatus { get; private set; }

        private Result(TipoErroEnum? tipoErro, IEnumerable<string> erros)
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;
            Sucesso = false;
            Valor = default;
            Erros = erros;
            TipoErro = tipoErro;
            ResultadoStatus = default;
        }

        private Result(ResultadoStatusEnum resultadoStatus, T? valor)
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;
            Sucesso = true;
            Valor = valor;
            Erros = [];
            TipoErro = default;
            ResultadoStatus = resultadoStatus;
        }

        public static Result<T> WithFailure(TipoErroEnum tipoErro, string erro, params string[] erros) =>
            new(tipoErro, [erro, .. erros]);

        public static Result<T> WithFailure(TipoErroEnum tipoErro, IEnumerable<string> errors) =>
            new(tipoErro, errors);

        public static Result<T> Ok(T valor) =>
            new(ResultadoStatusEnum.OK, valor);

        public static Result<T> Created(T valor) =>
            new(ResultadoStatusEnum.CRIADO, valor);

        public static Result<T> NoContent() =>
            new(ResultadoStatusEnum.CRIADO, default);
    }
}
