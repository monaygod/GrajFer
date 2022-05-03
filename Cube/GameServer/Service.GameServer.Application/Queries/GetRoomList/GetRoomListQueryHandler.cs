using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Query.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.GameServer.Repository;

namespace Service.GameServer.Application.Queries.GetRoomList;

public class GetRoomListQueryHandler : IQueryHandler<GetRoomListQuery, GetRoomListQueryResult>
{
    private readonly GameDbContext _context;

    public GetRoomListQueryHandler(GameDbContext context)
    {
        _context = context;
    }
    
    public async Task<GetRoomListQueryResult> Handle(GetRoomListQuery request, CancellationToken cancellationToken)
    {
        var count = _context.Rooms.Select(x=>x.Id)
            .CountAsync(cancellationToken: cancellationToken);
        var rooms = _context.Rooms
            .Skip((request.Page-1)*request.PageSize)
            .Take(request.PageSize).
            ToListAsync(cancellationToken: cancellationToken);

        return new GetRoomListQueryResult()
        {
            PaginatedResult = await rooms,
            TotalCount = await count
        };
    }
}