using System.Collections.Generic;
using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.DataAccess.Core.Sharepoint;
namespace RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces{
    public interface IHrDocPermissionListRepository : ISharepointRepository<HrDocPermission>{
        IEnumerable<HrDocPermission> GetNotStartedItems();
        IEnumerable<HrDocPermission> GetMustBeDoneItems();
    }

}