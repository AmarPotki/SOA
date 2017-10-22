using System.Data.Entity.ModelConfiguration;
using RahyabServices.Business.Domain.Models.Delinquent;
namespace RahyabServices.DataAccess.Mappings{
    public class NotificationMap : EntityTypeConfiguration<Notification>{
        public NotificationMap(){
            Property(x => x.CustomerDelinquentId).HasColumnName("CustomerDelinquent_Id");
        }
    }
}