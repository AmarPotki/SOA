using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RahyabServices.Business.Domain.Models.BranchMarketing
{
    [Table("DailyRevertCustomer")]
    public  class DailyRevertCustomers : IBranchMarketingEntity
    {
        [Column("KeyID")]
        [Key]
        public long KeyId { get; set; }
        public string FullName { get; set; }
        public string CUST_NO { get; set; }
        public string Rank { get; set; }

        public long? RECENCY { get; set; }
        public long? FREQUENCY { get; set; }
        public string MONETRAY { get; set; }
        public DateTime DelinqStartDate { get; set; }
        public DateTime DelinqDateSarresidNahayi { get; set; }
        public long? MablaqeTahsilateEtaiTakonun { get; set; }
        public string CUStTYPE { get; set; }
        //موجودی حسلب مشتری در زمان تعریف کمپین
        public long? Remaining { get; set; }
        public string OPNBR { get; set; }
        public string ACNO { get; set; }
        public DateTime? Hisdate { get; set; }

        public string phone { get; set; }

        public string TySandoghCertNOpe { get; set; }

        public string SandoghDate { get; set; }
        public string SandoghStatus { get; set; }
        public string SandoghBranch { get; set; }

        public string MelliCode { get; set; }

        public double CertDepBase { get; set; }

        public double CertDepCn { get; set; }
        public double SandoghValue { get; set; }

        public string BranchName { get; set; }
        //موجودی جاری مشتری که هر روز به روز رسانی می شود
        public double CurrentRemaining { get; set; }

        public long? FirstRemaining { get; set; }
        //کد منطقه
        public string RegionCode { get; set; }
        public string SetadiName { get; set; }
        public double CampID { get; set; }
    }
}
