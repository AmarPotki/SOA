using System.Collections.Generic;
using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.DataAccess.Core.Sharepoint;
namespace RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces{
    public interface ISuppliesRequestRepository : ISharepointRepository<SuppliesRequest>
    {
        bool IsValid(int id, string state);
        IEnumerable<SuppliesRequest> GetRequestByState(double state);
    }
}