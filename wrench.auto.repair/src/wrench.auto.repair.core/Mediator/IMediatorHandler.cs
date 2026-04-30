using wrench.auto.repair.core.Errors;
using wrench.auto.repair.core.Messages;
using wrench.auto.repair.core.Messages.CommonMessages.IntegratedQueries;

namespace wrench.auto.repair.core.Mediator
{
    public interface IMediatorHandler
    {
        Task<bool> ConsultaIntegrada<T>(T consulta) where T : IntegratedQuery;
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<Result> EnviarComando<T>(T comando) where T : Command;
        Task<Result<TOut>> EnviarComando<T, TOut>(T comando)
            where T : Command<TOut>;
    }
}
