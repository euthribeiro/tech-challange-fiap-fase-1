using wrench.auto.repair.core.Errors;
using wrench.auto.repair.core.Messages;

namespace wrench.auto.repair.core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<Result> EnviarComando<T>(T comando) where T : Command;
        Task<Result<TOut>> EnviarComando<T, TOut>(T comando)
            where T : Command<TOut>
            where TOut : class;
    }
}
