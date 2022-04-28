using System;

namespace Service.GameServer.Domain.ValueObject
{
    public class Password : Infrastructure.DDD.ValueObject
    {
        public byte[] PassHash { get; set; }
        public int SaltValue { get; set; }

        public bool ValidatePassword(string pass)
        {
            return Convert.ToBase64String(HashingHelper.ComputeSaltedHash(pass, SaltValue)) == Convert.ToBase64String(PassHash);
        }
    }
}