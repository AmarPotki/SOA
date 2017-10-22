using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Types;
using RahyabServices.Common.Convertors;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;

namespace RahyabServices.DataAccess.Repositories.Delinquent.Implementations
{
    public class SmsTemplateRepository : DelinquentRepositoryBase<SmsTemplate>, ISmsTemplateRepository
    {
        private readonly IDataContextFactory _dataContextFactory;
        private readonly IDateTimeConvertor _dateTimeConvertor;
        public SmsTemplateRepository(IDataContextFactory databaseFactory, IDateTimeConvertor dateTimeConvertor)
            : base(databaseFactory)
        {
            _dataContextFactory = databaseFactory;
            _dateTimeConvertor = dateTimeConvertor;
        }

        public async Task<SmsTemplate> GetSmsTemplateByTypeAsync(TemplateType type)
        {
            return await QueryAsync(async q => await q.SingleOrDefaultAsync(t => t.Type == type));
        }

        public SmsTemplate GetSmsTemplateByType(TemplateType type)
        {
            return Query(q => q.SingleOrDefault(t => t.Type == type));
        }
    }
}
