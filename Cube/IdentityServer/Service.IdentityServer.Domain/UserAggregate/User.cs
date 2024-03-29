﻿using System;
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
        private static int expireHours = -9;
        
        private string _userName;
        private Password _password;
        private List<RefreshToken> _refreshTokens = new();
        private List<UserPermission> _userPermissions = new();
        private User() { }
        public string UserName => _userName;
        public Password Password => _password;
        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens;
        public IReadOnlyCollection<UserPermission> Permission => _userPermissions;
        
        public User(Guid id, string userName, string password, List<string> permissions = null)
        {
            Id = id;
            _password = new Password(password);
            _userName = userName;
            _userPermissions = permissions?.Select(x => new UserPermission(x))
                .ToList();
        }
        
        public bool ValidatePassword(string pass)
        {
            return Password.ValidatePassword(pass);
        }

        public string GenerateRefreshToken(string refreshToken = "")
        {
            RevokeExpiredRefreshTokens();

            if (string.IsNullOrEmpty(refreshToken) == false)
            {
                RevokeRefreshToken(refreshToken);
            }

            var newRefreshToken = JwtUtils.GenerateRefreshToken();
            _refreshTokens.Add(new RefreshToken(newRefreshToken));
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
            var refToken = RefreshTokens.FirstOrDefault(rt => Convert.ToBase64String(rt.Token).Equals(refreshToken));
            refToken.AccessTokenCreationDate = DateTime.UtcNow;
            //TODO NOWY EVENT
            return accessToken;
        }

        public void RevokeRefreshToken(string refreshToken)
        {
            var refToken = RefreshTokens.FirstOrDefault(rt => Convert.ToBase64String(rt.Token).Equals(refreshToken));
            _refreshTokens.Remove(refToken);
            //RefreshTokens.Clear();
        }

        public void RevokeExpiredRefreshTokens()
        {
            var refTokens = RefreshTokens.Where(rt => rt.RefreshTokenCreationDate <= DateTime.UtcNow.AddHours(expireHours)).ToArray();

            for(int i=0; i < refTokens.Length; i++)
            {
                _refreshTokens.Remove(refTokens[i]);
            }

        }
        
        public bool CheckAccessTokenExpireDate()
        {
            var lastAccessTokenCreationTime = RefreshTokens.Select(rt => rt.AccessTokenCreationDate).FirstOrDefault();
            return lastAccessTokenCreationTime >= DateTime.UtcNow.AddHours(expireHours);
        }

    }
}