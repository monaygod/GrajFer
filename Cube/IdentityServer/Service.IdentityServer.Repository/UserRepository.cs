using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.DDD.Interface;
using Service.IdentityServer.Domain.UserAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Service.IdentityServer.Repository
{
    
    public class UserRepository : IRepository<User>
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }
        public User GetById(Guid id)
        {
            var user = _context.Users.Include(x => x.Password)
                .Include(x => x.Permission)
                .Include(x => x.RefreshTokens)
                .FirstOrDefault(x => x.Id == id);
            
            return user;
        }
        
        public User GetByUserName(string userName)
        {
            try
            {
                var user = _context.Users.Include(x => x.Password)
                    .Include(x => x.Permission)
                    .Include(x => x.RefreshTokens)
                    .First(x => x.UserName == userName);
            
                return user;
            }
            catch
            {
                throw new BadHttpRequestException("Wrong Username! Cannot find user!");
            }
            
        }

        public User GetByRefreshToken(byte[] refreshToken)
        {
            try
            {
                var user = _context.Users.Include(x => x.Password)
                    .Include(x => x.Permission)
                    .Include(x => x.RefreshTokens)
                    .First(x => x.RefreshTokens.Any(y=>y.Token==refreshToken));
            
                return user;
            }
            catch
            {
                throw new BadHttpRequestException("Wrong refresh token! Cannot find user!");
            }
        }

        public Task<User> GetByIdAsync(Guid id)
        {
            var user = _context.Users.Include(x => x.Password)
                .Include(x => x.Permission)
                .Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            return user;
        }

        public User Add(User user)
        {
            _context.Users.Add(user);
            return user;
        }
    }
}