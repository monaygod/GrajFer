using System.Collections.Generic;
using Infrastructure.DDD;
using Service.IdentityServer.Domain.UserAggregate;

namespace Service.IdentityServer.Domain.ValueObject
{
    public class UserPermission : Infrastructure.DDD.ValueObject
    {
        public string ScopeName { get; set; }
    }
}