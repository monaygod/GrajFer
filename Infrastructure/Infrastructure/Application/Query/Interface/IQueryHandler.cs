using MediatR;

namespace Infrastructure.Application.Query.Interface
{
    public interface  IQueryHandler<in TQuery, TQueryResult> :
        IRequestHandler<TQuery, TQueryResult> 
        where TQuery : QueryBase<TQueryResult>
        where TQueryResult : QueryResultBase
    {
    }
}
