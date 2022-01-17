using System.Collections.Generic;
using Service.IdentityServer.Domain.UserAggregate;

namespace Service.IdentityServer.Domain.ValueObject
{
    public class UserPermission
    {
        public string ScopeName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}