using RahyabServices.Business.Domain.Models.Delinquent;
namespace RahyabServices.Business.Domain.Factories.Delinquent.Interfaces{
    public interface INotificationFactory{
        Notification Create(string title, string description, CustomerDelinquent customerDelinquent, NotificationType notificationType);
    }
}