using System;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
using RahyabServices.Business.SharepointAutoMapper.Models;
namespace RahyabServices.Business.Domain.Models.TatCharity{
    [SharepointListId("{1594D219-143F-40A2-91FF-7988E1EFDB70}")]
    public class TatLoanFunds : IEntitySharepointMapper{
        [SharepointFieldName("ID")]
        public int? Id { get; set; }
        //  [MapperLookupValue("Loan")]
        [SharepointFieldName("Loan")]
        public LookupFieldMapper Loan { get; set; }
        //  [MapperLookupValue("Applicant")]
        [SharepointFieldName("Applicant")]
        public LookupFieldMapper Applicant { get; set; }
        public string DocNo { get; set; }
        public double PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }        
        public string Payer { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        
    }
}