using FluentValidation;
using Infrastructure.Application.Command.Interface;

namespace Infrastructure.Application.Command
{
    public abstract class CommandValidator<T> : AbstractValidator<T>, ICommandValidator<T>
    {
        
    }
}