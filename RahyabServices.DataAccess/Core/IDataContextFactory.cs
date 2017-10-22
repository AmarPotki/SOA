using System;
using System.Net;
using RahyabServices.DataAccess.Core.Bank;
using RahyabServices.DataAccess.Core.BankPerson;
using RahyabServices.DataAccess.Core.BranchMarketing;
using RahyabServices.DataAccess.Core.Delinquent;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Core.Supplies;
using RahyabServices.DataAccess.Core.VipBanking;
namespace RahyabServices.DataAccess.Core{
    public interface IDataContextFactory : IDisposable{
        NetworkCredential Credential { get; set; }
        string ConnectionName { get; set; }
        DelinquentDataContext GetRahyabServicesDataContext();
        VipBankingDataContext GetVipBankingDataContext();
        BankPersonDataContext GetBankPersonDataContext();
        BankDataContext GetBankDataContext(string connectionName = "TAT_DWBI_ODS");
        SharepointDataContext GetSharepointDataContext(string siteCollectionName,
            string url = "https://abportal.ab.net/", NetworkCredential credential = null);
        AbisLoanDataContext GetAbisLoanDataContext();
        IranNaraDataContext GetIranNaraDataContext();
        BranchMarketingDataContext GetBranchMarketingDataContext();
    }
}