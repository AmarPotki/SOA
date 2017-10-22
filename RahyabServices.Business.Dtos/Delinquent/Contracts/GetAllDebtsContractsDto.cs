using System.Runtime.Serialization;
using RahyabServices.Common.Dto;

namespace RahyabServices.Business.Dtos.Delinquent.Contracts
{
    public class GetAllDebtsContractsDto:IDto
    {
        [DataMember]
        public string UserName { get; set; }
    }
}
