

using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Bank;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Bank.Implementations{
    public class GuaranteeRepository : BankRepositoryBase<Guarantee>, IGuaranteeRepository{
        public GuaranteeRepository(IDataContextFactory databaseFactory) : base(databaseFactory){}
    }
}