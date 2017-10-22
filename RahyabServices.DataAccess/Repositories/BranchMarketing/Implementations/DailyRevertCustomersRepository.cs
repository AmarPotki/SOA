using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.BranchMarketing;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.BranchMarketing;
using RahyabServices.DataAccess.Repositories.BranchMarketing.Interfaces;
namespace RahyabServices.DataAccess.Repositories.BranchMarketing.Implementations
{
    public class DailyRevertCustomersRepository : BranchMarketingRepositoryBase<DailyRevertCustomers>,IDailyRevertCustomersRepository    {
        public DailyRevertCustomersRepository(IDataContextFactory databaseFactory) : base(databaseFactory){ }
    
    }
}
