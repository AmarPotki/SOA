using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.ParsLogic{
    public class BranchInformationDtc : SharepointRequestDto{
        public string BranchCode { get; set; }
        public string Description { get; set; }
        public string RequestType { get; set; }
    }
}