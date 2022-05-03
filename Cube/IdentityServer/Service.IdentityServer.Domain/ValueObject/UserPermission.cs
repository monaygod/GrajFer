namespace Service.IdentityServer.Domain.ValueObject
{
    public class UserPermission : Infrastructure.DDD.ValueObject
    {
        public string ScopeName { get; set; }
        
        private UserPermission(){}

        public UserPermission(string scopeName)
        {
            ScopeName = scopeName;
        }
    }
}