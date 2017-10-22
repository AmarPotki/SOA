using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Notification{
    [DataContract]
    public class RemoveNotificationDto:IDto{
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public int NotificationId { get; set; }
    }
}