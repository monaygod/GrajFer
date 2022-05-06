using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.DDD.Interface;
using Microsoft.EntityFrameworkCore;
using Service.GameServer.Domain.PlayerAggregate;

namespace Service.GameServer.Repository
{
    
    public class PlayerRepository : IRepository<Player>
    {
        private readonly GameDbContext _context;

        public PlayerRepository(GameDbContext context)
        {
            _context = context;
        }
        public Player GetById(Guid id)
        {
            var room = _context.Players
                .Include(x => x.Rooms)
                .FirstOrDefault(x => x.PlayerId == id);
            
            return room;
        }

        public Task<Player> GetByIdAsync(Guid id)
        {
            var room = _context.Players
                .Include(x => x.Rooms)
                .FirstOrDefaultAsync(x => x.PlayerId == id);
            
            return room;
        }

        public Player Add(Player room)
        {
            _context.Players.Add(room);
            return room;
        }
    }
}