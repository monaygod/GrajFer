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
        private readonly IEntityMapper<User, Model.User> _mapper;

        public UserRepository(UserContext context, IEntityMapper<User, Model.User> mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public User GetById(int id)
        {
            //TODO lazyloading
            //var anyref = _context.UserRefreshTokens.ToList();
            var queryResult = _context.Users
                .Include(x => x.UserAuthorizations)
                .Include(x=>x.UserPermissions)
                .ThenInclude(x=>x.Permission)
                .FirstOrDefault(x => x.UserId == id && x.UserStatusId == 1);
            
            return _mapper.MapToDDD(queryResult);
        }
        
        public User GetByEmail(string email)
        {
            try
            {
                var queryResult = _context.Users
                    .Include(x => x.UserAuthorizations)
                    .Include(x => x.UserPermissions)
                    .ThenInclude(x => x.Permission)
                    .First(x => x.Email == email && x.UserStatusId == 1);

                return _mapper.MapToDDD(queryResult);
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
                var userByRefToken = _context.UserAuthorizations.FirstOrDefault(x => x.RefreshToken == refreshToken);
                var user = _context.Users
                    .Include(x => x.UserAuthorizations)
                    .Include(x => x.UserPermissions)
                    .ThenInclude(x => x.Permission)
                    .First(x => x.UserId == userByRefToken.UserId);
                return _mapper.MapToDDD(user);
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