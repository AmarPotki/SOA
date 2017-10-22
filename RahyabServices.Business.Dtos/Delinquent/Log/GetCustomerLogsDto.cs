using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Log
{
    [DataContract]
    public class GetCustomerLogsDto:IDto
    {
        [DataMember]
        public int CustomerDelinquentId { get; set; }
        [DataMember]
        public string UserName { get; set; }
    }
}