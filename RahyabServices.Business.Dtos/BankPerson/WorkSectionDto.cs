using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.BankPerson{
    public class WorkSectionDto : IDto{
        public int WorkSectionId { get; set; }
        public string WorkSectionTitle { get; set; }
    }
}