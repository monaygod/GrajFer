using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Command.Interface;
using Infrastructure.Database;
using MediatR;

namespace Infrastructure.Application.TransactionalCommand
{
    public class TransactionalCommandHandler : ICommandHandlerBase<TransactionalCommand>
    {
        private readonly IMediator _mediator;
        private readonly TransactionManager _transactionManager;

        public TransactionalCommandHandler(IMediator mediator, TransactionManager transactionManager)
        {
            _mediator = mediator;
            _transactionManager = transactionManager;
        }

        public async Task<Unit> Handle(TransactionalCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                _transactionManager.BeginTransaction();
                foreach (var command in request.commands.Where(x=> x != null))
                {
                    await _mediator.Send(command, cancellationToken);
                }

                _transactionManager.CommitTransaction();
            }
            catch (Exception e)
            {
                _transactionManager.RollbackTransaction();
                throw e;
            }
            finally
            {
                _transactionManager.Dispose();
            }

            return Unit.Value;
        }
    }
}