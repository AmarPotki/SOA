

using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Sharepoint.Implementations{
    public class ClearingLogListRepository : SharepointRepositoryBase<ClearingLogList>, IClearingLogListRepository{
        public ClearingLogListRepository(IDataContextFactory databaseFactory) : base(databaseFactory){}
    }
}