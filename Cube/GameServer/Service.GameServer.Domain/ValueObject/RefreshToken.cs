using System;

namespace Service.GameServer.Domain.ValueObject
{
    public class RefreshToken : Infrastructure.DDD.ValueObject
    {
        public byte[] Token { get; set; }
        public DateTime RefreshTokenCreationDate { get; set; }
        public DateTime AccessTokenCreationDate { get; set; }
    }
}