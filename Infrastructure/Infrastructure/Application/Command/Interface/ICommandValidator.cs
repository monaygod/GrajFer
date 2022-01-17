using FluentValidation;

namespace Infrastructure.Application.Command.Interface
{
    public interface ICommandValidator<in T> : IValidator<T>
    {
        
    }
}