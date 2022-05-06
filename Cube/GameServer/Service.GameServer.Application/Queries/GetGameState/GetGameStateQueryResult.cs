using System.Collections.Generic;
using Infrastructure.Application.Query;
using Service.GameServer.Domain.RoomAggregate;
using Service.GameServer.Domain.ValueObject;

namespace Service.GameServer.Application.Queries.GetGameState;

public class GetGameStateQueryResult : QueryResultBase
{
    public Game GameState { get; set; }
}