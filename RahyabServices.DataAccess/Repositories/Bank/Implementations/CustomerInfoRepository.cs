using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Bank;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;

namespace RahyabServices.DataAccess.Repositories.Bank.Implementations
{
    public class CustomerInfoRepository : BankRepositoryBase<CustomerInfo>, ICustomerInfoRepository
    {
        private readonly IDataContextFactory _dataContextFactory;
        public CustomerInfoRepository(IDataContextFactory databaseFactory)
            : base(databaseFactory)
        {
            _dataContextFactory = databaseFactory;
        }

        public CustomerInfo GetByCustomerNumber(string customerNumber)
        {
            return Task.Run(async () => await GetByCustomerNumberAsync(customerNumber)).Result;
        }

        public async Task<CustomerInfo> GetByCustomerNumberAsync(string customerNumber)
        {
            return
                await
                    QueryAsync(async q => await q.SingleOrDefaultAsync(w => w.CustomerNumber == customerNumber));
        }

        public async Task<bool> IsExistAsync(string customerNumber)
        {
            return
                await
                    QueryAsync(async q => await q.AnyAsync(w => w.CustomerNumber == customerNumber));
        }
        public  async Task<IEnumerable<CustomerInfo>> GetCustomersAsync(IEnumerable<string> custmerNumbers){
            return await QueryAsync(async q => await q.Where(x => custmerNumbers.Contains(x.CustomerNumber)).ToListAsync());
        }
        public async Task<IEnumerable<CustomerInfo>> GetCustomersAsync()
        {
            return await QueryAsync(async q => await q.Where(x =>x.Id !=0).ToListAsync());
        }
        public bool IsExist(string customerNumber)
        {
            return Query(q => q.Any(w => w.CustomerNumber == customerNumber));
        }
    }
}