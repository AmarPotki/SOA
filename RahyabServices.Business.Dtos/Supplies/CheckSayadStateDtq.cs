using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Supplies{
    public class CheckSayadStateDtq : SharepointRequestDto{
        public int ItemId { get; set; }
    }
    public class CheckSayadStateResultDto 
    {
        public string ReasonCode { get; set; }
        public string StatusDescription { get; set; }
        public string StatusCode { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }

    }

}