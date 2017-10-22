using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.VipBanking{
    public class GetPotentialByCustomerNumberDtq: SharepointRequestDto{
        public string CustomerNumber { get; set; }
    }
}