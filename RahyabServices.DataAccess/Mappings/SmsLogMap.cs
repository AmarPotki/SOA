using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;

namespace RahyabServices.DataAccess.Mappings
{
    public class SmsLogMap : EntityTypeConfiguration<SmsLog>
    {
        public SmsLogMap()
        {
            Property(x => x.SmsTemplateId).HasColumnName("SmsTemplate_Id");
        }
    }
}
