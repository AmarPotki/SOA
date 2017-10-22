using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Contracts{
    [DataContract]
    public class CountAndSumDto:IDto{
        public CountAndSumDto(int count,decimal sum){
            Count = count;
            Sum = sum;
        }
        public string UserName { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public decimal Sum { get; set; }
    }
}