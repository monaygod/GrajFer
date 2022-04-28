using System;
using System.Threading.Tasks;
using Infrastructure.DDD.Interface;
using Microsoft.AspNetCore.Http;
using Service.GameServer.Domain.UserAggregate;

namespace Service.GameServer.Repository
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
            //var queryResult = _context.Users
            //    .FirstOrDefault();
            
            return User.Create();
        }
        
        public User GetByUserName(string userName)
        {
            try
            {
                //var queryResult = _context.Users
                //    .First(x => x.UserName == userName);

                return User.Create();
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
                //var user = _context.Users
                //    .First(x => x.RefreshTokens.Any(y=>y.Token == refreshToken));
                //return user;
                return User.Create();
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

        public User GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public User Add(User id)
        {
            throw new System.NotImplementedException();
        }
    }
}