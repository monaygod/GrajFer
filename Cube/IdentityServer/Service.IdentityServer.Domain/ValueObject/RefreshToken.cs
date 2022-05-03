using System;

namespace Service.IdentityServer.Domain.ValueObject
{
    public class RefreshToken : Infrastructure.DDD.ValueObject
    {
        public byte[] Token { get; set; }
        public DateTime RefreshTokenCreationDate { get; set; }
        public DateTime AccessTokenCreationDate { get; set; }
        
        private RefreshToken(){ }

        public RefreshToken(byte[] token)
        {
            Token = token;
            RefreshTokenCreationDate = DateTime.UtcNow;
        }
    }
}