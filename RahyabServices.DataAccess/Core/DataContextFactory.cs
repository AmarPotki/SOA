using System.Data.Entity.Infrastructure;
using System.Net;
using RahyabServices.DataAccess.Core.Bank;
using RahyabServices.DataAccess.Core.BankPerson;
using RahyabServices.DataAccess.Core.BranchMarketing;
using RahyabServices.DataAccess.Core.Delinquent;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Core.Supplies;
using RahyabServices.DataAccess.Core.VipBanking;

namespace RahyabServices.DataAccess.Core
{
    public class DataContextFactory : IDbContextFactory<DelinquentDataContext>, IDataContextFactory
    {
        private readonly DelinquentDataContext _delinquentDataContext;
        public NetworkCredential Credential { get; set; }
        public string ConnectionName { get; set; }
        public string BankConnectionName { get; set; }
        public string AbisLoanConnectionName { get; set; }
        public string VipBankingConnectionName { get; set; }
        public string BankPersonConnectionName { get; set; }
        public string SharepointConnectionUrl { get; set; }
        public string IranNaraConnectionName { get; set; }
        public string BranchMarketingConnectionName { get; set; }
        public DataContextFactory()
        {
            BankConnectionName = "TAT_DWBI_ODS";
            AbisLoanConnectionName = "AbisLoan";
            VipBankingConnectionName = "VIP";
            BankPersonConnectionName = "Hamyar";
            IranNaraConnectionName = "IranNara";
            BranchMarketingConnectionName = "BranchMarketing";
        }

        public DataContextFactory(DelinquentDataContext delinquentDataContext)
            : this()
        {
            _delinquentDataContext = delinquentDataContext;
        }

        public DelinquentDataContext Create()
        {
            return new DelinquentDataContext(ConnectionName);
        }

        public BankDataContext CreateBankDataContext(string connectionName)
        {
            return new BankDataContext(connectionName);
        }

        public VipBankingDataContext GetVipBankingDataContext()
        {
            return CreateVipBanking();
        }
        public BankPersonDataContext GetBankPersonDataContext(){
            return CreateBankPerson();
        }
        private BankPersonDataContext CreateBankPerson(){
            return new BankPersonDataContext(BankPersonConnectionName);
        }
        public DelinquentDataContext GetRahyabServicesDataContext()
        {
            return Create();
        }
        public VipBankingDataContext CreateVipBanking()
        {
            return new VipBankingDataContext(VipBankingConnectionName);
        }
        public BankDataContext GetBankDataContext(string connectionName = "TAT_DWBI_ODS")
        {
            return CreateBankDataContext(connectionName);
        }
        public AbisLoanDataContext GetAbisLoanDataContext(){
            return CreateAbisDataContext();
        }
        public IranNaraDataContext GetIranNaraDataContext(){
            return CreateIranNaraDataContext();
        }
        public BranchMarketingDataContext GetBranchMarketingDataContext(){
            return CreateBranchMarketing();
        }
        private BranchMarketingDataContext CreateBranchMarketing(){
            return  new BranchMarketingDataContext(BranchMarketingConnectionName);
        }
        public IranNaraDataContext CreateIranNaraDataContext()
        {
            return  new IranNaraDataContext(IranNaraConnectionName);
        }
        private AbisLoanDataContext CreateAbisDataContext(){
            return new AbisLoanDataContext(AbisLoanConnectionName);
        }
        public SharepointDataContext GetSharepointDataContext(string siteCollectionName, string url = "http://", NetworkCredential credential= null){

            Credential = credential ?? new NetworkCredential("", "", "");
            SharepointConnectionUrl = url ?? "http://";
            ConnectionName = siteCollectionName;
            return CreateDataContext(siteCollectionName);
        }
        public SharepointDataContext CreateDataContext(string siteCollectionName)
        {

            return new SharepointDataContext(SharepointConnectionUrl + siteCollectionName, Credential);
        }
        public void Dispose()
        {
            if (_delinquentDataContext != null)
            {
                _delinquentDataContext.Dispose();
            }
        }
    }
}
