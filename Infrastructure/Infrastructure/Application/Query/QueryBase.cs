using Infrastructure.Application.Query.Interface;

namespace Infrastructure.Application.Query
{
    public class QueryBase<TQueryResult> : IQueryBase<TQueryResult> where TQueryResult : QueryResultBase
    {
        
    }
}