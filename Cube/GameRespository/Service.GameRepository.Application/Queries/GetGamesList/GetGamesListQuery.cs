using Infrastructure.Application.Query;

namespace Service.GameRepository.Application.Queries.GetGamesList;

public class GetGamesListQuery: QueryPaginationBase<GetGamesListQueryResult, GameInfo>
{
}