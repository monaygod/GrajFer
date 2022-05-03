using Infrastructure.Application.Query;
using Microsoft.AspNetCore.Mvc;
using Service.GameServer.Domain.UserAggregate;

namespace Service.GameServer.Application.Queries.GetRoomList;

public class GetRoomListQueryResult : QueryPaginationResultBase<Room>
{
}