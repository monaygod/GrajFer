using System.Collections.Generic;
using Infrastructure.Application.Query.Interface;

namespace Infrastructure.Application.Query
{
    public abstract class QueryPaginationResultBase<TPaginationResultType>: QueryResultBase, IQueryPaginationResultBase<TPaginationResultType>
    {
        public int TotalCount { get; set; }
        public IEnumerable<TPaginationResultType> PaginatedResult { get; set; }
    }
}