using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Notification{
    [DataContract]
    public class GetNotificationsDto:IDto{
        [DataMember]
        public string UserName { get; set; }
    }
}