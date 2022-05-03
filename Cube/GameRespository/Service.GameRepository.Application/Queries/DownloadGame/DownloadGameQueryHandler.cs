using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Query.Interface;
using Microsoft.AspNetCore.Mvc;
using Service.GameRepository.Domain.GameFileAggregate;

namespace Service.GameRepository.Application.Queries.DownloadGame;

public class DownloadGameQueryHandler : IQueryHandler<DownloadGameQuery, DownloadGameQueryResult>
{
    private readonly Repository.GameRepository _gameRepository;

    public DownloadGameQueryHandler(Repository.GameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }
    
    public async Task<DownloadGameQueryResult> Handle(DownloadGameQuery request, CancellationToken cancellationToken)
    {
        GameFile game = _gameRepository.GetById(request.Id);
        const string contentType = "application/png";
        return new DownloadGameQueryResult()
        {
            File = new FileContentResult(System.IO.File.ReadAllBytes(game.FilePath), contentType)
            {
                FileDownloadName = $"{game.GameName.Replace(" ", "")}.zip"
            }
        };
    }
}