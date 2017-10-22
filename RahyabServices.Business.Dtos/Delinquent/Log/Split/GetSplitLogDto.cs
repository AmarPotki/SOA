using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Log.Split{
    [DataContract]
    public class GetSplitLogDto:IDto{
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public int RequestId { get; set; }
    }
}