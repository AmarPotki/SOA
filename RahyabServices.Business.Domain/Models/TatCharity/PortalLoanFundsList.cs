using System;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.Domain.Models.TatCharity{
    [SharepointListId("{22201F23-871F-4678-9C78-D4439C08E45D}")]
    public class PortalLoanFunds : IEntitySharepointMapper
    {
        [SharepointFieldName("ID")]
        public int? Id { get; set; }
        public string Title { get; set; }
        public int ApplicantId { get; set; }
        public int LoanId { get; set; }
        public string Payer { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string DocNumber { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}