using System;

namespace Service.IdentityServer.Domain.ValueObject
{
    public class Password : Infrastructure.DDD.ValueObject
    {
        public byte[] PassHash { get; set; }
        public int SaltValue { get; set; }

        private Password() { }
        public Password(string password)
        {
            SaltValue = new Random().Next();
            PassHash = HashingHelper.ComputeSaltedHash(password, SaltValue);
        }
        public bool ValidatePassword(string pass)
        {
            return Convert.ToBase64String(HashingHelper.ComputeSaltedHash(pass, SaltValue)) == Convert.ToBase64String(PassHash);
        }
    }
}