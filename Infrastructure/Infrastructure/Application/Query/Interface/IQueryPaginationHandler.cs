using MediatR;

namespace Infrastructure.Application.Query.Interface
{
    public interface  IQueryPaginationHandler<in TQuery, TQueryResult, TPaginationResultType> :
        IRequestHandler<TQuery, TQueryResult> 
        where TQuery : QueryPaginationBase<TQueryResult, TPaginationResultType>
        where TQueryResult : QueryPaginationResultBase<TPaginationResultType>
    {
    }
}
