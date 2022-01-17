using System;
using Infrastructure.DDD;

namespace Service.IdentityServer.Domain.ValueObject
{
    public class RefreshToken : Entity
    {
        public Guid UserId { get; set; }
        public byte[] Token { get; set; }
        public DateTime RefreshTokenCreationDate { get; set; }
        public DateTime AccessTokenCreationDate { get; set; }
    }
}