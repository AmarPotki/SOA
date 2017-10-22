using System.Data.Entity.ModelConfiguration;
using RahyabServices.Business.Domain.Models.Delinquent;
namespace RahyabServices.DataAccess.Mappings{
    public class BranchClaimMap : EntityTypeConfiguration<BranchClaim>{
        public BranchClaimMap(){
            Property(x => x.BranchId).HasColumnName("Branch_Id");
        }
    }
}