using System;
using RahyabServices.Common.Dto;

namespace RahyabServices.Business.Dtos.VipBanking
{
    public class VipDto : IDto
    {
        public string CustomerID { get; set; }
        public decimal? MeanTurnover { get; set; }
        public decimal? AccountCounts { get; set; }
        public DateTime? DateMain { get; set; }
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
        public DateTime? OpenDateShamsi { get; set; }
        public string SexCode { get; set; }
        public string StrOpenDateShamsi { get; set; }
        public string OpenDateYearSeason { get; set; }
        public string OpenDateSeason { get; set; }
        public string OpenDateYearMonth { get; set; }
        public string Address { get; set; }
        public string AddressII { get; set; }
        public string Job { get; set; }
        public string MarriedCode { get; set; }
        public DateTime? DateCurrentRemaining { get; set; }
        public long? RejChequeCash { get; set; }
        public string StatusChequeCode { get; set; }
        public string DateRejCheque { get; set; }
        public int? CountRejCheque { get; set; }
        public decimal? AccountCountMojudi { get; set; }
        public string InventoryStatus { get; set; }
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


        public string NewBranchCode { get; set; }


        public string BranchClass { get; set; }


        public string BranchTel { get; set; }


        public string BirhDateShamsi { get; set; }

        public DateTime? BirthDateMiladi { get; set; }


        public string OpenDateYear { get; set; }


        public string StrBirthDate { get; set; }


        public string BranchName { get; set; }


        public string BranchTitle { get; set; }


        public string PrivateCustomerLevel { get; set; }

        public decimal? ID { get; set; }
        public long KeyId { get; set; }}
    }
