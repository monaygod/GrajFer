using FluentValidation;
using Infrastructure.Application.Query.Interface;

namespace Infrastructure.Application.Query
{
    public abstract class QueryValidator<T> : AbstractValidator<T>, IQueryValidator<T>
    {
        
    }
}