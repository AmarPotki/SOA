using System.Data.Entity.ModelConfiguration;
using RahyabServices.Business.Domain.Models.Delinquent;

namespace RahyabServices.DataAccess.Mappings
{
    public class CustomerDelinquentMap : EntityTypeConfiguration<CustomerDelinquent>
    {
        public CustomerDelinquentMap(){
            Property(x => x.CurrentStateId).HasColumnName("RahyabServicesState_Id");
            Property(x => x.BranchId).HasColumnName("Branch_Id");
        }
    }
}
