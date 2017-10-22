using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Delinquent.Notification;
namespace RahyabServices.Business.Services.Intefaces.Delinquent{
    public interface INotificationService{
        Task<IEnumerable<NotificationDto>> GetNotifications(GetNotificationsDto getNotificationsDto);
        Task UpdateToSeen(UpdateNotificationToSeenDto updateNotificationToSeenDto);
        Task RemoveNotification(RemoveNotificationDto removeNotification);
    }
}