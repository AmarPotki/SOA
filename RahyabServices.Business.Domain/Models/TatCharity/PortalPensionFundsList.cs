using System;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.Domain.Models.TatCharity{
    [SharepointListId("{F74C3D03-68B1-4D20-8D91-25E58D539080}")]
    public class PortalPensionFunds : IEntitySharepointMapper
    {
        [SharepointFieldName("ID")]
        public int? Id { get; set; }
        public string Title { get; set; }
        public int ApplicantId { get; set; }
        public int PensionId { get; set; }
        public string Payer { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string DocNumber { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}