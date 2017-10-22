using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Log{
    [DataContract]
    public class YesterdayLogDto :IDto{
        [DataMember]
        public int CustomerDelinquentId { get; set; }
        public string UserName { get; set; }
        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Created { get; set; }
        [DataMember]
        public string CustomerCode { get; set; }
        [DataMember]
        public string ContractCode { get; set; }
    }
}