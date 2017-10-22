﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RahyabServices.Business.Domain.Models.VipBanking
{
    [Table("Potential")]
    public class Potential :IVipBankingEntity
    {
        [StringLength(10)]
        public string CustomerID { get; set; }

        public long? MeanTurnover { get; set; }

        public long? AccountCounts { get; set; }

        public string DateMainTurnOver { get; set; }

        public long? CountAll { get; set; }

        [StringLength(10)]
        public string Hisdate { get; set; }

        public string ReadFromQueryJoint { get; set; }

        public string FullName { get; set; }

        public string MelliCode { get; set; }

        public string BranchCode { get; set; }

        public string CityName { get; set; }

        public string Birthdate { get; set; }

        public string CustomerCityName { get; set; }

        public string CustTypeCode { get; set; }

        public string ProfCode { get; set; }

        public string CurrentTransDate { get; set; }

        public string TahsilatLevel { get; set; }

        public long? OpenDateBigint { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OpenDate { get; set; }

        [StringLength(10)]
        public string SexCode { get; set; }

        public string StrOpenDateShamsi { get; set; }

        public string OpenDateYearSeason { get; set; }

        public string OpenDateSeason { get; set; }

        public string OpenDateYearMonth { get; set; }

        public string Address { get; set; }

        public string AddressII { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ID { get; set; }

        public string Job { get; set; }

        public string MarriedCode { get; set; }

        public string DelinqBranchCode { get; set; }

        public string DateSarresidNahayi { get; set; }

        public string StartDate { get; set; }

        public string ContractStatusNumber { get; set; }

        public string DelinqID { get; set; }

        public string HisDateDelinq { get; set; }

        public string MablaqeTahsilateEtaiTakonun { get; set; }

        public string NerkheSoud { get; set; }

        public string RemainPenalty { get; set; }

        public string BankType { get; set; }

        public string DelinqType { get; set; }

        public string BranchId { get; set; }

        public string ContractType { get; set; }

        public string ContractDesc { get; set; }

        public string VaziatTazmin { get; set; }

        public string OrginalRemaining { get; set; }

        public string MandeSoud { get; set; }

        public string DelinqAllDepts { get; set; }

        public string DelinqCurrentRemaining { get; set; }

        public string RemainingNotCurrent { get; set; }

        public string DeliqDatePayment { get; set; }

        public string TotalPenalty { get; set; }

        public string DelinqAzMahaleBedehkaran { get; set; }

        public string DelinqBranchId { get; set; }

        public string TelNo { get; set; }

        [Column(TypeName = "money")]
        public decimal? CurrentRemaining { get; set; }

        public long? HisDateMojudi { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AccountCountMojudi { get; set; }

        [StringLength(50)]
        public string LookupInventoryStatus { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? InputCodeMojudy { get; set; }

        public long? RejChequeCash { get; set; }

        [StringLength(3)]
        public string StatusChequeCode { get; set; }

        [StringLength(4)]
        public string BranchCodeRejCheque { get; set; }

        [StringLength(6)]
        public string DateRejCheque { get; set; }

        public int? CountRejCheque { get; set; }

        [StringLength(50)]
        public string CopyOfStatus { get; set; }

        public long? ScoreOpenDate { get; set; }

        public long? ScoreDelinq { get; set; }

        public long? ScoreRejCheque { get; set; }

        public long? ScoreInventory { get; set; }

        public long? ScoreTurnOver { get; set; }

        public long? ScoreFinal { get; set; }

        public long? TedadeRade { get; set; }

        public long? SumDelinqAndRejCheque { get; set; }

        public string TashilatTitle { get; set; }

        public string SexTitle { get; set; }

        public string MarriedTitle { get; set; }

        public string OpenRegion { get; set; }

        public string OpenBranchName { get; set; }

        public string OpenCityofBranch { get; set; }

        public string DelinqTitleKind { get; set; }

        public string VaziateVorudeMoshtari { get; set; }

        [StringLength(10)]
        public string NewBranchCode { get; set; }

        [StringLength(10)]
        public string BranchClass { get; set; }

        [StringLength(50)]
        public string BranchTel { get; set; }

        public string BirhDateShamsi { get; set; }

        [Key]
        public long KeyId { get; set; }

        public string InputCode { get; set; }
    }
}