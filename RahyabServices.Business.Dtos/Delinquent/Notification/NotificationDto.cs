using System.Runtime.Serialization;
namespace RahyabServices.Business.Dtos.Delinquent.Notification{
    [DataContract]
    public class NotificationDto{
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int CustomerDelinquentId { get; set; }
        [DataMember]
        public int NotificationType { get; set; }
    }
}