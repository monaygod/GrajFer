using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Query.Interface;
using MediatR;

namespace Infrastructure.Application.Query.PipelineDecorator
{
    public class QueryHandlerValidationDecorator<TQuery, TQueryResult> : IPipelineBehavior<TQuery, TQueryResult>
        where TQuery : QueryBase<TQueryResult>
        where TQueryResult : QueryResultBase
    {
        private readonly IQueryValidator<TQuery> _queryValidator;
        
        public QueryHandlerValidationDecorator(IServiceProvider serviceProvider)
        {
            var queryValidator = (IQueryValidator<TQuery>)serviceProvider.GetService(typeof(IQueryValidator<TQuery>));
            _queryValidator = queryValidator;
        }

        public async Task<TQueryResult> Handle(TQuery request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TQueryResult> next)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{typeof(TQuery)} is null");
            }
            
            if (_queryValidator != null)
            {
                var result = await _queryValidator.ValidateAsync(request, cancellationToken);
                if (!result.IsValid)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(x => x.ErrorMessage)));
                }
            }

            return await next();
        }
    }
}