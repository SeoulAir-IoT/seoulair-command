using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SeoulAir.Command.Domain.Dtos;
using SeoulAir.Command.Repositories.Entities;

namespace SeoulAir.Command.Repositories.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResultDto<TDto>> GetPaginatedAsync<TDto, TEntity>(
            this IQueryable<TEntity> queryable, Paginator paginator, IMapper mapper)
            where TDto : BaseDtoWithId 
            where TEntity : BaseEntityWithId
        {
            IOrderedQueryable<TEntity> orderedQueryable = queryable.OrderBy(paginator.OrderBy, paginator.IsDescending);
            
            int count = orderedQueryable.Count();
            List<TEntity> items = await orderedQueryable.Skip((paginator.PageIndex - 1) * paginator.PageSize)
                .Take(paginator.PageSize).ToListAsync();

            return new PaginatedResultDto<TDto>
            {
                PageIndex = paginator.PageIndex,
                PageSize = paginator.PageSize,
                Result = mapper.Map<List<TDto>>(items),
                TotalRecords = count
            };
        }

        private static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> queryable,
            string orderBy, bool isDescending)
            where TEntity : BaseEntityWithId
        {
            var filterParam = Expression.Parameter(typeof(TEntity), nameof(TEntity));
            var filterProperty =
                orderBy.Split('.').Aggregate<string, Expression>(filterParam, Expression.PropertyOrField);

            return isDescending
                ? (IOrderedQueryable<TEntity>) queryable.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        "OrderByDescending",
                        new[] {typeof(TEntity), filterProperty.Type},
                        queryable.Expression,
                        Expression.Lambda(filterProperty, filterParam)))
                : (IOrderedQueryable<TEntity>) queryable.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        "OrderBy",
                        new[] {typeof(TEntity), filterProperty.Type},
                        queryable.Expression,
                        Expression.Lambda(filterProperty, filterParam)));
        }
    }
}
