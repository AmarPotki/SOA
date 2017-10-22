

using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Sharepoint.Implementations{
    public class GivingAChanceLogListRepository : SharepointRepositoryBase<GivingAChanceLogList>, IGivingAChanceLogListRepository{
        public GivingAChanceLogListRepository(IDataContextFactory databaseFactory) : base(databaseFactory) { }
    }
}