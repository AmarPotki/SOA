using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;

namespace RahyabServices.DataAccess.Mappings
{
    public class SmsTemplateMap : EntityTypeConfiguration<SmsTemplate>
    {
        public SmsTemplateMap()
        {
        }
    }
}
