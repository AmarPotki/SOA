

using RahyabServices.Business.Domain.Models.Supplies;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Sharepoint.Implementations{
    public class DeactivateBaseIbanRequestRepository : SharepointRepositoryBase<DeactivateBaseIbanRequest>, IDeactivateBaseIbanRequestRepository{
        public DeactivateBaseIbanRequestRepository(IDataContextFactory databaseFactory) : base(databaseFactory){
            SiteCollection = "AbSupplies";
        }
    }
}