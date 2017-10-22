using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RahyabServices.Business.Domain.Models.BranchMarketing
{
    [Table("MainRevertCusts")]
    public class MainRevertCusts:IBranchMarketingEntity
    {
        [Key]
     [Column("KeyID")]
      public long KeyId { get; set; }

      public string CUST_NO { get; set; }
 
        public string Rank { get; set; }

        public DateTime? Hisdate { get; set;  }

        public long? FREQUENCY { get; set; }

        public string MONETRAY { get; set; }
         public  long? CampianID { get; set; }
        public  long? SPListItem { get; set; }
        public  bool? IsDeleted { get; set; }

    }
}
