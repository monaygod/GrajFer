namespace Infrastructure.Application.Query.Interface
{
    public interface IQueryPaginationBase<TQueryResult, TPaginationResultType> : IQueryBase<TQueryResult> where TQueryResult : IQueryPaginationResultBase<TPaginationResultType>
    {
    }
}