using System;
using System.Collections.Generic;
using Infrastructure.Application.Command;

namespace Service.IdentityServer.Application.UserAggregate.AddUser
{
    public class AddUserCommand : CommandBase
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<string> Claims { get; set; }
    }
}