using Infrastructure.Application.Query.Interface;

namespace Infrastructure.Application.Query
{
    public abstract class QueryPaginationBase <TQueryResult, TPaginationResultType> : QueryBase<TQueryResult>, IQueryPaginationBase<TQueryResult,TPaginationResultType> where TQueryResult : QueryPaginationResultBase<TPaginationResultType>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}