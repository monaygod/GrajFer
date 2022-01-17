using Infrastructure.Application.Query.Interface;

namespace Infrastructure.Application.Query
{
    public interface IQueryPaginationBase<TQueryResult, TPaginationResultType> : IQueryBase<TQueryResult> where TQueryResult : IQueryPaginationResultBase<TPaginationResultType>
    {
    }
}