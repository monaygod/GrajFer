namespace Service.GameServer.Domain.ValueObject
{
    public class UserPermission : Infrastructure.DDD.ValueObject
    {
        public string ScopeName { get; set; }
    }
}