using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.VipBanking{
    public  class GeneralReport : IVipBankingEntity
    {
        public int? CountAllBankCusts { get; set; }

        public int? CountRealCusts { get; set; }

        public string CUSTYPE { get; set; }

        public long? CountLegalCusts { get; set; }

        public int? CountVipI { get; set; }

        [StringLength(50)]
        public string PrivateCustomerLevel { get; set; }

        public long? CurrentRemainingCustomerVipI { get; set; }

        [Column(TypeName = "money")]
        public decimal? MeanTurnoverVipI { get; set; }

        public int? CountSharedCusts { get; set; }

        public int? CountVipII { get; set; }

        public long? CurrentRemainingCustomerVipII { get; set; }

        [Column(TypeName = "money")]
        public decimal? MeanTurnoverVipII { get; set; }

        public int? CountVipIII { get; set; }

        public int? CountPrivate { get; set; }

        public int? Upgraded { get; set; }

        public int? Downgraded { get; set; }

        public int? PotentialCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? MeanTurnoverPrivate { get; set; }

        public long? CurrentRemainingCustomerVipIII { get; set; }

        [Column(TypeName = "money")]
        public decimal? CurrentRemainingCustomerPrivate { get; set; }

        [Column(TypeName = "money")]
        public decimal? MeanTurnoverVipIII { get; set; }

        public long? AllCashBank { get; set; }

        [Key]
        public long KeyId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? CountAllVip { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(10)]
        public string RatioCountDowngrade { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(10)]
        public string RatioCountUpgrade { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(11)]
        public string RatioCountVipIDivAllVip { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(11)]
        public string RatioCountVipIIDivAllVip { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(11)]
        public string RatioCountVipIIIDivAllVip { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(11)]
        public string RationCountPrivateDivAllVip { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioCountVipIDivAll { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioCountVipIIDivAll { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioCountVipIIIDivAll { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioCountPrivateDivAll { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long? CountSumRealVsLegalCusts { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long? CountSumRealVsLegalVsSharedCusts { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? CountSumRealVsSharedCusts { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioCountPrivateDivSumRealVsShared { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioCountVipIDivSumRealVsShared { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioCountVipIIDivSumRealVsShared { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioCountVipIIIDivSumRealVsShared { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioCountVipIDivSumRealVsSharedVsLegal { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioCountVipIIDivSumRealVsSharedVsLegal { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioCountVipIIIDivSumRealVsSharedVsLegal { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioCountPrivateDivSumRealVsSharedVsLegal { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioAllVipDivSumRealVsShared { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioAllVipDivSumRealVsSharedVsLegal { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioUpgradeDivSumRealVsShared { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioUpgradeDivSumRealVsSharedVsLegal { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioDowngradeDivSumRealVsShared { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioDowngradeDivSumRealVsSharedVsLegal { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioPotentialDivSumAllBank { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioPotentialDivSumRealVsShared { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioPotentialDivSumRealVsSharedVsLegal { get; set; }

        [Column(TypeName = "money")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? AllCashVip { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioCashVipIIDivAll { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioCashVipIDivAll { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(16)]
        public string RatioCashVipIIIDivAll { get; set; }

        [Column(TypeName = "money")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? RatioCashPrivateDivAll { get; set; }

       // [Column(TypeName = "money")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string RatioCashVipIDivAllVip { get; set; }

        //[Column(TypeName = "money")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string RatioCashVipIIDivAllVip { get; set; }

        //[Column(TypeName = "money")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string RatioCashVipIIIDivAllVip { get; set; }

       // [Column(TypeName = "money")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string RatioCashPrivateDivAllVip { get; set; }
    }
}