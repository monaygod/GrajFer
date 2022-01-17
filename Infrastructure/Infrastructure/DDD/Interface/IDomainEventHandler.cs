using MediatR;

namespace Infrastructure.DDD.Interface
{
    public interface IDomainEventHandler<T>  : INotificationHandler<T> where T:INotification  
    {
        
    }
}