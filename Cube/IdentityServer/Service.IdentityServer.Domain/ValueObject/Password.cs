using System;
using Infrastructure.DDD;

namespace Service.IdentityServer.Domain.ValueObject
{
    public class Password : Entity
    {
        public byte[] PassHash { get; set; }
        public int SaltValue { get; set; }

        public bool ValidatePassword(string pass)
        {
            return Convert.ToBase64String(HashingHelper.ComputeSaltedHash(pass, SaltValue)) == Convert.ToBase64String(PassHash);
        }
    }
}