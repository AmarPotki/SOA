using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos{
    public class ErrorInfoDto : IDto{
        public string Description { get; set; }
        public bool IsError { get; set; }
    }
}