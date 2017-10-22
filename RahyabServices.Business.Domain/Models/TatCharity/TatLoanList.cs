using System;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.Domain.Models.TatCharity{
    [SharepointListId("{A4B245F7-778B-497B-98B2-56F210833CC3}")]
    public class TatLoan : IEntitySharepointMapper
    {
        [SharepointFieldName("ID")]
        public int? Id { get; set; }
        public double Price { get; set; }
        public double InstallmentCount { get; set; }
        public double InstallmentPrice { get; set; }
        public DateTime InstallmentStartDate { get; set; }
        public DateTime InstallmentDueDate { get; set; }

    }
}