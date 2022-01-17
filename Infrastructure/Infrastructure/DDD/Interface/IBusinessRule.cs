namespace Infrastructure.DDD.Interface
{
    public interface IBusinessRule
    {
        bool IsBroken();

        string Message { get; }
    }
}