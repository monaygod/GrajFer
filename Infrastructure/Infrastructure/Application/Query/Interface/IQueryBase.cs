using MediatR;

namespace Infrastructure.Application.Query.Interface
{
    public interface IQueryBase<TQueryResult> : IRequest<TQueryResult> where TQueryResult : IQueryResultBase
    {
        
    }
}