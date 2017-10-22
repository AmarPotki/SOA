using System.Runtime.Serialization;
using RahyabServices.Business.Dtos.Delinquent.Contracts.Manager;
namespace RahyabServices.Business.Dtos.Delinquent.BranchClaim{
    [DataContract]
    public class GetLastSevenDaysBranchClaimsDto:ManagerRequestDto{
        public int BranchId { get; set; }
    }
}