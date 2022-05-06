using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.DDD.Interface;
using Microsoft.EntityFrameworkCore;
using Service.GameServer.Domain.RoomAggregate;

namespace Service.GameServer.Repository
{
    
    public class RoomRepository : IRepository<Room>
    {
        private readonly GameDbContext _context;

        public RoomRepository(GameDbContext context)
        {
            _context = context;
        }
        public Room GetById(Guid id)
        {
            var room = _context.Rooms
                .Include(x => x.Password)
                .Include(x => x.Host)
                .Include(x => x.Players)
                .FirstOrDefault(x => x.Id == id);
            
            return room;
        }

        public Task<Room> GetByIdAsync(Guid id)
        {
            var room = _context.Rooms
                .Include(x => x.Password)
                .Include(x => x.Host)
                .Include(x => x.Players)
                .Include(x=>x.Game)
                .ThenInclude(x=>x.StaticFields)
                .ThenInclude(x=>x.ActiveElements)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            return room;
        }

        public Room Add(Room room)
        {
            _context.Rooms.Add(room);
            return room;
        }
    }
}