 using RahyabServices.Business.Domain.Models.Sharepoint.AbsorbResourses;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Sharepoint.Implementations{
    public class RequestRepository : SharepointRepositoryBase<Request>, IRequestRepository{
        public RequestRepository(IDataContextFactory databaseFactory) : base(databaseFactory){
            SiteCollection = "AbsorbResources";
        }
    }
}