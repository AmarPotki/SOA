using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Log.Split{
    public class RespondRequestSplitDto:IDto{
        public string UserName { get; set; }
        [DataMember]
        public bool Approve { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int RequestStateHandlerId { get; set; }
        [DataMember]
        public int CustomerDelinquentId { get; set; }
        [DataMember]
        public string RespondUserName { get; set; }
    }
}