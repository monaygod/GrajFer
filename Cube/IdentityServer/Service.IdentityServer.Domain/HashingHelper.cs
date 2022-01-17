using System;
using System.Security.Cryptography;
using System.Text;

namespace Service.IdentityServer.Domain
{
    public class HashingHelper
    {
        public static string HashUsingPbkdf2(string password, string salt)
        {
            using var bytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256);
            var derivedRandomKey = bytes.GetBytes(32);
            var hash = Convert.ToBase64String(derivedRandomKey);
            return hash;
        }

        public static byte[] ComputeSaltedHash(string password, int salt)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] _secretBytes = encoder.GetBytes(password);

            byte[] _saltBytes = new byte[4];
            _saltBytes[0] = (byte)(salt >> 24);
            _saltBytes[1] = (byte)(salt >> 16);
            _saltBytes[2] = (byte)(salt >> 8);
            _saltBytes[3] = (byte)(salt);

            byte[] toHash = new Byte[_secretBytes.Length + _saltBytes.Length];
            Array.Copy(_secretBytes, 0, toHash, 0, _secretBytes.Length);
            Array.Copy(_saltBytes, 0, toHash, _secretBytes.Length, _saltBytes.Length);

            SHA512 sha = SHA512.Create();
            byte[] computedHash = sha.ComputeHash(toHash);

            StringBuilder sOutput = new StringBuilder(computedHash.Length);
            for (int i = 0; i < computedHash.Length; i++)
            {
                sOutput.Append(computedHash[i].ToString("X2"));
            }

            return Encoding.UTF8.GetBytes(sOutput.ToString());
        }
    }
}