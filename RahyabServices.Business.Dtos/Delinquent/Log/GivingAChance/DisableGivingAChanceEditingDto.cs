using System.Runtime.Serialization;
using RahyabServices.Common.Dto;

namespace RahyabServices.Business.Dtos.Delinquent.Log.GivingAChance{
    [DataContract]
    public class DisableGivingAChanceEditingDto : IDto
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public int CustomerDelinquentId { get; set; }
    }
}