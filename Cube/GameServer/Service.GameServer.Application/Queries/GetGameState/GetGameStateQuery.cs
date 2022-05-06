using System;
using Infrastructure.Application.Query;
using Service.GameServer.Domain.RoomAggregate;

namespace Service.GameServer.Application.Queries.GetGameState;

public class GetGameStateQuery: QueryBase<GetGameStateQueryResult>
{
    public Guid RoomId { get; set; }
}