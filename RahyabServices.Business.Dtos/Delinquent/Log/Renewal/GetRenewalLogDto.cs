using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Log.Renewal{
    public class GetRenewalLogDto : IDto{
        [DataMember]
        public int RequestId { get; set; }
        [DataMember]
        public string UserName { get; set; }
    }
}