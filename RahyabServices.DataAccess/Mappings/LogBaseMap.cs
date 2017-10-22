using System.Data.Entity.ModelConfiguration;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
namespace RahyabServices.DataAccess.Mappings
{
    public class LogBaseMap : EntityTypeConfiguration<LogBase>
    {
        public LogBaseMap()
        {
            Property(x => x.CustomerDelinquentId).HasColumnName("CustomerDelinquent_Id");
        }
    }
}