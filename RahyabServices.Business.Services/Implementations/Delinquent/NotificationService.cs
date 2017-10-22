using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Dtos.Delinquent.Notification;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.Common.Cryptography;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Delinquent
{
    public class NotificationService : INotificationService
    {
        private readonly ICryptographer _cryptographer;
        private readonly IHrFacade _hrFacade;
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(ICryptographer cryptographer, IHrFacade hrFacade, INotificationRepository notificationRepository)
        {
            _cryptographer = cryptographer;
            _hrFacade = hrFacade;
            _notificationRepository = notificationRepository;
        }
        public async Task<IEnumerable<NotificationDto>> GetNotifications(GetNotificationsDto getNotificationsDto)
        {
            var userName = _cryptographer.Decrypt(getNotificationsDto.UserName);
            var branchCode = _hrFacade.GetBranchCode(userName);
            var notifications = await _notificationRepository.GetNotificationByBranchCode(branchCode);
            return Mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationDto>>(notifications);
        }
        public async Task UpdateToSeen(UpdateNotificationToSeenDto updateNotificationToSeenDto)
        {
            var notification = await _notificationRepository.OneAsync(updateNotificationToSeenDto.NotificationId);
            notification.IsSeen = true;
            await _notificationRepository.SaveAsync(notification);
        }
        public async Task RemoveNotification(RemoveNotificationDto removeNotification)
        {
            var notification = await _notificationRepository.OneAsync(removeNotification.NotificationId);
            notification.IsDone = true;
            await _notificationRepository.SaveAsync(notification);
        }
    }
}