using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Delinquent.Implementations{
    public class BranchRepository : DelinquentRepositoryBase<Branch>, IBranchRepository{
        private readonly IDataContextFactory _dataContextFactory;
        public BranchRepository(IDataContextFactory databaseFactory) : base(databaseFactory){
            _dataContextFactory = databaseFactory;
        }
        public async Task<Branch> GetBranchByCode(string branchCode){
          return await QueryAsync(async f=>await f.FirstOrDefaultAsync(x=>x.Code==branchCode));
        }
        public async Task<Branch> GetBranchByCodeAsNoTracking(string branchCode)
        {
            return await QueryAsync(async f => await f.FirstOrDefaultAsync(x => x.Code == branchCode),true);
        }
        public async Task<IEnumerable<Branch>> GetMergBranchChildren(int parentId){
            return await QueryAsync(async f => await f.Where(x => x.BankServiceId == parentId && x.OldCode !=null).ToListAsync(), true);
        }
        public async Task<IEnumerable<Branch>> GetBranchChildrenForBranchLevel(int parentId)
        {
            return await QueryAsync(async f => await f.Where(x => x.BankServiceId == parentId && x.OldCode != null).ToListAsync(), true);
        }
        public async Task<IEnumerable<Branch>> GetAllAsNoTracking()
        {
            return await QueryAsync(async f => await f.Where(x => x.OldCode==null && x.Level==0).ToListAsync(),true);
        }
    }
}