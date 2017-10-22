using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.VipBanking{
    [Table("ChequeEn")]
    public class Cheque : IVipBankingEntity
    {
        [StringLength(10)]
        public string CustomerId { get; set; }

        [StringLength(2)]
        public string ChequeStatusCode { get; set; }

        [StringLength(4)]
        public string ChequeBranchCode { get; set; }

        [StringLength(16)]
        public string ChequeRejDate { get; set; }

        public int? ChequeRejCount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ChequeSumRejCash { get; set; }

        public DateTime? CurrentTransDate { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ID { get; set; }

        [Key]
        public long KeyId { get; set; }
    }
}