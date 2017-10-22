using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.AbsorbResources{
    
    public class GetCustomerInformationDtq :SharepointRequestDto{
        public string CustomerNumber { get; set; }
    }
}