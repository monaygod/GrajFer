using MediatR;

namespace Infrastructure.Application.Command.Interface
{
    public interface ICommandBase : IRequest<Unit>
    {
        
    }
    public interface ICommandBase<TCommandResult> : IRequest<TCommandResult> where TCommandResult : ICommandResultBase
    {
        
    }
}