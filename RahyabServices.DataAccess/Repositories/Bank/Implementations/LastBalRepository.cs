using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.Business.Domain.Models.Sharepoint.BranchMarketing;
using RahyabServices.Business.Dtos.Bank;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Bank;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Bank.Implementations{
    public class LastBalRepository : BankRepositoryBase<LastBal>, ILastBalRepository{
        private readonly IDataContextFactory _dataContextFactory;
        public LastBalRepository(IDataContextFactory databaseFactory, IDataContextFactory dataContextFactory)
            : base(databaseFactory){
            _dataContextFactory = dataContextFactory;
        }
        public async Task<IEnumerable<LastBalDetail>> GetThirtyLastBal(string custNumber){
            using (var dc = _dataContextFactory.GetBankDataContext("Data Source=biha.ab.net;initial catalog=TAT_DWBI_ODS;user id=VipCustApp;Password=H6KO0G$FiY5D&NL;MultipleActiveResultSets=True;App=EntityFramework")) {
               return await dc.Database.SqlQuery<LastBalDetail>(@"select
                       top(30)
                   [CUSTNO] as 'CustomerNumber'  ,
                 sum([Remaining_Amount_Current]) as 'RemainingAmountCurrent',
                count([ACNO]) as 'TotalAccountNumber' ,
                    [HisDate] as 'HisDate'
                    FROM[TAT_DWBI_ODS].[dbo].[LASTBAL]
                      where[CUSTNO]=@CustNumber
                     group by[CUSTNO],[HisDate]
                 order by[HisDate] desc", new SqlParameter("CustNumber", custNumber)).ToListAsync();
            }
         
        }
        public async Task<decimal> GetLastBal(string customerNumbe)
        {
            using (var dc = _dataContextFactory.GetBankDataContext())
            {
               return await dc.Database.SqlQuery <decimal>(@"declare
@ToDay varchar(6)=(select max(hisdate)from [dbo].[LASTBAL])
                  select convert(money,sum ([Remaining_Amount_effective])) as Mande,custno as CustomerNo
                  from [dbo].[LASTBAL]
                  where CUSTNO=@customercode
                  and HisDate=@ToDay
                  group by custno,hisdate ", new SqlParameter("customercode", customerNumbe)).FirstOrDefaultAsync();

               
            }
        }
        public async Task<IEnumerable<CustomerLastBal>> GetLastBal(string[] customerNumbes,string actiondate){
            //var str = string.Join(",", customerNumbes);
            var str = customerNumbes.Aggregate("", (current, cust) => current + $"'{cust}',");
          str=  str.Remove(str.Length - 1);
            var strDate = "'" + actiondate + "'";
            //var strDate=actiondates.Aggregate
            var query = @"declare
@ToDay varchar(6)=(select max(hisdate)from [dbo].[LASTBAL])
                  select convert(money,sum ([Remaining_Amount_effective])) as TotalAccountNumber ,[CUSTNO] as CustomerNumber
                  from [dbo].[LASTBAL]
                  where CUSTNO in (" + str + @")
                  and HisDate="+ strDate+@"group by custno,hisdate";
            using (var dc = _dataContextFactory.GetBankDataContext())
            {
                return await dc.Database.SqlQuery<CustomerLastBal>(query).ToListAsync();


            }
        }
    }
}