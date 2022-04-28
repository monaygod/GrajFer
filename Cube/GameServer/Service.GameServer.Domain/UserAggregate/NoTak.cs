namespace Service.GameServer.Domain.UserAggregate;

public class NoTak
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    private NoTak()
    {
        
    }

    public static NoTak Create()
    {
        return new NoTak()
        {
            Name = "asd"
        };
    }
}