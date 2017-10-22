using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.BranchMarketing;
using RahyabServices.DataAccess.Core.BranchMarketing;
namespace RahyabServices.DataAccess.Repositories.BranchMarketing.Interfaces
{
  public interface IDailyRevertCustomersRepository : IBranchMarketingRepository<DailyRevertCustomers>
    {
    }
}
