using MediatR;

namespace Infrastructure.Application.Command.Interface
{
    public interface ICommandHandlerBase<in TCommand> :
        IRequestHandler<TCommand,Unit> where TCommand : ICommandBase
    {
    }
    
    public interface ICommandHandlerBase<in TCommand, TCommandResult> :
        IRequestHandler<TCommand,TCommandResult> where TCommand : ICommandBase<TCommandResult> where TCommandResult : ICommandResultBase
    {
    }
}