namespace RahyabServices.ServiceTest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VIP")]
    public partial class VIP
    {
        [StringLength(10)]
        public string CustomerID { get; set; }

        [Column(TypeName = "money")]
        public decimal? MeanTurnover { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? AccountCounts { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

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

        public string OpenDateShamsi2 { get; set; }

        public string OpenDate_Year_Season { get; set; }

        public string OpenDate_Season { get; set; }

        public string OpenDate_Year_Month { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

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

        [Column("AccountCount(Mojudi)", TypeName = "numeric")]
        public decimal? AccountCount_Mojudi_ { get; set; }

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

        public int? Delinq_BranchCode { get; set; }

        public long? MandeSoud { get; set; }

        public long? Delinq_AllDepts { get; set; }

        public long? Delinq_CurrentRemaining { get; set; }

        public long? Delinq_AzMahaleBedehkaran { get; set; }

        public string DelinqID { get; set; }

        public DateTime? Todate { get; set; }

        [Column("Copy of InventoryStatus")]
        public long? Copy_of_InventoryStatus { get; set; }

        [Column("Copy of Status")]
        public long? Copy_of_Status { get; set; }

        public string CustomerLevel { get; set; }

        public long? Score_OpenDate { get; set; }

        public long? Score_Delinq { get; set; }

        public long? Score_RejCheque { get; set; }

        public long? Score_Inventory { get; set; }

        public long? Score_TurnOver { get; set; }

        public long? Score_Final { get; set; }

        public long? TedadeRade { get; set; }

        [Column("Sum_Delinq&RejCheque")]
        public long? Sum_Delinq_RejCheque { get; set; }

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
        public string BirhDate_Shamsi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate_Miladi { get; set; }

        [StringLength(10)]
        public string OpenDate_Year { get; set; }

        [StringLength(50)]
        public string BirthDate2 { get; set; }

        [Column("Branch Name")]
        [StringLength(50)]
        public string Branch_Name { get; set; }

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
