using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Finance{
    public class GetReportFacilityDetailDtq : SharepointRequestDto{
        public string FromPersianDate{ get; set; }
        public string ToPersianDate{ get; set; }
    }
}