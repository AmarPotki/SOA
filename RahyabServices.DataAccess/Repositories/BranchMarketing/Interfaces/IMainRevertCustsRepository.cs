using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.BranchMarketing;
using RahyabServices.DataAccess.Core.BranchMarketing;
namespace RahyabServices.DataAccess.Repositories.BranchMarketing.Interfaces{
    public interface IMainRevertCustsRepository : IBranchMarketingRepository<MainRevertCusts>{
        Task<IEnumerable<CommunicationCustomerData>> GetItems();
    }
}