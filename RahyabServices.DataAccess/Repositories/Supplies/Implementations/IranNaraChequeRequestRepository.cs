using RahyabServices.Business.Domain.Models.Supplies;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Supplies;
using RahyabServices.DataAccess.Repositories.Supplies.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Supplies.Implementations{
    public class IranNaraChequeRequestRepository : IranNaraRepositoryBase<IranNaraChequeRequest>,
        IIranNaraChequeRequestRepository{
        private readonly IDataContextFactory _dataContextFactory;
        public IranNaraChequeRequestRepository(IDataContextFactory dataContextFactory) : base(dataContextFactory){
            _dataContextFactory = dataContextFactory;
        }
        public int SaveAdo(IranNaraChequeRequest instance){
            using (var db = _dataContextFactory.GetIranNaraDataContext()){
                var query =
                    "INSERT INTO[dbo].[tbl_ABChequeRequest]([CustomerRequestNumber],[BranchRequestNumber],[SendingDate],[BranchCode]," +
                    "[CustomerAccountNumber],[CustomerName],[CheckbookCode],[SendingBranchCode],[Iban],[User],[ChequeCnt],[MelliCode]," +
                    "[IsPrint]) VALUES";
                query +=
                    $" ('{instance.CustomerRequestNumber}','{instance.BranchRequestNumber}','{instance.SendingDate}','{instance.BranchCode}'," +
                    $"'{instance.CustomerAccountNumber}','{instance.CustomerName}','{instance.CheckbookCode}','{instance.SendingBranchCode}'," +
                    $"'{instance.Iban}','{instance.User}',{instance.ChequeCnt},'{instance.MelliCode}','{instance.IsPrint}')";
                var command = db.Database.ExecuteSqlCommand(query);
              
                return 0;
            }
        }
    }
}