using System;
namespace RahyabServices.Business.Dtos.VipBanking{
    public class VipChequeDto{
        public string CustomerId { get; set; }
        public string ChequeStatusCode { get; set; }
        public string ChequeBranchCode { get; set; }
        public string ChequeRejDate { get; set; }
        public int? ChequeRejCount { get; set; }
        public decimal? ChequeSumRejCash { get; set; }
        public DateTime? CurrentTransDate { get; set; }
        public decimal? ID { get; set; }
        public long KeyId { get; set; }
    }
}