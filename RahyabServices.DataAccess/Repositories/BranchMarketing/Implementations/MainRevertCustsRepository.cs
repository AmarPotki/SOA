

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.Business.Domain.Models.BranchMarketing;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.BranchMarketing;
using RahyabServices.DataAccess.Repositories.BranchMarketing.Interfaces;
namespace RahyabServices.DataAccess.Repositories.BranchMarketing.Implementations{
    public class MainRevertCustsRepository : BranchMarketingRepositoryBase<MainRevertCusts>, IMainRevertCustsRepository{
        private readonly IDataContextFactory _dataContextFactory;
        public MainRevertCustsRepository(IDataContextFactory databaseFactory) : base(databaseFactory){
            _dataContextFactory = databaseFactory;
        }
        public async Task<IEnumerable<CommunicationCustomerData>> GetItems()
        {
            using (var db = _dataContextFactory.GetBranchMarketingDataContext())
            {
                return await (from s in db.MainRevertCustses
                              join c in db.DailyRevertCustomerses on s.CUST_NO equals c.CUST_NO
                              where s.SPListItem  == null
                              select new CommunicationCustomerData
                              {
                                  //شعبه هدف
                                 Branch =c.OPNBR,
                                  CustomerID=c.CUST_NO,
                                  Rank=s.Rank,
                                  CustType=c.CUStTYPE,
                                  CustomerName=c.FullName,
                                  CustomerTell=c.phone,
                                  //موجودی حساب مشتری در زمان تعریف مشتری یا کمپین
                                  AccountBalance=c.FirstRemaining,
                                  //آخرین موجودی حساب مشتری در زمان انتقال اطلاعات به شیرپوینت
                                  Latestaccountbalance=c.CurrentRemaining,
                                  //مبلغ صندوق
                                  cashdesk=c.SandoghValue,
                                  Facility=c.MablaqeTahsilateEtaiTakonun ,
                                  CampainId=s.CampianID.ToString(),
                                  RECENCY=c.RECENCY,
                                  FREQUENCY=c.FREQUENCY,
                                  Main=s,
                                  CheckingUnit= c.OPNBR,

                              }).ToListAsync();

            }
        }

    }
}