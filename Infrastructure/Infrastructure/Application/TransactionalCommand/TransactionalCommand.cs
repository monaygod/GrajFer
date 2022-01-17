using System.Collections.Generic;
using System.Linq;
using Infrastructure.Application.Command.Interface;

namespace Infrastructure.Application.TransactionalCommand
{
    public class TransactionalCommand : ICommandBase
    {
        public List<ICommandBase> commands { get; private set; } 

        public TransactionalCommand() //TODO fluentbuilder?
        {
            commands = new List<ICommandBase>();
        }

        public TransactionalCommand(IEnumerable<ICommandBase> enumerableCommand)
        {
            commands = enumerableCommand.ToList();
        }

        public void AddCommand(ICommandBase commandBase)
        {
            commands.Add(commandBase);
        }
        public void AddCommand(IEnumerable<ICommandBase> commandBase)
        {
            commands.AddRange(commandBase);
        }
    }
}