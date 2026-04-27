using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using wrench.auto.repair.core.Data;
using wrench.auto.repair.core.DomainObjects;
using wrench.auto.repair.core.Pagination;

namespace wrench.auto.repair.autenticacao.infra.Repositories
{
    public class Repository<TEntity>(AutenticacaoContext _context) : IRepository<TEntity>
        where TEntity : Entity, IAggregateRoot
    {
        protected readonly DbSet<TEntity> DbSet = _context.Set<TEntity>();

        IUnitOfWork IRepository<TEntity>.UnitOfWork => _context;

        public virtual async Task<TEntity?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            return await DbSet.ToListAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
        }

        public virtual async Task Adicionar(TEntity entidade, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entidade, cancellationToken);
        }

        public virtual async Task Atualizar(TEntity entidade)
        {
            DbSet.Update(entidade);
        }

        public virtual async Task Remover(TEntity entidade)
        {
            DbSet.Remove(entidade);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<ResultadoPaginado<TEntity>> BuscaPaginadaAsync(RequisicaoPaginada request, CancellationToken cancellationToken)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.OrdenarPor))
            {
                query = request.Decrescente
                    ? query.OrderByDescending(e => EF.Property<object>(e, request.OrdenarPor))
                    : query.OrderBy(e => EF.Property<object>(e, request.OrdenarPor));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync(cancellationToken);

            return new ResultadoPaginado<TEntity>(items, totalCount, request.NumeroPagina, request.TamanhoPagina);
        }
    }
}
