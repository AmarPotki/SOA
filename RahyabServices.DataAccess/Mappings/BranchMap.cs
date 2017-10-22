using System.Data.Entity.ModelConfiguration;
using RahyabServices.Business.Domain.Models.Delinquent;
namespace RahyabServices.DataAccess.Mappings{
    public class BranchMap : EntityTypeConfiguration<Branch>{
        public BranchMap(){
            Property(x => x.ParentId).HasColumnName("Parent_Id");
        }
    }
}