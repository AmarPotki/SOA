using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Delinquent.Implementations{
    public class BranchClaimRepository : DelinquentRepositoryBase<BranchClaim>, IBranchClaimRepository{
        public BranchClaimRepository(IDataContextFactory databaseFactory) : base(databaseFactory){}
        public async Task<BranchClaim> GetBranchClaim(int branchId, DateTime date){
            return await QueryAsync(f => f.FirstOrDefaultAsync(x => x.Created == date && x.BranchId==branchId));
        }
        public async Task<IEnumerable<BranchClaim>> GetDaysBranchClaims(int branchId, DateTime date){
            return await QueryAsync(f => f.Where(x => x.Created >= date && x.BranchId == branchId).ToListAsync());
        }
    }
}