using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Query.Interface;
using Service.GameRepository.Repository;

namespace Service.GameRepository.Application.Queries.GetGamesList;

public class GetGamesListQueryHandler : IQueryPaginationHandler<GetGamesListQuery, GetGamesListQueryResult, GameInfo>
{
    private readonly GameRepositoryContext _gameRepositoryContext;

    public GetGamesListQueryHandler(GameRepositoryContext gameRepository)
    {
        _gameRepositoryContext = gameRepository;
    }
    
    public async Task<GetGamesListQueryResult> Handle(GetGamesListQuery request, CancellationToken cancellationToken)
    {
        var count = _gameRepositoryContext.Games.Select(x=>x.Id).Count();
        var games = _gameRepositoryContext.Games
            .Select(x => new GameInfo() { Id = x.Id, Name = x.GameName })
            .Skip((request.Page-1)*request.PageSize)
            .Take(request.PageSize);

        return new GetGamesListQueryResult()
        {
            PaginatedResult = games,
            TotalCount = count
        };
    }
}