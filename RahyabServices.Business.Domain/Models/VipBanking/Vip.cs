using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RahyabServices.Business.Domain.Models.VipBanking
{
    [Table("Private")]
    public class Vip : IVipBankingEntity
    {
        [StringLength(10)]
        public string CustomerID { get; set; }

        [Column(TypeName = "money")]
        public decimal? MeanTurnover { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AccountCounts { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateMain { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public string FullName { get; set; }

        public string MelliCode { get; set; }

        public string BranchCode { get; set; }

        public string TelNo { get; set; }

        public string Birthdate { get; set; }

        public string CityName { get; set; }

        public string CustTypeCode { get; set; }

        public string ProfCode { get; set; }

        public string CurrentTransDate { get; set; }

        public string TahsilatLevel { get; set; }

        public long? OpenDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OpenDateShamsi { get; set; }

        [StringLength(10)]
        public string SexCode { get; set; }

        public string StrOpenDateShamsi { get; set; }

        public string OpenDateYearSeason { get; set; }

        public string OpenDateSeason { get; set; }

        public string OpenDateYearMonth { get; set; }

        public string Address { get; set; }

        public string AddressII { get; set; }

        public string Job { get; set; }

        public string MarriedCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateCurrentRemaining { get; set; }

        public long? RejChequeCash { get; set; }

        [StringLength(3)]
        public string StatusChequeCode { get; set; }

        [StringLength(6)]
        public string DateRejCheque { get; set; }

        public int? CountRejCheque { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AccountCountMojudi { get; set; }

        [StringLength(50)]
        public string InventoryStatus { get; set; }

        [StringLength(4)]
        public string BranchCodeRejCheque { get; set; }

        public string DelinqBranchCode { get; set; }

        public string DateSarresidNahayi { get; set; }

        public string ContractStatusNumber { get; set; }

        public string MablaqeTahsilateEtaiTakonun { get; set; }

        public string NerkheSoud { get; set; }

        public string DelinqType { get; set; }

        public string RemainPenalty { get; set; }

        public string ContractDesc { get; set; }

        public string ContractType { get; set; }

        public string RemainingNotCurrent { get; set; }

        public string OrginalRemaining { get; set; }

        [Column(TypeName = "money")]
        public decimal? CurrentRemainingCustomer { get; set; }

        public DateTime? DelinqStartDate { get; set; }

        public string HisDate { get; set; }

        public int? DelinqBankCode { get; set; }

        public long? MandeSoud { get; set; }

        public long? DelinqAllDepts { get; set; }

        public long? DelinqCurrentRemaining { get; set; }

        public long? DelinqAzMahaleBedehkaran { get; set; }

        public string DelinqID { get; set; }

        public DateTime? Todate { get; set; }

        public long? CopyOfInventoryStatus { get; set; }

        public long? CopyOfStatus { get; set; }

        public string CustomerLevel { get; set; }

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

        [StringLength(255)]
        public string BirhDateShamsi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDateMiladi { get; set; }

        [StringLength(10)]
        public string OpenDateYear { get; set; }

        [StringLength(50)]
        public string StrBirthDate { get; set; }

        [StringLength(50)]
        public string BranchName { get; set; }

        [StringLength(50)]
        public string BranchTitle { get; set; }

        [StringLength(50)]
        public string PrivateCustomerLevel { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? ID { get; set; }

        [Key]
        public long KeyId { get; set; }
    }
}