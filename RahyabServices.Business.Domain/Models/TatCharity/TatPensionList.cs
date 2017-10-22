using System;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.Domain.Models.TatCharity{
    [SharepointListId("{31FACAE3-68AC-4290-AC8C-E05A76D77E0C}")]
    public class TatPension : IEntitySharepointMapper
    {
        [SharepointFieldName("ID")]
        public int? Id { get; set; }
        public double PensionCount { get; set; }
        public double PensionPrice { get; set; }
        public DateTime PaymentDate { get; set; }
        public string AccountNo { get; set; }
    }
}