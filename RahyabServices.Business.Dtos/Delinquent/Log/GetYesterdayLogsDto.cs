using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Log{
    [DataContract]
    public class GetYesterdayLogsDto : IDto{
        [DataMember]
        public string UserName { get; set; }
    
    }
}