using System.Linq.Expressions;
using wrench.auto.repair.core.DomainObjects;

namespace wrench.auto.repair.core.Data
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : Entity, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        Task Adicionar(TEntity entidade, CancellationToken cancellationToken);
        Task Atualizar(TEntity entidade);
        Task Remover(TEntity entidade);
        Task<TEntity?> ObterPorId(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> ObterTodos(CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    }
}
