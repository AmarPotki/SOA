using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.VipBanking{
    [Table("DelinquentEn")]
    public class VipDelinquent : IVipBankingEntity{
        public string CustomerId { get; set; }
        public string DeliqBranchCode { get; set; }
        public DateTime? DelinqDateSarresidNahayi { get; set; }
        public DateTime? DelinqStartDate { get; set; }
        public string ContractStatusNumber { get; set; }
        [Column("DelinqID")]
        public string DelinqId { get; set; }
        public long? DelinqHisdate { get; set; }
        public double? NerkheSoud { get; set; }
        public long? RemainPenalty { get; set; }
        public int? BankType { get; set; }
        public int? DelinqBranchCodeInt { get; set; }
        public int? ContractType { get; set; }
        public string ContractDesc { get; set; }
        public string WarrantyStatus { get; set; }
        public long? OrginalRemaining { get; set; }
        public long? MandeSoud { get; set; }
        public long? DelinqAllDepts { get; set; }
        public long? DelinqCurrentRemaining { get; set; }
        public long? RemainingNotCurrent { get; set; }
        public string PaymentDate { get; set; }
        public string TotalPenalty { get; set; }
        public long? DelinqAzMahaleBedehkaran { get; set; }
        public string DelinqBranchCode { get; set; }
        public string DelinqBranchName { get; set; }
        public string DelinqType { get; set; }
        public DateTime? CurrentTransDate { get; set; }
        public string DerivedCustomerIdTen { get; set; }
        [Column("ID")]
        public string Id { get; set; }
        public string CustomerIdTen { get; set; }
        [StringLength(50)]
        public string CopyOfContractStatusNumber { get; set; }
        public long? MablaqeTahsilateEtaiTakonun { get; set; }
        [Key]
        public long KeyId { get; set; }
    }
}