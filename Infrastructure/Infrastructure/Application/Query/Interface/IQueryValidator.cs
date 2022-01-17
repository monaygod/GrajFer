using FluentValidation;

namespace Infrastructure.Application.Query.Interface
{
    public interface IQueryValidator<in T> : IValidator<T>
    {
        
    }
}