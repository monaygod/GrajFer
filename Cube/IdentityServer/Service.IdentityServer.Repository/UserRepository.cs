using System.Linq;
using System.Threading.Tasks;
using Infrastructure.DDD.Interface;
using Infrastructure.Mapping;
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
        public User GetById(int id)
        {
            //TODO lazyloading
            //var anyref = _context.UserRefreshTokens.ToList();
            var queryResult = _context.Users
                .FirstOrDefault();
            
            return queryResult;
        }
        
        public User GetByUserName(string userName)
        {
            try
            {
                var queryResult = _context.Users
                    .First(x => x.UserName == userName);

                return queryResult;
            }
            catch
            {
                throw new BadHttpRequestException("Wrong email! Cannot find user!");
            }
            
        }

        public User GetByRefreshToken(byte[] refreshToken)
        {
            try
            {
                var user = _context.Users
                    .First(x => x.RefreshTokens.Any(y=>y.Token == refreshToken));
                return user;
            }
            catch
            {
                throw new BadHttpRequestException("Wrong refresh token! Cannot find user!");
            }
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public User Add(User id)
        {
            throw new System.NotImplementedException();
        }
    }
}