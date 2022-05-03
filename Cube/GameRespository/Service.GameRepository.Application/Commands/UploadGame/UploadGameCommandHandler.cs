using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Command.Interface;
using Infrastructure.DDD.Interface;
using MediatR;
using Service.GameRepository.Domain.GameFileAggregate;
using Service.GameRepository.Repository;

namespace Service.GameRepository.Application.Commands.UploadGame
{
    public class UploadGameCommandHandler : ICommandHandlerBase<UploadGameCommand>
    {
        private readonly Repository.GameRepository _gameRepository;
        private readonly IUnitOfWork<GameRepositoryContext> _unitOfWork;

        public UploadGameCommandHandler(Repository.GameRepository gameRepository,
            IUnitOfWork<GameRepositoryContext> unitOfWork)
        {
            _gameRepository = gameRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UploadGameCommand command, CancellationToken cancellationToken)
        {
            var newGame = GameFile.Create(command.Id, command.GameName, command.File);
            
            _gameRepository.Add(newGame);
            
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }
    }
}