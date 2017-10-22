using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Ebanking{
    public class GetThursdayShiftDtq : SharepointRequestDto{
        public string YearId { get; set; }
        public string MonthId { get; set; }
        public string WorkSectionId { get; set; }
    }
}