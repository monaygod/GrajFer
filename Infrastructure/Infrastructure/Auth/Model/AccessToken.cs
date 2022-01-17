using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Infrastructure.Auth.JwtUtils;
using Newtonsoft.Json;

namespace Infrastructure.Auth.Model
{
    public class AccessToken
    {
        public Guid UserId { get; set; }
        public DateTime ExpTime { get; set; }
        public ICollection<string> Scopes { get; set; }

        public AccessToken()
        {
        }

        public AccessToken(IEnumerable<Claim> claims)
        {
            var enumerable = claims as Claim[] ?? claims.ToArray();
            UserId = Guid.Parse(enumerable.GetClaimValue("UserId"));
            int seconds = Convert.ToInt32(enumerable.GetClaimValue("exp"));
            ExpTime = DateTimeOffset.FromUnixTimeSeconds(seconds)
                .UtcDateTime;
            Scopes = enumerable.GetClaimArrayValue("Scopes")
                .ToArray();
        }

        public static implicit operator ClaimsIdentity(AccessToken a)
        {
            var result = new ClaimsIdentity();
            result.AddClaim(new Claim("UserId", a.UserId.ToString()));
            result.AddClaim(new Claim("Scopes", JsonConvert.SerializeObject(a.Scopes), JsonClaimValueTypes.JsonArray));
            return result;
        }
        //TODO zmienić nazwy claimów na nie opujące wartości
    }
}