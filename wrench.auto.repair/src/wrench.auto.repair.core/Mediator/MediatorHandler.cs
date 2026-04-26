using MediatR;
using wrench.auto.repair.core.Errors;
using wrench.auto.repair.core.Messages;

namespace wrench.auto.repair.core.Mediator
{
    public class MediatorHandler(IMediator _mediator) : IMediatorHandler
    {
        public async Task<Result> EnviarComando<T>(T comando) where T : Command
        {
            return await _mediator.Send(comando);
        }

        public async Task<Result<TOut>> EnviarComando<T, TOut>(T comando)
            where T : Command<TOut>
        {
            return await _mediator.Send(comando);
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }
    }
}
