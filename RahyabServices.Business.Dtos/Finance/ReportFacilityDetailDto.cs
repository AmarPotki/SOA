using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Finance{
    public class ReportFacilityDetailDto : IDto{
        public string Description { get; set; }
        public string PersianDate{ get; set; }
        public double Amount { get; set; }
        public int Priority { get; set; }
    }
}