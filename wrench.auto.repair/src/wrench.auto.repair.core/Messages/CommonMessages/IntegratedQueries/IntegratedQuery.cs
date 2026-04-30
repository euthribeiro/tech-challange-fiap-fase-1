using MediatR;

namespace wrench.auto.repair.core.Messages.CommonMessages.IntegratedQueries
{
    public abstract class IntegratedQuery : Message, IRequest<bool>
    {

    }
}
