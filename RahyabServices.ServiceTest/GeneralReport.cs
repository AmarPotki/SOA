namespace RahyabServices.ServiceTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GeneralReport
    {
        public int? CountAllBankCusts { get; set; }

        public int? CountRealCusts { get; set; }

        public string CUSTYPE { get; set; }

        public long? CountLegalCusts { get; set; }

        public int? CountVipI { get; set; }

        [StringLength(50)]
        public string PrivateCustomerLevel { get; set; }

        [Column(TypeName = "money")]
        public decimal? CurrentRemainingCustomerVipI { get; set; }

        [Column(TypeName = "money")]
        public decimal? MeanTurnoverVipI { get; set; }

        public int? CountSharedCusts { get; set; }

        public int? CountVipII { get; set; }

        [Column(TypeName = "money")]
        public decimal? CurrentRemainingCustomerVipII { get; set; }

        [Column(TypeName = "money")]
        public decimal? MeanTurnoverVipII { get; set; }

        public int? CountVipIII { get; set; }

        public int? CountPrivate { get; set; }

        public int? Upgraded { get; set; }

        public int? Downgraded { get; set; }

        public int? PotentialCount { get; set; }

        [Column(TypeName = "money")]
        public decimal? MeanTurnoverPrivate { get; set; }

        [Column(TypeName = "money")]
        public decimal? CurrentRemainingCustomerVipIII { get; set; }

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
        public int? RatioCountDowngrade { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? RatioCountUpgrade { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? RatioCountVipIDivAllVip { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? RatioCountVipIIDivAllVip { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? RatioCountVipIIIDivAllVip { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? RationCountPrivateDivAllVip { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? RatioCountVipIDivAll { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? RatioCountVipIIDivAll { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? RatioCountVipIIIDivAll { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? RatioCountPrivateDivAll { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long? CountSumRealVsLegalCusts { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long? CountSumRealVsLegalVsSharedCusts { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? CountSumRealVsSharedCusts { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? RatioCountPrivateDivSumRealVsShared { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? RatioCountVipIDivSumRealVsShared { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? RatioCountVipIIDivSumRealVsShared { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? RatioCountVipIIIDivSumRealVsShared { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long? RatioCountVipIDivSumRealVsSharedVsLegal { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long? RatioCountVipIIDivSumRealVsSharedVsLegal { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long? RatioCountVipIIIDivSumRealVsSharedVsLegal { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long? RatioCountPrivateDivSumRealVsSharedVsLegal { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? RatioAllVipDivSumRealVsShared { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long? RatioAllVipDivSumRealVsSharedVsLegal { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? RatioUpgradeDivSumRealVsShared { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long? RatioUpgradeDivSumRealVsSharedVsLegal { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? RatioDowngradeDivSumRealVsShared { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long? RatioDowngradeDivSumRealVsSharedVsLegal { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? RatioPotentialDivSumAllBank { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? RatioPotentialDivSumRealVsShared { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? RatioPotentialDivSumRealVsSharedVsLegal { get; set; }

        [Column(TypeName = "money")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? RatioCashVipIDivAllVip { get; set; }

        [Column(TypeName = "money")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? RatioCashVipIIDivAllVip { get; set; }

        [Column(TypeName = "money")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? RatioCashVipIIIDivAllVip { get; set; }

        [Column(TypeName = "money")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? RatioCashPrivateDivAllVip { get; set; }

        [Column(TypeName = "money")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? AllCashVip { get; set; }

        public long? RatioCashVipIIDivAll { get; set; }

        public long? RatioCashVipIDivAll { get; set; }

        public long? RatioCashVipIIIDivAll { get; set; }

        public long? RatioCashPrivateDivAll { get; set; }
    }
}
