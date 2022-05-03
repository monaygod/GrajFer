using System;
using Infrastructure.Application.Query;
using Service.GameServer.Domain.UserAggregate;

namespace Service.GameServer.Application.Queries.GetRoomList;

public class GetRoomListQuery: QueryPaginationBase<GetRoomListQueryResult, Room>
{
}