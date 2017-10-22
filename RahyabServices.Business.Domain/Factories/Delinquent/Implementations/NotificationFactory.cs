using RahyabServices.Business.Domain.Factories.Delinquent.Interfaces;
using RahyabServices.Business.Domain.Models.Delinquent;
namespace RahyabServices.Business.Domain.Factories.Delinquent.Implementations{
    public class NotificationFactory : INotificationFactory{
        public Notification Create(string title, string description, CustomerDelinquent customerDelinquent, NotificationType notificationType){
            var notification = new Notification
            {
                Title = title +" شماره تسهیلات : "+ customerDelinquent.ContractCode,
                Description = description +" نام مشتری : " + customerDelinquent.FullName,
                IsDone = false,
                NotificationType = notificationType
            };
            notification.SetCustomerDelinquentId(customerDelinquent.Id);
            return notification;
        }
    }
}