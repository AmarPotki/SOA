using System.Data.Common;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog{
    [DataContract]
    public class GetClearingLogDto:IDto{
        [DataMember]
        public int ClearingRequestId { get; set; }
        [DataMember]
        public string UserName { get; set; }
    }
}