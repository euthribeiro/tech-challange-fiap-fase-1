using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using wrench.auto.repair.core.Pagination;
using wrench.auto.repair.ordem.servico.domain.Data;
using wrench.auto.repair.ordem.servico.domain.Entities;
using wrench.auto.repair.ordem.servico.infra.Context;

namespace wrench.auto.repair.ordem.servico.infra.Repositories
{
    public class OrdemServicoRepository(OrdemServicoDbContext _context) : Repository<OrdemServico>(_context), IOrdemServicoRepository
    {
        public async Task<ResultadoPaginado<OrdemServico>> BuscaPaginadaAsync(Guid clienteId, Guid? veiculoId, RequisicaoPaginada request, Dictionary<string, Expression<Func<OrdemServico, object?>>> sortMap, CancellationToken cancellationToken)
        {
            var query = _context.Set<OrdemServico>().AsQueryable()
                .Where(c => c.ClienteId == clienteId);

            if (veiculoId.HasValue && veiculoId != Guid.Empty)
                query = query.Where(o => o.VeiculoId == veiculoId);

            ValidarOrdenacao(request, sortMap);

            if (!string.IsNullOrWhiteSpace(request.OrdenarPor)
                && sortMap.TryGetValue(request.OrdenarPor, out var orderExpression))
            {
                query = request.Decrescente
                    ? query.OrderByDescending(orderExpression)
                    : query.OrderBy(orderExpression);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync(cancellationToken);

            return new ResultadoPaginado<OrdemServico>(
                items,
                totalCount,
                request.NumeroPagina,
                request.TamanhoPagina,
                sortMap.Keys);
        }
    }
}

