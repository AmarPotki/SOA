using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Bank{
    public class LastBalDetailDto : IDto{
        public string CustomerNumber { get; set; }
        public int TotalAccountNumber { get; set; }
        public long? RemainingAmountCurrent { get; set; }
        public string HisDate { get; set; }
    }
}