using System;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.TatCharity{
    [DataContract]
    public class AddTatFundDtc : SharepointRequestDto{
        [DataMember]
        public int ApplicantId { get; set; }
        [DataMember]
        public int LoanId { get; set; }
        [DataMember]
        public string Payer { get; set; }
        [DataMember]
        public string BranchName { get; set; }
        [DataMember]
        public string BranchCode { get; set; }
        [DataMember]
        public string DocNo { get; set; }
        [DataMember]
        public double PaymentAmount { get; set; }
        [DataMember]
        public DateTime PaymentDate { get; set; } = DateTime.Now;
    }
}