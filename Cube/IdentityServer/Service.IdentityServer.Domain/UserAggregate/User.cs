using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Auth.JwtUtils;
using Infrastructure.Auth.Model;
using Infrastructure.DDD;
using Infrastructure.DDD.Interface;
using Service.IdentityServer.Domain.ValueObject;

namespace Service.IdentityServer.Domain.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
        public string UserName { get; private set; }
        public Password Password { get; private set; }
        public virtual List<RefreshToken> RefreshTokens { get; private set; }
        public virtual List<UserPermission> Permission { get; private set; }

        public static int expireHours = -9;
        
        private User() { }
        public bool ValidatePassword(string pass)
        {
            return Password.ValidatePassword(pass);
        }

        public string GenerateRefreshToken(string refreshToken = "")
        {
            //RefreshTokens.Clear();
            RevokeExpiredRefreshTokens();

            if (string.IsNullOrEmpty(refreshToken) == false)
            {
                RevokeRefreshToken(refreshToken);
            }

            var newRefreshToken = JwtUtils.GenerateRefreshToken();
            RefreshTokens.Add(new RefreshToken()
            {
                Token = newRefreshToken,
                RefreshTokenCreationDate = DateTime.UtcNow,
                UserId = Id
            });
            //TODO NOWY EVENT?
            return Convert.ToBase64String(newRefreshToken);
            
        }

        public string GenerateAccessToken(string refreshToken)
        {
            var accessTokenInfo = new AccessToken()
            {
                UserId = Id,
                Scopes = Permission.Select(x => x.ScopeName).ToList()
            };

            var accessToken = JwtUtils.GenerateAccessToken(accessTokenInfo);
            var refToken = RefreshTokens.Where(rt => Convert.ToBase64String(rt.Token).Equals(refreshToken)).FirstOrDefault();
            refToken.AccessTokenCreationDate = DateTime.UtcNow;
            //TODO NOWY EVENT
            return accessToken;
        }

        public void RevokeRefreshToken(string refreshToken)
        {
            var refToken = RefreshTokens.FirstOrDefault(rt => Convert.ToBase64String(rt.Token).Equals(refreshToken));
            RefreshTokens.Remove(refToken);
            //RefreshTokens.Clear();
        }

        public void RevokeExpiredRefreshTokens()
        {
            var refTokens = RefreshTokens.Where(rt => rt.RefreshTokenCreationDate <= DateTime.UtcNow.AddHours(expireHours)).ToArray();

            for(int i=0; i < refTokens.Length; i++)
            {
                RefreshTokens.Remove(refTokens[i]);
            }

        }
        
        public bool CheckAccessTokenExpireDate()
        {
            var lastAccessTokenCreationTime = RefreshTokens.Select(rt => rt.AccessTokenCreationDate).FirstOrDefault();
            return lastAccessTokenCreationTime >= DateTime.UtcNow.AddHours(expireHours);
        }

    }
}