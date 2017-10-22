using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Bank;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Bank.Implementations{
    public class RptftGuarantorRepository : BankRepositoryBase<RptftGuarantor>, IRptftGuarantorRepository{
        private readonly IDataContextFactory _databaseFactory;
        public RptftGuarantorRepository(IDataContextFactory databaseFactory) : base(databaseFactory){
            _databaseFactory = databaseFactory;
        }
        public async Task<IEnumerable<RptftGuarantor>> GetGuarantor(string contractCode){
            using (var db = _databaseFactory.GetBankDataContext()) {
                return await db.Database.SqlQuery<RptftGuarantor>(@" DECLARE @maxDate varchar(30);
                        select @maxDate= max([HisDate]) from [RPTFT067] where   [Contract] = {0}
                               select    [ID] as Id,[Contract],[Customer_Code] as CustomerCode,[Guaranty_Remaining] as GuarantyRemaining,[HisDate]
                                	FROM [TAT_DWBI_ODS].[dbo].[RPTFT067]
                                  where [Contract] = {0} and [HisDate]= @maxDate", contractCode).ToListAsync();
            }
        }
    }
}