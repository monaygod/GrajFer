using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Infrastructure.Auth.JwtUtils
{
    public static class JwtUtils
    {
        private static readonly string secret = "fsefrgcrgerwgcrgwrvwgergvewgvre";


        public static string GenerateAccessToken(ClaimsIdentity claims)
        {
            return JwtTools.GenerateAccessToken(claims, DateTime.UtcNow.AddMinutes(1), secret);
        }

        public static IEnumerable<Claim> ValidateJwtToken(string token)
        {
            return JwtTools.DecodeJwtToken(token, secret)
                ?.Claims;
        }

        public static byte[] GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            using SHA512 shaM = new SHA512Managed();
            var refreshToken = shaM.ComputeHash(randomNumber);
            return refreshToken;
        }

        public static string GetClaimValue(this IEnumerable<Claim> claims, string claimName)
        {
            return claims.FirstOrDefault(x => x.Type.Equals(claimName))
                ?.Value;
        }

        public static IEnumerable<string> GetClaimArrayValue(this IEnumerable<Claim> claims, string claimName)
        {
            return claims.Where(x => x.Type.Equals(claimName)).Select(x => x.Value);
        }
    }
}