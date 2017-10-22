using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Log.Split{
    [DataContract]
    public class CancelRequestSplitDto : IDto{
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public int CustomerDelinquentId { get; set; }
        [DataMember]
        public int RequestLogId { get; set; }

    }
}