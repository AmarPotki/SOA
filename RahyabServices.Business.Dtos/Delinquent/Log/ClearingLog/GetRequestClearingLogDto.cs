using System.Data.Common;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog{
    [DataContract]
    public class GetRequestClearingLogDto : IDto
    {
        [DataMember]
        public int RequestId { get; set; }
        [DataMember]
        public string UserName { get; set; }
    }
}