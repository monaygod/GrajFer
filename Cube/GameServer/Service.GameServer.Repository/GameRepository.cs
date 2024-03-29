﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.DDD.Interface;
using Microsoft.EntityFrameworkCore;
using Service.GameServer.Domain.RoomAggregate;

namespace Service.GameServer.Repository
{
    
    public class GameRepository : IRepository<Room>
    {
        private readonly GameDbContext _context;

        public GameRepository(GameDbContext context)
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