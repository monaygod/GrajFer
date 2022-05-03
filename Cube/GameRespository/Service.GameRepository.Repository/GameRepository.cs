using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.DDD.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.GameRepository.Domain.GameFileAggregate;

namespace Service.GameRepository.Repository
{
    public class GameRepository : IRepository<GameFile>
    {
        private readonly GameRepositoryContext _context;

        public GameRepository(GameRepositoryContext context)
        {
            _context = context;
        }
        public GameFile GetById(Guid id)
        {
            var game = _context.Games
                .FirstOrDefault(x => x.Id == id);
            
            return game;
        }

        public Task<GameFile> GetByIdAsync(Guid id)
        {
            var game = _context.Games
                .FirstOrDefaultAsync(x => x.Id == id);
            
            return game;
        }
        
        
        public GameFile GetByGameName(string gameName)
        {
            var game = _context.Games
                .FirstOrDefault(x => x.GameName == gameName);
            
            return game;
        }

        public Task<GameFile> GetByGameNameAsync(string gameName)
        {
            var game = _context.Games
                .FirstOrDefaultAsync(x => x.GameName == gameName);
            
            return game;
        }

        public GameFile Add(GameFile game)
        {
            _context.Games.Add(game);
            return game;
        }
    }
}