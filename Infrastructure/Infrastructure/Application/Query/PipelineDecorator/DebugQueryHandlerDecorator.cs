using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Infrastructure.Application.Query.PipelineDecorator
{
    public class DebugQueryHandlerDecorator <TQuery, TQueryResult> : IPipelineBehavior<TQuery, TQueryResult> 
        where TQuery : QueryBase<TQueryResult>
        where TQueryResult : QueryResultBase
    {

        public async Task<TQueryResult> Handle(TQuery request, CancellationToken cancellationToken, RequestHandlerDelegate<TQueryResult> next)
        {
            //Przykład pipeline behavior
            return await next();
        }
    }
}