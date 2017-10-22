using System.Runtime.Serialization;
namespace RahyabServices.Business.Dtos.Delinquent.Contracts.Manager{
    [DataContract]
    public class GetAllBranchActivityDtq : ManagerRequestDto
    {
        [DataMember]
        public string FromPersianDate { get; set; }
        [DataMember]
        public string ToPersianDate { get; set; }
    }
}