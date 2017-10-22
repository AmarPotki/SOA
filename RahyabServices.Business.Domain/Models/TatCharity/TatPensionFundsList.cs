using System;
using RahyabServices.Business.SharepointAutoMapper;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
using RahyabServices.Business.SharepointAutoMapper.Models;
namespace RahyabServices.Business.Domain.Models.TatCharity{
    [SharepointListId("{CE59D57A-852C-4739-8DA7-C6877F853C0E}")]
    public class TatPensionFundsList : IEntitySharepointMapper{
        [SharepointFieldName("ID")]
        public int? Id { get; set; }
        [SharepointFieldName("Pension")]
        public LookupFieldMapper Pension { get; set; }
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