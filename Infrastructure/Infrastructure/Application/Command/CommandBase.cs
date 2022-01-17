using Infrastructure.Application.Command.Interface;

namespace Infrastructure.Application.Command
{
    public abstract class CommandBase : ICommandBase
    {

    }

    public abstract class CommandBase<TCommandResult> : ICommandBase<TCommandResult> where TCommandResult : ICommandResultBase
    {

    }
}