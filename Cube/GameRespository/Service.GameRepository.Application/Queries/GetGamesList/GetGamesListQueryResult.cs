using System;
using Infrastructure.Application.Query;

namespace Service.GameRepository.Application.Queries.GetGamesList;

public class GetGamesListQueryResult : QueryPaginationResultBase<GameInfo>
{
}

public class GameInfo
{
    public string Name { get; set; }
    public Guid Id { get; set; }
}