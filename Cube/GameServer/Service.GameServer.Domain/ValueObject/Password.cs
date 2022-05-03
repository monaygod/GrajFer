using System;

namespace Service.GameServer.Domain.ValueObject
{
    public class Password : Infrastructure.DDD.ValueObject
    {
        public byte[] PassHash { get; set; }
        public int SaltValue { get; set; }

        private Password() { }
        public Password(string password)
        {
            if (password is null || password.Length == 0)
            {
                PassHash = Array.Empty<byte>();
                SaltValue = 0;
            }
            SaltValue = new Random().Next();
            PassHash = HashingHelper.ComputeSaltedHash(password, SaltValue);
        }
        public bool ValidatePassword(string pass)
        {
            if (SaltValue == 0 && PassHash.Length == 0)
                return true;
            return Convert.ToBase64String(HashingHelper.ComputeSaltedHash(pass, SaltValue)) == Convert.ToBase64String(PassHash);
        }
    }
}